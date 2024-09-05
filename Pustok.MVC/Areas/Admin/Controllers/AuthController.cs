using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Core.Models;
using Pustok.MVC.Areas.Admin.ViewModels;


namespace Pustok.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
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
        public async Task<IActionResult> Login(AdminLoginVM vm)
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
            var result= await signInManager.PasswordSignInAsync(appUser,vm.Password, vm.IsPersistent,false);
            if (!result.Succeeded) {
                ModelState.AddModelError("", "Invalid credentials!");
                return View();
            }
            return RedirectToAction("Index","home");
        }

    }
}
