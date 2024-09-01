using Pustok.Business.ViewModels.Author;
using Pustok.Business.ViewModels.Book;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Interfaces
{
   public  interface IBookService
    {
        Task CreateAsync(BookCreateVM vm);
        Task UpdateAsync(int id, BookUpdateVM vm);
        Task<Book> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<ICollection<Book>> GetAllAsync();
    }
}
