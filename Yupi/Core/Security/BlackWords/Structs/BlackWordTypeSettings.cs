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

namespace Yupi.Core.Security.BlackWords.Structs
{
    /// <summary>
    ///     Struct BlackWordTypeSettings
    /// </summary>
    internal struct BlackWordTypeSettings
    {
        /// <summary>
        ///     The filter
        /// </summary>
        public string Filter, Alert, ImageAlert;

        /// <summary>
        ///     The maximum advices
        /// </summary>
        public uint MaxAdvices;

        /// <summary>
        ///     The automatic ban
        /// </summary>
        public bool AutoBan, ShowMessage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BlackWordTypeSettings" /> struct.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="alert">The alert.</param>
        /// <param name="maxAdvices">The maximum advices.</param>
        /// <param name="imageAlert">The image alert.</param>
        /// <param name="autoBan">if set to <c>true</c> [automatic ban].</param>
        /// <param name="showMessage"></param>
        public BlackWordTypeSettings(string filter, string alert, uint maxAdvices, string imageAlert, bool autoBan,
            bool showMessage)
        {
            Filter = filter;
            Alert = alert;
            MaxAdvices = maxAdvices;
            ImageAlert = imageAlert;
            AutoBan = autoBan;
            ShowMessage = showMessage;
        }
    }
}