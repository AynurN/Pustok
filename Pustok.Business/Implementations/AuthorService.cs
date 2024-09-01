using Pustok.Business.Exceptions;
using Pustok.Business.Interfaces;
using Pustok.Business.ViewModels.Author;
using Pustok.Core.IRepositories;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public async Task CreateAsync(AuthorCreateVM vm)
        {

            if (string.IsNullOrEmpty(vm.Name))
            {
                throw new NotValidException("Name", "Author name can not be empty!");

            }
            if (authorRepository.entities.Any(g => g.Name.ToLower().Trim() == vm.Name.ToLower().Trim()))
            {
                throw new NotValidException("Name", "Author already exists!");
            }
            Author author = new Author
            {
                Name = vm.Name,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
                
            };
            await authorRepository.Create(author);
            await authorRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1)
            {
                throw new IdIsNotValidException("Id is not valid!");
            }
            Author? author = authorRepository.entities.FirstOrDefault(g => g.Id == id);
            if (author == null)
            {
                throw new AuthorNotFoundException("Author", "Author does not exist!");
            }
            authorRepository.Delete(author);
            await authorRepository.CommitAsync();
        }

            public async Task<ICollection<Author>> GetAllAsync()
        {
            var result = await authorRepository.GetAll(null, null);
            return result.ToList();

        }

        public Task<Author> GetByIdAsync(int id)
        {
            if (id < 1)
            {
                throw new IdIsNotValidException("Id is not valid!");
            }
            Author? author = authorRepository.entities.FirstOrDefault(g => g.Id == id);
            if (author == null)
            {
                throw new AuthorNotFoundException("Author", "Author does not exist!");
            }
            return authorRepository.GetById(id);
        }

        public async Task UpdateAsync(int id, AuthorUpdateVM vm)
        {

            if (id < 1)
            {
                throw new IdIsNotValidException("Id is not valid!");
            }
            Author? author = authorRepository.entities.FirstOrDefault(g => g.Id == id);
            if (author == null)
            {
                throw new AuthorNotFoundException("Author", "Author does not exist!");
            }

            if (string.IsNullOrEmpty(vm.Name))
            {
                throw new NotValidException("Name", "Author name can not be empty!");

            }
            if (authorRepository.entities.Any(g => g.Name.ToLower().Trim() == vm.Name.ToLower().Trim()))
            {
                throw new NotValidException("Name", "Author already exists!");
            }
            author.UpdateDate = DateTime.Now;
            author.Name = vm.Name;
            await authorRepository.CommitAsync();
        }
    }
}
