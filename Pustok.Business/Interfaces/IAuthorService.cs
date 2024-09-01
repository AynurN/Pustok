using Pustok.Business.ViewModels.Author;
using Pustok.Business.ViewModels.Genre;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Interfaces
{
    public interface IAuthorService
    {
        Task CreateAsync(AuthorCreateVM vm);
        Task UpdateAsync(int id, AuthorUpdateVM vm);
        Task<Author> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<ICollection<Author>> GetAllAsync();
    }
}
