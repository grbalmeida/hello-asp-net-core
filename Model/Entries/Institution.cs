using System.Collections.Generic;

namespace Model.Entries
{
    public class Institution
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}