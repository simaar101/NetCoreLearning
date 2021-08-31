using AutoMapper;
using UrlShortener.Api.Models;
using UrlShortener.Api.Dtos;


namespace UrlShortener.Api.Profiles
{
    public class UrlsProfiles:Profile
    {
        public UrlsProfiles()
        {
            CreateMap<Url, UrlDto>();
        }
    }
}