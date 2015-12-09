using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Sessions.Interfaces;
using Yupi.Game.RoomBots.Enumerators;
using Yupi.Game.RoomBots.Interfaces;

namespace Yupi.Game.RoomBots
{
    /// <summary>
    ///     Class BotManager.
    /// </summary>
    internal class BotManager
    {
        /// <summary>
        ///     The _bots
        /// </summary>
        public List<RoomBot> Bots { get; }

        /// <summary>
        ///     The clothing items
        /// </summary>
        internal static Dictionary<string, CatalogBot> CatalogBots;

        /// <summary>
        ///     The clothing items
        /// </summary>
        internal static Dictionary<uint, BotCommand> BotCommands;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotManager"/> class.
        /// </summary>
        internal BotManager()
        {
            Bots = new List<RoomBot>();

            LoadCatalogBots(Yupi.GetDatabaseManager().GetQueryReactor());

            LoadBotsCommands(Yupi.GetDatabaseManager().GetQueryReactor());
        }

        /// <summary>
        ///     Generates the bot from row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>RoomBot.</returns>
        internal static RoomBot GenerateBotFromRow(DataRow row)
        {
            if (row == null)
                return null;

            uint id = Convert.ToUInt32(row["id"]);

            List<string> speeches = null;

            if (!row.IsNull("speech") && !string.IsNullOrEmpty(row["speech"].ToString()))
                speeches = row["speech"].ToString().Split(';').ToList();

            RoomBot bot = new RoomBot(id, Convert.ToUInt32(row["user_id"]), AiType.Generic, row["bot_type"].ToString());

            bot.Update(Convert.ToUInt32(row["room_id"]), (string) row["walk_mode"], (string) row["name"], (string) row["motto"], (string) row["look"], int.Parse(row["x"].ToString()), int.Parse(row["y"].ToString()), int.Parse(row["z"].ToString()), 4, 0, 0, 0, 0, speeches, null, (string) row["gender"], (int) row["dance"], (int) row["speaking_interval"], Convert.ToInt32(row["automatic_chat"]) == 1, Convert.ToInt32(row["mix_phrases"]) == 1);

            return bot;
        }

        /// <summary>
        /// Loads the bots commands.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void LoadBotsCommands(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM bots_commands");

            BotCommands = new Dictionary<uint, BotCommand>();

            DataTable table = dbClient.GetTable();

            foreach (DataRow dataRow in table.Rows)
                BotCommands.Add(uint.Parse(dataRow["id"].ToString()), new BotCommand(dataRow));
        }

        /// <summary>
        /// Loads the catalog bots.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void LoadCatalogBots(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM catalog_bots");

            CatalogBots = new Dictionary<string, CatalogBot>();

            DataTable table = dbClient.GetTable();

            foreach (DataRow dataRow in table.Rows)
                CatalogBots.Add(dataRow["bot_type"].ToString(), new CatalogBot(dataRow));
        }

        /// <summary>
        /// Creates the bot from catalog.
        /// </summary>
        /// <param name="botType">Type of the bot.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>RoomBot.</returns>
        internal static RoomBot CreateBotFromCatalog(string botType, uint userId)
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                CatalogBot catalogBot = GetCatalogBot(botType);

                queryReactor.SetQuery($"INSERT INTO bots_data (user_id,name,motto,look,gender,walk_mode,ai_type,bot_type) VALUES ('{userId}', '{catalogBot.BotName}', '{catalogBot.BotMission}', '{catalogBot.BotLook}', '{catalogBot.BotGender}', 'freeroam', 'generic', '{catalogBot.BotType}')");

                return new RoomBot(Convert.ToUInt32(queryReactor.InsertQuery()), userId, 0u, AiType.Generic, "freeroam", catalogBot.BotName, catalogBot.BotMission, catalogBot.BotLook, 0, 0, 0.0, 0, null, null, catalogBot.BotGender, 0, catalogBot.BotType);
            }
        }

        /// <summary>
        ///     Gets the bots for room.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <returns>List&lt;RoomBot&gt;.</returns>
        internal List<RoomBot> GetBotsForRoom(uint roomId) => new List<RoomBot>(from p in Bots where p.RoomId == roomId select p);

        /// <summary>
        ///     Gets the bot.
        /// </summary>
        /// <param name="botId">The bot identifier.</param>
        /// <returns>RoomBot.</returns>
        internal RoomBot GetBot(uint botId) => Bots.FirstOrDefault(p => p.BotId == botId);

        /// <summary>
        /// Gets the catalog bot.
        /// </summary>
        /// <param name="botType">Type of the bot.</param>
        /// <returns>Azure.Game.RoomBots.Interfaces.CatalogBot.</returns>
        internal static CatalogBot GetCatalogBot(string botType) => CatalogBots.FirstOrDefault(p => p.Key == botType).Value;

        /// <summary>
        /// Gets the bot command by identifier.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <returns>Azure.Game.RoomBots.Interfaces.BotCommand.</returns>
        internal static BotCommand GetBotCommandById(uint commandId) => BotCommands.FirstOrDefault(p => p.Key == commandId).Value;

        /// <summary>
        /// Gets the bot command by input.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <returns>Azure.Game.RoomBots.Interfaces.BotCommand.</returns>
        internal static BotCommand GetBotCommandByInput(string userInput) => BotCommands.FirstOrDefault(p => p.Value.SpeechInput == userInput || p.Value.SpeechInputAlias.Contains(userInput)).Value;
    }
}