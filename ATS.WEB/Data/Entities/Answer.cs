using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATS.WEB.Data.Entities {
    public class Answer {
        public int Id { get; set; }
        [Display(Name = "Текст ответа")]
        public string AnswerText { get; set; }
        public int? QuestionId { get; set; }
        [Display(Name = "Вопрос")]
        public virtual Question Question { get; set; }
        [Display(Name = "Правильный")]
        public bool IsRight { get; set; }
        [Display(Name = "Ответ студента")]
        public bool RightStudent { get; set; }
        public int? TestResultQuestionId { get; set; }
        public int? TestResultId { get; set; }
        public virtual TestResult TestResult { get; set; }
    }
}