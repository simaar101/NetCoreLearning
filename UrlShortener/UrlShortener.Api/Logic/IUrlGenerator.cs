namespace UrlShortener.Api.Logic
{
    public interface IUrlGenerator
    {
        string GetHashFunction(string LongNameUrl);
    }
}