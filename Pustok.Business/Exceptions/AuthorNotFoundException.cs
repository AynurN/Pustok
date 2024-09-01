using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions
{
    public class AuthorNotFoundException : Exception
    {
        public string PropName { get; set; }

        public AuthorNotFoundException()
        {
        }

        public AuthorNotFoundException(string? message) : base(message)
        {
        }
        public AuthorNotFoundException(string propName,string? message) : base(message)
        {
            PropName = propName;
        }
    }
}
