using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Core.Models
{
    public class Author :BaseEntity
    {
        public string Name { get; set; }
        //relational
        public ICollection<Book> Books { get; set; }
    }
}
