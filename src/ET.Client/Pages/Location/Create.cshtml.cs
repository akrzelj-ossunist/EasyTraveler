using ET.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ET.Core.Entities;
using ET.Application.Models.LocationDtos;

namespace ET.Client.Pages.Location
{
    public class CreateModel : PageModel
    {
        private readonly LocationServices _locationService;
        [BindProperty]
        public required LocationDto Location { get; set; }

        public CreateModel(LocationServices locationService)
        {
            _locationService = locationService;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid) 
            {
                _locationService.Create(Location);
                return RedirectToPage("/Index");
            }
            else 
            { 
                return Page();
            }
        }
    }
}
