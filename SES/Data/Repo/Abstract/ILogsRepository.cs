using SES.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Data.Repo.Abstract
{
    public interface ILogsRepository
    {
        public LogEntity AddLog(LogEntity log);
        public LogEntity GetLog(int id);
        public ICollection<LogEntity> GetAll();
    }
}
