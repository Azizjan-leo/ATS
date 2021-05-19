using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATS.WEB.Data.Entities {
    public class Question {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public int? RightAnswerId { get; set; }
        [NotMapped]
        public virtual Answer RightAnswer { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}