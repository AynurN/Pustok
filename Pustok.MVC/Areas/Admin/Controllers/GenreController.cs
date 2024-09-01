using Microsoft.AspNetCore.Mvc;
using Pustok.Core.IRepositories;
using Pustok.Core.Models;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.ViewModels.Genre;
using Pustok.Business.Interfaces;
using Pustok.Business.Exceptions;
using Humanizer.Localisation;

namespace Pustok.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }
        public async Task<IActionResult> Index()
        {
            ICollection<Genre> genres = await genreService.GetAllAsync();
            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreCreateVM genreVM)
        {
            if (!ModelState.IsValid)
            {
                return View(genreVM);
            }
            try
            {
                await genreService.CreateAsync(genreVM);
            }
            catch (NotValidException ex)
            {
                ModelState.AddModelError(ex.PropName, ex.Message);
                return View(genreVM);
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
                return View(genreVM);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id) {
          
            try
            {
                Genre genre = await genreService.GetByIdAsync(id);
                GenreUpdateVM genreVM = new GenreUpdateVM
                {
                    Name = genre.Name
                };
                return View(genreVM);
            }
            catch (IdIsNotValidException ex) {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (GenreNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,GenreUpdateVM genreVM)
        {
            try
            {
                await genreService.UpdateAsync(id, genreVM);
            }
            catch (IdIsNotValidException ex)
            {
                ModelState.AddModelError(ex.PropName, ex.Message);
                return View(genreVM);
            }
            catch (GenreNotFoundException ex)
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
                await genreService.DeleteAsync(id);
            }
            catch (IdIsNotValidException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (GenreNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
