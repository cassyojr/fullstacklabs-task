using Domain.Dto;
using Domain.Entity;
using Domain.Services;
using HeyUrlChallengeCodeDotnet.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shyjus.BrowserDetection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tests
{
    public class UrlsControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Index_Should_Return_Not_Null_TypeOf_IActionResult_When_Executed_Successfully()
        {
            var logger = new Mock<ILogger<UrlsController>>();
            var browserDetector = new Mock<IBrowserDetector>();
            var service = new Mock<IUrlService>();
            service.Setup(x => x.GetUrls()).Returns(Task.FromResult<IEnumerable<Url>>(new List<Url>()));
            var controller = new UrlsController(logger.Object, browserDetector.Object, service.Object);

            var result = await controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [TestCase("")]
        [TestCase(null)]
        public async Task Show_Should_Return_400_When_Executed_With_Empty_Url(string url)
        {
            var logger = new Mock<ILogger<UrlsController>>();
            var browserDetector = new Mock<IBrowserDetector>(MockBehavior.Loose);
            var service = new Mock<IUrlService>();
            var controller = new UrlsController(logger.Object, browserDetector.Object, service.Object);

            var result = await controller.Show(url) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Test]
        public async Task Show_Should_Return_400_When_Executed_With_Not_Existent_Url()
        {
            var logger = new Mock<ILogger<UrlsController>>();
            var browserDetector = new Mock<IBrowserDetector>(MockBehavior.Loose);
            var service = new Mock<IUrlService>();
            service.Setup(x => x.GetUrlByShortUrl(It.IsAny<string>())).Returns(Task.FromResult((Url)null));
            var controller = new UrlsController(logger.Object, browserDetector.Object, service.Object);

            var result = await controller.Show("ABCDE") as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Test]
        public async Task Show_Should_Return_Not_Null_TypeOf_IActionResult_When_Executed_With_Existent_Url()
        {
            var url = new Url();
            var logger = new Mock<ILogger<UrlsController>>();
            var browserDetector = new Mock<IBrowserDetector>(MockBehavior.Loose);
            var service = new Mock<IUrlService>();
            service.Setup(x => x.GetUrlByShortUrl(It.IsAny<string>())).Returns(Task.FromResult(url));
            service.Setup(x => x.GetUrlMetricsByShortUrl(It.IsAny<string>())).Returns(Task.FromResult(new UrlMetricDto { Url = url }));
            var controller = new UrlsController(logger.Object, browserDetector.Object, service.Object);

            var result = await controller.Show("ABCDE") as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task Create_Should_Return_Null_When_Executed_With_Invalid_Url()
        {
            var url = new Url { ShortUrl = "ABCDE" };
            var teste = new Mock<IHttpContextAccessor>();
            var logger = new Mock<ILogger<UrlsController>>();
            var browserDetector = new Mock<IBrowserDetector>(MockBehavior.Loose);
            var service = new Mock<IUrlService>();
            service.Setup(x => x.GenerateUrl(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(url));
            service.Setup(x => x.GetUrlMetricsByShortUrl(It.IsAny<string>())).Returns(Task.FromResult(new UrlMetricDto { Url = url }));
            var controller = new UrlsController(logger.Object, browserDetector.Object, service.Object);

            var result = await controller.Create("ABCDE") as RedirectToActionResult;

            Assert.IsNull(result);
        }
    }
}