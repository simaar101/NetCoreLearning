using System;

namespace UrlShortener.Dtos
{
    public class UrlDto
    {
        public Guid Id { get; set; }
        public string ShortNameUrl { get; set; }
        public string LongNameUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}