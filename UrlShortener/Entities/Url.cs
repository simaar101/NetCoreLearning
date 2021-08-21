using System;

namespace UrlShortener.Entities
{
    public class Url
    {
        public Guid Id { get; set; }
        public string ShortNameUrl { get; set; }
        public string LongNameUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}