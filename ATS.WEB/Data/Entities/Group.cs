using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATS.WEB.Data.Entities {
    public class Group {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Название")]
        public virtual string Name { get; set; }

        public virtual Cathedra Cathedra { get; set; }

        public virtual List<Student> Students { get; set; }
    }
}