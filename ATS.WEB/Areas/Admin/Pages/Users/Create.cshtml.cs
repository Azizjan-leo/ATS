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
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IEnumerable<SelectListItem> RolesList {
            get {
                return Enum.GetNames(typeof(Role)).Where(x => x != Role.Admin.ToString())
                                .Select(x => new SelectListItem { Text = x, Value = x });
            }
        }

        public class InputModel {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public Role Role { get; set; }
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = new ApplicationUser {
                Name = Input.Name,
                Email = Input.Email,
                UserName = Input.Email
            };

            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, Input.Role.ToString());
            }
                return RedirectToPage("./Index");
        }
    }
}
