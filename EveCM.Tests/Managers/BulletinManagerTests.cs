using EveCM.Data.Repositories.Contracts;
using EveCM.Managers.Bulletin;
using EveCM.Models;
using EveCM.Models.Bulletin;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EveCM.Tests.Managers
{
    [TestClass]
    public class BulletinManagerTests
    {
        [TestMethod]
        public void GetBulletins_Should_Return_All_ByDefault()
        {
            List<Bulletin> bulletins = new List<Bulletin>();
            int entityCount = 11;
            for (int i = 0; i < entityCount; i++)
            {
                bulletins.Add(new Bulletin()
                {
                    AuthorId = "12345",
                    Content = $"Test content: {i}",
                    Title = $"Test title: {i}",
                    Id = i,
                    CreatedDate = DateTime.Now
                });
            }

            var claimsUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                    new Claim(ClaimTypes.Name, "fakeusername")
                })
            );

            ApplicationUser user = new ApplicationUser()
            {
                Id = "1235"
            };

            Mock<IBulletinRepository> mockBulletinRepository = new Mock<IBulletinRepository>();
            int outVal = 0;
            mockBulletinRepository.Setup(x => x.GetBulletins(out outVal, null)).Returns(bulletins);

            Mock<UserManager<ApplicationUser>> userManager = MockUserManager<ApplicationUser>();
            userManager.Setup(x => x.GetUserAsync(claimsUser)).ReturnsAsync(user);

            BulletinManager manager = new BulletinManager(mockBulletinRepository.Object, userManager.Object);

            var result = manager.GetBulletins(out int total);

            Assert.AreEqual(entityCount, result.Count());
            mockBulletinRepository.Verify(x => x.GetBulletins(out outVal, It.IsAny<int?>()), Times.Once);
        }

        [TestMethod]
        public void GetBulletins_Should_Return_SpecifiedCount()
        {
            List<Bulletin> bulletins = new List<Bulletin>();
            for (int i = 0; i < 5; i++)
            {
                bulletins.Add(new Bulletin()
                {
                    AuthorId = "12345",
                    Content = $"Test content: {i}",
                    Title = $"Test title: {i}",
                    Id = i,
                    CreatedDate = DateTime.Now
                });
            }

            var claimsUser = new ClaimsPrincipal(
               new ClaimsIdentity(
                   new Claim[] {
                    new Claim(ClaimTypes.Name, "fakeusername")
               })
            );

            ApplicationUser user = new ApplicationUser()
            {
                Id = "1235"
            };

            Mock<UserManager<ApplicationUser>> userManager = MockUserManager<ApplicationUser>();
            userManager.Setup(x => x.GetUserAsync(claimsUser)).ReturnsAsync(user);

            int expectedCount = 5;
            int outVal;

            Mock<IBulletinRepository> mockRepository = new Mock<IBulletinRepository>();
            mockRepository.Setup(x => x.GetBulletins(out outVal, It.IsAny<int>())).Returns(bulletins);

            BulletinManager manager = new BulletinManager(mockRepository.Object, userManager.Object);

            var result = manager.GetBulletins(out outVal, expectedCount);

            Assert.AreEqual(expectedCount, result.Count());
            mockRepository.Verify(x => x.GetBulletins(out outVal, expectedCount), Times.Once);
        }

        [TestMethod]
        public void SaveBulletins_Should_Save_Notification()
        {
            int expectedCount = 5;

            Bulletin bulletin = new Bulletin()
            {
                Id = 123,
                CreatedDate = DateTime.Now,
                Title = "TestTitle",
                Content = "TestContent",
                AuthorId = "12345"
            };

            var claimsUser = new ClaimsPrincipal(
                 new ClaimsIdentity(
                     new Claim[] {
                         new Claim(ClaimTypes.Name, "fakeusername")
                 })
            );

            ApplicationUser user = new ApplicationUser()
            {
                Id = "1235"
            };

            Mock<UserManager<ApplicationUser>> userManager = MockUserManager<ApplicationUser>();
            userManager.Setup(x => x.GetUserAsync(claimsUser)).ReturnsAsync(user);

            Mock<IBulletinRepository> mockRepository = new Mock<IBulletinRepository>();
            mockRepository.Setup(x => x.SaveBulletin(It.IsAny<Bulletin>()))
                .Returns((Bulletin bulletinSaved) => { return bulletinSaved; });

            BulletinManager manager = new BulletinManager(mockRepository.Object, userManager.Object);

            manager.SaveNewBulletin(bulletin, claimsUser);

            mockRepository.Verify(x => x.SaveBulletin(bulletin), Times.Once);
        }

        [TestMethod]
        public void SaveBulletins_Should_Include_Date()
        {
            Bulletin bulletin = new Bulletin()
            {
                Id = 123,
                Title = "TestTitle",
                Content = "TestContent",
                AuthorId = "12345"
            };

            var claimsUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                         new Claim(ClaimTypes.Name, "fakeusername")
                })
           );

            ApplicationUser user = new ApplicationUser()
            {
                Id = "1235"
            };

            Mock<UserManager<ApplicationUser>> userManager = MockUserManager<ApplicationUser>();
            userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            Bulletin bulletinSaved = new Bulletin();
            Mock<IBulletinRepository> mockRepository = new Mock<IBulletinRepository>();
            mockRepository.Setup(x => x.SaveBulletin(bulletin))
                .Returns((Bulletin bulletinPassed) => { return bulletinPassed; })
                .Callback((Bulletin bulletinPassedIn) =>
                {
                    bulletinSaved = bulletin;
                });

            BulletinManager manager = new BulletinManager(mockRepository.Object, userManager.Object);

            Bulletin actualBulletin = manager.SaveNewBulletin(bulletin, claimsUser);

            mockRepository.Verify(x => x.SaveBulletin(bulletin), Times.Once);
            Assert.AreEqual(DateTime.Now.ToShortDateString(), actualBulletin.CreatedDate.ToShortDateString());
        }

        [TestMethod]
        public void SaveBulletins_Should_Include_AuthorId()
        {
            Bulletin bulletin = new Bulletin()
            {
                Id = 123,
                Title = "TestTitle",
                Content = "TestContent"
            };

            var claimsUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                         new Claim(ClaimTypes.Name, "fakeusername")
                })
            );

            ApplicationUser user = new ApplicationUser()
            {
                Id = "1235"
            };

            Mock<UserManager<ApplicationUser>> userManager = MockUserManager<ApplicationUser>();
            userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            Bulletin bulletinSaved = new Bulletin()
            {
                Id = bulletin.Id,
                Title = bulletin.Title,
                Content = bulletin.Content,
                AuthorId = user.Id
            };
            Mock<IBulletinRepository> mockRepository = new Mock<IBulletinRepository>();
            mockRepository.Setup(x => x.SaveBulletin(bulletin)).Returns(bulletinSaved);

            BulletinManager manager = new BulletinManager(mockRepository.Object, userManager.Object);

            manager.SaveNewBulletin(bulletin, claimsUser);

            mockRepository.Verify(x => x.SaveBulletin(bulletin), Times.Once);
            Assert.AreEqual(user.Id, bulletinSaved.AuthorId);
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
