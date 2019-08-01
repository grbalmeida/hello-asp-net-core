namespace InstitutionOfHigherEducation.Models
{
    public class Department
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? InstitutionId { get; set; }
        public Institution Institution { get; set; }
    }
}