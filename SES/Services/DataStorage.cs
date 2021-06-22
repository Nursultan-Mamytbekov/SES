using SES.Services.Abstract;
using SES.Services.Soap.ServiceReference;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Services
{
    public class DataStorage : IDataStorage
    {        
        private List<PersonalAccountInfoWithSumResponse> PersonalAccounts = new List<PersonalAccountInfoWithSumResponse>();
        private object locker = new object();

        public void Add(PersonalAccountInfoWithSumResponse model)
        {
            lock (locker)
            {
                if (PersonalAccounts.Count() >= 1000) PersonalAccounts.Clear();
                PersonalAccounts.Add(model); 
            }
        }

        public PersonalAccountInfoWithSumResponse Get(string pin)
        {
            lock (locker)
            {
                return PersonalAccounts.FirstOrDefault(p => p.WorkPeriodInfo.PIN == pin);
            }
        }
    }
}
