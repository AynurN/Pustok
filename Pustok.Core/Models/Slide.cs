using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Core.Models
{
    public class Slide :BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageURL { get; set; }
    }
}
