using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using StudentPlatformAPI.Models;
using StudentPlatformAPI.Models.Auth;

namespace StudentPlatformAPI.Data
{
    public class StudentPlatformSeeder
    {
        public static void SeedData
            (UserManager<User> userManager, StudentPlatformContext _context)
        {
            _context.Database.EnsureCreated();
            SeedUsers(userManager);
            SeedCalendarEvents(_context, userManager);
            SeedTasks(_context, userManager);

            _context.SaveChanges();
        }

        public static void SeedUsers
            (UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync
                ("test").Result == null)
            {
                var user = new User();
                user.Id = new Guid("138490B4-7843-4169-D631-08D9EE03FCD5");
                user.UserName = "test";
                user.Email = "test@test.pl";
                user.FirstName = "Test";
                user.LastName = "Testowsky";

                var result = userManager.CreateAsync(user, "zaq1@WSX").Result;
            }
        }

        public static void SeedCalendarEvents(StudentPlatformContext _context, UserManager<User> userManager)
        {
            if (!_context.CalendarEvents.Any())
            {
                var seededUser = userManager.FindByNameAsync("test").Result;
                CalendarEvent[] events =
                {
                    new()
                    {
                        Title = "test",
                        Description = "test desc",
                        StartDate = new DateTime(2022, 1, 1, 20, 30, 00),
                        EndDate = new DateTime(2022, 1, 1, 21, 30, 00),
                        UserId = seededUser.Id
                    },
                    new()
                    {
                        Title = "test to be updated",
                        Description = "test to be updated",
                        StartDate = new DateTime(2022, 1, 1, 20, 30, 00),
                        EndDate = new DateTime(2022, 1, 1, 21, 30, 00),
                        UserId = seededUser.Id
                    }
                };
                _context.CalendarEvents.AddRange(events);
            }
        }

        public static void SeedTasks(StudentPlatformContext _context, UserManager<User> userManager)
        {
            if (!_context.Tasks.Any())
            {
                var seededUser = userManager.FindByNameAsync("test").Result;
                Task[] tasks =
                {
                    new()
                    {
                        Title = "test",
                        StatusId = (int)Statuses.ToDo,
                        UserId = seededUser.Id
                    },
                    new()
                    {
                        Title = "test to be updated",
                        StatusId = (int)Statuses.ToDo,
                        UserId = seededUser.Id
                    }
                };
                _context.Tasks.AddRange(tasks);
            }
        }
    }
}