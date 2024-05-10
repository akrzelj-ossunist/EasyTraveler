using ET.Application.Models;
using ET.Application.Models.CompanyDtos;
using ET.Application.Models.CompanyDtos.Response;
using ET.Application.Models.UserDtos.Response;

namespace ET.Application.Services.Impl
{
    public class CompanyServiceImpl : CompanyService
    {
        public bool CompanyDelete(Guid id)
        {
            throw new NotImplementedException();
        }

        public CompanyResponseDto CompanyEdit(Guid id, CompanyRegisterDto companyRegisterDto)
        {
            throw new NotImplementedException();
        }

        public List<CompanyResponseDto> CompanyFilterList(Dictionary<string, string> filters)
        {
            throw new NotImplementedException();
        }

        public List<CompanyResponseDto> CompanyList()
        {
            throw new NotImplementedException();
        }

        public LoginResponseDto CompanyLogin(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public CompanyResponseDto CompanyRegister(CompanyRegisterDto companyRegisterDto)
        {
            throw new NotImplementedException();
        }

        public bool CompanyUpdatePassword(Guid id, PasswordChangeDto passwordChangeDto)
        {
            throw new NotImplementedException();
        }

        public CompanyResponseDto FindUserById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
