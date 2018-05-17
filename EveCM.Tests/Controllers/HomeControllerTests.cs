using EveCM.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveCM.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Should_Return_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
