using Application.Services;
using Domain.Dto;
using Domain.Entity;
using Domain.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tests.Services
{
    [TestFixture]
    public class UrlServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GenerateUrl_Should_Return_Created_Url_When_Executed()
        {
            var baseUrl = "baseUrl";
            var originalUrl = "originalUrl";
            var url = new Url(baseUrl, originalUrl);
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            urlRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Url>())).Returns(() => Task.FromResult(url));
            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            var generatedUrl = await service.GenerateUrl(baseUrl, originalUrl);

            Assert.AreEqual(url, generatedUrl);
        }

        [Test]
        public async Task GetUrlByShortUrl_Should_Return_Url_When_Existent()
        {
            var url = new Url("baseUrl", "originalUrl");
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            urlRepositoryMock.Setup(x => x.GetByShortUrl(It.IsAny<string>())).Returns(() => Task.FromResult(url));
            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            var generatedUrl = await service.GetUrlByShortUrl(url.ShortUrl);

            Assert.AreEqual(url, generatedUrl);
        }

        [Test]
        public async Task GetUrlByShortUrl_Should_Return_Null_When_Not_Found()
        {
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            urlRepositoryMock.Setup(x => x.GetByShortUrl(It.IsAny<string>())).Returns(Task.FromResult((Url)null));
            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            var generatedUrl = await service.GetUrlByShortUrl("ABCDE");

            Assert.IsNull(generatedUrl);
        }

        [Test]
        public async Task GetUrls_Should_Return_List_When_Data_Exists()
        {
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            urlRepositoryMock.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Url>>(new List<Url> { new Url() }));
            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            var urls = await service.GetUrls();

            Assert.IsNotEmpty(urls);
            Assert.AreEqual(1, urls.Count());
        }

        [Test]
        public async Task GetUrls_Should_Return_Empty_List_When_Data_Does_Not_Exists()
        {
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            urlRepositoryMock.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Url>>(new List<Url>()));
            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            var urls = await service.GetUrls();

            Assert.IsEmpty(urls);
            Assert.AreEqual(0, urls.Count());
        }

        [Test]
        public async Task GenerateMetric_Should_Add_New_UrlMetric_When_Executed()
        {
            var url = new Url { Id = Guid.NewGuid() };
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            urlRepositoryMock.Setup(x => x.GetByShortUrl(It.IsAny<string>())).Returns(Task.FromResult(url));
            urlRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Url>())).Returns(Task.FromResult(url));
            urlMetricRepositoryMock.Setup(x => x.AddAsync(It.IsAny<UrlMetric>())).Returns(Task.FromResult(new UrlMetric()));
            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            await service.GenerateMetric("ABCDE", "browser", "platform");

            Assert.AreEqual(1, url.Count);
        }

        [Test]
        public void GenerateMetric_Should_Thow_NullReferenceException_When_Url_Was_Not_Found()
        {
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            urlRepositoryMock.Setup(x => x.GetByShortUrl(It.IsAny<string>())).Returns(Task.FromResult((Url)null));
            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            Assert.ThrowsAsync<NullReferenceException>(async () => await service.GenerateMetric("ABCDE", "browser", "platform"));
        }

        [Test]
        public async Task GetUrlMetricsByShortUrl_Should_Return_Object_When_Url_Exists()
        {
            var url = new Url { Id = Guid.NewGuid() };
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            urlRepositoryMock.Setup(x => x.GetByShortUrl(It.IsAny<string>())).Returns(Task.FromResult(url));
            urlRepositoryMock.Setup(x => x.GetUrlMetrics(It.IsAny<string>())).Returns(Task.FromResult(new UrlMetricDto { Url = url }));

            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            var urlMetrics = await service.GetUrlMetricsByShortUrl("ABCDE");

            Assert.IsNotNull(urlMetrics);
            Assert.AreEqual(url, urlMetrics.Url);
        }

        [Test]
        public void GetUrlMetricsByShortUrl_Should_Thow_NullReferenceException_When_Url_Was_Not_Found()
        {
            var url = new Url { Id = Guid.NewGuid() };
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            urlRepositoryMock.Setup(x => x.GetByShortUrl(It.IsAny<string>())).Returns(Task.FromResult((Url)null));

            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            Assert.ThrowsAsync<NullReferenceException>(async () => await service.GetUrlMetricsByShortUrl("ABCDE"));
        }

        [Test]
        public void IsValidUrl_Should_Return_True_When_Url_Contains_Http_Scheme()
        {
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            var result = service.IsValidUrl("http://www.test.com");

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidUrl_Should_Return_True_When_Url_Contains_Https_Scheme()
        {
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            var result = service.IsValidUrl("https://www.test.com");

            Assert.IsTrue(result);
        }

        [TestCase("htps://www.test")]
        [TestCase("//www.test.com")]
        [TestCase("://www.test.com")]
        public void IsValidUrl_Should_Return_False_When_Url_Is_Invalid(string url)
        {
            var urlRepositoryMock = new Mock<IUrlRepository>();
            var urlMetricRepositoryMock = new Mock<IUrlMetricRepository>();
            var service = new UrlService(urlRepositoryMock.Object, urlMetricRepositoryMock.Object);

            var result = service.IsValidUrl(url);

            Assert.IsFalse(result);
        }
    }
}
