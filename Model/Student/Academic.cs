using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Student
{
    public class Academic
    {
        public long? Id { get; set; }

        [StringLength(10, MinimumLength = 10)]
        [RegularExpression("([0-9]{10})")]
        [Required]
        public string AcademicRecord { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required]

        public DateTime? DateOfBirth { get; set; }
    }
}