namespace ATS.WEB.Data.Entities {
    public class Teacher {
        public int Id { get; set; }
        public virtual Cathedra Cathedra { get; set; }
    }
}
