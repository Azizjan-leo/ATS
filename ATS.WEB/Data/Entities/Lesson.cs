using System.Collections.Generic;

namespace ATS.WEB.Data.Entities {
    public class Lesson {
        public int Id { get; set; }
        public virtual Teacher Teacher { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}
