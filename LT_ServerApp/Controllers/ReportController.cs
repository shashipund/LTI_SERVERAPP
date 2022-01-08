using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LT_ServerApp.Services;
using Newtonsoft.Json;
using System.Collections;

namespace LT_ServerApp.Controllers
{
    public class ReportController : Controller
    {

        TestBenchDataService _service = new TestBenchDataService();
        public ActionResult Report()
        {
            return View();
        }

        [HttpGet]
        public string GetBBTHVTableData(int testBenchID, string tableName, string fromDate, string toDate)
        {
            DataTable dt = new DataTable();
            dt = _service.GetBBTHVTableData(testBenchID, tableName,fromDate,toDate);
            var d = (from r in dt.AsEnumerable()
                     select r["Barcode"]).Distinct().ToList();
            return JsonConvert.SerializeObject(dt);
        }

        [HttpGet]
        public string GetBBTIRTableData(int testBenchID, string tableName, string fromDate, string toDate)
        {
            DataTable dt = new DataTable();
            dt = _service.GetBBTIRTableData(testBenchID, tableName, fromDate, toDate);
            return JsonConvert.SerializeObject(dt);
        }

        [HttpGet]
        public string GetBBT_IR_Settings(int testBenchID, string tableName, string fromDate, string toDate)
        {
            DataTable dt = new DataTable();
            dt = _service.GetBBTIRSettings(testBenchID, tableName, fromDate, toDate);
            return JsonConvert.SerializeObject(dt);
        }
    }
}
