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

//// DbContext
//builder.Services.AddDbContext<ProjektContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<SurveyManagerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    .AddEntityFrameworkStores<SurveyManagerContext>();

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

    var context = services.GetRequiredService<SurveyManagerContext>();

    // Retry na wypadek gdy baza jeszcze nie jest gotowa
    var retries = 5;
    for (int i = 0; i < retries; i++)
    {
        try
        {
            context.Database.Migrate();
            Console.WriteLine("Baza danych zaktualizowana do najnowszej migracji.");
            break; // sukces, wychodzimy z pêtli
        }
        catch (Exception ex)
        {
            Console.WriteLine($"B³¹d migracji bazy (próba {i + 1}/{retries}): {ex.Message}");
            if (i == retries - 1) throw; // ostatnia próba, wyrzucamy wyj¹tek
            Thread.Sleep(5000); // czekaj 5 sekund przed kolejn¹ prób¹
        }
    }

    await DbInitializer.InitializeAsync(services);
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
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

app.Urls.Add("http://0.0.0.0:5000");

app.Run();
