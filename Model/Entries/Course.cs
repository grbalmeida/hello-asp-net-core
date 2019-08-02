using System.Collections.Generic;

namespace Model.Entries
{
    public class Course
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? DepartmentId { get; set; }
        public Department Department { get; set; }
        public virtual ICollection<CourseHasLesson> CoursesHasLessons { get; set; }
    }
}