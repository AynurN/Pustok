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
        public string Author { get; set; }
        public string Genre { get; set; }
        public decimal SalePrice { get; set; }
        public int StockCount { get; set; }
        public string ImageURl { get; set; }
    }
}
