using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LT_ServerApp.Services;
using LT_ServerApp.Models;
using Newtonsoft.Json;

namespace LT_ServerApp.Controllers
{
    public class RestoreController : Controller
    {
        Connection con = null;
        BackUpService _backUpService = new BackUpService();
        public ActionResult Restore()
        {
            return View();
        }

        [HttpGet]
        public string GetBackUpLog()
        {
            List<BackUpModel> backupList = _backUpService.GetBackUpLog();
            return JsonConvert.SerializeObject(backupList);
        }

        [HttpGet]
        public string BackUpDatabase(string dbName)
        {
            try
            {
                string DBSource = Create_Local_Connection(dbName);
                string _BackupName = dbName + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".bak";
                con = new Connection(DBSource);
                LoggingService ser = new LoggingService("DB Connection =", "Connection Success to DB", System.DateTime.Now);
                ser.LogError();
                string sqlQuery = "BACKUP DATABASE [TBA 079] TO DISK = 'D:\\SQLServerBackups\\" + _BackupName + "' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = '" + _BackupName + "';";
                
                LoggingService ser1 = new LoggingService("SQL Query", sqlQuery, System.DateTime.Now);
                ser.LogError();
                con.SqlQuery(sqlQuery);
                con.NonQueryEx();

                BackUpDetail backup = new BackUpDetail();
                backup.DBName = dbName;
                backup.BackUpName = _BackupName;
                backup.BackUpDate = DateTime.Now;

                 _backUpService.InsertBackUpLog(backup);
                return "Success";
                
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("WebApp - RestoreController/BackUpDatabase", ex.Message, System.DateTime.Now);
                service.LogError();
                return "Fail";
            }
        }
        private string Create_Local_Connection(string DBName)
        {
            try
            {
                //string DBSource = "";  //_testBenchService.GeneratelocalDBConnectionString(DBName);
                string databaseSource="";
                string source = ".";
                databaseSource = @"Data Source=.;Initial Catalog=TBA 079;user id=sa;password=sa@12345";
                LoggingService ser=new LoggingService ("Create_Local_Connection",databaseSource, System.DateTime.Now);
                ser.LogError();
                 return databaseSource;
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("WebApp - RestoreController/Create_Local_Connection", ex.Message, System.DateTime.Now);
                service.LogError();
                return "";
            }
        }

        [HttpGet]
        public string RestoreDatabase(string testBenchID, string backUpFile)
        {
            try
            {
                TestBenchDataService _service = new TestBenchDataService();
                ServerDataModel dbSource = _service.Create_Remote_Datasource(Convert.ToInt32(testBenchID));
                con = new Connection(dbSource.DBSource);
                string sqlQuery = "RESTORE DATABASE ["+ dbSource.DBName +"] FROM DISK ='D:\\SQLServerBackups\\" + backUpFile + "'";
                con.SqlQuery(sqlQuery);
                con.NonQueryEx();
                return "Success";
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("WebApp - RestoreController/RestoreDatabase", ex.Message, System.DateTime.Now);
                service.LogError();
                return "Fail";
            }
        }
        
    }
}
