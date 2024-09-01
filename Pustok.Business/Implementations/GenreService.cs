using Pustok.Business.Exceptions;
using Pustok.Business.Interfaces;
using Pustok.Business.ViewModels.Genre;
using Pustok.Core.IRepositories;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }
        public async Task CreateAsync(GenreCreateVM vm)
        {
            if (string.IsNullOrEmpty(vm.Name))
            {
                throw new NotValidException("Name", "Category name can not be empty!");
               
            }
            if(genreRepository.entities.Any(g=>g.Name.ToLower().Trim()== vm.Name.ToLower().Trim()))
            {
                throw new NotValidException("Name", "Category already exists!");
            }
            Genre genre = new Genre
            {
                Name = vm.Name,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            await genreRepository.Create(genre);
            await genreRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1)
            {
                throw new IdIsNotValidException("Id is not valid!");
            }
            Genre? genre = genreRepository.entities.FirstOrDefault(g => g.Id == id);
            if (genre == null)
            {
                throw new GenreNotFoundException("Genre", "Genre does not exist!");
            }
            genreRepository.Delete(genre);
            await genreRepository.CommitAsync();

        }

        public async Task<ICollection<Genre>> GetAllAsync()
        {
            var result = await genreRepository.GetAll(null, null);
            return result.ToList();
            
        }
        public Task<Genre> GetByIdAsync(int id)
        {
            if (id < 1)
            {
                throw new IdIsNotValidException("Id is not valid!");
            }
            Genre? genre=genreRepository.entities.FirstOrDefault(g=>g.Id==id);
            if(genre == null)
            {
                throw new GenreNotFoundException("Genre", "Genre does not exist!");
            }
                 return genreRepository.GetById(id);
        }

        public async Task UpdateAsync(int id, GenreUpdateVM vm)
        {

            if (id < 1)
            {
                throw new IdIsNotValidException("Id is not valid!");
            }
            Genre? genre = genreRepository.entities.FirstOrDefault(g => g.Id == id);
            if (genre == null)
            {
                throw new GenreNotFoundException("Genre", "Genre does not exist!");
            }

            if (string.IsNullOrEmpty(vm.Name))
            {
                throw new NotValidException("Name", "Category name can not be empty!");

            }
            if (genreRepository.entities.Any(g => g.Name.ToLower().Trim() == vm.Name.ToLower().Trim()))
            {
                throw new NotValidException("Name", "Category already exists!");
            }
            genre.UpdateDate = DateTime.Now;
            genre.Name = vm.Name;
            await genreRepository.CommitAsync();
        }
    }
}
