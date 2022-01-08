using LT_ServerApp.Models;
using LT_ServerApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LT_ServerApp.Controllers
{
   
    public class HomeController : Controller
    {
        ErrorLogService _service=new ErrorLogService();
        TestBenchService _test = new TestBenchService();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetWebJobLog()
        {
            List<WebJobLogModel> roleList = _service.GetWebJobLog();
            return JsonConvert.SerializeObject(roleList);
        }

        [HttpGet]
        public string GetTestBenchCount()
        {
            int count = _test.GetTestBenchCount();
            return JsonConvert.SerializeObject(count);
        }
    }
}
