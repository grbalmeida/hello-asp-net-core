namespace Model.Entries
{
    public class CourseHasLesson
    {
        public long? CourseId { get; set; }
        public Course Course { get; set; }
        public long? LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}