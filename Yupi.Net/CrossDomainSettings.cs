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

namespace Yupi.Net
{
    using System;
    using System.Collections.ObjectModel;
    using System.Text;

    /// <summary>
    ///     Cross Domain Policy for Adobe Flash Player
    /// </summary>
    public class CrossDomainSettings
    {
        #region Fields

        private byte[] xmlPolicyBytes;

        #endregion Fields

        #region Constructors

        public CrossDomainSettings(string domain, int port)
        {
            string[] lines = new string[]
            {
                "<?xml version=\"1.0\"?>",
                "<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">",
                "<cross-domain-policy>",
                "<allow-access-from domain=\"" + domain + "\" to-ports=\"" + port + "\" />",
                "</cross-domain-policy>\0"
            };

            xmlPolicyBytes = Encoding.ASCII.GetBytes(String.Join("\r\n", lines));
        }

        #endregion Constructors

        #region Methods

        public byte[] GetBytes()
        {
            return xmlPolicyBytes;
        }

        #endregion Methods
    }
}