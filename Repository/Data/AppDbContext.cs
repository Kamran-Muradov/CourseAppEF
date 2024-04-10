using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-7GHJ7SJ\\SQLEXPRESS;" +
                "Database=CourseAppEFDb;" +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=True;" +
                "Trust Server Certificate=True;" +
                "Application Intent=ReadWrite;" +
                "Multi Subnet Failover=False");
        }
    }
}
