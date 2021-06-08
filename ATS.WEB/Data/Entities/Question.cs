using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ATS.WEB.Data.Entities {
    public class Question {
        public int Id { get; set; }
        [Display(Name = "Текст вопроса")]
        public string QuestionText { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public int? RightAnswerId { get; set; }
        [NotMapped]
        public virtual Answer RightAnswer
        {
            get
            {
                return Answers != null && Answers.Any() ? Answers.FirstOrDefault(a => a.IsRight) : null;
            }
        }
        public virtual Lesson Lesson { get; set; }
    }
}