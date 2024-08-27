using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Core.Models
{
    public class Book :BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public int DisPercent { get; set; }
        public int StockCount { get; set; }

        //relational
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<BookImage> BookImages { get; set; }
    }
}
