using Domain.Entity;
using System;
using System.Collections.Generic;

namespace tests.Helpers
{
    internal static class UrlMetricSeed
    {


        public static IEnumerable<UrlMetric> Seeds = new List<UrlMetric>()
        {
             new()
                {
                    Id = UrlIds.UrlMetricId1,
                    UrlId = UrlIds.UrlId1,
                    Browser = "Firefox",
                    Platform = "Windows",
                    CreatedAt =DateTime.Now
                },
                new()
                {
                    Id = UrlIds.UrlMetricId2,
                    UrlId = UrlIds.UrlId1,
                    Browser = "IE9",
                    Platform = "Windows",
                    CreatedAt =DateTime.Now
                },
                 new()
                 {
                    Id = UrlIds.UrlMetricId3,
                    UrlId = UrlIds.UrlId1,
                    Browser = "IE9",
                    Platform = "Windows",
                    CreatedAt =DateTime.Now
                },
                new()
                {
                    Id = UrlIds.UrlMetricId4,
                    UrlId = UrlIds.UrlId2,
                    Browser = "Safari",
                    Platform = "macOS",
                    CreatedAt =DateTime.Now
                },
                new()
                {
                    Id = UrlIds.UrlMetricId5,
                    UrlId = UrlIds.UrlId2,
                    Browser = "Firefox",
                    Platform = "Windows",
                    CreatedAt =DateTime.Now
                },
                new()
                {
                    Id = UrlIds.UrlMetricId6,
                    UrlId = UrlIds.UrlId2,
                    Browser = "Firefox",
                    Platform = "Windows",
                    CreatedAt =DateTime.Now
                },
                new()
                {
                    Id = UrlIds.UrlMetricId7,
                    UrlId = UrlIds.UrlId2,
                    Browser = "Chrome",
                    Platform = "Windows",
                    CreatedAt =DateTime.Now
                }
        }
       .ToArray();
    }
}
