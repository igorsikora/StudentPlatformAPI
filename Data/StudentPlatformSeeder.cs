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
            (UserManager<User> userManager, StudentPlatformContext context)
        {
            SeedUsers(userManager);
            SeedCalendarEvents(context, userManager);
            SeedTasks(context, userManager);

            context.SaveChanges();
        }

        public static void SeedUsers
            (UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync
                ("test").Result == null)
            {
                var user = new User
                {
                    Id = new Guid("138490B4-7843-4169-D631-08D9EE03FCD5"),
                    UserName = "test",
                    Email = "test@test.pl",
                    FirstName = "Test",
                    LastName = "Testowsky"
                };

                var result = userManager.CreateAsync(user, "zaq1@WSX").Result;

                user = new User
                {
                    Id = new Guid("138490B4-7843-4169-D631-08D9EE03FCD6"),
                    UserName = "changeUserName",
                    Email = "changeUserName@changeUserName.pl",
                    FirstName = "Change",
                    LastName = "UserName"
                };

                result = userManager.CreateAsync(user, "zaq1@WSX").Result;

                user = new User
                {
                    Id = new Guid("138490B4-7843-4169-D631-08D9EE03FCD7"),
                    UserName = "changePasswordUser",
                    Email = "changePasswordUser@changePasswordUser.pl",
                    FirstName = "Change",
                    LastName = "PasswordUser"
                };

                result = userManager.CreateAsync(user, "zaq1@WSX").Result;
                
                user = new User
                {
                    Id = new Guid("138490B4-7843-4169-D631-08D9EE03FCD8"),
                    UserName = "changeUserDetail",
                    Email = "changeUserDetail@changeUserDetail.pl",
                    FirstName = "Change",
                    LastName = "UserDetail"
                };

                result = userManager.CreateAsync(user, "zaq1@WSX").Result;
            }
        }

        public static void SeedCalendarEvents(StudentPlatformContext context, UserManager<User> userManager)
        {
            if (!context.CalendarEvents.Any())
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
                        StartDate = new DateTime(2022, 1, 2, 20, 30, 00),
                        EndDate = new DateTime(2022, 1, 2, 21, 30, 00),
                        UserId = seededUser.Id
                    }
                };
                context.CalendarEvents.AddRange(events);
            }
        }

        public static void SeedTasks(StudentPlatformContext context, UserManager<User> userManager)
        {
            if (!context.Tasks.Any())
            {
                var seededUser = userManager.FindByNameAsync("test").Result;
                Task[] tasks =
                {
                    new()
                    {
                        Title = "test",
                        Status = Statuses.ToDo,
                        UserId = seededUser.Id
                    },
                    new()
                    {
                        Title = "test to be updated",
                        Status = Statuses.ToDo,
                        UserId = seededUser.Id
                    }
                };
                context.Tasks.AddRange(tasks);
            }
        }
    }
}