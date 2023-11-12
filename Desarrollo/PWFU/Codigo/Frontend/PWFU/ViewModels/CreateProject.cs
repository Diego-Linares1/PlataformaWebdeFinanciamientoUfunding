using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PWFU.Models;

namespace PWFU.ViewModels;

public class CreateProject
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public IFormFile Image { get; set; }
    [Required]
    public string ProjectGoal { get; set; } = null!;
    [Required]
    public float MoneyGoal { get; set; }
    [Required]
    public DateTime DeadLine { get; set; }
    public string History { get; set; } = null!;
    [Required]
    public string BankAccount { get; set; } = null!;
    [Required]
    public Guid CategoryId { get; set; }
    public List<Category>? Categories { get; set; }
}