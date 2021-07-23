using Microsoft.AspNetCore.Identity;

using SES.Data.Entities;
using SES.Services.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SES.Services
{
    public class UserRolesManager : IUserRolesManager
    {
        private readonly UserManager<User> _userManager;

        public UserRolesManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsAdminAsync(ClaimsPrincipal username)
        {
            var user = await _userManager.GetUserAsync(username);
            return await _userManager.IsInRoleAsync(user, "admin");
        }

    }
}
