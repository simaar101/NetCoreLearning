using System;
using UrlShortener;

namespace UrlShortener.Services
{
    public class UrlGenerator : IUrlGenerator
    {      
        
        public string getShortName(string source)
        {
            if (String.IsNullOrEmpty(source))
            {
                return "";
            }

            if (!source.CheckUrlValid())
            {
                // if(CheckUrlValid(LongUrlName))
                // {
                //     return LongUrlName;
                // }
                return "";
            }
            return "";
        }



    }
}