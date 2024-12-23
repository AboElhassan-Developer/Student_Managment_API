using System.ComponentModel.DataAnnotations;

namespace Student_Management_API.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }=string.Empty;

        [Required(ErrorMessage = "Grade is required.")]
        [RegularExpression(@"^(Grade [1-9]|Grade 1[0-2])$", ErrorMessage = "Grade must be 'Grade 1' to 'Grade 12'.")]
        public string Grade { get; set; } = string.Empty;
        [Required(ErrorMessage = "Age is required.")]
        [Range(5, 18, ErrorMessage = "Age must be between 5 and 18.")]
        public int Age { get; set; }

        public bool IsDeleted { get; set; } = false; // Soft delete flag
    }
}
