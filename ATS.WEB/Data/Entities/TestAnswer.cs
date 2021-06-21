using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATS.WEB.Data.Entities
{
    public class TestAnswer
    {
        public int Id { get; set; }
        [Display(Name = "Ответ студента")]
        public bool UserAnswer { get; set; }
        public int? TestResultId { get; set; }
        public virtual TestResult TestResult { get; set; }
        public int? QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int? AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
    }
}
