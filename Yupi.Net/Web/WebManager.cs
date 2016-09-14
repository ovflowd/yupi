// ---------------------------------------------------------------------------------
// <copyright file="WebManager.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
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