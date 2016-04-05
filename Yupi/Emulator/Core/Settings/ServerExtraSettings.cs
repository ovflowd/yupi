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
using System.Linq;
using System.Text;

namespace Yupi.Emulator.Core.Settings
{
    /// <summary>
    ///     Class ServerExtraSettings.
    /// </summary>
     class ServerExtraSettings
    {
        /// <summary>
        ///     The currency loop enabled
        /// </summary>
         static bool CurrencyLoopEnabled = true;

        /// <summary>
        ///     The Currently loop time in minutes
        /// </summary>
         static int CurrentlyLoopTimeInMinutes = 15;

        /// <summary>
        ///     The credits to give
        /// </summary>
         static int CreditsToGive = 3000;

        /// <summary>
        ///     The pixels to give
        /// </summary>
         static int PixelsToGive = 100;

        /// <summary>
        ///     The youtube thumbnail suburl
        /// </summary>
         static string YoutubeThumbnailSuburl = "youtubethumbnail.php?Video";

        /// <summary>
        ///     The diamonds loop enabled
        /// </summary>
         static bool DiamondsLoopEnabled = true;

        /// <summary>
        ///     The diamonds vip only
        /// </summary>
         static bool DiamondsVipOnly = true;

        /// <summary>
        ///     The diamonds to give
        /// </summary>
         static int DiamondsToGive = 1;

        /// <summary>
        ///     The change name staff
        /// </summary>
         static bool ChangeNameStaff = true;

        /// <summary>
        ///     The change name vip
        /// </summary>
         static bool ChangeNameVip = true;

        /// <summary>
        ///     The change name everyone
        /// </summary>
         static bool ChangeNameEveryone = true;

        /// <summary>
        ///     The ne w_users_gifts_ enabled
        /// </summary>
         static bool NewUsersGiftsEnabled = true;

        /// <summary>
        ///     The ServerCamera from Stories
        /// </summary>
         static string StoriesApiServerUrl = "";

        /// <summary>
        ///     The ServerCamera from Stories
        /// </summary>
         static string StoriesApiThumbnailServerUrl = "";

        /// <summary>
        ///     The ServerCamera from Stories
        /// </summary>
         static string StoriesApiHost = "";

        /// <summary>
        ///     The enable beta camera
        /// </summary>
         static bool EnableBetaCamera = true;

        /// <summary>
        ///     The new user gift yttv2 identifier
        /// </summary>
         static uint NewUserGiftYttv2Id = 4930;

        /// <summary>
        ///     The everyone use floor
        /// </summary>
         static bool EveryoneUseFloor = true;

        /// <summary>
        ///     The new page commands
        /// </summary>
         static bool NewPageCommands;

        /// <summary>
        ///     The figure data URL
        /// </summary>
         static string FigureDataUrl = "http://localhost/gamedata/figuredata/1.xml";

        /// <summary>
        ///     The furniture data URL
        /// </summary>
         static string FurnitureDataUrl;

        /// <summary>
        ///     The admin can use HTML
        /// </summary>
         static bool AdminCanUseHtml = true;

        /// <summary>
        ///     The encryption client side
        /// </summary>
         static bool EncryptionClientSide;

        /// <summary>
        ///     The welcome message
        /// </summary>
         static string WelcomeMessage = "";

        /// <summary>
        ///     The game center stories URL
        /// </summary>
         static string GameCenterStoriesUrl;

        /// <summary>
        ///     Runs the extra settings.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
         static bool RunExtraSettings()
        {
            if (File.Exists(Path.Combine(Yupi.YupiVariablesDirectory, "Settings", "Welcome" ,"message.txt")))
                WelcomeMessage =
                    File.ReadAllText(Path.Combine(Yupi.YupiVariablesDirectory, "Settings", "Welcome", "message.txt"));

            if (!File.Exists(Path.Combine(Yupi.YupiVariablesDirectory, "Settings", "other.ini")))
                return false;

            foreach (
                string[] settingsParameters in
                    from line in
				File.ReadAllLines(Path.Combine(Yupi.YupiVariablesDirectory, "Settings", "other.ini"),
                            Encoding.Default)
                    where !string.IsNullOrWhiteSpace(line) && line.Contains("=")
                    select line.Split('='))
            {
                switch (settingsParameters[0])
                {
                    case "currency.loop.enabled":
                        CurrencyLoopEnabled = settingsParameters[1] == "true";
                        break;

                    case "gamecenter.stories.url":
                        GameCenterStoriesUrl = settingsParameters[1];
                        break;

                    case "currency.loop.time.in.minutes":
                        int i;
                        if (int.TryParse(settingsParameters[1], out i))
                            CurrentlyLoopTimeInMinutes = i;
                        break;

                    case "credits.to.give":
                        int j;
                        if (int.TryParse(settingsParameters[1], out j))
                            CreditsToGive = j;
                        break;

                    case "pixels.to.give":
                        int k;
                        if (int.TryParse(settingsParameters[1], out k))
                            PixelsToGive = k;
                        break;

                    case "diamonds.loop.enabled":
                        DiamondsLoopEnabled = settingsParameters[1] == "true";
                        break;

                    case "diamonds.to.give":
                        int l;
                        if (int.TryParse(settingsParameters[1], out l))
                            DiamondsToGive = l;
                        break;

                    case "diamonds.vip.only":
                        DiamondsVipOnly = settingsParameters[1] == "true";
                        break;

                    case "change.name.staff":
                        ChangeNameStaff = settingsParameters[1] == "true";
                        break;

                    case "change.name.vip":
                        ChangeNameVip = settingsParameters[1] == "true";
                        break;

                    case "change.name.everyone":
                        ChangeNameEveryone = settingsParameters[1] == "true";
                        break;

                    case "stories.api.enabled":
                        EnableBetaCamera = settingsParameters[1] == "true";
                        break;

                    case "newuser.gift.yttv2.id":
                        uint u;
                        if (uint.TryParse(settingsParameters[1], out u))
                            NewUserGiftYttv2Id = u;
                        break;

                    case "furnidata.url":
                        FurnitureDataUrl = settingsParameters[1];
                        break;

                    case "commands.new.page":
                        NewPageCommands = settingsParameters[1] == "true";
                        break;

                    case "stories.api.url":
                        StoriesApiServerUrl = settingsParameters[1];
                        break;

                    case "stories.api.thumbnail.url":
                        StoriesApiThumbnailServerUrl = settingsParameters[1];
                        break;

                    case "stories.api.host":
                        StoriesApiHost = settingsParameters[1];
                        break;

                    case "rc4.client.side.enabled":
                        EncryptionClientSide = settingsParameters[1] == "true";
                        break;
                }
            }

            return true;
        }
    }
}