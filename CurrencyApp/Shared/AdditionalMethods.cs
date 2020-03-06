using System;
using System.IO;
using System.Net;

namespace CurrencyApp.Shared
{
    public static class AdditionalMethods
    {
        public static string createResponse(string requestMessage)
        {
            WebRequest request = WebRequest.Create(requestMessage);
            WebResponse response = request.GetResponse();
            string info;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    info = reader.ReadToEnd();
                }
            }
            response.Close();
            return info;
        }
    }
}
