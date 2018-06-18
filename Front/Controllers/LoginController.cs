using Newtonsoft.Json;
using Front.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Front.Controllers
{
    public class LoginController : Controller
    {



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPost(string username, string password)
        {
            string respToken = "";
            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost:1414/Login");

                var response = client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(new { Username = username, Password = password }), Encoding.UTF8, "application/json")).Result;

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = true;
                    return View("../Login/Login");
                }

                respToken = response.Content.ReadAsStringAsync().Result;
                var rest = JsonConvert.DeserializeObject<string>(respToken);
                respToken = rest;
            }

            User user = new User()
            {
                authenticated = true,
                name = username,
                token = respToken

            };
            Session["user"] = user;
            return View("../Home/Index");
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return View("../Login/Login");
        }

        public string GetUser()
        {
            User user = Session["user"] as User;
            return JsonConvert.SerializeObject(user);

        }

        public bool CheckLogin()
        {
            User user = Session?["user"] as User;
            return user != null && user.authenticated;
        }

    }
}