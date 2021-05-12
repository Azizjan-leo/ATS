using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
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

        public class EditViewModel {
            public ApplicationUser ApplicationUser { get; set; }

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

            Model = new EditViewModel { ApplicationUser = await _userManager.FindByIdAsync(id) };

            if (Model.ApplicationUser == null)
            {
                return NotFound();
            }

            Model.Role = (await _userManager.GetRolesAsync(Model.ApplicationUser)).FirstOrDefault();

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

            var user = await _userManager.FindByIdAsync(Model.ApplicationUser.Id);
            if(user is null) {
                return NotFound();
            }

            try
            {
                user.Name = Model.ApplicationUser.Name;
             
                if(!String.IsNullOrWhiteSpace(Model.Password)) {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, Model.Password);
                }
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
                await _userManager.AddToRoleAsync(user, Model.Role);
                await _userManager.SetEmailAsync(user, Model.ApplicationUser.Email);
                await _userManager.UpdateAsync(user);
            }
            catch (Exception)
            {
            }

            return RedirectToPage("./Index");
        }

    }
}
