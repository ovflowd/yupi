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

namespace Yupi.Data.Interfaces
{
    /// <summary>
    ///     Description of IPlugin.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        ///     Gets the plugin_name.
        /// </summary>
        /// <value>The plugin_name.</value>
        string PluginName { get; }

        /// <summary>
        ///     Gets the plugin_version.
        /// </summary>
        /// <value>The plugin_version.</value>
        string PluginVersion { get; }

        /// <summary>
        ///     Gets the plugin_author.
        /// </summary>
        /// <value>The plugin_author.</value>
        string PluginAuthor { get; }

        /// <summary>
        ///     Message_voids this instance.
        /// </summary>
        void message_void();

        /// <summary>
        ///     Content_voids this instance.
        /// </summary>
        void content_void();

        /// <summary>
        ///     Packets_voids this instance.
        /// </summary>
        void packets_void();

        /// <summary>
        ///     Habbo_voids this instance.
        /// </summary>
        void habbo_void();
    }
}