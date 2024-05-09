using ET.Application.Models.UserDtos;
using ET.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.User
{
    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public required UserRegisterDto RegisterDto { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _userService.UserRegister(RegisterDto);
                return RedirectToPage("/User/Login");
            }
            else
            {
                // If the model state is not valid, return the page with validation errors
                return Page();
            }
        }

    }
}
