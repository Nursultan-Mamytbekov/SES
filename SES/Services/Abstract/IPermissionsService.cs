using SES.Models;
using SES.Services.Soap.ServiceReference;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Services.Abstract
{
    public interface IPermissionsService
    {
        public Task<RequestForPermissionResponse> SendRequestForPermission(RequestPermissionModel model);
        public Task<SendCodeForPermissionResponse> SendCodeForPermission(CodeForPermissionModel model);
    }
}
