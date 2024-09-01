using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.ViewModels.Book
{
    public class BookGetVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public  Pustok.Core.Models.Author Author { get; set; }
        public Pustok.Core.Models.Genre Genre { get; set; }
        public double SalePrice { get; set; }
        public int StockCount { get; set; }
        public string? ImageURl { get; set; }
    }
}
