using Graduation_Project.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Graduation_Project.Infrastructure.SeedData
{
    public class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context,
        UserManager<IdentityUser<Guid>> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
        {

            context.Database.EnsureCreated();

            string asdminRole = "Admin";
            string userRole = "User";
            string password4all = "123456789";

            if (await roleManager.FindByNameAsync(asdminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(asdminRole));
            }

            if (await roleManager.FindByNameAsync(userRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(userRole));
            }

            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new IdentityUser<Guid>
                {
                    UserName = "admin",
                    Email = "admin",
                    PhoneNumber = "1242235345"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, asdminRole);
                }
            }

            //if (await userManager.FindByNameAsync("mm@mm.mm") == null)
            //{
            //    var user = new IdentityUser<Guid>
            //    {
            //        UserName = "mm@mm.mm",
            //        Email = "mm@mm.mm",
            //        PhoneNumber = "1112223333"
            //    };

            //    var result = await userManager.CreateAsync(user);
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddPasswordAsync(user, password4all);
            //        await userManager.AddToRoleAsync(user, StudentRole);
            //    }
            //}
        }
    }
}
