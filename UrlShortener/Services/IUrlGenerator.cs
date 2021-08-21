namespace UrlShortener.Services
{
    public interface IUrlGenerator
    {
        string getShortName(string ShortNameUrl);
    }
}