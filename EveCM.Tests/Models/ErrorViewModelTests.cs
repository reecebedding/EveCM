using EveCM.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EveCM.Tests.Models
{
    [TestClass]
    public class ErrorViewModelTests
    {
        [TestMethod]
        public void ErrorViewModel_ShowRequestId_ShouldReturn_Id_True()
        {
            ErrorViewModel model = new ErrorViewModel();
            model.RequestId = "fakeid";

            Assert.IsTrue(model.ShowRequestId);
        }

        [TestMethod]
        public void ErrorViewModel_ShowRequestId_ShouldReturn_Id_False()
        {
            ErrorViewModel model = new ErrorViewModel();

            Assert.IsFalse(model.ShowRequestId);
        }
    }
}
