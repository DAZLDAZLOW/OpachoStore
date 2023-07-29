using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OpachoStore.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "SomePassword123$";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            OpachoIdentityContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<OpachoIdentityContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if( user == null)
            {
                user = new IdentityUser("Admin");
                user.Email = "admin@example.com";
                user.PhoneNumber = "228-1337-1488";
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
