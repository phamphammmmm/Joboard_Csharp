using Joboard.Entities;
using Joboard.Entities.Customer;
using Microsoft.EntityFrameworkCore;

namespace Joboard.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Create_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<User>()
                .Property(u => u.Update_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Role>()
                .Property(u => u.Create_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Role>()
                .Property(u => u.Update_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Tag>()
               .Property(u => u.Create_at)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Tag>()
                .Property(u => u.Update_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Category>()
               .Property(u => u.Create_at)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Category>()
                .Property(u => u.Update_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Company>()
                .Property(u => u.Create_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Company>()
                .Property(u => u.Update_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Job>()
                .Property(u => u.Create_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Job>()
                .Property(u => u.Update_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


            modelBuilder.Entity<UserRole>()
                .HasKey(wt => new { wt.User_Id, wt.Role_Id });
            modelBuilder.Entity<UserRole>()
                .HasOne(wt => wt.User)
                .WithMany(u => u.UserRoles) 
                .HasForeignKey(wt => wt.User_Id);
            modelBuilder.Entity<UserRole>()
                .HasOne(wt => wt.Role)
                .WithMany(r => r.UserRoles) 
                .HasForeignKey(wt => wt.Role_Id);
        }
    }
}
