using System.ComponentModel.DataAnnotations.Schema;

namespace PWFU.Models;

[Table("projects")]
public class Project : Base
{
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string ProjectGoal { get; set; } = null!;
    public float MoneyGoal { get; set; }
    public DateTime DeadLine { get; set; }
    public string History { get; set; } = null!;
    public string BankAccount { get; set; } = null!;
    [ForeignKey("Student")]
    public Guid StudentId { get; set; }
    public User Student { get; set; } = null!;
    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }
    public IEnumerable<Donation> Donations { get; set; } = null!;
    public Category Category { get; set; }
}