using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using Microsoft.AspNetCore.Identity;
using Projekt.Data.Initializers;

var builder = WebApplication.CreateBuilder(args);

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// DbContext
builder.Services.AddDbContext<ProjektContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
})
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ProjektContext>();

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//    options.SlidingExpiration = true;
//    options.LoginPath = "/Account/Login";
//    options.AccessDeniedPath = "/Account/AccessDenied";
//});



// MVC + Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.InitializeAsync(services);
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Mapowanie routingu
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
