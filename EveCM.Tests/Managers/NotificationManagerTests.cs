using EveCM.Data.Repositories.Contracts;
using EveCM.Managers.Bulletin;
using EveCM.Models.Bulletin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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

            Mock<INotificationRepository> mockNotificationRepository = new Mock<INotificationRepository>();
            mockNotificationRepository.Setup(x => x.GetNotifications(It.IsAny<int>())).Returns(notifications);

            NotificationManager manager = new NotificationManager(mockNotificationRepository.Object);

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

            int expectedCount = 5;

            Mock<INotificationRepository> mockNotificationRepository = new Mock<INotificationRepository>();
            mockNotificationRepository.Setup(x => x.GetNotifications(It.IsAny<int>())).Returns(notifications);

            NotificationManager manager = new NotificationManager(mockNotificationRepository.Object);

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

            Mock<INotificationRepository> mockNotificationRepository = new Mock<INotificationRepository>();

            NotificationManager manager = new NotificationManager(mockNotificationRepository.Object);

            manager.SaveNewNotification(notification);
            
            mockNotificationRepository.Verify(x => x.SaveNotification(notification), Times.Once);
        }
    }
}
