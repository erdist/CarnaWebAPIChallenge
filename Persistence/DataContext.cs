using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<AuthAdmin> AuthAdmins { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AuthAdmin>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}