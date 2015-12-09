using System.Data;

namespace Yupi.Game.RoomBots.Interfaces
{
    /// <summary>
    /// Class CatalogBot.
    /// </summary>
    internal class CatalogBot
    {
        /// <summary>
        /// The bot type
        /// </summary>
        internal string BotType;

        /// <summary>
        /// The bot name
        /// </summary>
        internal string BotName;

        /// <summary>
        /// The bot look
        /// </summary>
        internal string BotLook;

        /// <summary>
        /// The bot mission
        /// </summary>
        internal string BotMission;

        /// <summary>
        /// The bot gender
        /// </summary>
        internal string BotGender;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogBot"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        internal CatalogBot(DataRow row)
        {
            BotType = row["bot_type"].ToString();
            BotName = row["bot_name"].ToString();
            BotLook = row["bot_look"].ToString();
            BotMission = row["bot_mission"].ToString();
            BotGender = row["bot_gender"].ToString();
        }
    }
}
