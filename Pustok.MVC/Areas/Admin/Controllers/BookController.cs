using Microsoft.AspNetCore.Mvc;

namespace Pustok.MVC.Areas.Admin.Controllers
{
    public class BookController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
