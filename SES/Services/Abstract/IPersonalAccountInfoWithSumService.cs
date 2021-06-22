using SES.Services.Soap.ServiceReference;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Services.Abstract
{
    public interface IPersonalAccountInfoWithSumService
    {
        public Task<PersonalAccountInfoWithSumResponse> GetPersonalAccountInfoAsync(string pin);
    }
}
