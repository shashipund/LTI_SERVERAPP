using LT_ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LT_ServerApp.Services
{
    public class TestBenchService
    {
        private Connection con;
        string _query;
        bool _status;
        DataTable dt;
        public List<TestBenchModel> GetTestBenchList()
        {
            IEnumerable<TestBenchModel> priorityList = null;
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    var t = (from t1 in entity.TestBenchDetails
                             select new TestBenchModel
                             {
                                 ID=t1.ID,
                                 TestBenchID=t1.TestBenchID,
                                 TestBenchName=t1.TestBenchName,
                                 DBName=t1.DBName,
                                 DBPassword=t1.DBPassword,
                                 DBUser=t1.DBUser,
                                 PortNo=t1.PortNo,
                                 IPAddress=t1.IPAddress
                                 
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
        public List<PriorityModel> GetPriorityList()
        {
            IEnumerable<PriorityModel> priorityList = null;
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    var t = (from t1 in entity.Priorities
                             select new PriorityModel
                             {
                                 PriorityName = t1.PriorityName,
                                 ID = t1.ID,
                                 Frequency = t1.Frequency
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
        public JSONResult AddPriority(Priority _priorityDetails)
        {
            int result = 0;
            JSONResult json = new JSONResult();
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    Priority   priority = new Priority();
                    priority.PriorityName = _priorityDetails.PriorityName;
                    priority.Frequency = _priorityDetails.Frequency;
                    entity.Priorities.Add(priority);
                    result = entity.SaveChanges();
                }
                json.StatusCode = 200;
                json.Message = "Success";
                return json;
            }
            catch (Exception e)
            {
                json.StatusCode = 201;
                json.Message = "Fail";
                return json;
            }

        }

        public JSONResult AddTestBench(TestBenchDetail _testBenchDetails)
        {
            int result = 0;
            JSONResult json = new JSONResult();
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    TestBenchDetail test = new TestBenchDetail();
                    test.TestBenchID = _testBenchDetails.TestBenchID;
                    test.TestBenchName = _testBenchDetails.TestBenchName;
                    test.DBName = _testBenchDetails.DBName;
                    test.DBPassword = _testBenchDetails.DBPassword;
                    test.DBUser = _testBenchDetails.DBUser;
                    test.PortNo=_testBenchDetails.PortNo;
                    test.IPAddress = _testBenchDetails.IPAddress;
                    entity.TestBenchDetails.Add(test);
                    result = entity.SaveChanges();
                }
                json.StatusCode = 200;
                json.Message = "Success";
                return json;
            }
            catch (Exception e)
            {
                json.StatusCode = 201;
                json.Message = "Fail";
                return json;
            }

        }

        public JSONResult AddTablePriority(List<TablePriority> _tablePriorityDetails)
        {
            int result = 0;
            JSONResult json = new JSONResult();
            using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
            {
                using (var transaction = entity.Database.BeginTransaction())
                {
                    try
                    {

                        foreach (var item in _tablePriorityDetails)
                        {


                            TablePriority test = new TablePriority();
                            test.TestBenchID = item.TestBenchID;
                            test.TableName = item.TableName;
                            test.PriorityID = item.PriorityID;

                            entity.TablePriorities.Add(test);
                            result = entity.SaveChanges();
                        }

                        transaction.Commit();
                        json.StatusCode = 200;
                        json.Message = "Success";
                        return json;

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        json.StatusCode = 201;
                        json.Message = "Fail";
                        return json;
                    }
                }
            }
        }
        public DataTable CheckConnection(TestBenchDetail _testBenchDetails)
        {
            try
            {
                string ip= _testBenchDetails.IPAddress;
                string port = _testBenchDetails.PortNo.ToString() ;
                string dbname=_testBenchDetails.DBName;
                string user=_testBenchDetails.DBUser;
                string pass = _testBenchDetails.DBPassword;
                string source = ip + ", " + port;
                con = new Connection(source,dbname,user,pass);
                _query = "SELECT  DISTINCT * FROM  information_schema.tables;";
                con.SqlQuery(_query);
                dt = con.QueryEx();
                con.closeConnection();

            }
            catch (Exception epx)
            {
                //LogLibrary.WriteErrorLog("Error in getData Function in From Table Class" + epx);
                Debug.WriteLine(epx);
            }
            return dt;
        }

    }
}