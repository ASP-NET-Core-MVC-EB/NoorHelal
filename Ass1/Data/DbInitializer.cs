using Microsoft.AspNetCore.Identity;
using RestaurantApplication.Models;

namespace RestaurantApplication.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            

            if(! await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if(!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var email = "admin@res.com";
            var adminUser = await userManager.FindByEmailAsync(email);
            if(adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, "Admin@123");
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            
        }
    }
}
