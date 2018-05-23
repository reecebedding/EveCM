using EveCM.Managers;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EveCM.Tests.Managers
{
    [TestClass]
    public class OAuthManagerTests
    {
        [TestMethod]
        public void EVERedirectUrl_Should_Contain_CorrectSettings()
        {
            IOptions<EveSettings> eveSettings = Options.Create<EveSettings>(new EveSettings()
            {
                SSO = new EveSettings.SSOConnection()
                {
                    CallBackPath = "/fake/callback",
                    ClientId = "fakeclient",
                    ClientSecret = "fakeclientsecret",
                    LoginHost = "fakehost",
                    ResponseType = "code"
                }
            });

            string expectedUrl = $"fakehost/authorize?response_type=code&redirect_uri=/fake/callback&client_id=fakeclient";

            OAuthManager manager = new OAuthManager(eveSettings);

            string result = manager.EVERedirectUrl();

            Assert.AreEqual(expectedUrl, result);
        }
        

    }
}
