using System.Collections.Generic;

namespace ATS.WEB.Data.Entities {
    public class Lesson {
        public int Id { get; set; }
        public virtual ApplicationUser Teacher { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}
