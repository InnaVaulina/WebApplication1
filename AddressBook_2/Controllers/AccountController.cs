using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AddressBook_2mvc.IdentityModel;
using AddressBook_2mvc.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using AddressBook_2mvc.Data;
using Microsoft.EntityFrameworkCore;
using AddressBook_2mvc.AddressBookException;

namespace AddressBook_2mvc.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AddressBook_2mvcContext _context;

        public AccountController(UserManager<AppUser> userManager, 
                                 SignInManager<AppUser> signInManager,
                                 AddressBook_2mvcContext context) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpGet]
        [Route("Account/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.LoginProp
                };
                
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                   // await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }



        [HttpGet]
        [Route("Account/Login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(EntryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.LoginProp, model.Password,false, false);

                if (result.Succeeded)
                {
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Account/AdminPage")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdminPage()
        {
            var users = from user in _context.Users
                        join userRole in _context.UserRoles on user.Id equals userRole.UserId
                        join roles in _context.Roles on userRole.RoleId equals roles.Id
                        select new UserModel
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            UserRole = roles.Name,
                        };

            return View(users);
        }

        [HttpGet]
        [Route("Account/UserPage")]
        [Authorize(Roles = "user")]
        public IActionResult UserPage()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
          
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return RedirectToAction("AdminPage", "Account");
            else 
            {
                var errorList = result.Errors.ToList();
                string errors = "";
                foreach (var err in errorList)
                    errors = $"{err.Description}; {errors}";
                return NotFound(errors);
            } 
            
        }



        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeUserRole(UserRole user)
        {
            var curUser = await _userManager.GetUserAsync(HttpContext.User);
            if (curUser.Id != user.Id)
            {
                AppUser targetUser = _context.Users.SingleOrDefault(x => x.Id == user.Id);

                var roles = await _userManager.GetRolesAsync(targetUser);
                await _userManager.RemoveFromRolesAsync(targetUser, roles);
                string newrole = "user";
                if (string.Compare(user.Role, "user") == 0) newrole = "admin";
                if (string.Compare(user.Role, "admin") == 0) newrole = "user";
                await _userManager.AddToRoleAsync(targetUser, newrole);
                return RedirectToAction("AdminPage", "Account");
            }
            else return BadRequest("Вы пытаетесь изменить роль администратора на роль пользователя");

        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.LoginProp
                };

                if(string.Compare(model.Password, model.ConfirmPassword) == 0) 
                {
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "user");
                        return RedirectToAction("AdminPage", "Account");
                    }
                    else
                    {

                    }
                }
                
            }
            return RedirectToAction("AdminPage", "Account");
        }
    }
}
