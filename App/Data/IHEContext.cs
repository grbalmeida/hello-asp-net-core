using Microsoft.EntityFrameworkCore;

using Model.Entries;
using Model.Student;

namespace InstitutionOfHigherEducation.Data
{
    public class IHEContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Academic> Academics { get; set; }

        public IHEContext(DbContextOptions<IHEContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CourseHasLesson>()
                .HasKey(courseHasLesson => new {
                    courseHasLesson.CourseId,
                    courseHasLesson.LessonId 
                });

            modelBuilder.Entity<CourseHasLesson>()
                .HasOne(courseHasLesson => courseHasLesson.Course)
                .WithMany(course => course.CoursesHasLessons)
                .HasForeignKey(course => course.CourseId);

            modelBuilder.Entity<CourseHasLesson>()
                .HasOne(courseHasLesson => courseHasLesson.Lesson)
                .WithMany(lesson => lesson.CoursesHasLessons)
                .HasForeignKey(lesson => lesson.LessonId);
        }
    }
}