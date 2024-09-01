using Microsoft.AspNetCore.Http;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.ViewModels.Book
{
    public class BookCreateVM
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public int DisPercent { get; set; }
        public int StockCount { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public IFormFile MainPhoto { get; set; }
        public IFormFile HoverPhoto { get; set; }
        public ICollection<IFormFile>? Photos { get; set; }


    }
}
