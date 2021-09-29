using FinalAssignment.DAL.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinalAssignment.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }

    public class UserDbContext : DbContext
    {

        public UserDbContext()
        {
        }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { set; get; }
        public DbSet<Minute> Minutes { set; get; }
        public DbSet<Crew> Crews { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(employee => employee.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Employee>().Property(employee => employee.CreatedAt).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Crew>().Property(crew => crew.CreatedAt).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Crew>().Property(crew => crew.IsActive).HasDefaultValue(true);

            modelBuilder.Entity<Minute>().Property(minute => minute.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Minute>().Property(minute => minute.CreatedAt).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Minute>().Property(minute => minute.ApprovalStatus).HasDefaultValue(false);
            modelBuilder.Entity<Minute>().Property(minute => minute.ApprovalHistory).HasDefaultValue(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=vinayakt\\SQLEXPRESS;Database=FinalAssignment;User Id=sa;Password=password@123;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
