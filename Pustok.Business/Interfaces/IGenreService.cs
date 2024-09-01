using Pustok.Business.ViewModels.Genre;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Interfaces
{
    public interface IGenreService
    {
        Task CreateAsync(GenreCreateVM vm);
        Task UpdateAsync(int id,GenreUpdateVM vm);
        Task<Genre> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<ICollection<Genre>> GetAllAsync();
    }
}
