using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotelOne19679091.Models;

namespace HotelOne19679091.Data
{
    public class SeedRoles
    {
        public static async Task CreateRoles (IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            // RoleManager and Usermanager objects
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //role names here
            string[] roleNames = { "Admin", "Customer" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                //see if the role already exists
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
                //making an admin user who will maintain web app
                //their username are read from the configuration file: appsetting.json

                var poweruser = new IdentityUser
                {
                    UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                    Email = Configuration.GetSection("UserSettings")["UserEmail"]
                };

                string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];
                var user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

                //if the admin details aren't in the databse, add it. otherwise do nothing
                if (user == null)
                {
                    var createPowerUser = await UserManager.CreateAsync(poweruser, userPassword);
                    if (createPowerUser.Succeeded)
                    {
                        //here we assign the new user the admin role
                        await UserManager.AddToRoleAsync(poweruser, "Admin");
                    }
                }
            
        }
    }
}
