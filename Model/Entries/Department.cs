using System.Collections.Generic;

namespace Model.Entries
{
    public class Department
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}