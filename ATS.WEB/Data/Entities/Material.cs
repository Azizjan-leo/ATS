using System;

namespace ATS.WEB.Data.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public Teacher Teacher { get; set; }
        public DateTime UploadDate { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
    }
}
