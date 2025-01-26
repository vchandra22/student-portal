using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entity;

public class Users
{
    [Key]
    public Guid UserId { get; set; } = Guid.NewGuid();
    
    [Required]
    public string? Name { get; set; }
    
    public int Age { get; set; }
}