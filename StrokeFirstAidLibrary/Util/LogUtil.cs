using StrokeFirstAidLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StrokeFirstAidLibrary.StrokeFirstAidDBContext;

namespace StrokeFirstAidLibrary.Util
{
    public class LogUtil
    {
        public void Add(LogCode logCode, string content, User? user = null)
        {
            Log log = new Log();
            log.CreateDate = DateTime.Now;
            log.Content = content;
            log.LogCode = logCode;

            if (user != null)
            {
                log.UserID = user.ID;
            }
            else
            {
                log.UserID = null;
            }

            freeSQL.GetRepository<Log>().Insert(log);
        }
    }
}
