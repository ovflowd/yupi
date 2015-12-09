using System.IO;
using System.Linq;
using System.Text;

namespace Yupi.Core.Settings
{
    /// <summary>
    /// Class ServerExtraSettings.
    /// </summary>
    internal class ServerExtraSettings
    {
        /// <summary>
        /// The currency loop enabled
        /// </summary>
        internal static bool CurrencyLoopEnabled = true;

        /// <summary>
        /// The Currently loop time in minutes
        /// </summary>
        internal static int CurrentlyLoopTimeInMinutes = 15;

        /// <summary>
        /// The credits to give
        /// </summary>
        internal static int CreditsToGive = 3000;

        /// <summary>
        /// The pixels to give
        /// </summary>
        internal static int PixelsToGive = 100;

        /// <summary>
        /// The youtube thumbnail suburl
        /// </summary>
        internal static string YoutubeThumbnailSuburl = "youtubethumbnail.php?Video";

        /// <summary>
        /// The diamonds loop enabled
        /// </summary>
        internal static bool DiamondsLoopEnabled = true;

        /// <summary>
        /// The diamonds vip only
        /// </summary>
        internal static bool DiamondsVipOnly = true;

        /// <summary>
        /// The diamonds to give
        /// </summary>
        internal static int DiamondsToGive = 1;

        /// <summary>
        /// The change name staff
        /// </summary>
        internal static bool ChangeNameStaff = true;

        /// <summary>
        /// The change name vip
        /// </summary>
        internal static bool ChangeNameVip = true;

        /// <summary>
        /// The change name everyone
        /// </summary>
        internal static bool ChangeNameEveryone = true;

        /// <summary>
        /// The ne w_users_gifts_ enabled
        /// </summary>
        internal static bool NewUsersGiftsEnabled = true;

        /// <summary>
        /// The ServerCamera from Stories
        /// </summary>
        internal static string StoriesApiServerUrl = "";

        /// <summary>
        /// The ServerCamera from Stories
        /// </summary>
        internal static string StoriesApiThumbnailServerUrl = "";

        /// <summary>
        /// The ServerCamera from Stories
        /// </summary>
        internal static string StoriesApiHost = "";

        /// <summary>
        /// The enable beta camera
        /// </summary>
        internal static bool EnableBetaCamera = true;

        /// <summary>
        /// The new user gift yttv2 identifier
        /// </summary>
        internal static uint NewUserGiftYttv2Id = 4930;

        /// <summary>
        /// The everyone use floor
        /// </summary>
        internal static bool EveryoneUseFloor = true;

        /// <summary>
        /// The new page commands
        /// </summary>
        internal static bool NewPageCommands;

        /// <summary>
        /// The figure data URL
        /// </summary>
        internal static string FigureDataUrl = "http://localhost/gamedata/figuredata/1.xml";

        /// <summary>
        /// The furniture data URL
        /// </summary>
        internal static string FurnitureDataUrl;

        /// <summary>
        /// The admin can use HTML
        /// </summary>
        internal static bool AdminCanUseHtml = true;

        /// <summary>
        /// The encryption client side
        /// </summary>
        internal static bool EncryptionClientSide;

        /// <summary>
        /// The welcome message
        /// </summary>
        internal static string WelcomeMessage = "";

        /// <summary>
        /// The game center stories URL
        /// </summary>
        internal static string GameCenterStoriesUrl;

        /// <summary>
        /// Runs the extra settings.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal static bool RunExtraSettings()
        {
            if (File.Exists("Settings/Welcome/message.txt"))
                WelcomeMessage = File.ReadAllText("Settings/Welcome/message.txt");

            if (!File.Exists("Settings/other.ini"))
                return false;

            foreach (var settingsParameters in from line in File.ReadAllLines("Settings/other.ini", Encoding.Default) where !string.IsNullOrWhiteSpace(line) && line.Contains("=") select line.Split('='))
            {
                switch (settingsParameters[0])
                {
                    case "currency.loop.enabled":
                        CurrencyLoopEnabled = settingsParameters[1] == "true";
                        break;

                    case "youtube.thumbnail.suburl":
                        YoutubeThumbnailSuburl = settingsParameters[1];
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

                    case "enable.beta.camera":
                        EnableBetaCamera = settingsParameters[1] == "true";
                        break;

                    case "newuser.gifts.enabled":
                        NewUsersGiftsEnabled = settingsParameters[1] == "true";
                        break;

                    case "newuser.gift.yttv2.id":
                        uint u;
                        if (uint.TryParse(settingsParameters[1], out u))
                            NewUserGiftYttv2Id = u;
                        break;

                    case "everyone.use.floor":
                        EveryoneUseFloor = settingsParameters[1] == "true";
                        break;

                    case "figuredata.url":
                        FigureDataUrl = settingsParameters[1];
                        break;

                    case "furnidata.url":
                        FurnitureDataUrl = settingsParameters[1];
                        break;

                    case "admin.can.useHTML":
                        AdminCanUseHtml = settingsParameters[1] == "true";
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