using EveCM.Data;
using EveCM.Data.Repositories.Contracts;
using EveCM.Data.Repositories.PSQL;
using EveCM.Models.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace EveCM.Tests.Data.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        [TestMethod]
        public void GetNotifications_Should_Return_DefaultOfTwo()
        {
            var options = new DbContextOptionsBuilder<EveCMContext>()
               .UseInMemoryDatabase(databaseName: "GetNotification_Default")
               .Options;

            using (var context = new EveCMContext(options))
            {
                int count = 10;
                for (int i = 1; i < count - 1; i++)
                {
                    context.Notifications.Add(new Notification()
                    {
                        Id = i,
                        AuthorId = i.ToString(),
                        Content = $"This is some content: {i}",
                        Title = $"This is some title: {i}",
                        Date = DateTime.Now.AddDays(i - 1)
                    });
                    context.SaveChanges();
                }
            }

            using (var context = new EveCMContext(options))
            {
                INotificationRepository repository = new NotificationRepository(context);
                var result = repository.GetNotifications();

                Assert.AreEqual(3, result.Count());
            }
        }

        [TestMethod]
        public void GetNotifications_Should_Return_SpecifiedCount()
        {
            int expectedCount = 5;

            var options = new DbContextOptionsBuilder<EveCMContext>()
                .UseInMemoryDatabase(databaseName: "GetNotification_Specific_Count")
                .Options;

            using (var context = new EveCMContext(options))
            {
                int count = 10;
                for (int i = 1; i < count - 1; i++)
                {
                    context.Notifications.Add(new Notification()
                    {
                        Id = i,
                        AuthorId = i.ToString(),
                        Content = $"This is some content: {i}",
                        Title = $"This is some title: {i}",
                        Date = DateTime.Now.AddDays(i - 1)
                    });
                    context.SaveChanges();
                }
            }

            using (var context = new EveCMContext(options))
            {
                INotificationRepository repository = new NotificationRepository(context);
                var result = repository.GetNotifications(expectedCount);

                Assert.AreEqual(expectedCount, result.Count());
            }
        }

        [TestMethod]
        public void GetNotifications_Should_OrderByRecent()
        {
            var options = new DbContextOptionsBuilder<EveCMContext>()
               .UseInMemoryDatabase(databaseName: "GetNotification_Order")
               .Options;

            using (var context = new EveCMContext(options))
            {
                int count = 10;
                for (int i = count; i >= 0; i--)
                {
                    context.Notifications.Add(new Notification()
                    {
                        //allow for id = 0
                        Id = i + 1,
                        AuthorId = i.ToString(),
                        Content = $"This is some content: {i}",
                        Title = $"This is some title: {i}",
                        //add dates in past
                        Date = DateTime.Now.AddDays(i * -1)
                    });
                    context.SaveChanges();
                }
            }

            using (var context = new EveCMContext(options))
            {
                INotificationRepository repository = new NotificationRepository(context);
                var result = repository.GetNotifications();


                Assert.AreEqual(3, result.Count());
                Assert.AreEqual(DateTime.Now.ToShortDateString(), result.First().Date.ToShortDateString());
                Assert.AreEqual(DateTime.Now.AddDays(-2).ToShortDateString(), result.Last().Date.ToShortDateString());
            }
        }

        [TestMethod]
        public void SaveNotification_Should_SaveToDatabase()
        {
            var options = new DbContextOptionsBuilder<EveCMContext>()
               .UseInMemoryDatabase(databaseName: "GetNotification_Order")
               .Options;


            Notification notification = new Notification()
            {
                AuthorId = "1",
                Content = "TestContent",
                Date = DateTime.Now,
                Id = 1234,
                Title = "TestTitle"
            };

            using (var context = new EveCMContext(options))
            {
                INotificationRepository notificationRepository = new NotificationRepository(context);
                notificationRepository.SaveNotification(notification);
            }
            using (var context = new EveCMContext(options))
            {
                Notification savedNotification = context.Notifications.Where(x => x.Id == notification.Id).First();

                Assert.AreEqual(notification.Id, savedNotification.Id);
            }
        }
    }
}
