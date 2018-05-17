using evecm.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace EveCM.Tests.Authentication
{
    [TestClass]
    public class IdentityExtensionsTests
    {
        [TestMethod]
        public void PortraitUrl_Should_Return_Url_DefaultSize()
        {
            string characterId = "12345";
            string expectedUrl = $"https://image.eveonline.com/Character/{characterId}_256.jpg";

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, characterId),
                    new Claim(ClaimTypes.Name, "fakeusername")
                })
            );

            string result = user.Identity.PortraitUrl();

            Assert.AreEqual(expectedUrl, result);
        }

        [TestMethod]
        public void PortraitUrl_Should_Return_Url_SpecificSize()
        {
            string characterId = "12345";
            EveImageHelper.CharacterAvatarSize size = EveImageHelper.CharacterAvatarSize.Thirty_Two;
            string expectedUrl = $"https://image.eveonline.com/Character/{characterId}_{(int)size}.jpg";

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, characterId),
                    new Claim(ClaimTypes.Name, "fakeusername")
                })
            );

            string result = user.Identity.PortraitUrl(size);

            Assert.AreEqual(expectedUrl, result);
        }
    }
}
