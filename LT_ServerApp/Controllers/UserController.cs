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
    public class UserController : Controller
    {
        UserService userService = new UserService();
        // GET: User
        public ActionResult Users()
        {
            return View();
        }

        
        public string GetUserList()
        {
            List<UserModel> roleList = userService.GetRoleList();
            return JsonConvert.SerializeObject(roleList);
        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public string InsertUser(User user)
        {
           JSONResult result = userService.AddUser(user);
            return JsonConvert.SerializeObject(result);
        }
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
