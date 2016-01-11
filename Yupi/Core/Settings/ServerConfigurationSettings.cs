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

using System;
using System.Collections.Generic;
using System.IO;

namespace Yupi.Core.Settings
{
    /// <summary>
    ///     Class ServerConfigurationSettings.
    /// </summary>
    internal static class ServerConfigurationSettings
    {
        /// <summary>
        ///     The data
        /// </summary>
        internal static Dictionary<string, string> Data = new Dictionary<string, string>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerConfigurationSettings" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="mayNotExist">if set to <c>true</c> [may not exist].</param>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        internal static void Load(string filePath, bool mayNotExist = false)
        {
            if (!File.Exists(filePath))
            {
                if (!mayNotExist)
                    throw new ArgumentException($"Configuration file not found in '{filePath}'.");

                return;
            }

            try
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    string text;

                    while ((text = streamReader.ReadLine()) != null)
                    {
                        if (text.Length < 1 || text.StartsWith("#"))
                            continue;

                        int num = text.IndexOf('=');

                        if (num == -1)
                            continue;

                        string key = text.Substring(0, num);
                        string value = text.Substring(num + 1);

                        Data.Add(key, value);
                    }

                    streamReader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not process configuration file: {ex.Message}");
            }
        }
    }
}