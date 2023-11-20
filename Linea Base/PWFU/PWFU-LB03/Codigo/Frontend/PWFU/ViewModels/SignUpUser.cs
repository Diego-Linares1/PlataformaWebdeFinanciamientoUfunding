namespace PWFU.ViewModels;

public class SignUpUser
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Password { get; set; }
    public string? Speciality { get; set; }
    public string? University { get; set; }
    public int? YearOfEntry { get; set; }
    public int? Semester { get; set; }
    public bool IsStudent { get; set; }
}