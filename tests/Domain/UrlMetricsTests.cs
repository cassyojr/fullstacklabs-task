using Domain.Entity;
using NUnit.Framework;
using System;

namespace tests
{
    [TestFixture]
    public class UrlMetricsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UrlMetric_Should_Throw_NullReferenceException_When_UrlId_Is_Guid_Empty()
        {
            Assert.Catch<NullReferenceException>(() => new UrlMetric(urlId: Guid.Empty, "browser", "platform"));
        }

        [TestCase(null, "platform")]
        [TestCase("", "platform")]
        public void UrlMetric_Should_Throw_NullReferenceException_When_Browser_Is_Null_Or_Empty(string browser, string platform)
        {
            Assert.Catch<NullReferenceException>(() => new UrlMetric(urlId: Guid.NewGuid(), browser, platform));
        }

        [TestCase("browser", null)]
        [TestCase("browser", "")]
        public void UrlMetric_Should_Throw_NullReferenceException_When_Platform_Is_Null_Or_Empty(string browser, string platform)
        {
            Assert.Catch<NullReferenceException>(() => new UrlMetric(urlId: Guid.NewGuid(), browser, platform));
        }

        [Test]
        public void UrlMetric_Id_Should_Not_Be_Guid_Empty_When_Instance_Is_Created()
        {
            var urlMetric = new UrlMetric(urlId: Guid.NewGuid(), "browser", "platform");
            Assert.AreNotEqual(Guid.Empty, urlMetric.Id);
        }

        [Test]
        public void UrlMetric_CreatedAt_Should_Not_Be_Default_When_Instance_Is_Created()
        {
            var urlMetric = new UrlMetric(urlId: Guid.NewGuid(), "browser", "platform");
            Assert.AreNotEqual(default(DateTime), urlMetric.CreatedAt);
        }

        [Test]
        public void UrlMetric_UrlId_Should_Be_Equal_When_Instance_Is_Created_With_Valid_Guid()
        {
            var urlId = Guid.NewGuid();

            var urlMetric = new UrlMetric(urlId, "browser", "platform");

            Assert.AreEqual(urlId, urlMetric.UrlId);
        }

        [Test]
        public void UrlMetric_Browser_Should_Be_Equal_When_Instance_Is_Created_With_Valid_Browser()
        {
            var browser = "browser";

            var urlMetric = new UrlMetric(urlId: Guid.NewGuid(), browser, "platform");

            Assert.AreEqual(browser, urlMetric.Browser);
        }

        [Test]
        public void UrlMetric_Platform_Should_Be_Equal_When_Instance_Is_Created_With_Valid_Platform()
        {
            var platform = "platform";

            var urlMetric = new UrlMetric(urlId: Guid.NewGuid(), "browser", platform);

            Assert.AreEqual(platform, urlMetric.Platform);
        }
    }
}