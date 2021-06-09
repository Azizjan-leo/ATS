using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATS.WEB.Data.Entities {
    public class Answer {
        public int Id { get; set; }

        [Display(Name = "Текст ответа")]
        public string AnswerText { get; set; }
        [Display(Name = "Вопрос")]
        public virtual Question Question { get; set; }
        [Display(Name = "Правильный")]
        public bool IsRight { get; set; }
    }
}