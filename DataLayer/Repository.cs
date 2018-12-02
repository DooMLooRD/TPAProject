using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataLayer
{
    public class Repository
    {
        public void Log(LogEntity entity)
        {
            using (LogDataContext context = new LogDataContext())
            {
                context.LogEntities.Add(entity);
                context.SaveChanges();
            }
        }

    }
}
