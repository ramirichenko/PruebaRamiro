using Front.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Front.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (!CheckLogin()){return View("../Login/Login");}
            return View();
        }


        public bool CheckLogin()
        {
            User user = Session?["user"] as User;
            return user != null && user.authenticated;
        }
    }
}