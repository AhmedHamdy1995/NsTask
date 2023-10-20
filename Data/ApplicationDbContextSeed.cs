using Microsoft.AspNetCore.Identity;
using NsTask.Api.Domain.Enteties;

namespace NsTask.Api.Data
{
    public static class ApplicationDbContextSeed
    {
        // method to create new user with attributes from parameters and to role from parameters
        public static async Task<ApplicationUser> SeedUser(UserManager<ApplicationUser> _userManager, ApplicationUser InputUser, string Password, string Role)
        {
            var user = new ApplicationUser
            {
                FirstName = InputUser.FirstName,
                LastName = InputUser.LastName,
                UserName = InputUser.UserName,
                Email = InputUser.Email,
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(user, Password);
            await _userManager.AddToRoleAsync(user, Role);
            return user;
        }

        // method to seed roles
        public static async Task SeedRolesAsync(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
                await context.Roles.AddAsync(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            }
        }

        // method to add the default users
        public static async Task SeedDefaultUsersAsync(UserManager<ApplicationUser> _userManager, ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var user = new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    EmailConfirmed = true,
                };

                var password = "123@Admin";
                var role = "Admin";
                await SeedUser(_userManager, user, password, role);

            }

        }
    }
}
