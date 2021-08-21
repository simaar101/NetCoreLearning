using AutoMapper;
using UrlShortener.Entities;
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