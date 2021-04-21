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
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly AppSettings _appSettings;
        private readonly RoleStore<IdentityRole> _roleStore;
        private readonly UserStore<ApplicationUser> _userStore;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seeder(ApplicationDbContext applicationDbContext, IOptions<AppSettings> options, RoleManager<IdentityRole> roleManager) {
            _applicationDbContext = applicationDbContext;
            _appSettings = options.Value;
            _roleStore = new RoleStore<IdentityRole>(_applicationDbContext);
            _userStore = new UserStore<ApplicationUser>(_applicationDbContext);
            _roleManager = roleManager;
        }
        public async Task SeedRoles() {
            foreach (var role in Enum.GetValues(typeof(Roles))) { 
                if (!_applicationDbContext.Roles.Any(r => r.Name == role.ToString())) {
                    await _roleStore.CreateAsync(new IdentityRole(role.ToString()));
                }
            }

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task SeedAdminUser() {

            var user = new ApplicationUser {
                Name = Roles.Admin.ToString(),
                UserName =  _appSettings.AdminEmail,
                NormalizedUserName = _appSettings.AdminEmail.ToUpper(),
                Email = _appSettings.AdminEmail,
                NormalizedEmail = _appSettings.AdminEmail.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!_applicationDbContext.Roles.Any(r => r.Name == Roles.Admin.ToString())) {
                await _roleStore.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
            }
            var admin = _applicationDbContext.Users.Where(x => x.Email == _appSettings.AdminEmail).FirstOrDefault();
        

            if (admin == null) {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, _appSettings.AdminPassword);
                user.PasswordHash = hashed;
        
                await _userStore.CreateAsync(user);
            }
             
            await _userStore.AddToRoleAsync(user, Roles.Admin.ToString());
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
