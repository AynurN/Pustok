using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Exceptions;
using Pustok.Business.Interfaces;
using Pustok.Core.Models;

namespace Pustok.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IBookService bookService;

        public ShopController( IBookService bookService)
        {
            this.bookService = bookService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetProductModal(int id)
        {
            if (id < 1)
            {
                throw new IdIsNotValidException("Id is not valid");
            }

            Book book= await bookService.GetByIdAsync(id);
            if (book == null) {
                throw new BookNotFoundException("Book not found!");
                    }
            return PartialView("BookModalPartial",book);
        }
    }
}
