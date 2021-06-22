using SES.Services.Soap.ServiceReference;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Services.Abstract
{
    public interface IDataStorage
    {
        public void Add(PersonalAccountInfoWithSumResponse request);
        public PersonalAccountInfoWithSumResponse Get(string pin);
    }
}
