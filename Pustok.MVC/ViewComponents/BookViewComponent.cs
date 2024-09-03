using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pustok.Business.Implementations;
using Pustok.Business.Interfaces;
using Pustok.Core.Models;
using Pustok.MVC.ViewModels;

namespace Pustok.MVC.ViewComponents
{
    public class BookViewComponent :ViewComponent
    {
        private readonly IBookService bookService;

        public BookViewComponent(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();
            BasketItemVM basketItemVM = null;
            string? basketItemStr = HttpContext.Request.Cookies["basketItems"];
            if (basketItemStr != null)
            {
                {
                    basketItemVMs = JsonConvert.DeserializeObject<List<BasketItemVM>>(basketItemStr);
                }
            }
            List<Book> books = new List<Book>();
            foreach (var vm in basketItemVMs)
            {
                books.Add(bookService.GetByIdAsync(vm.BookId).Result);
            }
            return View(books);

        }
    }
}
