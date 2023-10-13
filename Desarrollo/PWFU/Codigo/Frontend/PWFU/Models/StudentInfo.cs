using System.ComponentModel.DataAnnotations.Schema;

namespace PWFU.Models;

[Table("student_info")]
public class StudentInfo : Base
{
    public string Speciality { get; set; } = null!;
    public int YearOfEntry { get; set; }
    public int Semester { get; set; }
    public string University { get; set; } = null!;
    [ForeignKey("Student")]
    public Guid StudentId { get; set; }
    public User Student { get; set; } = null!;
}