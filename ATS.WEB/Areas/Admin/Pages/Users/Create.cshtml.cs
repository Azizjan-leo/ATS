using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using ATS.WEB.Enums;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ATS.WEB.Areas.Admin.Pages.Users {
    public class CreateModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            Input = new InputModel();
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel {
            public UserViewModel ApplicationUser { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public List<SelectListItem> RolesList { get; set; } = Enum.GetNames(typeof(Role)).Select(x => new SelectListItem { Text = x, Value = x}).ToList();

        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = new ApplicationUser {
                Name = Input.ApplicationUser.Name,
                Email = Input.ApplicationUser.Email,
                UserName = Input.ApplicationUser.Email
            };

            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, Input.ApplicationUser.Role);
            }
                return RedirectToPage("./Index");
        }
    }
}
