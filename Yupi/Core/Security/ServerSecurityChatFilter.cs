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
using System.Data;
using System.Linq;
using Yupi.Core.Io;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Core.Security
{
    /// <summary>
    ///     Class ServerSecurityChatFilter.
    /// </summary>
    internal class ServerSecurityChatFilter
    {
        /// <summary>
        ///     The word
        /// </summary>
        internal static List<string> Word;

        /// <summary>
        ///     Determines whether this instance can talk the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="message">The message.</param>
        /// <returns><c>true</c> if this instance can talk the specified session; otherwise, <c>false</c>.</returns>
        internal static bool CanTalk(GameClient session, string message)
        {
            if (CheckForBannedPhrases(message) && session.GetHabbo().Rank < 4)
            {
                if (!Yupi.MutedUsersByFilter.ContainsKey(session.GetHabbo().Id))
                    session.GetHabbo().BobbaFiltered++;

                if (session.GetHabbo().BobbaFiltered < 3)
                    session.SendNotif(
                        "Your language is inappropriate. If you do not change this , measures are being taken by the automated system of Habbo.");
                else if (session.GetHabbo().BobbaFiltered >= 3)
                {
                    if (session.GetHabbo().BobbaFiltered == 3)
                    {
                        session.GetHabbo().BobbaFiltered = 4;
                        Yupi.MutedUsersByFilter.Add(session.GetHabbo().Id,
                            uint.Parse((Yupi.GetUnixTimeStamp() + 300*60).ToString()));

                        return false;
                    }

                    if (session.GetHabbo().BobbaFiltered == 4)
                        session.SendNotif(
                            "Now you can not talk for 5 minutes . This is because your exhibits inappropriate language in Habbo Hotel.");
                    else if (session.GetHabbo().BobbaFiltered == 5)
                        session.SendNotif("You risk a ban if you continue to scold it . This is your last warning");
                    else if (session.GetHabbo().BobbaFiltered >= 7)
                    {
                        session.GetHabbo().BobbaFiltered = 0;

                        Yupi.GetGame().GetBanManager().BanUser(session, "Auto-system-ban", 3600, "ban.", false, false);
                    }
                }
            }

            if (Yupi.MutedUsersByFilter.ContainsKey(session.GetHabbo().Id))
            {
                if (Yupi.MutedUsersByFilter[session.GetHabbo().Id] < Yupi.GetUnixTimeStamp())
                    Yupi.MutedUsersByFilter.Remove(session.GetHabbo().Id);
                else
                {
                    DateTime now = DateTime.Now;
                    TimeSpan timeStillBanned = now - Yupi.UnixToDateTime(Yupi.MutedUsersByFilter[session.GetHabbo().Id]);

                    session.SendNotif("Damn! you can't talk for " + timeStillBanned.Minutes.ToString().Replace("-", "") +
                                      " minutes and " + timeStillBanned.Seconds.ToString().Replace("-", "") +
                                      " seconds.");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Initializes the swear word.
        /// </summary>
        internal static void InitSwearWord()
        {
            Word = new List<string>();
            Word.Clear();

            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery("SELECT `word` FROM wordfilter");
                DataTable table = adapter.GetTable();

                if (table == null)
                    return;

                foreach (DataRow row in table.Rows)
                    Word.Add(row[0].ToString().ToLower());
            }

            Writer.WriteLine("Loaded " + Word.Count + " Bobba Filters", "Yupi.Security");
        }

        /// <summary>
        ///     Checks for banned phrases.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal static bool CheckForBannedPhrases(string message)
        {
            message = message.ToLower();

            message = message.Replace(".", "");
            message = message.Replace(" ", "");
            message = message.Replace("-", "");
            message = message.Replace(",", "");

            return Word.Any(mWord => message.Contains(mWord.ToLower()));
        }
    }
}