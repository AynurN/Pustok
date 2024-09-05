using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Core.Models;
using Pustok.MVC.ViewModels;

namespace Pustok.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberLoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();
            }
            AppUser appUser = null;

            appUser = await userManager.FindByNameAsync(vm.Username);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Invalid credentials!");
                return View();
            }
            var result = await signInManager.PasswordSignInAsync(appUser, vm.Password, vm.IsPersistent, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid credentials!");
                return View();
            }
            return RedirectToAction("Index", "home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MemberRegisterVM vm)
        {
            if (!ModelState.IsValid) { 
              return View();
            }
            AppUser appUser = null;
            appUser= await userManager.FindByNameAsync(vm.Username);
            if (appUser != null) {
                ModelState.AddModelError("Username", "Member with this username already exists!");
            }
            appUser = await userManager.FindByEmailAsync(vm.Email);
            if (appUser != null)
            {
                ModelState.AddModelError("Email", "Member with this email already exists!");
            }

            appUser = new AppUser()
            {
                Fullname = vm.Fullname,
                Email = vm.Email,
                UserName=vm.Username,
                

            };
             var result=await userManager.CreateAsync(appUser,vm.Password);
            if (!result.Succeeded) {
                foreach (var item in result.Errors) { 
                    ModelState.AddModelError("",item.Description);
                }
            }
            var member= await userManager.FindByNameAsync(vm.Username);
            if (member != null) {
                await userManager.AddToRoleAsync(member, "Member");
            }
            
            return RedirectToAction("login","account");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");

        }
    }
}
