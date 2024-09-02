using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.ViewModels.Home
{
    public class HomeVM
    {
       public List<Core.Models.Book> FeaturedBooks { get; set; }
      public  List<Core.Models.Book> NewBooks { get; set; }
     public   List<Core.Models.Book> ExpensiveBooks { get; set; }
    }
}
