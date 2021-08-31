using Domain.Entity;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Url> Urls { get; set; } = new List<Url>();
        public Url NewUrl { get; set; } = new();
    }
}
