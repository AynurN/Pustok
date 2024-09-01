using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions
{
   
    public class NotValidException : Exception
    {
        public string PropName { get; set; }
        public NotValidException()
        {
        }

        public NotValidException(string? message) : base(message)
        {
        }
        public NotValidException(string prop,string? message) : base(message)
        {
            PropName = prop;

        }
    }
}
