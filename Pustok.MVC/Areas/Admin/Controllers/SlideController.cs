﻿using Microsoft.AspNetCore.Mvc;

namespace Pustok.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
