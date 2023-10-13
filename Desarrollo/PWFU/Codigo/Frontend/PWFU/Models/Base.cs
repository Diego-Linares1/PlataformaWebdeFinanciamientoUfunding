using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PWFU.Models;

public class Base
{
    [Key]
    public Guid Id { get; set; }
}