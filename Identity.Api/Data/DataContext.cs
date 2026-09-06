using Identity.Api.Data.Mappings;
using Identity.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DataContext()
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=localhost;
                                Database=Identity;
                                User Id=sa;
                                Password=1q2w3e4r@;
                                TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
        }
    }
}