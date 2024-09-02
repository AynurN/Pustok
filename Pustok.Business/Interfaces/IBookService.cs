using Pustok.Business.ViewModels.Author;
using Pustok.Business.ViewModels.Book;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<ICollection<Book>> GetAllByAsync(Expression<Func<Book,bool>>? expression, params string[] includes);
        Task<ICollection<Book>> GetAllOrderDescAsync(Expression<Func<Book, bool>>? expression,Expression<Func<Book,dynamic>> orderExpression, params string[] includes);
        Task<bool> IsExist(Expression<Func<Book, bool>> expression);
    }
}
