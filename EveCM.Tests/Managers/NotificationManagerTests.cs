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
    public class NotificationManagerTests
    {
        [TestMethod]
        public void GetNotifications_Should_Return_DefaultCount()
        {
            List<Notification> notifications = new List<Notification>();
            for (int i = 0; i < 3; i++)
            {
                notifications.Add(new Notification()
                {
                    AuthorId = "12345",
                    Content = $"Test content: {i}",
                    Title = $"Test title: {i}",
                    Id = i,
                    Date = DateTime.Now
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

            Mock<INotificationRepository> mockNotificationRepository = new Mock<INotificationRepository>();
            mockNotificationRepository.Setup(x => x.GetNotifications(It.IsAny<int>())).Returns(notifications);

            Mock<UserManager<ApplicationUser>> userManager = MockUserManager<ApplicationUser>();
            userManager.Setup(x => x.GetUserAsync(claimsUser)).ReturnsAsync(user);

            NotificationManager manager = new NotificationManager(mockNotificationRepository.Object, userManager.Object);

            var result = manager.GetNotifications();

            Assert.AreEqual(3, result.Count());
            mockNotificationRepository.Verify(x => x.GetNotifications(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetNotifications_Should_Return_SpecifiedCount()
        {
            List<Notification> notifications = new List<Notification>();
            for (int i = 0; i < 5; i++)
            {
                notifications.Add(new Notification()
                {
                    AuthorId = "12345",
                    Content = $"Test content: {i}",
                    Title = $"Test title: {i}",
                    Id = i,
                    Date = DateTime.Now
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

            Mock<INotificationRepository> mockNotificationRepository = new Mock<INotificationRepository>();
            mockNotificationRepository.Setup(x => x.GetNotifications(It.IsAny<int>())).Returns(notifications);

            NotificationManager manager = new NotificationManager(mockNotificationRepository.Object, userManager.Object);

            var result = manager.GetNotifications(expectedCount);

            Assert.AreEqual(expectedCount, result.Count());
            mockNotificationRepository.Verify(x => x.GetNotifications(expectedCount), Times.Once);
        }

        [TestMethod]
        public void SaveNotifications_Should_Save_Notification()
        {
            int expectedCount = 5;

            Notification notification = new Notification()
            {
                Id = 123,
                Date = DateTime.Now,
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

            Mock<INotificationRepository> mockNotificationRepository = new Mock<INotificationRepository>();

            NotificationManager manager = new NotificationManager(mockNotificationRepository.Object, userManager.Object);

            manager.SaveNewNotification(notification);

            mockNotificationRepository.Verify(x => x.SaveNotification(notification), Times.Once);
        }

        [TestMethod]
        public void SaveNotifications_Should_Include_Date()
        {
            Notification notification = new Notification()
            {
                Id = 123,
                Title = "TestTitle",
                Content = "TestContent",
                AuthorId = "12345"
            };

            ApplicationUser user = new ApplicationUser()
            {
                Id = "1235"
            };

            Mock<UserManager<ApplicationUser>> userManager = MockUserManager<ApplicationUser>();
            userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            Notification notificationSaved = new Notification();
            Mock<INotificationRepository> mockNotificationRepository = new Mock<INotificationRepository>();
            mockNotificationRepository.Setup(x => x.SaveNotification(notification)).Callback((Notification Notification) =>
            {
                notificationSaved = notification;
            });

            NotificationManager manager = new NotificationManager(mockNotificationRepository.Object, userManager.Object);

            manager.SaveNewNotification(notification);

            mockNotificationRepository.Verify(x => x.SaveNotification(notification), Times.Once);
            Assert.AreEqual(DateTime.Now.ToShortDateString(), notificationSaved.Date.ToShortDateString());
        }

        [TestMethod]
        public void SaveNotifications_Should_Include_AuthorId()
        {
            Notification notification = new Notification()
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

            Notification notificationSaved = new Notification();
            Mock<INotificationRepository> mockNotificationRepository = new Mock<INotificationRepository>();
            mockNotificationRepository.Setup(x => x.SaveNotification(notification)).Callback((Notification Notification) =>
            {
                notificationSaved = notification;
            });

            NotificationManager manager = new NotificationManager(mockNotificationRepository.Object, userManager.Object);

            manager.SaveNewNotification(notification, claimsUser);

            mockNotificationRepository.Verify(x => x.SaveNotification(notification), Times.Once);
            Assert.AreEqual(user.Id, notificationSaved.AuthorId);
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
