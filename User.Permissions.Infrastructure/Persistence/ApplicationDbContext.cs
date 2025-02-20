using Microsoft.EntityFrameworkCore;
using User.Permissions.Domain.Entities;

namespace User.Permissions.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasKey(p => p.Id);
            modelBuilder.Entity<PermissionType>().HasKey(pt => pt.Id);

            modelBuilder.Entity<Permission>()
                .Property(p => p.EmployeeForename)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Permission>()
                .Property(p => p.EmployeeSurname)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.PermissionTypeNavigation) 
                .WithOne(pt => pt.Permission)
                .HasForeignKey<Permission>(p => p.PermissionType);
        }
    }
}
