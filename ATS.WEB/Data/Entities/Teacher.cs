using System.Collections.Generic;

namespace ATS.WEB.Data.Entities {
    public class Teacher {
        public int Id { get; set; }
        public virtual Cathedra Cathedra { get; set; }
        public virtual List<Lesson> Lessons { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
