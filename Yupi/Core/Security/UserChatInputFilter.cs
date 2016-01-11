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

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Yupi.Core.Io;

namespace Yupi.Core.Security
{
    /// <summary>
    ///     Class Filter.
    /// </summary>
    internal static class UserChatInputFilter
    {
        /// <summary>
        ///     The dictionary
        /// </summary>
        private static readonly Dictionary<string, Dictionary<string, string>> Dictionary =
            new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        ///     Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static string Default { get; private set; }

        /// <summary>
        ///     Loads this instance.
        /// </summary>
        public static void Load()
        {
            foreach (
                string line in
                    File.ReadAllLines($"{Yupi.YupiVariablesDirectory}\\Settings\\filter.ini", Encoding.Default)
                        .Where(line => !line.StartsWith("#") || !line.StartsWith("//") || line.Contains("=")))
            {
                string[] array = line.Split('=');
                string mode = array[0];

                string jsonStr = string.Join("=", array.Skip(1));

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                dynamic items = serializer.Deserialize<object[]>(jsonStr);

                Dictionary<string, string> dic = new Dictionary<string, string>();

                foreach (object[] item in items)
                {
                    string key = item[0].ToString();
                    string value = string.Empty;

                    if (item.Length > 1)
                        value = item[1].ToString();

                    dic.Add(key, value);
                }

                if (dic.ContainsKey(mode))
                    continue;

                if (Default == null)
                    Default = mode;

                Dictionary.Add(mode, dic);
            }

            Writer.WriteLine("Loaded " + Dictionary.Count + " filter modes.", "Yupi.Security");
        }

        /// <summary>
        ///     Reloads this instance.
        /// </summary>
        public static void Reload()
        {
            Dictionary.Clear();
            Load();
        }

        /// <summary>
        ///     Replaces the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string Replace(string mode, string str)
        {
            str = str.RemoveDiacritics().ToLower();

            return !Dictionary.ContainsKey(mode) || string.IsNullOrEmpty(str)
                ? str
                : Dictionary[mode].Aggregate(str, (current, array) => current.Replace(array.Key, array.Value));
        }

        private static string RemoveDiacritics(this string s)
        {
            string normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (
                char c in
                    normalizedString.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                )
                stringBuilder.Append(c);

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}