using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SES.Services.Soap.ServiceReference;

namespace SES.Services.Abstract
{
    public interface IDataStoragePension 
    {
        public void Add(PensionInfoWithSumResponse request);
        public PensionInfoWithSumResponse Get(string pin);
    }
}
