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

using System.Text;

namespace Yupi.Net
{
    /// <summary>
    ///     Cross Domain Policy for Adobe Flash Player
    /// </summary>
    public class CrossDomainSettings
    {
        private readonly byte[] xmlPolicyBytes;

        public CrossDomainSettings(string domain, int port)
        {
            string[] lines =
            {
                "<?xml version=\"1.0\"?>",
                "<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">",
                "<cross-domain-policy>",
                "<allow-access-from domain=\"" + domain + "\" to-ports=\"" + port + "\" />",
                "</cross-domain-policy>\0"
            };

            xmlPolicyBytes = Encoding.ASCII.GetBytes(string.Join("\r\n", lines));
        }

        public byte[] GetBytes()
        {
            return xmlPolicyBytes;
        }
    }
}