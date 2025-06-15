using Microsoft.EntityFrameworkCore;
using MogonebaAPI.Models;

namespace MogonebaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Memory> Memories { get; set; }
    }
}
