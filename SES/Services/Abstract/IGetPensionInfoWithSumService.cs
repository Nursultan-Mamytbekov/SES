
using SES.Services.Soap.ServiceReference;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Services.Abstract
{
    public interface IGetPensionInfoWithSumService
    {
        public Task<PensionInfoWithSumResponse> GetPensionInfoWithSumAsync(string pin);
    }
}
