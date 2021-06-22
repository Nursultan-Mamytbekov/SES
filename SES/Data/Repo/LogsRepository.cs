using SES.Data.Entities;
using SES.Data.Repo.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Data.Repo
{
    public class LogsRepository : ILogsRepository
    {
        private readonly ApplicationDbContext context;

        public LogsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public LogEntity AddLog(LogEntity log)
        {
            context.Logs.Add(log);
            context.SaveChanges();

            return log;
        }

        public ICollection<LogEntity> GetAll()
        {
            return context.Logs.ToList();
        }

        public LogEntity GetLog(int id)
        {
            return context.Logs.FirstOrDefault(p => p.Id == id);
        }
    }
}
