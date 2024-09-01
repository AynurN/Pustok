using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions
{
    public class GenreNotFoundException : Exception
    {
        public string PropName { get; set; }
        public GenreNotFoundException()
        {
        }

        public GenreNotFoundException(string? message) : base(message)
        {
        }
        public GenreNotFoundException(string propName,string? message) : base(message)
        {
            PropName = propName;
        }
    }
}
