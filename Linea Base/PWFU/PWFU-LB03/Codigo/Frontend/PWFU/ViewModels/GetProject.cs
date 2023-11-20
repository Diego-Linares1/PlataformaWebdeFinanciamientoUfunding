using PWFU.Models;

namespace PWFU.ViewModels;

public class GetProject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImagePath { get; set; }
    public string ProjectGoal { get; set; } = null!;
    public float MoneyGoal { get; set; }
    public DateTime DeadLine { get; set; }
    public string History { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public List<Donation>? Donations { get; set; }
}