using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.Business.Implementations;
using Pustok.Business.Interfaces;
using Pustok.Core.Models;
using Pustok.Data.DAL;
using Pustok.MVC.ViewModels;
using System.Net;

namespace Pustok.MVC.ViewComponents
{
    public class BookViewComponent :ViewComponent
    {
        private readonly IBookService bookService;
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext context;

        public BookViewComponent(IBookService bookService, UserManager<AppUser> userManager, AppDbContext context)
        {
            this.bookService = bookService;
            this.userManager = userManager;
            this.context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();
            AppUser appUser = null;
            BasketItemVM basketItemVM = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            if (appUser == null)
            {
                string? basketItemStr = HttpContext.Request.Cookies["basketItems"];
                if (basketItemStr != null)
                {
                    {
                        basketItemVMs = JsonConvert.DeserializeObject<List<BasketItemVM>>(basketItemStr);
                    }
                }
               

            }
            else
            {
                List<BasketItem> basketItems = await context.BasketItems.Where(b => b.AppUserId == appUser.Id && b.IsDeleted==false).ToListAsync();
                foreach (var item in basketItems)
                {
                    BasketItemVM ıtemVM = new BasketItemVM
                    {
                        BookId = item.BookId,
                        Count = item.Count,

                    };
                    basketItemVMs.Add(ıtemVM);
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
