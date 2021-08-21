using System;
using System.Text;

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
        //returns the byte array to a hex string
        public static string ByteArrayToString(this byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i=0;i < arrInput.Length; i++)
            {
                // X2 It formats the string as two uppercase hexadecimal characters
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}