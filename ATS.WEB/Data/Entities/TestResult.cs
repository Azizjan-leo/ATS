using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ATS.WEB.Data.Entities {
    [Index(nameof(AnswererId), nameof(TopicId), IsUnique = true)]
    public class TestResult {
        public int Id { get; set; }
        public int AnswererId { get; set; }
        public Student Answerer { get; set; }
        public int TopicId { get; set; }
        public Lesson Topic { get; set; }
        public Teacher Reviewer { get; set; }
        public DateTime PassDate { get; set; }
        public List<Answer> Answers { get; set; }
        public int? Score { get; set; }
    }
}
