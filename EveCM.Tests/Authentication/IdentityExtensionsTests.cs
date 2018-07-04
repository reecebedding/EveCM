using EveCM.Models;
using EveCM.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
                    new Claim("PrimaryCharacterId", characterId),
                    new Claim(ClaimTypes.Name, "fakeusername")
                })
            );

            ApplicationUser expectedApplicationUser = new ApplicationUser()
            {
                PrimaryCharacterId = characterId,
                Id = "1"
            };

            Mock<UserManager<ApplicationUser>> mockUserManager = MockUserManager<ApplicationUser>();
            mockUserManager.Setup(x => x.GetUserAsync(user)).ReturnsAsync(expectedApplicationUser);

            string result = IdentityExtensions.AvatarUrl(user, mockUserManager.Object);

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
                    new Claim("PrimaryCharacterId", characterId),
                    new Claim(ClaimTypes.Name, "fakeusername")
                })
            );

            ApplicationUser expectedApplicationUser = new ApplicationUser()
            {
                PrimaryCharacterId = characterId,
                Id = "1"
            };

            Mock<UserManager<ApplicationUser>> mockUserManager = MockUserManager<ApplicationUser>();
            mockUserManager.Setup(x => x.GetUserAsync(user)).ReturnsAsync(expectedApplicationUser);

            string result = IdentityExtensions.AvatarUrl(user, mockUserManager.Object, size);

            Assert.AreEqual(expectedUrl, result);
        }

        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }
    }
}
