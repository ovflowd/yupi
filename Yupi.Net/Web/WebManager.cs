#region Header

/**
     Because i love chocolat...
                                    88 88
                                    "" 88
                                       88
8b       d8 88       88 8b,dPPYba,  88 88
`8b     d8' 88       88 88P'    "8a 88 88
 `8b   d8'  88       88 88       d8 88 ""
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88
    d8'                 88
   d8'                  88

   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake.
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

#endregion Header

namespace Yupi.Net.Web
{
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Cache;

    using Newtonsoft.Json;

    public static class WebManager
    {
        #region Other

        // TODO Implement
        /*
        /// <summary>
        ///     Does REST GET Request with jSON Return
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>System.String.</returns>
        public static string HttpGetJson(string uri)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "GET";

            Stream httpResponseStream = httpWebRequest.GetResponse().GetResponseStream();

            if (httpResponseStream == null)
                return string.Empty;

            using (StreamReader streamReader = new StreamReader(httpResponseStream))
                return streamReader.ReadToEnd();
        }

        /// <summary>
        ///     Does REST GET Request with jSON Return
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>Dynamic</returns>
        public static dynamic HttpGetJsonObject(string uri) => JsonConvert.DeserializeObject(new WebClient().DownloadString(uri));

        /// <summary>
        ///     Does REST GET Request with jSON Return
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>DataSet.</returns>
        public static DataSet HttpGetJsonDataset(string uri)
        {
            WebClient client = new WebClient {CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache)};

            return JsonConvert.DeserializeObject<DataSet>(client.DownloadString(uri));
        }

        /// <summary>
        ///     Does REST POST Request with jSON Write and Return
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="json">The json.</param>
        /// <returns>System.String.</returns>
        public static string HttpPostJson(string uri, string json)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);

            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            Stream httpResponseStream = httpWebRequest.GetResponse().GetResponseStream();

            if (httpResponseStream == null)
                return string.Empty;

            using (StreamReader streamReader = new StreamReader(httpResponseStream))
                return streamReader.ReadToEnd();
        }*/

        #endregion Other
    }
}