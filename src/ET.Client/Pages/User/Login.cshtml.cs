using ET.Application.Models.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.User
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserLoginDto LoginDto { get; set; }

        public void OnGet()
        {
            // Initialization logic if needed
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                // Perform login logic
                Console.WriteLine("\nEmail: " + LoginDto.Email + "\nPassword: " + LoginDto.Password);
                if (true)
                {
                    // Login successful, redirect to home page or dashboard
                    RedirectToPage("/Index");
                }
                else
                {
                    // Login failed, add model error
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                }
            }
        }

    }
}
