using AddressBook_2mvc.Data;
using AddressBook_2mvc.ViewData;
using AddressBook_2mvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AddressBook_2mvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IAddressBookDBContex, AbstractDb>();
builder.Services.AddSingleton<INotesCollection, NotesCollection>();
builder.Services.AddSingleton<IIndexViewData, IndexViewData>();

builder.Services.AddControllersWithViews();



builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AddressBook_2mvcContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(
    options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 10;
        options.Lockout.AllowedForNewUsers = true;

        options.User.RequireUniqueEmail = false;
        
    });


builder.Services.ConfigureApplicationCookie(
    options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

// загрузка в бд первичных данных
var provider = builder.Services.BuildServiceProvider();

await provider.InitializeUserAsync();
provider.GetRequiredService<AddressBook_2mvcContext>()
    .InitializeNote();

//_______________________________

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

