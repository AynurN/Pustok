using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Exceptions;
using Pustok.Business.Implementations;
using Pustok.Business.Interfaces;
using Pustok.Business.ViewModels.Author;
using Pustok.Business.ViewModels.Genre;
using Pustok.Core.IRepositories;
using Pustok.Core.Models;

namespace Pustok.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService= authorService;
        }
        public async Task<IActionResult> Index()
        {
            ICollection<Author> authors = await authorService.GetAllAsync();
            return View(authors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateVM authorVM)
        {
            if (!ModelState.IsValid)
            {
                return View(authorVM);
            }
            try
            {
                await authorService.CreateAsync(authorVM);
            }
            catch (NotValidException ex)
            {
                ModelState.AddModelError(ex.PropName, ex.Message);
                return View(authorVM);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(authorVM);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            try
            {
                Author author = await authorService.GetByIdAsync(id);
                AuthorUpdateVM authorVM = new AuthorUpdateVM
                {
                    Name = author.Name
                };
                return View(authorVM);
            }
            catch (IdIsNotValidException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (AuthorNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AuthorUpdateVM genreVM)
        {
            try
            {
                await authorService.UpdateAsync(id, genreVM);
            }
            catch (IdIsNotValidException ex)
            {
                ModelState.AddModelError(ex.PropName, ex.Message);
                return View(genreVM);
            }
            catch (AuthorNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(genreVM);
            }
            catch (NotValidException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(genreVM);
            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await authorService.DeleteAsync(id);
            }
            catch (IdIsNotValidException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (AuthorNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
