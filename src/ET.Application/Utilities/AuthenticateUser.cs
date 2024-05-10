using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using ET.Application.Services;
using ET.Application.Models;
using ET.Application.Models.UserDtos.Response;
using ET.Application.Models.CompanyDtos.Response;
using ET.Core.Entities.Enums;
using ET.Application.Exceptions;

namespace ET.Application.Utilities
{
    public class AuthenticateUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserService _userService;
        private readonly CompanyService _companyService;
        public required UserResponseDto User {  get; set; }
        public required CompanyResponseDto Company { get; set; }

        public AuthenticateUser(IHttpContextAccessor httpContextAccessor, UserService userService, CompanyService companyService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _companyService = companyService;
        }

        public AuthenticatedDto CreateAuthentication()
        {
            var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("_JwtToken");
            var isAuthenticated = !string.IsNullOrEmpty(jwtToken);

            if (isAuthenticated)
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwtToken);

                string id = token.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;

                Guid guid = Guid.TryParse(id, out Guid convertedId) ? convertedId : Guid.NewGuid();
                
                try 
                { 
                    User = _userService.FindUserById(guid); 
                }
                catch(NotFoundException ex) { 
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    Company = _companyService.FindCompanyById(guid);
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return new AuthenticatedDto
                {
                    Id = guid,
                    JwtToken = jwtToken,
                    IsAuthenticated = isAuthenticated,
                    Role = User == null ? UserRole.Company : User.Role,
                };
            }
            else return new AuthenticatedDto();

        }
    }
}
