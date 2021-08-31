using System;
using UrlShortener.Api;
using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Api.Logic
{
    public class UrlGenerator : IUrlGenerator
    {      
        public string GetHashFunction(string LongNameUrl)
        {
            if (String.IsNullOrEmpty(LongNameUrl) || !LongNameUrl.CheckUrlValid() )
            {
                throw new ArgumentException(String.Format("{0} is not  a valid URL", LongNameUrl));
            }
            byte[] tmpSource;
            byte[] tmpHash;
            //Create a byte array from source data.
            tmpSource = ASCIIEncoding.ASCII.GetBytes(LongNameUrl);
            //Compute hash based on source data.
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return tmpHash.ByteArrayToString();
        }
    }
}