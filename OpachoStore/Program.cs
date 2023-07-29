using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpachoStore.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OpachoStoreContext>(opts =>
{
    opts.UseSqlite(builder.Configuration["ConnectionStrings:OpachoStoreConnection"]);
});


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IStoreRepository,EFStoreRepository>();

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<OpachoIdentityContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:IdentityConnection"]);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<OpachoIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute("catpage", "Products/{category}/Page{productPage:int}", new { Controller = "Products", action = "Index" });

app.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Products", action = "Index" });

app.MapControllerRoute("category", "Products/{category:alpha}", new { Controller = "Products", action = "Index", productPage = 1 });

app.MapControllerRoute("productpage", "Products/{productId:int}", new { Controller = "Products", action = "Product" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.Run();
