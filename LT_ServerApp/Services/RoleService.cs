using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LT_ServerApp.Models;

namespace LT_ServerApp.Services
{
    public class RoleService
    {

        public List<RoleModel> GetRoleList()
        {
            IEnumerable<RoleModel> roleList = null;
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    var t = (from t1 in entity.Roles
                     select new RoleModel
                     {
                         RoleID = t1.RoleID,
                         RoleName = t1.RoleName
                     }).ToList();
                  roleList = t.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return roleList.ToList();
        }
        public JSONResult AddRole(Role _roleDetails)
        {
            int result = 0;
            JSONResult json = new JSONResult();
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    Role role = new Role();
                    role.RoleName = _roleDetails.RoleName;

                    entity.Roles.Add(role);
                    result =  entity.SaveChanges();
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
    }
}