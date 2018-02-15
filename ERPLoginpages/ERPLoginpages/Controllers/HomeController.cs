using ERPLoginpages.Models;
using System;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace ERPLoginpages.Controllers
{
    public class HomeController : Controller
    {
        Thermal_PMSEntities db = new Thermal_PMSEntities();
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(User_Login user)
        {
            if (user.username != null && user.Password != null)
            {
                var login = db.User_Login.Where(a => a.username == user.username).Single();
                int userid = Convert.ToInt32(login.userid);
                Session["userid"] = userid.ToString();
                Session["Name"] = login.username;
                var UsersInRole = db.UsersInRoles.Where(b => b.UserId == userid.ToString().Single());
                string rolename = UsersInRole.ToString();
                Session["rolename"] = rolename;
                if (UsersInRole != null)
                {

                    if (rolename == "Admin")
                    {
                        Session["rolename"] = "Admin";

                        return RedirectToAction("Admin", "Admin");
                    }

                }
                else if (rolename == "user")
                {
                    Session["rolename"] = "User";
                    return RedirectToAction("Admin", "Admin");
                }

            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect");
            }


            return View(user);
        }

       

       
    }
}