using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace EveCM.Tests.Utils
{
    [TestClass]
    public class IdentityExtensionsTests
    {
        [TestMethod]
        public void PortaitUrl_Should_Return_CorrectUrl()
        {
            IIdentity identity = new GenericIdentity("fakeuser");
        }
    }
}
