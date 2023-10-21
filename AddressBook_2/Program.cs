using AddressBook_2mvc.Data;
using AddressBook_2mvc.ViewData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AddressBook_2mvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AddressBook_2mvcContext") ?? throw new InvalidOperationException("Connection string 'AddressBook_2mvcContext' not found.")));


builder.Services.AddScoped<IAddressBookDBContex, AbstractDb>();
builder.Services.AddSingleton<INotesCollection, NotesCollection>();
builder.Services.AddSingleton<IIndexViewData, IndexViewData>();

builder.Services.AddControllersWithViews();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

