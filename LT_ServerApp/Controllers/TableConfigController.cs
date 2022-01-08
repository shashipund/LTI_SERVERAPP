using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LT_ServerApp.Models;
using LT_ServerApp.Services;
using Newtonsoft.Json;

namespace LT_ServerApp.Controllers
{
    public class TableConfigController : Controller
    {

        TableConfigService _service = new TableConfigService();
        // GET: TableConfig
        public ActionResult TableConfig()
        {
            return View();
        }

        // GET: TableConfig/Details/5
        public string GetTableConfigList()
        {
            List<TableConfigModel> roleList = _service.GetTableConfig();
            return JsonConvert.SerializeObject(roleList);
        }
    }
}
