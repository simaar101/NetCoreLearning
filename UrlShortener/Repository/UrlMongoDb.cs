using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using UrlShortener.Entities;

namespace UrlShortener.Repository
{
    public class UrlMongoDb : IUrlRepo
    {
        private readonly string collectionName = "urlCollection" ;
        private readonly string DatabaseName = "UrlDb";
        private readonly IMongoCollection<Url> urlCollection;
        public UrlMongoDb(IMongoClient client)
        {
           urlCollection =  client.GetDatabase(DatabaseName).GetCollection<Url>(collectionName);
        }
        public async Task CreateUrlAsync(Url url)
        {
            if(url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }
           await  urlCollection.InsertOneAsync(url);
        }

        public async Task DeleteUrlAsync(Guid id)
        {
           await urlCollection.DeleteOneAsync( s => s.Id == id);
        }

        public async Task<Url> GetUrlAsync(Guid id)
        {
            return await urlCollection.Find<Url>(s=>s.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Url>> GetUrlsAsync()
        {
            return await urlCollection.Find(Url => true).ToListAsync();
        }

        public async Task UpdateUrlAsync(Url url)
        {
            if(url is null)
            {
                throw new ArgumentNullException(nameof(url));
            };
            await urlCollection.ReplaceOneAsync(s=> s.Id == url.Id, url);
        }
    }
}