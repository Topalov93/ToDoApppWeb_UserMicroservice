using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;
using ToDoApp.Models.Users;

namespace DAL.Data
{
    public class ToDoAppDbContext : DbContext
    {
        private const string _connectionString = "Data Source = .;Initial Catalog = ToDoAppdbWeb_UserMicroservice;Integrated Security = True;TrustServerCertificate = False;";

        public ToDoAppDbContext()
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_connectionString);

                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(_connectionString);
            }
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
