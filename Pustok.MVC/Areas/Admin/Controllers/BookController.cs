using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Exceptions;
using Pustok.Business.Implementations;
using Pustok.Business.Interfaces;
using Pustok.Business.ViewModels.Book;
using Pustok.Core.Models;

namespace Pustok.MVC.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class BookController : Controller
    {

        private readonly IBookService bookService;
        private readonly IGenreService genre;
        private readonly IAuthorService author;

        public BookController(IBookService bookService,IGenreService genre, IAuthorService author)
        {
            this.bookService = bookService;
            this.genre = genre;
            this.author = author;
        }
        public async Task<IActionResult> Index()
        {
            List<BookGetVM> bookVMs = new List<BookGetVM>();
            foreach (Book book in await bookService.GetAllAsync())
            {
                BookGetVM bookGetVM = new BookGetVM()
                {
                    Title = book.Title,
                    Author = book.Author,
                    Genre = book.Genre,
                    SalePrice = book.SalePrice,
                    Id = book.Id,
                    StockCount = book.StockCount,
                    ImageURl=book.BookImages?.FirstOrDefault(i=>i.IsPrimary==true)?.ImageUrl

                };
                bookVMs.Add(bookGetVM);
            }
            return View(bookVMs);
        }

        public async Task< IActionResult> Create()
        {
            BookCreateVM bookCreateVM = new BookCreateVM()
            {
                Genres = await genre.GetAllAsync(),
                Authors = await author.GetAllAsync(),
            };

            return View(bookCreateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateVM bookVM)
        {
            if (!ModelState.IsValid)
            {
                bookVM.Genres = await genre.GetAllAsync();
                bookVM.Authors = await author.GetAllAsync();
                return View(bookVM);
            }
            try
            {
                await bookService.CreateAsync(bookVM);
            }
            catch (NotValidException ex)
            {
                ModelState.AddModelError(ex.PropName, ex.Message);
                bookVM.Genres = await genre.GetAllAsync();
                bookVM.Authors = await author.GetAllAsync();
                return View(bookVM);
            }
            catch(GenreNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropName, ex.Message);
                bookVM.Genres = await genre.GetAllAsync();
                bookVM.Authors = await author.GetAllAsync();
                return View(bookVM);
            }
            catch(AuthorNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropName, ex.Message);
                bookVM.Genres = await genre.GetAllAsync();
                bookVM.Authors = await author.GetAllAsync();
                return View(bookVM);

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                bookVM.Genres = await genre.GetAllAsync();
                bookVM.Authors = await author.GetAllAsync();
                return View(bookVM);
            }

            return RedirectToAction(nameof(Index));


        }


    }
}
