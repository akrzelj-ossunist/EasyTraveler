using ET.Application.Models.UserDtos.Response;
using ET.Application.Models.UserDtos;
using ET.Application.Models;
using ET.Application.Models.CompanyDtos.Response;
using ET.Application.Models.CompanyDtos;

namespace ET.Application.Services;

public interface CompanyService
{
    public CompanyResponseDto CompanyRegister(CompanyRegisterDto companyRegisterDto);

    public CompanyLoginResponseDto CompanyLogin(LoginDto loginDto);

    public bool CompanyUpdatePassword(Guid id, PasswordChangeDto passwordChangeDto);

    public CompanyResponseDto CompanyEdit(Guid id, CompanyRegisterDto companyRegisterDto);

    public bool CompanyDelete(Guid id);

    public List<CompanyResponseDto> CompanyList();

    public List<CompanyResponseDto> CompanyFilterList(Dictionary<string, string> filters);

    public CompanyResponseDto FindCompanyById(Guid id);
}
