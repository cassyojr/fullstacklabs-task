using System;
using System.Threading.Tasks;
using Domain.Services;
using hey_url_challenge_code_dotnet.ViewModels;
using HeyUrlChallengeCodeDotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private readonly IBrowserDetector browserDetector;
        private readonly IUrlService _urlService;

        public UrlsController(
            ILogger<UrlsController> logger,
            IBrowserDetector browserDetector,
            IUrlService urlService)
        {
            this.browserDetector = browserDetector;
            _logger = logger;
            _urlService = urlService;
        }

        [HttpGet("/urls")]
        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new();
            model.Urls = await _urlService.GetUrls();

            return View(model);
        }

        [HttpGet("/{url}")]
        public async Task<IActionResult> Visit(string url)
        {
            try
            {
                await _urlService.GenerateMetric(url, browser: this.browserDetector.Browser.Name, platform: this.browserDetector.Browser.OS);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = e.Message, ReturnUrl = "/urls" });
            }
        }

        [HttpGet("urls/{url}")]
        public async Task<IActionResult> Show(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url)) return BadRequest("Invalid url");

                var existentUrl = await _urlService.GetUrlByShortUrl(shortUrl: url);
                if (existentUrl is null) return BadRequest("Invalid url to get metrics");

                var metrics = await _urlService.GetUrlMetricsByShortUrl(shortUrl: url);

                return View(new ShowViewModel
                {
                    Url = metrics.Url,
                    BrowseClicks = metrics.BrowseClicks,
                    DailyClicks = metrics.DailyClicks,
                    PlatformClicks = metrics.PlatformClicks
                });
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = e.Message, ReturnUrl = "/urls" });
            }
        }

        [HttpPost("urls/{url}")]
        public async Task<IActionResult> Create([FromForm] string url)
        {
            try
            {
                var isValidUrl = _urlService.IsValidUrl(url);
                if (!isValidUrl)
                    return View("Error", new ErrorViewModel { ErrorMessage = $"Invalid Url: {url}", ReturnUrl = "/urls" });

                var baseUrl = $"{Request?.Scheme}://{Request?.Host.Value}/";
                var generatedUrl = await _urlService.GenerateUrl(baseUrl: baseUrl, originalUrl: url);

                return RedirectToAction("Show", new { url = generatedUrl.ShortUrl });
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = e.Message, ReturnUrl = "/urls" });
            }
        }
    }
}