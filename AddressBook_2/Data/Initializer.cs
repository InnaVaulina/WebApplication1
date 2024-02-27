using AddressBook_2mvc.AddressBookException;
using AddressBook_2mvc.IdentityModel;
using AddressBook_2mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AddressBook_2mvc.Data
{

    public class AddressBookContextFactory : IDesignTimeDbContextFactory<AddressBook_2mvcContext>
    {
        public AddressBook_2mvcContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();        
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var builder = new DbContextOptionsBuilder<AddressBook_2mvcContext>();
            builder.UseSqlServer(connectionString);
            
            return new AddressBook_2mvcContext(builder.Options);
        }
    }

    public static class Initializer 
    {
        public static void InitializeNote(this AddressBook_2mvcContext context) 
        {
            if (context.Note.Any()) return;

            Note note1 = new Note()
            {
                Id = 1,
                FamilyName = "Раскольников",
                Name = "Родион",
                PatronymicName = "",
                Tel = "8(692)123123",
                Address = "Бахчисарай, ул. Фрунзе, дом 26",
                Description = "долг 6 000 000 рублей"
            };

            Note note2 = new Note()
            {
                Id = 2,
                FamilyName = "Свидригайло",
                Name = "Аркадий",
                PatronymicName = "",
                Tel = "8(692)123125",
                Address = "Бахчисарай, ул. Фрунзе, дом 26",
                Description = "долг 7 000 000 рублей"
            };

            Note note3 = new Note()
            {
                Id = 3,
                FamilyName = "Мармеладова",
                Name = "Соня",
                PatronymicName = "",
                Tel = "8(692)123125",
                Address = "Бахчисарай, ул. Фрунзе, дом 26",
                Description = "долг 5 000 000 рублей"
            };

            using(var transaction = context.Database.BeginTransaction()) 
            {
                context.Note.Add(note1);
                context.Note.Add(note2);
                context.Note.Add(note3);
                context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT [dbo].[Note] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT [dbo].[Note] OFF");
                transaction.Commit();
            }
            
        }

        public static async Task InitializeUserAsync(this ServiceProvider provider)
        {
            var context = provider.GetRequiredService<AddressBook_2mvcContext>();
          
            var admins = from user in context.Users
                            join userRole in context.UserRoles on user.Id equals userRole.UserId
                            join roles in context.Roles on userRole.RoleId equals roles.Id
                            where roles.Name == "admin"
                        select new UserModel
                            {
                                Id = user.Id,
                                UserName = user.UserName,
                                UserRole = roles.Name,
                            };

            if (admins.Any()) return;
            

            var userManager = provider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            AppUser admin = new AppUser()
            {
                UserName = "Admin"
            };

            string defaultPassword = "123qwe";

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            await roleManager.CreateAsync(role1);
            await roleManager.CreateAsync(role2);

            var result = await userManager.CreateAsync(admin, defaultPassword);
            if (result.Succeeded) 
            {
                await userManager.AddToRoleAsync(admin, role1.Name);
            }
        }
    }
}
