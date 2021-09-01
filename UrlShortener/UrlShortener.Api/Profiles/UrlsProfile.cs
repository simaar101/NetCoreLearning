using AutoMapper;
using UrlShortener.Api.Models;
using UrlShortener.Api.Dtos;
using System.Collections.Generic;

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