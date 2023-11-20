using Microsoft.AspNetCore.Identity;

namespace PWFU.Models;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<IdentityUserClaim<Guid>>? Claims { get; set; }
    public ICollection<IdentityUserLogin<Guid>>? Logins { get; set; }
    public ICollection<IdentityUserToken<Guid>>? Tokens { get; set; }
    public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<Project>? Projects { get; set; }
    public ICollection<Donation>? Donations { get; set; }
    public StudentInfo? StudentInfo { get; set; }
}