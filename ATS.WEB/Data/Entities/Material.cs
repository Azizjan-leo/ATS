using System;
using System.ComponentModel.DataAnnotations;

namespace ATS.WEB.Data.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public Teacher Teacher { get; set; }
        public DateTime UploadDate { get; set; }
        [Display(Name = "Название файла")]
        public string FileName { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}
