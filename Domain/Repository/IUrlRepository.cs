using Domain.Dto;
using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IUrlRepository : IGenericRepository<Url>
    {
        Task<Url> GetByShortUrl(string shortUrl);
        Task<UrlMetricDto> GetUrlMetrics(string shortUrl);

        Task<IEnumerable<Url>> GetNewestUrls(int count);
    }
}
