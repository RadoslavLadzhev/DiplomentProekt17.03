using ASPBMWElit.Data;
using Microsoft.AspNetCore.Identity;

namespace ASPBMWElit.Services
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDataBase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<Client>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                //Sazdavane na roles
                await SeedRolesAsync(roleManager);
                //sazdavane na SUPER ADMIN s vsi4kite mu roli
                await SeedSuperAdminAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }

            return app;
        }
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {

            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
            await roleManager.CreateAsync(new IdentityRole("Guest"));
        }

        public static async Task SeedSuperAdminAsync(UserManager<Client> userManager)
        {
            var email = "superadmin@gmail.com";

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new Client
                {
                    UserName = "superadmin",
                    Email = email,
                    FirstName = "ivan",
                    LastName = "ivanov",
                    PhoneNumber = "0899999999",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "123!@#Qwe");

                if (!result.Succeeded)
                    return;
            }

            // 🔥 ТОВА Е МЯСТОТО
            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}