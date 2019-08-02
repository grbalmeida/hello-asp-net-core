using System.Collections.Generic;

namespace Model.Entries
{
    public class Lesson
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CourseHasLesson> CoursesHasLessons { get; set; }
    }
}