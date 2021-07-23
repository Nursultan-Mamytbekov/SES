using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SES.Services.Abstract
{
    public interface IUserRolesManager
    {
        public Task<bool> IsAdminAsync(ClaimsPrincipal username);
    }
}
