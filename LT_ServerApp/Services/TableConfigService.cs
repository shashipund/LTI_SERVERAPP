using LT_ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace LT_ServerApp.Services
{
    public class TableConfigService
    {
        public List<TableConfigModel> GetTableConfig()
        {
            IEnumerable<TableConfigModel> priorityList = null;
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    var t = (from t1 in entity.TablePriorityViews
                             select new TableConfigModel
                             {
                                 TestBenchID = t1.TestBenchID,
                                 TestBenchName = t1.TestBenchName,
                                 TableName=t1.TableName,
                                 PriorityName=t1.PriorityName,
                                 Frequency=t1.Frequency

                             }).ToList();
                    priorityList = t.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return priorityList.ToList();
        }
    }
}