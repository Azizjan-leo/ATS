using System.ComponentModel.DataAnnotations.Schema;

namespace ATS.WEB.Data.Entities {
    public class Answer {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public virtual Question Question { get; set; }
        [NotMapped]
        public bool IsRight { get; set; }
    }
}