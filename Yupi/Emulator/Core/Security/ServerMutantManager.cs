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
using System.Linq;
using Yupi.Game.Users.Figures;

namespace Yupi.Core.Security
{
    /// <summary>
    ///     Class ServerMutantManager.
    /// </summary>
    internal class ServerMutantManager
    {
        /// <summary>
        ///     The _parts
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, AvatarFigureParts>> _parts;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerMutantManager" /> class.
        /// </summary>
        public ServerMutantManager()
        {
            _parts = new Dictionary<string, Dictionary<string, AvatarFigureParts>>();
        }

        /// <summary>
        ///     Runs the look.
        /// </summary>
        /// <param name="look">The look.</param>
        /// <returns>System.String.</returns>
        internal string RunLook(string look)
        {
            List<string> toReturnFigureParts = new List<string>();
            List<string> fParts = new List<string>();
            string[] requiredParts = {"hd", "ch"};
            bool flagForDefault = false;

            string[] figureParts = look.Split('.');
            string genderLook = GetLookGender(look);
            foreach (string part in figureParts)
            {
                string newPart = part;
                string[] tPart = part.Split('-');
                if (tPart.Count() < 2)
                {
                    flagForDefault = true;
                    continue;
                }
                string partName = tPart[0];
                string partId = tPart[1];
                if (!_parts.ContainsKey(partName) || !_parts[partName].ContainsKey(partId) ||
                    (genderLook != "U" && _parts[partName][partId].Gender != "U" &&
                     _parts[partName][partId].Gender != genderLook))
                    newPart = SetDefault(partName, genderLook);
                if (!fParts.Contains(partName))
                    fParts.Add(partName);
                if (!toReturnFigureParts.Contains(newPart))
                    toReturnFigureParts.Add(newPart);
            }

            if (flagForDefault)
            {
                toReturnFigureParts.Clear();
                toReturnFigureParts.AddRange("hr-115-42.hd-190-1.ch-215-62.lg-285-91.sh-290-62".Split('.'));
            }

            foreach (string requiredPart in requiredParts.Where(requiredPart => !fParts.Contains(requiredPart) &&
                                                                             !toReturnFigureParts.Contains(
                                                                                 SetDefault(requiredPart, genderLook)))
                )
                toReturnFigureParts.Add(SetDefault(requiredPart, genderLook));
            return string.Join(".", toReturnFigureParts);
        }

        /// <summary>
        ///     Gets the look gender.
        /// </summary>
        /// <param name="look">The look.</param>
        /// <returns>System.String.</returns>
        private string GetLookGender(string look)
        {
            string[] figureParts = look.Split('.');

            foreach (string part in figureParts)
            {
                string[] tPart = part.Split('-');
                if (tPart.Count() < 2)
                    continue;
                string partName = tPart[0];
                string partId = tPart[1];
                if (partName != "hd")
                    continue;
                return _parts.ContainsKey(partName) && _parts[partName].ContainsKey(partId)
                    ? _parts[partName][partId].Gender
                    : "U";
            }
            return "U";
        }

        /// <summary>
        ///     Sets the default.
        /// </summary>
        /// <param name="partName">Name of the part.</param>
        /// <param name="gender">The gender.</param>
        /// <returns>System.String.</returns>
        private string SetDefault(string partName, string gender)
        {
            string partId = "0";
            if (!_parts.ContainsKey(partName))
                return $"{partName}-{partId}-0";
            KeyValuePair<string, AvatarFigureParts> part = _parts[partName].FirstOrDefault(x => x.Value.Gender == gender || gender == "U");
            partId = part.Equals(default(KeyValuePair<string, AvatarFigureParts>)) ? "0" : part.Key;
            return $"{partName}-{partId}-0";
        }
    }
}