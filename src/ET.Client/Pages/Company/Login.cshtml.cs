using ET.Application.Models;
using ET.Application.Models.CompanyDtos.Response;
using ET.Application.Models.UserDtos.Response;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.Company
{
    public class LoginModel : PageModel
    {
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        private readonly AuthenticateUser _authenticateUser;
        private readonly CompanyService _companyService;

        public LoginModel(CompanyService companyService, AuthenticateUser authenticateUser)
        {
            _companyService = companyService;
            _authenticateUser = authenticateUser;
        }

        [BindProperty]
        public LoginDto LoginDto { get; set; }

        public CompanyLoginResponseDto LoginResponseDto { get; set; }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            return AuthenticatedDto.IsAuthenticated ? RedirectToPage("/Index") : Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                LoginResponseDto = _companyService.CompanyLogin(LoginDto);

                return RedirectToPage("/Index");
            }
            else
            {
                // If the model state is not valid, return the page with validation errors
                return Page();
            }
        }
    }
}
