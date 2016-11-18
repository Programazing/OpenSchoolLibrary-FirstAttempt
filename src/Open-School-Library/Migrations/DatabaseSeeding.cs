using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Open_School_Library.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Open_School_Library.Models;
using Microsoft.AspNetCore.Identity;
using Open_School_Library.Data.Entities;

namespace Open_School_Library.Migrations
{
    public static class DatabaseSeeding
    {
        //TODO: Add logging to the exceptions. Break up Initialize into smaller methods.
        static IServiceProvider services;
        static UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();

        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var identityContext = serviceProvider.GetService<ApplicationDbContext>();
            var libraryContext = serviceProvider.GetService<LibraryContext>();

            try
            {
                if(!libraryContext.Settings.Any())
                {
                    var settings = new Setting()
                    {
                        AreFinesEnabled = true,
                        CheckoutDurationInDays = 30,
                        FineAmountPerDay = 0.25m
                    };

                    libraryContext.Add(settings);
                    await libraryContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }


            string[] roles = new string[] { "Administrator", "Librarian" };

            try
            {
                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(identityContext);

                    if (!identityContext.Roles.Any(r => r.Name == role))
                    {
                        await roleStore.CreateAsync(new IdentityRole()
                        {
                            Name = role,
                            NormalizedName = role.ToUpper()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }


            var adminUser = new ApplicationUser
            {
                Email = "admin@library.com",
                NormalizedEmail = "ADMIN@LIBRARY.COM",
                UserName = "Administrator",
                NormalizedUserName = "ADMINISTRATOR",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            try
            {
                if (!identityContext.Users.Where(u => u.UserName == adminUser.UserName).Any())
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(adminUser, "SuperSecretPassword2#");
                    adminUser.PasswordHash = hashed;

                    var userStore = new UserStore<ApplicationUser>(identityContext);
                    var result = userStore.CreateAsync(adminUser);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            try
            {
                await AssignRole(serviceProvider, adminUser.Email, roles[0]);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            try
            {
                await identityContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public static async Task<IdentityResult> AssignRole(IServiceProvider services, string email, string role)
        {
            UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRoleAsync(user, role);

            return result;
        }


    }
}
