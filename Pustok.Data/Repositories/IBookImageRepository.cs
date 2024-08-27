using Pustok.Core.IRepositories;
using Pustok.Core.Models;
using Pustok.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Data.Repositories
{
    public class BookImageRepository : GenericRepository<BookImage>, IBookImageRepository
    {
        public BookImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
