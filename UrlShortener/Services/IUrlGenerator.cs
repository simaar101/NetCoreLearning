namespace UrlShortener.Services
{
    public interface IUrlGenerator
    {
        string GetHashFunction(string LongNameUrl);
    }
}