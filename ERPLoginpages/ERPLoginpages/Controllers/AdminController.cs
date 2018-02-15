using ERPLoginpages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ERPLoginpages.Controllers
{


    public class AdminController : Controller
    {
        Thermal_PMSEntities db = new Thermal_PMSEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(User_Login user)
        {
            if (user.username != null && user.Password != null)
            {
                var login = (from l in db.User_Login where l.username == user.username select new { l.userid }).ToList();
                //db.User_Login.Where(a => a.username == user.username).Single();
                if (login.Count > 0)
                {
                    Session["userid"] = login[0].userid;
                    Session["UserName"] = user.username;
                    int uid = Convert.ToInt32(Session["userid"]);
                    // int userid = Convert.ToInt32(login.userid);
                    //Session["userid"] = userid.ToString();
                    //string name = login.username.ToString();
                    //Session["UserName"] = name;


                    var UsersInRole = (from k in db.UsersInRoles where k.UserId == uid select new { k.UserName }).ToList(); //db.UsersInRoles.Where(b => b.UserId == userid.ToString().FirstOrDefault());
                    string rolename = UsersInRole[0].UserName;
                    Session["rolename"] = rolename;
                    if (UsersInRole != null)
                    {

                        if (rolename == "admin")
                        {
                            Session["rolename"] = "admin";

                            return RedirectToAction("Admin", "Admin");
                        }

                    }
                    else if (rolename == "user")
                    {
                        Session["rolename"] = "User";
                        return RedirectToAction("Admin", "Admin");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect");
            }


            return View(user);
        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
