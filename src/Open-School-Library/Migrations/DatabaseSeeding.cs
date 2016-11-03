using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Open_School_Library.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Open_School_Library.Models;
using Microsoft.AspNetCore.Identity;

namespace Open_School_Library.Migrations
{
    public static class DatabaseSeeding
    {
        //TODO: Add logging to the exceptions. 
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            string[] roles = new string[] { "Administrator", "Librarian" };

            try
            {
                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);

                    if (!context.Roles.Any(r => r.Name == role))
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
                if (context.Users.Where(u => u.UserName == "Administrator") == null)
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(adminUser, "SuperSecretPassword2#");
                    adminUser.PasswordHash = hashed;

                    var userStore = new UserStore<ApplicationUser>(context);
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
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

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
