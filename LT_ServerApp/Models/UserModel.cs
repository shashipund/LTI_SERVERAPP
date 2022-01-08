using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LT_ServerApp.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Mobile { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }
    }
}