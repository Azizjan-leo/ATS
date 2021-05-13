using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using ATS.WEB.Enums;

namespace ATS.WEB.Areas.Admin.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        [BindProperty]
        public EditViewModel Model { get; set; }

        public IEnumerable<SelectListItem> RolesList {
            get {
                return Enum.GetNames(typeof(Role)).Where(x => x != Role.Admin.ToString()).Select(x => new SelectListItem { Text = x, Value = x });
            }
        }

        public class EditViewModel {
            public string Id { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public Role Role { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            Model = new EditViewModel { Id = user.Id, Email = user.Email, Name = user.Name };
            var roleStr = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            Model.Role = (Role)Enum.Parse(typeof(Role), roleStr);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(Model.Id);
            if(user is null) {
                return NotFound();
            }
            
            try
            {
                user.Name = Model.Name;
             
                if(!String.IsNullOrWhiteSpace(Model.Password)) {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, Model.Password);
                }
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
                await _userManager.AddToRoleAsync(user, Model.Role.ToString());
                await _userManager.SetEmailAsync(user, Model.Email);
                await _userManager.UpdateAsync(user);
            }
            catch (Exception)
            {
            }

            return RedirectToPage("./Index");
        }

    }
}
