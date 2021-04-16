using ATS.WEB.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ATS.WEB.Data.Seeds {
    public static class RolesSeeds  {
        public static ModelBuilder SeedRoles(this ModelBuilder builder) {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole(Roles.Admin.ToString()),
                new IdentityRole(Roles.Teacher.ToString()),
                new IdentityRole(Roles.Student.ToString())
                );

            return builder;
        }
    }
    
}
