using AutoMapper;
using UrlShortener.Models;
using UrlShortener.Dtos;


namespace UrlShortener.Profiles
{
    public class UrlsProfiles:Profile
    {
        public UrlsProfiles()
        {
            CreateMap<Url, UrlDto>();
        }
    }
}