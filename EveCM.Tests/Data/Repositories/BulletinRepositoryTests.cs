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
    public class BulletinRepositoryTests
    {
        [TestMethod]
        public void GetBulletins_Should_Return_DefaultOfTwo()
        {
            var options = new DbContextOptionsBuilder<EveCMContext>()
               .UseInMemoryDatabase(databaseName: "GetBulletins_Default")
               .Options;

            using (var context = new EveCMContext(options))
            {
                int count = 10;
                for (int i = 1; i < count - 1; i++)
                {
                    context.Bulletins.Add(new Bulletin()
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
                IBulletinRepository repository = new BulletinRepository(context);
                var result = repository.GetBulletins(out int total);

                Assert.AreEqual(3, result.Count());
            }
        }

        [TestMethod]
        public void GetBulletins_Should_Return_SpecifiedCount()
        {
            int expectedCount = 5;

            var options = new DbContextOptionsBuilder<EveCMContext>()
                .UseInMemoryDatabase(databaseName: "GetBulletins_Specific_Count")
                .Options;

            using (var context = new EveCMContext(options))
            {
                int count = 10;
                for (int i = 1; i < count - 1; i++)
                {
                    context.Bulletins.Add(new Bulletin()
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
                IBulletinRepository repository = new BulletinRepository(context);
                var result = repository.GetBulletins(out int total, expectedCount);

                Assert.AreEqual(expectedCount, result.Count());
            }
        }

        [TestMethod]
        public void GetBulletins_Should_OrderByRecent()
        {
            var options = new DbContextOptionsBuilder<EveCMContext>()
               .UseInMemoryDatabase(databaseName: "GetBulletins_Order")
               .Options;

            using (var context = new EveCMContext(options))
            {
                int count = 10;
                for (int i = count; i >= 0; i--)
                {
                    context.Bulletins.Add(new Bulletin()
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
                IBulletinRepository repository = new BulletinRepository(context);
                var result = repository.GetBulletins(out int total);


                Assert.AreEqual(3, result.Count());
                Assert.AreEqual(DateTime.Now.ToShortDateString(), result.First().Date.ToShortDateString());
                Assert.AreEqual(DateTime.Now.AddDays(-2).ToShortDateString(), result.Last().Date.ToShortDateString());
            }
        }

        [TestMethod]
        public void SaveBulletin_Should_SaveToDatabase()
        {
            var options = new DbContextOptionsBuilder<EveCMContext>()
               .UseInMemoryDatabase(databaseName: "GetBulletin_Order")
               .Options;


            Bulletin bulletin = new Bulletin()
            {
                AuthorId = "1",
                Content = "TestContent",
                Date = DateTime.Now,
                Id = 1234,
                Title = "TestTitle"
            };

            using (var context = new EveCMContext(options))
            {
                IBulletinRepository bulletinRepository = new BulletinRepository(context);
                bulletinRepository.SaveBulletin(bulletin);
            }
            using (var context = new EveCMContext(options))
            {
                Bulletin savedBulletin = context.Bulletins.Where(x => x.Id == bulletin.Id).First();

                Assert.AreEqual(bulletin.Id, savedBulletin.Id);
            }
        }
    }
}
