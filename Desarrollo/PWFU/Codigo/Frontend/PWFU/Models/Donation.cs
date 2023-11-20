using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PWFU.Models;

[Table("donations")]
public class Donation: Base
{
    public float Amount { get; set; }
    public DateTime Date { get; set; }
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    [ForeignKey("Project")]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}