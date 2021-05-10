using ATS.WEB.Data.Entities;
using ATS.WEB.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ATS.WEB.Data.Seeds {
    public class Seeder {
        private readonly AppSettings _appSettings;

        public Seeder(IOptions<AppSettings> options) {
            _appSettings = options.Value;
        }

        public void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private void SeedRoles(RoleManager<IdentityRole> roleManager) {
            foreach (var roleName in Enum.GetValues(typeof(Role))) {
                var name = roleName.ToString();
                if (!roleManager.RoleExistsAsync(name).Result) {
                    var role = new IdentityRole {
                        Name = name,
                        NormalizedName = name.ToUpper()
                    };
                    roleManager.CreateAsync(role).Wait();
                }
            }

        }

        private void SeedUsers(UserManager<ApplicationUser> userManager) {

            var user = new ApplicationUser{ 
                Name = Role.Admin.ToString(),
                Email = _appSettings.AdminEmail,
                NormalizedUserName = _appSettings.AdminEmail.ToUpper(),
                NormalizedEmail = _appSettings.AdminEmail.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            var admin = userManager.FindByEmailAsync(_appSettings.AdminEmail).Result;

            if (admin == null)
                userManager.CreateAsync(user, _appSettings.AdminPassword).Wait();
             
            userManager.AddToRoleAsync(user, Role.Admin.ToString()).Wait();
        }
    }
}
