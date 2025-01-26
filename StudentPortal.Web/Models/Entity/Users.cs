using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entity;

public class Users
{
    [Key]
    public Guid UserId { get; set; } = Guid.NewGuid();
    
    [Required]
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}