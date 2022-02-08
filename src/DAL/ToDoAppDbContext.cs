using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;
using ToDoApp.Models.Users;

namespace DAL.Data
{
    public class ToDoAppDbContext : DbContext
    {
        public ToDoAppDbContext(DbContextOptions<ToDoAppDbContext> options) : base(options)
        {
            
        }

        public ToDoAppDbContext()
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .Property(u => u.AddedOn)
               .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<User>()
              .Property(u => u.EditedOn)
              .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
