using System;
using UrlShortener;
using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Services
{
    public class UrlGenerator : IUrlGenerator
    {      
        private string curUrl = "https://localhost:5001/UrlGen/";
        public string getShortName(string source)
        {

            if (String.IsNullOrEmpty(source) || !source.CheckUrlValid() )
            {
                throw new ArgumentException(String.Format("{0} is not  a valid URL", source));
            }
            byte[] tmpSource;
            byte[] tmpHash;
            //Create a byte array from source data.
            tmpSource = ASCIIEncoding.ASCII.GetBytes(source);
            //Compute hash based on source data.
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            curUrl+= tmpHash.ByteArrayToString()+"/";
            return curUrl;
        }



    }
}