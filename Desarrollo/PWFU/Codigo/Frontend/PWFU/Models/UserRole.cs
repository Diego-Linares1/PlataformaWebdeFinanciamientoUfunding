using Microsoft.AspNetCore.Identity;

namespace PWFU.Models;

public class UserRole: IdentityUserRole<Guid>
{
    public User? User { get; set; }
    public Role? Role { get; set; }
}