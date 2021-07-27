using SES.Models;
using SES.Services.Soap.ServiceReference1;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Services.Abstract
{
    public interface IGetBankPinsService
    {
        public Task<bankPinServiceResponse> GetPinAsync(BankPinRequestModel model);
    }
}
