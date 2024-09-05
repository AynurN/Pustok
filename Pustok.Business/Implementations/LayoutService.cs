using Microsoft.AspNetCore.Identity;
using Pustok.Business.Interfaces;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Implementations
{
    public class LayoutService : ILayoutService
    {
        private readonly UserManager<AppUser> userManager;

        public LayoutService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<AppUser> GetUser(string username)
        {
            AppUser user = null;
            user=await userManager.FindByNameAsync(username);
            return user;
        }
    }
}
