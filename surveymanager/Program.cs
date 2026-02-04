using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using Microsoft.AspNetCore.Identity;
using Projekt.Data.Initializers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<SurveyManagerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SurveyManagerContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SurveyManagerContext>();

    var retries = 5;
    for (int i = 0; i < retries; i++)
    {
        try
        {
            context.Database.Migrate();
            Console.WriteLine("Baza danych zaktualizowana do najnowszej migracji.");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"B³¹d migracji bazy (próba {i + 1}/{retries}): {ex.Message}");
            if (i == retries - 1) throw;
            Thread.Sleep(5000);
        }
    }

    await DbInitializer.InitializeAsync(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Urls.Add("http://0.0.0.0:5000");

app.Run();
