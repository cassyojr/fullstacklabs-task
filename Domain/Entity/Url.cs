using Domain.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Domain.Entity
{
   public class Url
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public string FullUrl { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }


        [NotMapped]
        public string CreatedAtFormatted => CreatedAt.GetFormattedDateTime();

        public ICollection<UrlMetric> UrlMetrics { get; set; }

        public Url() { }

        public Url(string baseUrl, string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl)) throw new NullReferenceException(nameof(OriginalUrl));
            if (string.IsNullOrEmpty(baseUrl)) throw new NullReferenceException(nameof(baseUrl));

            Count = 0;
            OriginalUrl = originalUrl;
            ShortUrl = GenerateShortUrl();
            FullUrl = $"{baseUrl}{ShortUrl}";
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        private string GenerateShortUrl()
        {
            var base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var uniqueId = Regex.Replace(base64Guid, @"[^a-zA-Z]", string.Empty);
            var randomUrlIndex = new Random().Next(5, 10);

            return uniqueId.Substring(0, randomUrlIndex).ToUpper();
        }
    }
}
