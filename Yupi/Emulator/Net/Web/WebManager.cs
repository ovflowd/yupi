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

using System.IO;
using System.Net;

namespace Yupi.Net.Web
{
    internal static class WebManager
    {
        /// <summary>
        ///     HTTPs the post json.
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

            HttpWebResponse httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                return streamReader.ReadToEnd();
        }
    }
}