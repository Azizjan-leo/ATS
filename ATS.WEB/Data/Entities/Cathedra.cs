using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATS.WEB.Data.Entities {
    public class Cathedra {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Название")]
        public virtual string Name { get; set; }

        public virtual List<Group> Groups { get; set; }
    }
}