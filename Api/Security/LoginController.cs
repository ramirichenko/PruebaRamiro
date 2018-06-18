using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace Api.Security
{
    public class LoginController : ApiController
    {

        [HttpGet]
        [Route("GetUser")]
        public IHttpActionResult GetUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;            
            return Ok(JsonConvert.SerializeObject(identity));
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            bool isCredentialValid = (login.Password == "test");
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}