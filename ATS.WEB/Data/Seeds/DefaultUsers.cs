using ATS.WEB.Data.Entities;
using ATS.WEB.Enums;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace ATS.WEB.Data.Seeds {
    public class DefaultUsers {
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager) {
            var defaultUser = new ApplicationUser {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Administrator",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id)) {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null) {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
        }
    }
}
