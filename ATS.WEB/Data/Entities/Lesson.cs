using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATS.WEB.Data.Entities {
    public class Lesson {
        public int Id { get; set; }
        public virtual Teacher Teacher { get; set; }
        [Display(Name = "Тема")]
        public string Topic { get; set; }
        [Display(Name = "Содержане")]
        public string Content { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}
