namespace ATS.WEB.Data.Entities {
    public class Student {
        public int Id { get; set; }
        public virtual Group Group { get; set; }
        public virtual Cathedra Cathedra { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
