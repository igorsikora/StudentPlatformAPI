using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentPlatformAPI.models;
using StudentPlatformAPI.dto;

namespace StudentPlatformAPI.data
{
    public class StudentPlatformContext : DbContext
    {
        public StudentPlatformContext(DbContextOptions<StudentPlatformContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Task>().ToTable("Task").HasOne(t => t.Student).WithMany(s => s.Tasks)
                .HasForeignKey(t => t.StudentId);

            modelBuilder.Entity<CalendarEvent>().ToTable("CalendarEvent").HasOne(t => t.Student).WithMany(s => s.CalendarEvents)
                .HasForeignKey(t => t.StudentId);

            //Seed data with Statuses Enum
            foreach (Statuses status in Enum.GetValues(typeof(Statuses)).Cast<Statuses>())
            {
                Status s = new Status()
                {
                    Id = status,
                    Name = status.ToString()
                };

                modelBuilder.Entity<Status>().HasData(s);
            }
        }
    }
}