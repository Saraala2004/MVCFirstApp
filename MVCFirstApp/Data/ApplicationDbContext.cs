using Microsoft.EntityFrameworkCore;
using MVCFirstApp.Models;

namespace MVCFirstApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }

        public DbSet<Report> Reports { get; set; }
    }
}