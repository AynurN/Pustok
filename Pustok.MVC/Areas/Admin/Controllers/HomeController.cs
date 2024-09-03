using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Core.Models;

namespace Pustok.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser user = new AppUser
        //    {
        //        Fullname="Aynur Nazarli",
        //        UserName="SupderAdmin",
        //        Email="aynur@gmail.com"
        //    };
        //    await userManager.CreateAsync(user, "Aynur123@");
        //    return Ok();
        //}
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Admin");
        //    IdentityRole role3 = new IdentityRole("Member");
        //   await roleManager.CreateAsync(role1);
        //   await roleManager.CreateAsync(role2);
        //   await roleManager.CreateAsync(role3);
        //    return Ok();
        //}
        //public async Task<IActionResult> AddRole()
        //{
        //    AppUser? user = await userManager.FindByNameAsync("SuperAdmin");
            
        //     var result=  await userManager.AddToRoleAsync(user, "SuperAdmin");
            
        //    return Ok(result);
        //}
    }
}
