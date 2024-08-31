using Microsoft.AspNetCore.Mvc;
using Pustok.Core.IRepositories;
using Pustok.Core.Models;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.ViewModels.Genre;

namespace Pustok.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreRepository genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }
        public async Task<IActionResult> Index()
        {
            IQueryable<Genre> queryableGenres = genreRepository.GetAll(null, null).Result;
            List<Genre> genres = await queryableGenres.ToListAsync();
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
                ModelState.AddModelError("Name","Name is not valid!");
                return View();
            }
            Genre genre = new Genre
            {
                Name = genreVM.Name
            };
            await genreRepository.Create(genre);
            await genreRepository.CommitAsync();
            return  RedirectToAction(nameof(Index));
        }
    }
}
