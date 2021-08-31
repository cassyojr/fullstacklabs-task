using Domain.Dto;
using Domain.Entity;
using Domain.Repository;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUrlMetricRepository _urlMetricRepository;

        public UrlService(IUrlRepository urlRepository, IUrlMetricRepository urlMetricRepository)
        {
            _urlRepository = urlRepository;
            _urlMetricRepository = urlMetricRepository;
        }

        public async Task<IEnumerable<Url>> GetNewestUrls(int takeCount) => takeCount == 0 ? null : await _urlRepository.GetNewestUrls(takeCount);

        public async Task<IEnumerable<Url>> GetUrls() => await _urlRepository.GetAllAsync();

        public async Task GenerateMetric(string shortUrl, string browser, string platform)
        {
            var url = await _urlRepository.GetByShortUrl(shortUrl);
            if (url is null) throw new NullReferenceException($"Url not found with shortUrl: {shortUrl}");

            url.Count += 1;
            await _urlRepository.UpdateAsync(url);

            var urlMetric = new UrlMetric(url.Id, browser, platform);
            await _urlMetricRepository.AddAsync(urlMetric);
        }

        public async Task<Url> GenerateUrl(string baseUrl, string originalUrl)
        {
            var url = new Url(baseUrl, originalUrl);
            return await _urlRepository.AddAsync(url);
        }

        public async Task<Url> GetUrlByShortUrl(string shortUrl) => await _urlRepository.GetByShortUrl(shortUrl);

        public async Task<UrlMetricDto> GetUrlMetricsByShortUrl(string shortUrl)
        {
            var isUrlExistent = await _urlRepository.GetByShortUrl(shortUrl);
            if (isUrlExistent is null) throw new NullReferenceException("Invalid URL to get metrics");

            return await _urlRepository.GetUrlMetrics(shortUrl);
        }

        public bool IsValidUrl(string url)
        {
            var validUrlPattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            var regex = new Regex(validUrlPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return regex.IsMatch(url);
        }
    }
}
