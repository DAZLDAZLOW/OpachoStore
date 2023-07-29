using Microsoft.EntityFrameworkCore;

namespace OpachoStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            OpachoStoreContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<OpachoStoreContext>();
            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
