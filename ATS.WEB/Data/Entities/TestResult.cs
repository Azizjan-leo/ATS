using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATS.WEB.Data.Entities {
    [Index(nameof(AnswererId), nameof(TopicId), IsUnique = true)]
    public class TestResult {
        public int Id { get; set; }
        public int AnswererId { get; set; }
        [Display(Name = "Студент")]
        public Student Answerer { get; set; }
        public int TopicId { get; set; }
        [Display(Name = "Тема")]
        public Lesson Topic { get; set; }
        [Display(Name = "Проверяющий")]
        public Teacher Reviewer { get; set; }
        [Display(Name = "Дата сдачи")]
        public DateTime PassDate { get; set; }
        public List<Answer> Answers { get; set; }
        [Display(Name = "Результат")]
        public int? Score { get; set; }
    }
}
