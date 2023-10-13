using System.ComponentModel.DataAnnotations.Schema;

namespace PWFU.Models;

[Table("rewards")]
public class Reward: Base
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public float Amount { get; set; }
    public string Image { get; set; } = null!;
    [ForeignKey("Project")]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}