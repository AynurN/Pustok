using Microsoft.EntityFrameworkCore;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Data.DAL
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions options):base(options) { }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        


    }
}
