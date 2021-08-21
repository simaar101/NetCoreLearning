using System;

namespace UrlShortener
{
    public static class Extensions
    {
        public static bool CheckUrlValid( this string source)
        {
            if(String.IsNullOrEmpty(source))
            {
                return false;
            }
            Uri uriResult;
            bool result = Uri.TryCreate(source, UriKind.Absolute, out uriResult) 
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }
    }
}