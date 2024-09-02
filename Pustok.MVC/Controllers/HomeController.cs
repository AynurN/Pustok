using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pustok.Business.Interfaces;
using Pustok.Business.ViewModels.Home;
using Pustok.Core.Models;
using Pustok.MVC.ViewModels;
using System.Diagnostics;

namespace Pustok.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;

        public HomeController(IBookService bookService)
        {
            this.bookService = bookService;
        }


        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                FeaturedBooks = (List<Book>)await bookService.GetAllAsync(),
                ExpensiveBooks = (List<Book>)await bookService.GetAllOrderDescAsync(null, x => x.CostPrice, "Author", "Genre", "BookImages"),
                NewBooks = (List<Book>)await bookService.GetAllOrderDescAsync(null, x => x.CreateDate, "Author", "Genre", "BookImages")


            };
           return View(homeVM);
        }
        public async Task<IActionResult> AddToBasket(int bookId) {
            if(bookId<1) return NotFound();

            if (await bookService.IsExist(b=>b.Id==bookId)==false) return NotFound();


            List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();
            BasketItemVM basketItemVM = null;
            string? basketItemStr = HttpContext.Request.Cookies["basketItems"];
            if (basketItemStr != null)
            {
                basketItemVMs = JsonConvert.DeserializeObject<List<BasketItemVM>>(basketItemStr);
                basketItemVM = basketItemVMs.FirstOrDefault(i => i.BookId == bookId);
                if (basketItemVM != null)
                {
                    basketItemVM.Count++;
                }
                else
                {
                    basketItemVM = new BasketItemVM
                    {
                        BookId = bookId,
                        Count = 1
                    };
                    basketItemVMs.Add(basketItemVM);

                }
            }
            else
            {
                basketItemVM = new BasketItemVM
                {
                    BookId = bookId,
                    Count = 1
                };
                basketItemVMs.Add(basketItemVM);
            }
             basketItemStr=JsonConvert.SerializeObject(basketItemVMs);
            HttpContext.Response.Cookies.Append("basketItems",basketItemStr);
            return Ok();
        }


    }
}
