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
    public class DatabaseSeeding
    {
        //TODO: Add logging to the exceptions. Break up Initialize into smaller methods.
        //static IServiceProvider services;
        //static UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();

        //private static readonly ApplicationDbContext _identityContext;
        //private static readonly LibraryContext _libraryContext;

        //public DatabaseSeeding(IServiceProvider serviceProvider)
        //{
        //    _identityContext = serviceProvider.GetService<ApplicationDbContext>();
        //    _libraryContext = serviceProvider.GetService<LibraryContext>();
        //}


        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var identityContext = serviceProvider.GetService<ApplicationDbContext>();
            var libraryContext = serviceProvider.GetService<LibraryContext>();
            string[] roles = new string[] { "Administrator", "Librarian" };
            var adminUser = new ApplicationUser
            {
                Email = "admin@library.com",
                NormalizedEmail = "ADMIN@LIBRARY.COM",
                UserName = "admin@library.com",
                NormalizedUserName = "ADMIN@LIBRARY.COM",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            await PopulateSettings(libraryContext).Result;

            await CreateUserRoles(identityContext, roles);

            await AddAdminToUsers(identityContext, adminUser);
            
            await AssignAdminToRole(serviceProvider, adminUser.Email, roles[0]);
        }

        private static async Task<Setting> PopulateSettings(LibraryContext libraryContext)
        {
            var settings = new Setting()
            {
                AreFinesEnabled = true,
                CheckoutDurationInDays = 30,
                FineAmountPerDay = 0.25m
            };

            if (!libraryContext.Settings.Any())
            {
                try
                {
                    libraryContext.Add(settings);
                    await libraryContext.SaveChangesAsync();

                    return settings;
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            }

            return settings;
        }
        private static async Task CreateUserRoles (ApplicationDbContext identityContext, String[] roles)
        {
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

                await identityContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }
        private static async Task AddAdminToUsers(ApplicationDbContext identityContext,
            ApplicationUser adminUser)
        {
            try
            {
                if (!identityContext.Users.Where(u => u.UserName == adminUser.UserName).Any())
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(adminUser, "SuperSecretPassword2#");
                    adminUser.PasswordHash = hashed;

                    var userStore = new UserStore<ApplicationUser>(identityContext);
                    var result = await userStore.CreateAsync(adminUser);

                    await identityContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }
        private static async Task AssignAdminToRole(IServiceProvider services,
            string email, string role)
        {
            try
            {
                UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
                ApplicationUser user = await _userManager.FindByEmailAsync(email);
                var result = await _userManager.AddToRoleAsync(user, role);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                
            }
        }


    }
}
