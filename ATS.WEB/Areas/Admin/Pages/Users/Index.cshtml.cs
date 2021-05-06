using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ATS.WEB.Areas.Admin.Pages.Users
{
    public class IndexModel : PageModel {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public class ListModel{
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        public IList<ListModel> ApplicationUsers { get;set; }

        public async Task OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users) {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                ApplicationUsers.Add(new() { Email = user.Email, Name = user.Name, Role = role });
            }
        }
    }
}
