using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entity;

public class Student
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }

    public bool Subscribed { get; set; }
}