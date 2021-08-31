using Domain.Entity;
using NUnit.Framework;
using System;

namespace tests
{
    [TestFixture]
    public class UrlTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Url_Should_Return_New_Instance_When_Parameters_Are_Valid()
        {
            var baseUrl = "baseUrl";
            var originalUrl = "originalUrl";

            var url = new Url(baseUrl, originalUrl);

            Assert.IsInstanceOf<Url>(url);
            Assert.AreNotEqual(null, url.ShortUrl);
            Assert.AreNotEqual(string.Empty, url.ShortUrl);
        }

        [TestCase(null, "originalUrl")]
        [TestCase("", "originalUrl")]
        public void Url_Should_Throw_NullReferenceException_When_BaseUrl_Is_Null_Or_Empty(string baseUrl, string originalUrl)
        {
            Assert.Catch<NullReferenceException>(() => new Url(baseUrl, originalUrl));
        }

        [TestCase("baseUrl", null)]
        [TestCase("baseUrl", "")]
        public void Url_Should_Throw_NullReferenceException_When_OriginalUrl_Is_Null_Or_Empty(string baseUrl, string originalUrl)
        {
            Assert.Catch<NullReferenceException>(() => new Url(baseUrl, originalUrl));
        }

        [Test]
        public void ShortUrl_Should_Be_String_With_Minimum_Of_Five_Characters()
        {
            var url = new Url("baseUrl", "originalUrl");

            Assert.That(url.ShortUrl.Length >= 5);
        }

        [Test]
        public void Url_OriginalUrl_Should_Be_Equal_When_Instance_Is_Created_With_Valid_OriginalUrl()
        {
            var originalUrl = "originalUrl";

            var url = new Url("baseUrl", originalUrl);

            Assert.AreEqual(originalUrl, url.OriginalUrl);
        }

        [Test]
        public void Url_FullUrl_Should_Be_Equal_When_Instance_Is_Created_With_Valid_Urls()
        {
            var baseUrl = "baseUrl";

            var url = new Url(baseUrl, "originalUrl");

            Assert.AreEqual($"{baseUrl}{url.ShortUrl}", url.FullUrl);
        }

        [Test]
        public void Url_Id_Should_Not_Be_Guid_Empty_When_Instance_Is_Created()
        {
            var url = new Url("baseUrl", "originalUrl");

            Assert.AreNotEqual(Guid.Empty, url.Id);
        }


        [Test]
        public void Url_CreatedAt_Should_Not_Be_Default_When_Instance_Is_Created()
        {
            var url = new Url("baseUrl", "originalUrl");

            Assert.AreNotEqual(default(DateTime), url.CreatedAt);
        }

        [Test]
        public void Url_Count_Should_Be_Zero_When_Instance_Is_Created()
        {
            var url = new Url("baseUrl", "originalUrl");

            Assert.AreEqual(0, url.Count);
        }
    }
}