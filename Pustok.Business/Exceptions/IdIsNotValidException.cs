using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions
{
    public class IdIsNotValidException : Exception
    {
        public string PropName { get; set; }
        public IdIsNotValidException()
        {
        }

        public IdIsNotValidException(string? message) : base(message)
        {
        }
        public IdIsNotValidException(string prop,string? message) : base(message)
        {
            PropName = prop;
        }
    }
}
