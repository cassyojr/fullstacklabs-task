using Domain.Dto;
using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUrlService
    {
        Task<Url> GenerateUrl(string baseUrl, string originalUrl);
        Task<Url> GetUrlByShortUrl(string shortUrl);
        Task<IEnumerable<Url>> GetUrls();
        Task GenerateMetric(string shortUrl, string browser, string platform);
        Task<UrlMetricDto> GetUrlMetricsByShortUrl(string shortUrl);

        Task<IEnumerable<Url>> GetNewestUrls(int takeCount);

        bool IsValidUrl(string url);
    }
}
