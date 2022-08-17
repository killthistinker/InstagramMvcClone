using System.Threading.Tasks;
using InstagramMVC.Models;
using Microsoft.AspNetCore.Identity;

namespace InstagramMVC.Services
{
    public static class UserInitializer
    {
        public static async Task SeedUser(RoleManager<Role> roleManager)
        {

            string role = "user";
            if (await roleManager.FindByNameAsync(role) is null)
                    await roleManager.CreateAsync(new Role { Name = role });
        }
        
    }
}