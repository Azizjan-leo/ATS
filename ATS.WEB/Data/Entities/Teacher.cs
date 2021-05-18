namespace ATS.WEB.Data.Entities {
    public class Teachers {
        public int Id { get; set; }
        public virtual Cathedra Cathedra { get; set; }
    }
}
