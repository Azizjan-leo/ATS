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
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
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
            Model = new DetailsViewModel { Id = user.Id, Name = user.Name, Email = user.Email };

            Model.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return Page();
        }
    }
}
