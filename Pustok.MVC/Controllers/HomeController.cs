using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.Business.Interfaces;
using Pustok.Business.ViewModels.Home;
using Pustok.Core.Models;
using Pustok.Data.DAL;
using Pustok.MVC.ViewModels;
using System.Diagnostics;

namespace Pustok.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext context;

        public HomeController(IBookService bookService, UserManager<AppUser> userManager, AppDbContext context)
        {
            this.bookService = bookService;
            this.userManager = userManager;
            this.context = context;
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
        public async Task<IActionResult> AddToBasket(int bookId)
        {
            if (bookId < 1) return NotFound();

            if (await bookService.IsExist(b => b.Id == bookId) == false) return NotFound();

            AppUser appUser = null;

            List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();
            BasketItemVM basketItemVM = null;
            BasketItem basketItem = null;
            string? basketItemStr = HttpContext.Request.Cookies["basketItems"];
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser=await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            if (appUser == null) {
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
                basketItemStr = JsonConvert.SerializeObject(basketItemVMs);
                HttpContext.Response.Cookies.Append("basketItems", basketItemStr);
            }
            else
            {
                basketItem = await context.BasketItems.FirstOrDefaultAsync(b => b.BookId == bookId && b.AppUserId == appUser.Id);
                if (basketItem != null) { 
                 basketItem.Count++;
                }
                else
                {
                    basketItem = new BasketItem()
                    {
                        AppUserId = appUser.Id,
                        BookId = bookId,
                        Count = 1,
                        IsDeleted = false,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    };
                    await context.BasketItems.AddAsync(basketItem);
                }
                await context.SaveChangesAsync();

            }
           
            return Ok();
        }

    }
}
