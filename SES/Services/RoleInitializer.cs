using Microsoft.AspNetCore.Identity;

using SES.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Services
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (userManager.Users.Count() == 0)
            {
                User admin = new User { Email = "admin@gmail.com", UserName = "administrator" };
                User user = new User { Email = "user@gmail.com", UserName = "user" };
                IdentityResult adminResult = await userManager.CreateAsync(admin, "123qazedc");
                IdentityResult userResult = await userManager.CreateAsync(user, "asdasd123");
                if (adminResult.Succeeded && userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
        }
    }
}
