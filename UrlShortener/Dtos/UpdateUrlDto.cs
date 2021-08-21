using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dtos
{
    public class UpdateUrlDto
    {
        [Required]
        [MaxLength(200)]
        public string ShortNameUrl { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string LongNameUrl { get; set; }
    }
}