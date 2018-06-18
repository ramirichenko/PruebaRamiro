using Api.Models;
using Api.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    [Authorize]
    public class InventoryItemController : ApiController
    {




        /// <summary>
        /// Servicio PUT que recibe un nombre de un item del inventario para sacarlo, lo busca y si esta lo saca del inventario y notifica que ha salido.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("api/v1/inventory/output/{name}")]
        [HttpPut]
        public IHttpActionResult Output([FromUri]string name)
        {
            List<InventoryItem> items = new InventoryItem().GetItems(name).Where(i => !i.outputDate.HasValue).ToList();
            if (items == null || items.Count() <=0)
            {
                return StatusCode((HttpStatusCode)404);
            }
            string itemName = items.FirstOrDefault().name;
            try
            {
                new InventoryItem().OutputItem(items.FirstOrDefault().id);
            }
            catch (Exception e)
            {
                Logger.Error("Fallo al retirar de inventario -- Exception:  " + JsonConvert.SerializeObject(e), "api/v1/output");
                return StatusCode((HttpStatusCode)500);
            }

            //Notify
            try
            {          
                new Notifier().OutputNotify(itemName);
            }
            catch{}            

            return Ok();
        }


        /// <summary>
        /// Servicio de tipo POST que recibe un objeto InventoryItem para insertarlo en el inventario.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// Status code 200 : OK
        /// Status code 200 : Peticion incorrecta
        /// Status code 500 : Error del servidor
        /// </returns>
        [Route("api/v1/inventory/input")]
        [HttpPost]
        public IHttpActionResult Input([FromBody]InventoryItem item)
        {
            int statusCode = item.InputValidate();
            if (statusCode != 200)
            {
                Logger.Error("Fallo de validacion al insertar", "api/v1/input");
                return StatusCode((HttpStatusCode)statusCode);
            }

            try
            {
                item.InsertItem();
            }
            catch (Exception e)
            {
                Logger.Error("Fallo de validacion al insertar -- Exception:  " + JsonConvert.SerializeObject(e), "api/v1/input");
                return StatusCode((HttpStatusCode)500);
            }

            return Ok();
        }


        /// <summary>
        /// Servicio que devuelve todos los elementos del inventario
        /// </summary>
        /// <returns></returns>
        [Route("api/v1/inventory/list")]
        [HttpGet]
        public IHttpActionResult List()
        {
            try
            {
                List<InventoryItem> items = new InventoryItem().GetItemList();
                if (items == null) return StatusCode((HttpStatusCode)404);
                return Ok(items);
            }
            catch (Exception e)
            {
                Logger.Error("Fallo de validacion al insertar -- Exception:  " + JsonConvert.SerializeObject(e), "api/v1/input");
                return StatusCode((HttpStatusCode)500);
            }
        }

    }
}