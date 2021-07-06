using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Switch> Switches { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
