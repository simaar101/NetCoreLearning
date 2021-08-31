using System;

namespace UrlShortener.Api.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public string ShortNameUrl { get; set; }
        public string LongNameUrl { get; set; }
        public string HashFunction { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}