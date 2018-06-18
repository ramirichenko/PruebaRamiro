using Microsoft.VisualStudio.TestTools.UnitTesting;
using Front.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Front.Tests.Controllers
{
    [TestClass]
    public class InventoryItemControllerTest
    {
        [TestMethod]
        public void Index()
        {
            InventoryItemController controller = new InventoryItemController();

            ViewResult result = controller.ListItems() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
