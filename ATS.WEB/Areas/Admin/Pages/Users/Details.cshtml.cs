using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ATS.WEB.Areas.Admin.Pages.Users {
    public class DetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        [BindProperty]
        public DetailsViewModel Model { get; set; }

        public class DetailsViewModel {
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
    }
}
