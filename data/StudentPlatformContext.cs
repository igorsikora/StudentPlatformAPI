using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentPlatformAPI.Models;
using StudentPlatformAPI.Models.Auth;

namespace StudentPlatformAPI.Data
{
    public class StudentPlatformContext : IdentityDbContext<User, Role, Guid>
    {
        public StudentPlatformContext(DbContextOptions<StudentPlatformContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Task>().ToTable("Task").HasOne(t => t.User).WithMany(s => s.Tasks)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<CalendarEvent>().ToTable("CalendarEvent").HasOne(t => t.User).WithMany(s => s.CalendarEvents)
                .HasForeignKey(t => t.UserId);

        }
    }
}