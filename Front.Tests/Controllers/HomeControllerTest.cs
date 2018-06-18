using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Front;
using Front.Controllers;

namespace Front.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void IndexNotNull()
        {
        
            HomeController controller = new HomeController();

           
            ViewResult result = controller.Index() as ViewResult;

         
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void CheckLogin()
        {
            
            HomeController controller = new HomeController();

         
            bool result = controller.CheckLogin();

          
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CheckLoginNotLogged()
        {

            HomeController controller = new HomeController();


            bool result = controller.CheckLogin();


            Assert.IsFalse(result);
        }
    }
}
