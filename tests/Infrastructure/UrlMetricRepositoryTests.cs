using Domain.Entity;
using Domain.Repository;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using tests.Helpers;

namespace tests.Infrastructure
{
    [TestFixture]
    public class UrlMetricRepositoryTests
    {
        private IUrlMetricRepository _urlMetricRepository;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var _context = new ApplicationContext(builder.Options);
            _context.Urls.AddRange(UrlSeed.Seeds);
            _context.UrlMetrics.AddRange(UrlMetricSeed.Seeds);
            _context.SaveChanges();

            _urlMetricRepository = new UrlMetricRepository(_context);
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_Object_When_Exists()
        {
            var urlMetricId = UrlIds.UrlMetricId1;

            var urlMetric = await _urlMetricRepository.GetByIdAsync(urlMetricId);

            Assert.NotNull(urlMetric);
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_Null_When_Id_Not_Exists()
        {
            var urlMetricId = Guid.NewGuid();

            var urlMetric = await _urlMetricRepository.GetByIdAsync(urlMetricId);

            Assert.IsNull(urlMetric);
        }

        [Test]
        public async Task GetAllAsync_Should_Return_List_When_Data_Exists()
        {
            var urlMetrics = await _urlMetricRepository.GetAllAsync();

            Assert.IsNotEmpty(urlMetrics);
        }

        [Test]
        public async Task GetAllAsync_Should_Return_Empty_When_Data_Does_Not_Exists()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationContext(builder.Options);
            var repository = new UrlMetricRepository(context);

            var urlMetrics = await repository.GetAllAsync();

            Assert.IsEmpty(urlMetrics);
        }

        [Test]
        public async Task AddAsync_Should_Add_Entity_To_Database_When_Executed()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationContext(builder.Options);
            var repository = new UrlMetricRepository(context);

            var urlMetricToAdd = new UrlMetric(Guid.NewGuid(), "browser", "platform");
            var addedUrlMetric = await repository.AddAsync(urlMetricToAdd);
            var count = await context.UrlMetrics.CountAsync();

            Assert.AreEqual(urlMetricToAdd, addedUrlMetric);
            Assert.NotNull(addedUrlMetric);
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task UpdateAsync_Should_Update_Entity_When_Executed()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationContext(builder.Options);
            var repository = new UrlMetricRepository(context);
            var urlMetricId = Guid.NewGuid();
            var newPlatformName = "platformName";
            var urlMetricToAdd = new UrlMetric(urlMetricId, "browser", "platform");
            var addedUrlMetric = await repository.AddAsync(urlMetricToAdd);

            addedUrlMetric.Platform = newPlatformName;
            var updatedEntity = await repository.UpdateAsync(addedUrlMetric);

            Assert.AreEqual(addedUrlMetric, updatedEntity);
            Assert.NotNull(updatedEntity);
        }
    }
}
