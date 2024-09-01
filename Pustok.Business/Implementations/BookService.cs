using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Exceptions;
using Pustok.Business.Interfaces;
using Pustok.Business.Utilities.Enums;
using Pustok.Business.Utilities.Extensions;
using Pustok.Business.ViewModels.Book;
using Pustok.Core.IRepositories;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IGenreRepository genreRepository;
        private readonly IAuthorRepository authorRepository;

        public BookService(IBookRepository bookRepository, IGenreRepository genreRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.genreRepository = genreRepository;
            this.authorRepository = authorRepository;
        }
        public async Task CreateAsync(BookCreateVM vm)
        {

            if (string.IsNullOrEmpty(vm.Title))
            {
                throw new NotValidException("Title", "Title can not be empty!");
            }
            if (!vm.MainPhoto.ValidateType("image/"))
            {
                throw new NotValidException("MainPhoto", "The Type is not valid!");

            }
            if (!vm.MainPhoto.ValidateSize(FileSize.MB, 1))
            {
                throw new NotValidException("MainPhoto", "The size is not valid!");
            }

            if (!vm.HoverPhoto.ValidateType("image/"))
            {
                throw new NotValidException("HoverPhoto", "The Type is not valid!");
            }
            if (!vm.HoverPhoto.ValidateSize(FileSize.MB, 1))
            {
                throw new NotValidException("HoverPhoto", "The size is not valid!");
            }
            bool existGenre = await genreRepository.entities.AnyAsync(g => g.Id == vm.GenreId);
            if (!existGenre)
            {
                throw new GenreNotFoundException("Genre", "Genre not found!");
            }
            bool existAuthor = await authorRepository.entities.AnyAsync(g => g.Id == vm.AuthorId);
            if (!existGenre)
            {
                throw new AuthorNotFoundException("Author", "Author not found!");
            }

            BookImage mainImage = new BookImage
            {
                IsDeleted = false,
                CreateDate = DateTime.Now,
                IsPrimary = true,
                ImageUrl = await vm.MainPhoto.CreateFileAsync("C:\\Users\\user\\Desktop\\Pustok\\Pustok.MVC\\wwwroot\\", "assets", "image", "products")

            };
            BookImage hoverImage = new BookImage
            {
                IsDeleted = false,
                CreateDate = DateTime.Now,
                IsPrimary = false,
                ImageUrl = await vm.MainPhoto.CreateFileAsync("C:\\Users\\user\\Desktop\\Pustok\\Pustok.MVC\\wwwroot\\", "assets", "image", "products")

            };

            Book book = new Book
            {
                Title = vm.Title,
                Desc = vm.Desc,
                GenreId = vm.GenreId,
                AuthorId = vm.AuthorId,
                CostPrice = vm.CostPrice,
                SalePrice = vm.SalePrice,
                DisPercent = vm.DisPercent,
                StockCount = vm.StockCount,
                BookImages = new List<BookImage> { mainImage, hoverImage },
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            if (vm.Photos is not null)
            {
                foreach (IFormFile file in vm.Photos)
                {
                    if (!file.ValidateType("image/"))
                    {
                        continue;
                    }
                    if (!file.ValidateSize(FileSize.MB, 1))
                    {
                        continue;
                    }
                    BookImage addImage = new BookImage
                    {
                        IsDeleted = false,
                        CreateDate = DateTime.Now,
                        IsPrimary = null,
                        ImageUrl = await vm.MainPhoto.CreateFileAsync("C:\\Users\\user\\Desktop\\Pustok\\Pustok.MVC\\wwwroot\\", "assets", "image", "products")

                    };
                    book.BookImages.Add(addImage);
                }

            }
            await bookRepository.Create(book);
            await bookRepository.CommitAsync();
        }

        

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Book>> GetAllAsync()
        {
            var result = await bookRepository.GetAll(null, "Author","Genre","BookImages");
            return result.ToList();

        }

        public Task<Book> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, BookUpdateVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
