using System;

namespace Domain.Entity
{
    public class UrlMetric
    {
        public Guid Id { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UrlId { get; set; }
        public Url Url { get; set; }

        public UrlMetric() { }

        public UrlMetric(Guid urlId, string browser, string platform)
        {
            if (urlId.Equals(Guid.Empty)) throw new NullReferenceException(nameof(UrlId));
            if (string.IsNullOrEmpty(browser)) throw new NullReferenceException(nameof(Browser));
            if (string.IsNullOrEmpty(platform)) throw new NullReferenceException(nameof(Platform));

            UrlId = urlId;
            Browser = browser;
            Platform = platform;

            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}
