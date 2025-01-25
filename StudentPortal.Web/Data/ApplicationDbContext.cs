using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Models.Entity;

namespace StudentPortal.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<Users> Users { get; set; }
}