﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ET.Application.Models.UserDtos.Response;
using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using ET.Core.Enums;

namespace ET.Client.Pages.User
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticateUser _authenticateUser;
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public LogoutModel(IHttpContextAccessor httpContextAccessor, AuthenticateUser authenticateUser)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticateUser = authenticateUser;
        }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.User || AuthenticatedDto.Role == UserRole.Admin))
            {
                _httpContextAccessor.HttpContext.Session.Remove("_JwtToken");
                return RedirectToPage("/Index");
            }
            else
            {
                return RedirectToPage("/User/Login");
            }
        }
    }
}

