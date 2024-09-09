using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions
{
   public class BookNotFoundException : Exception
    {
        public string PropName { get; set; }
        public BookNotFoundException()
        {
        }

        public BookNotFoundException(string? message) : base(message)
        {
        }
        public BookNotFoundException(string? message, string prop) : base(message)
        {
            PropName = prop;
        }
    }
}
