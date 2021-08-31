using Domain.Dto;
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
    public class UrlRepositoryTests
    {
        private IUrlRepository _urlRepository;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new ApplicationContext(builder.Options);
            context.Urls.AddRange(UrlSeed.Seeds);
            context.UrlMetrics.AddRange(UrlMetricSeed.Seeds);
            context.SaveChanges();

            _urlRepository = new UrlRepository(context);
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_Object_When_Exists()
        {
            var urlId = UrlIds.UrlId1;

            var url = await _urlRepository.GetByIdAsync(urlId);

            Assert.NotNull(url);
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_Null_When_Id_Not_Exists()
        {
            var urlId = Guid.NewGuid();

            var url = await _urlRepository.GetByIdAsync(urlId);

            Assert.IsNull(url);
        }

        [Test]
        public async Task GetAllAsync_Should_Return_List_When_Data_Exists()
        {
            var urls = await _urlRepository.GetAllAsync();

            Assert.IsNotEmpty(urls);
        }

        [Test]
        public async Task GetAllAsync_Should_Return_Empty_When_Data_Does_Not_Exists()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationContext(builder.Options);
            var repository = new UrlRepository(context);

            var urls = await repository.GetAllAsync();

            Assert.IsEmpty(urls);
        }

        [Test]
        public async Task AddAsync_Should_Add_Entity_To_Database_When_Executed()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationContext(builder.Options);
            var repository = new UrlRepository(context);

            var urlToAdd = new Url("baseUrl", "originalUrl");
            var addedUrl = await repository.AddAsync(urlToAdd);
            var count = await context.Urls.CountAsync();

            Assert.AreEqual(urlToAdd, addedUrl);
            Assert.NotNull(addedUrl);
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task UpdateAsync_Should_Update_Entity_When_Executed()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationContext(builder.Options);
            var repository = new UrlRepository(context);
            var newOriginalUrl = "original1234";
            var urlToAdd = new Url("baseUrl", "originalUrl");
            var addedUrl = await repository.AddAsync(urlToAdd);

            addedUrl.OriginalUrl = newOriginalUrl;
            var updatedEntity = await repository.UpdateAsync(addedUrl);

            Assert.AreEqual(addedUrl, updatedEntity);
            Assert.NotNull(updatedEntity);
        }

        [Test]
        public async Task GetByShortUrl_Should_Return_Url_When_Exists()
        {
            var shortUrl = "ABCDE";

            var url = await _urlRepository.GetByShortUrl(shortUrl);

            Assert.NotNull(url);
        }

        [Test]
        public async Task GetByShortUrl_Should_Return_Null_When_Url_Was_Not_Found()
        {
            var shortUrl = "12345";

            var url = await _urlRepository.GetByShortUrl(shortUrl);

            Assert.IsNull(url);
        }

        [Test]
        public async Task GetUrlMetrics_Should_Return_Instance_Of_UrlMetricsDto()
        {
            var shortUrl = "12345";

            var url = await _urlRepository.GetUrlMetrics(shortUrl);

            Assert.IsInstanceOf<UrlMetricDto>(url);
        }
    }
}
