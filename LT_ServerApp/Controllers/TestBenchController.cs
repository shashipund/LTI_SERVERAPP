using LT_ServerApp.Models;
using LT_ServerApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LT_ServerApp.Controllers
{
    public class TestBenchController : Controller
    {
        // GET: TestBench
        TestBenchService _service = new TestBenchService();
        public ActionResult TestBench()
        {
            return View();
        }

        public string GetPriorityList()
        {
            List<PriorityModel> roleList = _service.GetPriorityList();
            return JsonConvert.SerializeObject(roleList);
        }


        [HttpPost]
        public string InsertPriority(Priority priority)
        {
            JSONResult result = _service.AddPriority(priority);
            return JsonConvert.SerializeObject(result);
        }
        // GET: TestBench/Create
        public string GetTestBenchList()
        {
            List<TestBenchModel> roleList = _service.GetTestBenchList();
            return JsonConvert.SerializeObject(roleList);
        }

        [HttpPost]
        public string InsertTestBench(TestBenchDetail testBench)
        {
            JSONResult result = _service.AddTestBench(testBench);
            return JsonConvert.SerializeObject(result);
        }

        // GET: TestBench/Edit/5
        public string GetTables(TestBenchDetail testBenchTable)
        {
            DataTable dt = new DataTable();
            dt = _service.CheckConnection(testBenchTable);

            return JsonConvert.SerializeObject(dt);
        }

        [HttpPost]
        public string InsertTablePriority(List<TablePriority> tablePriority)
        {
            
            JSONResult result = _service.AddTablePriority(tablePriority);
            return JsonConvert.SerializeObject(result);
        }
        // POST: TestBench/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TestBench/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestBench/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
