using Domain.Entity;
using System;
using System.Collections.Generic;

namespace tests.Helpers
{
    internal static class UrlSeed
    {
        public static IEnumerable<Url> Seeds = new List<Url>()
        {
             new()
                {
                    Id = UrlIds.UrlId1,
                    ShortUrl = "ABCDE",
                    Count = new Random().Next(1, 10),
                    CreatedAt = DateTime.Now,
                    OriginalUrl = "https://drive.google.com/file/d/1VdLgSSMojWFb1GRoBAFX_eXy7oX2J",
                    FullUrl = "https://localhost:5001/ABCDE"
                },
                new()
                {
                    Id = UrlIds.UrlId2,
                    ShortUrl = "FGHIJ",
                    Count = new Random().Next(1, 10),
                    CreatedAt = DateTime.Now,
                    OriginalUrl = "https://drive.google.com/file/d/1VdLgSSMojWFb1GRoBAFX_eXy7oX2J",
                    FullUrl = "https://localhost:5001/FGHIJ"
                },
                new()
                {
                    Id = UrlIds.UrlId3,
                    ShortUrl = "KLMNO",
                    Count = new Random().Next(1, 10),
                    CreatedAt = DateTime.Now,
                    OriginalUrl = "https://drive.google.com/file/d/1VdLgSSMojWFb1GRoBAFX_eXy7oX2J",
                    FullUrl = "https://localhost:5001/KLMNO"
                },
                new()
                {
                    Id = UrlIds.UrlId4,
                    ShortUrl = "PQRST",
                    Count = new Random().Next(1, 10),
                    CreatedAt = DateTime.Now,
                    OriginalUrl = "https://drive.google.com/file/d/1VdLgSSMojWFb1GRoBAFX_eXy7oX2J",
                    FullUrl = "https://localhost:5001/PQRST"
                },
                new()
                {
                    Id = UrlIds.UrlId5,
                    ShortUrl = "UVXZW",
                    Count = new Random().Next(1, 10),
                    CreatedAt = DateTime.Now,
                    OriginalUrl = "https://drive.google.com/file/d/1VdLgSSMojWFb1GRoBAFX_eXy7oX2J",
                    FullUrl = "https://localhost:5001/UVXZW"
                }
        }
       .ToArray();
    }
}
