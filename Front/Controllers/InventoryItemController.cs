using Newtonsoft.Json;
using Front.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Front.Controllers
{
    
    public class InventoryItemController : Controller
    {


        public ActionResult ListItems()
        {
            if (!CheckLogin()) { return View("../Login/Login"); }
            List<InventoryItem> items = new List<InventoryItem>();

            //Obtener items
            using (var client = new HttpClient())
            {
                string token = GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var uri = new Uri("http://localhost:1414/api/v1/inventory/list/");

                var response = client.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    items = JsonConvert.DeserializeObject<List<InventoryItem>>(response.Content.ReadAsStringAsync().Result);
                    string textResult = response.Content.ReadAsStringAsync().Result;
                    return View(items);
                }
                ViewBag.Error = "Error al obtener productos del inventario";
                return View("Error");
            }       
        }


        [HttpPost]
        public ActionResult RegisterRequest(string itemName, string itemType, DateTime expirationDate)
        {
            if (!CheckLogin()) { return View("../Home/Login"); }
            InventoryItem item = new InventoryItem(itemName, itemType, expirationDate);

            //Insertar en inventario
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
                var uri = new Uri("http://localhost:1414/api/v1/inventory/input/");

                var response = client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")).Result;

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Error al registrar producto en el inventario";
                    return View("RegisterForm");
                }
                string textResult = response.Content.ReadAsStringAsync().Result;
            }
            ViewBag.itemName = itemName;
            ViewBag.Success = true;
            return View("RegisterForm");
        }

        public ActionResult RegisterForm()
        {
            if (!CheckLogin()) { return View("../Login/Login"); }
            return View();
        }


        [HttpPost]
        public ActionResult OutputRequest(string itemName)
        {
            if (!CheckLogin()) { return View("../Login/Login"); }
            if (string.IsNullOrWhiteSpace(itemName))
            {
                return View();
            }

            //Retirar de inventario
            using (var client = new HttpClient())
            {
                string token = GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var uri = new Uri("http://localhost:1414/api/v1/inventory/output/" + itemName);

                var response = client.PutAsync(uri,null).Result;

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Error al retirar producto del inventario";
                    return View("OutputForm");
                }


                string textResult = response.Content.ReadAsStringAsync().Result;
            }
            ViewBag.itemName = itemName;
            ViewBag.Success = true;
            return View("OutputForm");
        }

        public ActionResult OutputForm()
        {
            if (!CheckLogin()) { return View("../Login/Login"); }
            return View();
        }

        private bool CheckLogin()
        {
            User user = Session?["user"] as User;
            return user != null && user.authenticated;
        }

        private string GetToken()
        {
            User user = Session?["user"] as User;
            return user.token;
        }

    }
}