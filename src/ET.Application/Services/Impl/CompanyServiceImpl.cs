using ET.Application.Exceptions;
using ET.Application.Mappers;
using ET.Application.Models;
using ET.Application.Models.CompanyDtos;
using ET.Application.Models.CompanyDtos.Response;
using ET.Application.Models.UserDtos;
using ET.Application.Models.UserDtos.Response;
using ET.Application.Utilities;
using ET.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;

namespace ET.Application.Services.Impl
{
    public class CompanyServiceImpl : CompanyService
    {
        private readonly CompanyRepository _companyRepository;
        private readonly CompanyMapper _companyMapper;
        private readonly JwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyServiceImpl(CompanyRepository companyRepository, CompanyMapper companyMapper, JwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _companyRepository = companyRepository;
            _companyMapper = companyMapper;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool CompanyDelete(Guid id)
        {
            var company = _companyRepository.FindById(id);
            if (company == null) throw new NotFoundException("Company with sent id doesnt exist!");

            _httpContextAccessor.HttpContext.Session.Remove("_JwtToken");

            return _companyRepository.Delete(company);
        }

        public CompanyResponseDto CompanyEdit(Guid id, CompanyRegisterDto companyRegisterDto)
        {
            if (companyRegisterDto == null) throw new InvalidArgumentsException("Sent company edit data cannot be null!");

            var company = _companyRepository.FindById(id);
            if (company == null) throw new NotFoundException("Company with sent id doesnt exist!");

            company.Phone = companyRegisterDto.Phone;
            company.Address = companyRegisterDto.Address;
            company.City = companyRegisterDto.City;
            company.Name = companyRegisterDto.Name;

            var eidted = _companyRepository.Update(company);

            return _companyMapper.CompanyToCompanyDto(eidted);
        }

        public CompanyLoginResponseDto CompanyLogin(LoginDto loginDto)
        {
            if (loginDto == null) throw new InvalidArgumentsException("Sent company login data cannot be null!");

            var company = _companyRepository.FindByEmail(loginDto.Email);
            if (company == null) throw new AlreadyExistsException("Company with this email doesn't exist!");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, company.Password)) throw new PasswordMissmatchException("Password used for login is invalid!");

            string jwtToken = _jwtService.GenerateJwtToken("Company", company.Id.ToString());

            _httpContextAccessor.HttpContext.Session.SetString("_JwtToken", jwtToken);

            var response = new CompanyLoginResponseDto
            {
                JwtToken = jwtToken,
                CompanyResponseDto = _companyMapper.CompanyToCompanyDto(company),
                IsSuccess = true
            };

            return response;
        }

        public CompanyResponseDto CompanyRegister(CompanyRegisterDto companyRegisterDto)
        {
            if (companyRegisterDto == null) throw new InvalidArgumentsException("Sent company register data cannot be null!");

            if (companyRegisterDto.Password != companyRegisterDto.ConfirmPassword) throw new PasswordMissmatchException("Sent passwords does not match!");

            var company = _companyRepository.FindByEmail(companyRegisterDto.Email);
            if (company != null) throw new AlreadyExistsException("Company with this email already exists!");

            companyRegisterDto.Password = BCrypt.Net.BCrypt.HashPassword(companyRegisterDto.Password);

            var newCompany = _companyRepository.Save(_companyMapper.CompanyDtoToCompany(companyRegisterDto));

            return _companyMapper.CompanyToCompanyDto(newCompany);
        }

        public bool CompanyUpdatePassword(Guid id, PasswordChangeDto passwordChangeDto)
        {
            if (passwordChangeDto == null) throw new InvalidArgumentsException("Sent company register data cannot be null!");

            var company = _companyRepository.FindById(id);
            if (company == null) throw new NotFoundException("Company with sent id doesnt exist!");

            if (passwordChangeDto.NewPassword != passwordChangeDto.NewPasswordRepeat) throw new PasswordMissmatchException("Sent passwords doesnt match!");

            if (!BCrypt.Net.BCrypt.Verify(passwordChangeDto.CurrentPassword, company.Password)) return false;

            company.Password = BCrypt.Net.BCrypt.HashPassword(passwordChangeDto.NewPassword);

            _companyRepository.Update(company);

            return true;
        }

        public CompanyResponseDto FindCompanyById(Guid id)
        {
            var company = _companyRepository.FindById(id);

            if (company == null) throw new NotFoundException("Company with sent id doesnt exist!");

            return _companyMapper.CompanyToCompanyDto(company);
        }

        public List<CompanyResponseDto> CompanyList()
        {
            throw new NotImplementedException();
        }

        public List<CompanyResponseDto> CompanyFilterList(Dictionary<string, string> filters)
        {
            throw new NotImplementedException();
        }

    }
}
