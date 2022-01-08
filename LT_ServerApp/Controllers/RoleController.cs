using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using LT_ServerApp.Services;
using LT_ServerApp.Models;

namespace LT_ServerApp.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
       

        // GET: Restore
        RoleService _roleService = new RoleService();
        public string GetRoles()
        {
            List<RoleModel> roleList = _roleService.GetRoleList();
            return JsonConvert.SerializeObject(roleList);
        }

        [HttpPost]
        public string InsertRole(Role role)
        {
            JSONResult result = _roleService.AddRole(role);
            return JsonConvert.SerializeObject(result);
        }
    }
}