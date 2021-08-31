using Domain.Entity;
using Domain.Helper;
using System.Collections.Generic;

namespace Domain.Dto
{
    public class UrlMetricDto
    {
        public Url Url { get; set; }
        public Dictionary<string, int> DailyClicks { get; set; }
        public Dictionary<string, int> BrowseClicks { get; set; }
        public Dictionary<string, int> PlatformClicks { get; set; }

        public string CreatedAtFormatted => Url?.CreatedAt.GetFormattedDateTime();
    }
}
