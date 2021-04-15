using ATS.WEB.Data.Entities;
using ATS.WEB.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ATS.WEB.Data.Seeds {
    public static class RolesSeeds  {
        public static ModelBuilder Seed(this ModelBuilder builder, IdentityRole identityRole) {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole(Roles.Admin.ToString()),
                new IdentityRole(Roles.Teacher.ToString()),
                new IdentityRole(Roles.Student.ToString())
                );

            return builder;
        }
    }
    
}
