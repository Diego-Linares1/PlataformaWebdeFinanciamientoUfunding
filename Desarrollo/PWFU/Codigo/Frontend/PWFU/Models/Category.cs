using System.ComponentModel.DataAnnotations.Schema;

namespace PWFU.Models;

[Table("categories")]
public class Category : Base
{
    public string Name { get; set; } = null!;
    public IEnumerable<Project> Projects { get; set; } = null!;
}