using LT_ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LT_ServerApp.Services
{
    public class UserService
    {
        public List<UserModel> GetRoleList()
        {
            IEnumerable<UserModel> userList = null;
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    var t = (from t1 in entity.Users
                             join t2 in entity.Roles on t1.RoleID equals t2.RoleID
                             select new UserModel
                             {
                                 UserID= t1.UserID,
                                 RoleID = t2.RoleID,
                                 RoleName = t2.RoleName,
                                 Name = t1.Name,
                                 Mobile=t1.Mobile,
                                 Email =t1.Email,
                                 Password= t1.Password
                             }).ToList();
                    userList = t.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return userList.ToList();
        }

        public JSONResult AddUser(User _userDetails)
        {
            int result = 0;
            JSONResult json = new JSONResult();
            try
            {
                using (LT_SERVER_DBEntities1 entity = new LT_SERVER_DBEntities1())
                {
                    User user = new User();
                    user.Name = _userDetails.Name;
                    user.Email = _userDetails.Email;
                    user.Mobile = _userDetails.Mobile;
                    user.Password = _userDetails.Password;
                    user.RoleID = _userDetails.RoleID;
                    entity.Users.Add(user);
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
    }
}