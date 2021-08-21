
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Entities;

namespace UrlShortener.Repository
{
    public interface IUrlRepo
    {
        Task<IEnumerable<Url>> GetUrlsAsync();
        Task<Url> GetUrlAsync(Guid id);
        Task DeleteUrlAsync(Guid id);
        Task CreateUrlAsync(Url url);
        Task UpdateUrlAsync(Url url);
        Task<Url> GetUrlByHashAsync(string hashFunction);
    }
}