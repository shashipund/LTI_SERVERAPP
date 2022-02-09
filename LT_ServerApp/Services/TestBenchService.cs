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
                                 ID = t1.ID,
                                 TestBenchID = t1.TestBenchID,
                                 TestBenchName = t1.TestBenchName,
                                 DBName = t1.DBName,
                                 DBPassword = t1.DBPassword,
                                 DBUser = t1.DBUser,
                                 PortNo = t1.PortNo,
                                 IPAddress = t1.IPAddress

                             }).ToList();
                    priorityList = t.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("TestBenchService/GetTestBenchList", ex.Message, System.DateTime.Now);
                service.LogError();
            }
            return priorityList.ToList();
        }

        public TestBenchDetail GetTestBenchInfo(int ID)
        {
            TestBenchDetail priorityList = null;
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    priorityList = entity.TestBenchDetails.Where(x => x.ID == ID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("TestBenchService/GetTestBenchInfo", ex.Message, System.DateTime.Now);
                service.LogError();
            }
            return priorityList;
        }

        public int GetTestBenchCount()
        {
            int count = 0;
            try
            {

                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    count = entity.TestBenchDetails.Count();
                }
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("TestBenchService/GetTestBenchCount", ex.Message, System.DateTime.Now);
                service.LogError();
            }
            return count;
        }


        public TestBenchDetail GetTestBenchInfo(int ID, LT_SERVER_DBEntities1 entity)
        {
            TestBenchDetail priorityList = null;
            try
            {
                priorityList = entity.TestBenchDetails.Where(x => x.ID == ID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("TestBenchService/GetTestBenchInfo", ex.Message, System.DateTime.Now);
                service.LogError();
            }
            return priorityList;
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
                LoggingService service = new LoggingService("TestBenchService/GetPriorityList", ex.Message, System.DateTime.Now);
                service.LogError();
            }
            return priorityList.ToList();
        }

        public List<TableModel> GetTableList(int testBenchID)
        {
            IEnumerable<TableModel> priorityList = null;
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    var t = (from t1 in entity.TablePriorities
                             select new TableModel
                             {
                                 ID = t1.ID,
                                 TableName= t1.TableName
                             }).ToList();
                    priorityList = t.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("TestBenchService/GetPriorityList", ex.Message, System.DateTime.Now);
                service.LogError();
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
                    Priority priority = new Priority();
                    priority.PriorityName = _priorityDetails.PriorityName;
                    priority.Frequency = _priorityDetails.Frequency;
                    entity.Priorities.Add(priority);
                    result = entity.SaveChanges();
                }
                json.StatusCode = 200;
                json.Message = "Success";
                return json;
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("TestBenchService/AddPriority", ex.Message, System.DateTime.Now);
                service.LogError();
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
                    entity.TestBenchDetails.Add(_testBenchDetails);
                    result = entity.SaveChanges();
                    
                }
                json.StatusCode = 200;
                json.Message = "Success";
                return json;
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("TestBenchService/AddTestBench", ex.Message, System.DateTime.Now);
                service.LogError();
                json.StatusCode = 201;
                json.Message = "Fail";
                return json;
            }

        }

        public JSONResult CreateDB(string _dbName)
        {
            int result = 0;
            JSONResult json = new JSONResult();
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    entity.Database.CurrentTransaction.UnderlyingTransaction.Dispose();
                    result =entity.CreateDatabase(_dbName);

                }
                json.StatusCode = 200;
                json.Message = "Success";
                return json;
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("TestBenchService/AddTestBench", ex.Message, System.DateTime.Now);
                service.LogError();
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

                        foreach (TablePriority item in _tablePriorityDetails)
                        {

                            //_testBenchID = item.TestBenchID;
                            TablePriority test = new TablePriority();
                            test.TestBenchID = item.TestBenchID;
                            test.TableName = item.TableName;
                            test.PriorityID = item.PriorityID;

                            entity.TablePriorities.Add(test);
                            result = entity.SaveChanges();
                            //CreateTableQuery(item, _testBenchID);
                            //SaveTableDetails(item.TestBenchID, item.TableName, entity);
                        }

                        transaction.Commit();
                        json.StatusCode = 200;
                        json.Message = "Success";

                        return json;

                    }
                    catch (Exception ex)
                    {
                        LoggingService service = new LoggingService("TestBenchService/AddTablePriority", ex.Message, System.DateTime.Now);
                        service.LogError();
                        transaction.Rollback();
                        json.StatusCode = 201;
                        json.Message = "Fail";
                        return json;
                    }
                }
            }
        }
        public DataTable GetTestBenchTable(int id)
        {
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    TestBenchDetail _testBenchDetails = GetTestBenchInfo(id, entity);
                    string ip = _testBenchDetails.IPAddress;
                    string port = _testBenchDetails.PortNo.ToString();
                    string dbname = _testBenchDetails.DBName;
                    string user = _testBenchDetails.DBUser;
                    string pass = _testBenchDetails.DBPassword;
                    string source = ip + ", " + port;
                    con = new Connection(source, dbname, user, pass);
                    _query = "SELECT  DISTINCT * FROM  information_schema.tables;";
                    con.SqlQuery(_query);
                    dt = con.QueryEx();
                    con.closeConnection();
                }
                //SaveTableDetails(_testBenchDetails.TestBenchID, dt);
            }
            catch (Exception epx)
            {
                //LogLibrary.WriteErrorLog("Error in getData Function in From Table Class" + epx);
                Debug.WriteLine(epx);
            }
            return dt;
        }

        public string GenerateConnectionString(int testBenchID)
        {
            string databaseSource = "";
            try
            {
                TestBenchDetail _testBenchDetails = GetTestBenchInfo(testBenchID);
                string ip = _testBenchDetails.IPAddress;
                string port = _testBenchDetails.PortNo.ToString();
                string dbname = _testBenchDetails.DBName;
                string user = _testBenchDetails.DBUser;
                string pass = _testBenchDetails.DBPassword;
                string source = ".";
                databaseSource = @"Data Source=" +source + ";Initial Catalog=" + dbname + ";Integrated Security=True";


            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("WebJob - Local_TestBenchService/GenerateConnectionString", ex.Message, System.DateTime.Now);
                service.LogError();
            }
            return databaseSource;
        }

        public ServerDataModel GenerateRemoteConnectionString(int testBenchID)
        {
            string databaseSource = "";
            ServerDataModel model = new ServerDataModel();
            try
            {
                TestBenchDetail _testBenchDetails = GetTestBenchInfo(testBenchID);
                string ip = _testBenchDetails.IPAddress;
                string port = _testBenchDetails.PortNo.ToString();
                string dbname = _testBenchDetails.DBName;
                string user = _testBenchDetails.DBUser;
                string pass = _testBenchDetails.DBPassword;
                string source = ip + ", " + port;
                databaseSource = @"Data Source=" + source + ";Initial Catalog=master;User ID=" + user + ";Password=" + pass + "";

                model.DBName = _testBenchDetails.DBName;
                model.DBSource = databaseSource;

            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("WebJob - Local_TestBenchService/GenerateConnectionString", ex.Message, System.DateTime.Now);
                service.LogError();
                return null;
            }
            return model;
        }
        public DataTable GetTableColumnData(string tableName, string _testBenchID, LT_SERVER_DBEntities1 entity)
        {
            TestBenchDetail _testBenchDetails = GetTestBenchInfo(Convert.ToInt32(_testBenchID), entity);
            string ip = _testBenchDetails.IPAddress;
            string port = _testBenchDetails.PortNo.ToString();
            string dbname = _testBenchDetails.DBName;
            string user = _testBenchDetails.DBUser;
            string pass = _testBenchDetails.DBPassword;
            string source = ip + ", " + port;
            con = new Connection(source, dbname, user, pass);

            //string table = "CREATE TABLE "+_testBenchID+"_" + _tablePriorityDetails.TableName + "(";
            //string type = "CREATE TYPE " + _testBenchID + "_" + _tablePriorityDetails.TableName + "Type AS TABLE (";

            _query = "SELECT  COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH " +
                    " FROM INFORMATION_SCHEMA.COLUMNS " +
                    "WHERE TABLE_NAME ='" + tableName + "'"; //"SELECT  DISTINCT * FROM  Test_Table1;";
            con.SqlQuery(_query);
            dt = con.QueryEx();
            con.closeConnection();
            return dt;
            //foreach (DataRow item in dt.Rows)
            //{
            //    table = table + item[0] + " " + item[1];
            //    type = type + item[0] + " " + item[1];
            //    if (item[2].ToString() != "")
            //    {
            //        table = table + "(" + item[2].ToString() + "),";
            //        type = type + "(" + item[2].ToString() + "),";
            //    }
            //    else
            //    {
            //        table = table + ",";
            //        type = type + ",";
            //    }
            //}
            //table = table + ")";
            //type = type + ")";
            //con.closeConnection();
            //CreateTable(table, _tablePriorityDetails.TableName);
            //CreateType(table, _tablePriorityDetails.TableName);
            //table = "";
            //type = "";

        }

        private void CreateTable(string query, string tableName, LT_SERVER_DBEntities1 entity)
        {
              entity.CreateTable(query, tableName);
        }
        private void CreateType(string query, string tableName, LT_SERVER_DBEntities1 entity)
        {
            var result = entity.CreateType(query, tableName);
        }

        private void SaveTableDetails(string testBenchID, string tableName, LT_SERVER_DBEntities1 entity)
        {
            try
            {
                string table = "CREATE TABLE " + tableName + "(";
                string type = "CREATE TYPE " + tableName + "Type AS TABLE (";
                DataTable dt = GetTableColumnData(tableName, testBenchID, entity);
                foreach (DataRow item in dt.Rows)
                {
                    
                    table = table + item[0] + " " + item[1];
                    type = type + item[0] + " " + item[1];
                    if (item[2].ToString() != "")
                    {
                        table = table + "(" + item[2].ToString() + "),";
                        type = type + "(" + item[2].ToString() + "),";
                    }
                    else
                    {
                        table = table + ",";
                        type = type + ",";
                    }
                    
                    
                    TableColumnInfo col = new TableColumnInfo();
                    col.TestBenchID = testBenchID;
                    col.ColumnName = item["COLUMN_NAME"].ToString();
                    col.DataType = item["DATA_TYPE"].ToString();
                    col.TableName = tableName;
                    entity.TableColumnInfoes.Add(col);
                    entity.SaveChanges();
                }
                table = table + ")";
                type = type + ")";
                entity.CreateTable(table, tableName);
                entity.CreateType(type, tableName);

                table = "";
                type = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}