using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using ET.Application.Services;
using ET.Application.Models;
using ET.Core.Entities;
using ET.Application.Models.UserDtos.Response;

namespace ET.Application.Utilities
{
    public class AuthenticateUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserService _userService;
        public required UserResponseDto User {  get; set; }
        //public required CompanyResponseDto Company { get; set; }

        public AuthenticateUser(IHttpContextAccessor httpContextAccessor, UserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
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

                User = _userService.FindUserById(guid);

                //Company = _companyService.FindCompanyById(guid);

                return new AuthenticatedDto
                {
                    Id = guid,
                    JwtToken = jwtToken,
                    IsAuthenticated = isAuthenticated,
                    Role = User.Role,
                };
            }
            else return new AuthenticatedDto();

        }
    }
}
