using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ATS.WEB.Areas.Admin.Pages.Users {
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        [BindProperty]
        public DeleteViewModel Model { get; set; }
     
        public class DeleteViewModel {
            public ApplicationUser ApplicationUser { get; set; }
            public string Role { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Model.ApplicationUser = await _userManager.FindByIdAsync(id);

            if (Model.ApplicationUser == null)
            {
                return NotFound();
            }
            Model.Role = (await _userManager.GetRolesAsync(Model.ApplicationUser)).FirstOrDefault();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToPage("./Index");
        }
    }
}
