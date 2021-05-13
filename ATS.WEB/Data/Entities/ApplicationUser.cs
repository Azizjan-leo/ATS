using ATS.WEB.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ATS.WEB.Data.Entities {
    public class ApplicationUser : IdentityUser {
        [Required]
        [StringLength(40, MinimumLength = 5)]
        public virtual string Name { get; set; }
    }

    public class UserViewModel {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}