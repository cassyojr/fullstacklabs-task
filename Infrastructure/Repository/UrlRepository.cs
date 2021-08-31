using Domain.Dto;
using Domain.Entity;
using Domain.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UrlRepository : GenericRepository<Url>, IUrlRepository
    {
        public UrlRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Url> GetByShortUrl(string shortUrl) => await _context.Urls.FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);

        public async Task<IEnumerable<Url>> GetNewestUrls(int count) => await _context.Urls.OrderByDescending(x => x.CreatedAt).Take(count).ToListAsync();

        public async Task<UrlMetricDto> GetUrlMetrics(string shortUrl)
        {
            var urlMetricDto = new UrlMetricDto();

            var url = await GetByShortUrl(shortUrl);
            urlMetricDto.Url = url;

            urlMetricDto.BrowseClicks = _context.UrlMetrics
                               .Include(x => x.Url)
                               .Where(x => x.Url.ShortUrl == shortUrl)
                               .GroupBy(f => f.Browser)
                               .Select(g => new { Browser = g.Key, Count = g.Count() })
                               .ToDictionary(k => k.Browser, i => i.Count);

            urlMetricDto.DailyClicks = _context.UrlMetrics
                               .Include(x => x.Url)
                               .Where(x => x.Url.ShortUrl == shortUrl && x.CreatedAt.Month == DateTime.Now.Month)
                               .GroupBy(f => f.CreatedAt.Day)
                               .Select(g => new { Browser = g.Key.ToString(), Count = g.Count() })
                               .ToDictionary(k => k.Browser, i => i.Count);

            urlMetricDto.PlatformClicks = _context.UrlMetrics
                               .Include(x => x.Url)
                               .Where(x => x.Url.ShortUrl == shortUrl)
                               .GroupBy(f => f.Platform)
                               .Select(g => new { Browser = g.Key, Count = g.Count() })
                               .ToDictionary(k => k.Browser, i => i.Count);

            return urlMetricDto;
        }
    }
}
