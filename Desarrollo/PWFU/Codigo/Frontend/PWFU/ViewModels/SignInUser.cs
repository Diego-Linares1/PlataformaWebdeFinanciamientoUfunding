using System.ComponentModel.DataAnnotations;

namespace PWFU.ViewModels;

public class SignInUser
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}