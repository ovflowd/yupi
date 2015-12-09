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
    /// Class Filter.
    /// </summary>
    internal static class UserChatInputFilter
    {
        /// <summary>
        /// The dictionary
        /// </summary>
        private static readonly Dictionary<string, Dictionary<string, string>> Dictionary = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static string Default { get; private set; }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public static void Load()
        {
            foreach (var line in File.ReadAllLines("Settings\\filter.ini", Encoding.Default).Where(line => !line.StartsWith("#") || !line.StartsWith("//") || line.Contains("=")))
            {
                var array = line.Split('=');
                var mode = array[0];

                var jsonStr = string.Join("=", array.Skip(1));

                var serializer = new JavaScriptSerializer();

                dynamic items = serializer.Deserialize<object[]>(jsonStr);

                var dic = new Dictionary<string, string>();

                foreach (object[] item in items)
                {
                    var key = item[0].ToString();
                    var value = string.Empty;

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
        /// Reloads this instance.
        /// </summary>
        public static void Reload()
        {
            Dictionary.Clear();
            Load();
        }

        /// <summary>
        /// Replaces the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string Replace(string mode, string str)
        {
            str = str.RemoveDiacritics().ToLower();

            return !Dictionary.ContainsKey(mode) || string.IsNullOrEmpty(str) ? str : Dictionary[mode].Aggregate(str, (current, array) => current.Replace(array.Key, array.Value));
        }

        private static string RemoveDiacritics(this string s)
        {
            var normalizedString = s.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
                stringBuilder.Append(c);

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}