using ET.Application.Models.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ET.Client.Pages.User
{
    public class ChangePasswordModel : PageModel
    {
        [BindProperty]
        public PasswordChangeDto ChangePasswordDto { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Check if the new passwords match
                if (ChangePasswordDto.NewPassword != ChangePasswordDto.NewPasswordRepeat)
                {
                    ModelState.AddModelError("ChangePasswordDto.ConfirmNewPassword", "The new passwords do not match.");
                    return Page();
                }

                // Check if the current password is correct (example implementation)
                if (!IsCurrentPasswordCorrect(ChangePasswordDto.CurrentPassword))
                {
                    ModelState.AddModelError("ChangePasswordDto.CurrentPassword", "Incorrect current password.");
                    return Page();
                }

                // Password change logic (example implementation)
                // Update the password in the database, or perform other necessary actions

                // Redirect the user to a success page or home page
                return RedirectToPage("/Index");
            }

            // If the model state is not valid, return the page with validation errors
            return Page();
        }

        // Example method to check if the current password is correct
        private bool IsCurrentPasswordCorrect(string currentPassword)
        {
            // Replace this with your actual logic to check if the current password matches the user's actual current password
            // For demonstration purposes, assume it always returns true
            return true;
        }
    }
}
