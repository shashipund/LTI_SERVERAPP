using LT_ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LT_ServerApp.Services
{
    public class BackUpService
    {
        public void InsertBackUpLog(BackUpDetail backupLog)
        {
            using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
            {
                entity.BackUpDetails.Add(backupLog);
                entity.SaveChanges();
            }
        }

        public List<BackUpModel> GetBackUpLog()
        {
            IEnumerable<BackUpModel> priorityList = null;
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    priorityList = (from t1 in entity.BackUpDetails
                             select new BackUpModel
                             {
                                 DBName = t1.DBName,
                                 BackUpName = t1.BackUpName
                             }).ToList();
                    
                }
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("TestBenchService/GetPriorityList", ex.Message, System.DateTime.Now);
                service.LogError();
            }
            return priorityList.ToList();
        }
    }
}