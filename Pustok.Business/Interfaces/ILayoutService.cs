using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Interfaces
{
    public interface ILayoutService
    {
        Task<AppUser> GetUser(string username);
    }
}
