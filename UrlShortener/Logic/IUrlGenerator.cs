namespace UrlShortener.Logic
{
    public interface IUrlGenerator
    {
        string GetHashFunction(string LongNameUrl);
    }
}