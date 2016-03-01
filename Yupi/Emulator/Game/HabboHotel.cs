using System;
using System.Threading;
using System.Threading.Tasks;
using Yupi.Emulator.Core.Io;
using Yupi.Emulator.Core.Io.Interfaces;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Security;
using Yupi.Emulator.Core.Security.BlackWords;
using Yupi.Emulator.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Achievements;
using Yupi.Emulator.Game.Browser;
using Yupi.Emulator.Game.Catalogs;
using Yupi.Emulator.Game.Commands;
using Yupi.Emulator.Game.GameClients;
using Yupi.Emulator.Game.Groups;
using Yupi.Emulator.Game.Items;
using Yupi.Emulator.Game.Items.Handlers;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.Polls;
using Yupi.Emulator.Game.RoomBots;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.SoundMachine;
using Yupi.Emulator.Game.Support;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Game.Users.Fuses;
using Yupi.Emulator.Game.Users.Guides;
using Yupi.Emulator.Game.Users.Messenger.Structs;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Enums;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game
{
    /// <summary>
    ///     Class HabboHotel.
    /// </summary>
    internal class HabboHotel
    {
        /// <summary>
        ///     The game loop enabled
        /// </summary>
        internal static bool GameLoopEnabled = true;

        /// <summary>
        ///     The _achievement manager
        /// </summary>
        private readonly AchievementManager _achievementManager;

        /// <summary>
        ///     The _ban manager
        /// </summary>
        private readonly ModerationBanManager _banManager;

        /// <summary>
        ///     The _bot manager
        /// </summary>
        private readonly BotManager _botManager;

        /// <summary>
        ///     The _catalog
        /// </summary>
        private readonly CatalogManager _catalog;

        /// <summary>
        ///     The _client manager
        /// </summary>
        private readonly GameClientManager _clientManager;

        /// <summary>
        ///     The _clothing manager
        /// </summary>
        private readonly ClothingManager _clothingManager;

        /// <summary>
        ///     The _clothing manager
        /// </summary>
        private readonly CrackableEggHandler _crackableEggHandler;

        /// <summary>
        ///     The _events
        /// </summary>
        private readonly RoomEvents _events;

        /// <summary>
        ///     The _group manager
        /// </summary>
        private readonly GroupManager _groupManager;

        /// <summary>
        ///     The _guide manager
        /// </summary>
        private readonly GuideManager _guideManager;

        private readonly HallOfFame _hallOfFame;

        /// <summary>
        ///     The _hotel view
        /// </summary>
        private readonly HotelLandingManager _hotelView;

        /// <summary>
        ///     The _item manager
        /// </summary>
        private readonly ItemManager _itemManager;

        /// <summary>
        ///     The _moderation tool
        /// </summary>
        private readonly ModerationTool _moderationTool;

        /// <summary>
        ///     The _navigatorManager
        /// </summary>
        private readonly HotelBrowserManager _navigatorManager;

        /// <summary>
        ///     The _pinata handler
        /// </summary>
        private readonly PinataHandler _pinataHandler;

        /// <summary>
        ///     The _pixel manager
        /// </summary>
        private readonly ExchangeManager _pixelManager;

        /// <summary>
        ///     The _poll manager
        /// </summary>
        private readonly PollManager _pollManager;

        /// <summary>
        ///     The _role manager
        /// </summary>
        private readonly RoleManager _roleManager;

        /// <summary>
        ///     The _room manager
        /// </summary>
        private readonly RoomManager _roomManager;

        /// <summary>
        ///     The _talent manager
        /// </summary>
        private readonly TalentManager _talentManager;

        private readonly TargetedOfferManager _targetedOfferManager;

        /// <summary>
        ///     The _game loop
        /// </summary>
        private Task _gameLoop;

        /// <summary>
        ///     The client manager cycle ended
        /// </summary>
        internal bool ClientManagerCycleEnded, RoomManagerCycleEnded;

        /// <summary>
        ///     Packet Debug Mode
        /// </summary>
        private readonly bool _packetDebugMode;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HabboHotel" /> class.
        /// </summary>
        internal HabboHotel()
        {
            YupiWriterManager.WriteLine("Starting up Yupi Emulator for " + Environment.MachineName + "...", "Yupi.Boot", ConsoleColor.DarkGray);

            _clientManager = new GameClientManager();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                AbstractBar bar = new AnimatedBar();

                const int wait = 5;
                const int end = 5;

                uint itemsLoaded;
                uint navigatorLoaded;
                uint roomModelLoaded;
                uint achievementLoaded;
                uint pollLoaded;

                _packetDebugMode = Yupi.PacketDebugMode;

                Yupi.PacketDebugMode = false;

                Progress(bar, wait, end, "Cleaning dirty in DataBase...");
                DatabaseCleanup(queryReactor);

                Progress(bar, wait, end, "Loading Packet Library...");
                PacketLibraryManager.Init();

                Progress(bar, wait, end, "Loading Chat Emotions...");
                ChatEmotions.Initialize();

                Progress(bar, wait, end, "Loading Furniture Data...");
                FurnitureDataManager.SetCache();

                Progress(bar, wait, end, "Loading Pet Commands...");
                PetCommandHandler.Init(queryReactor);

                Progress(bar, wait, end, "Loading Pet Speeches...");
                PetLocale.Init(queryReactor);

                Progress(bar, wait, end, "Loading Bans...");
                _banManager = new ModerationBanManager();
                _banManager.LoadBans(queryReactor);

                Progress(bar, wait, end, "Loading Roles...");
                _roleManager = new RoleManager();
                _roleManager.LoadRights(queryReactor);

                Progress(bar, wait, end, "Loading Items...");
                _itemManager = new ItemManager();
                _itemManager.LoadItems(queryReactor, out itemsLoaded);

                Progress(bar, wait, end, "Loading Catalog...");
                _catalog = new CatalogManager();

                Progress(bar, wait, end, "Loading Targeted Offers...");
                _targetedOfferManager = new TargetedOfferManager();

                Progress(bar, wait, end, "Loading Clothing...");
                _clothingManager = new ClothingManager();
                _clothingManager.Initialize(queryReactor);

                Progress(bar, wait, end, "Loading Rooms...");
                _roomManager = new RoomManager();
                _roomManager.LoadModels(queryReactor, out roomModelLoaded);

                Progress(bar, wait, end, "Loading NavigatorManager...");
                _navigatorManager = new HotelBrowserManager();
                _navigatorManager.Initialize(queryReactor, out navigatorLoaded);

                Progress(bar, wait, end, "Loading Groups...");
                _groupManager = new GroupManager();
                _groupManager.InitGroups();

                Progress(bar, wait, end, "Loading PixelManager...");
                _pixelManager = new ExchangeManager();

                Progress(bar, wait, end, "Loading HotelView...");
                _hotelView = new HotelLandingManager();

                Progress(bar, wait, end, "Loading Hall Of Fame...");
                _hallOfFame = new HallOfFame();

                Progress(bar, wait, end, "Loading ModerationTool...");
                _moderationTool = new ModerationTool();
                _moderationTool.LoadMessagePresets(queryReactor);
                _moderationTool.LoadPendingTickets(queryReactor);

                Progress(bar, wait, end, "Loading Bots...");
                _botManager = new BotManager();

                Progress(bar, wait, end, "Loading Events...");
                _events = new RoomEvents();

                Progress(bar, wait, end, "Loading Talents...");
                _talentManager = new TalentManager();
                _talentManager.Initialize(queryReactor);

                Progress(bar, wait, end, "Loading Pinata...");
                _pinataHandler = new PinataHandler();
                _pinataHandler.Initialize(queryReactor);

                Progress(bar, wait, end, "Loading Crackable Eggs...");
                _crackableEggHandler = new CrackableEggHandler();
                _crackableEggHandler.Initialize(queryReactor);

                Progress(bar, wait, end, "Loading Polls...");
                _pollManager = new PollManager();
                _pollManager.Init(queryReactor, out pollLoaded);

                Progress(bar, wait, end, "Loading Achievements...");
                _achievementManager = new AchievementManager(queryReactor, out achievementLoaded);

                Progress(bar, wait, end, "Loading Guides...");
                _guideManager = new GuideManager();

                Progress(bar, wait, end, "Wait for Continue Loading...");
            }
        }

        internal void Init()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                AbstractBar bar = new AnimatedBar();

                const int wait = 5;
                const int end = 5;

                int catalogPageLoaded;

                Progress(bar, wait, end, "Loading Offline Messages...");
                OfflineMessage.InitOfflineMessages(queryReactor);

                Progress(bar, wait, end, "Loading StaticMessages ...");
                StaticMessagesManager.Load();

                Progress(bar, wait, end, "Loading Commands...");
                CommandsManager.Register();

                Progress(bar, wait, end, "Loading Cache Manager...");
                CacheManager.StartProcess();

                Progress(bar, wait, end, "Loading Cross Domain Data...");
                CrossDomainSettings.Set();

                Progress(bar, wait, end, "Loading Public Rooms...");
                _navigatorManager.LoadNewPublicRooms();

                Progress(bar, wait, end, "Loading Pets Types...");
                PetTypeManager.Load();

                Progress(bar, wait, end, "Loading Catalog Manager...");
                _catalog.Init(out catalogPageLoaded);

                Progress(bar, wait, end, "Clearing Furni Data...");
                FurnitureDataManager.Clear();

                Progress(bar, wait, end, "Loading Chat Filter...");
                UserChatInputFilter.Load();

                Progress(bar, wait, end, "Loading Chat Security Module...");
                ServerSecurityChatFilter.Load();

                Progress(bar, wait, end, "Loading Server Black Words Service...");
                BlackWordsManager.Load();

                Progress(bar, wait, end, "Loading Sound Machine Module...");
                SoundMachineSongManager.Load();

                Progress(bar, wait, end, "Loading Server CPU Low Priority Worker...");
                ServerStatusUpdater.Load();

                Progress(bar, wait, end, "Loading Rooms Voting System...");
                _roomManager.InitVotedRooms();

                Progress(bar, wait, end, "Loading Rooms Competition System...");
                _roomManager.LoadCompetitionManager();

                Progress(bar, wait, end, "Starting Main Game Loop...");
                StartGameLoop();

                Progress(bar, wait, end, "Starting Exchange Service...");
                _pixelManager.StartTimer();

                Progress(bar, wait, end, "Wait...");

                Yupi.PacketDebugMode = _packetDebugMode;

                Console.Write("\r".PadLeft(Console.WindowWidth - Console.CursorLeft - 1));
            }
        }

        /// <summary>
        ///     Gets a value indicating whether [game loop enabled ext].
        /// </summary>
        /// <value><c>true</c> if [game loop enabled ext]; otherwise, <c>false</c>.</value>
        internal bool GameLoopEnabledExt => GameLoopEnabled;

        /// <summary>
        ///     Gets a value indicating whether [game loop active ext].
        /// </summary>
        /// <value><c>true</c> if [game loop active ext]; otherwise, <c>false</c>.</value>
        internal bool GameLoopActiveExt { get; private set; }

        /// <summary>
        ///     Gets the game loop sleep time ext.
        /// </summary>
        /// <value>The game loop sleep time ext.</value>
        internal int GameLoopSleepTimeExt => 25;

        /// <summary>
        ///     Progresses the specified bar.
        /// </summary>
        /// <param name="bar">The bar.</param>
        /// <param name="wait">The wait.</param>
        /// <param name="end">The end.</param>
        /// <param name="message">The message.</param>
        public static void Progress(AbstractBar bar, int wait, int end, string message)
        {
            bar.PrintMessage(message);

            for (int cont = 0; cont < end; cont++)
                bar.Step();
        }

        /// <summary>
        ///     Databases the cleanup.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        private static void DatabaseCleanup(IQueryAdapter dbClient)
        {
            dbClient.RunFastQuery("UPDATE users SET online = '0' WHERE online <> '0'");
            dbClient.RunFastQuery("UPDATE rooms_data SET users_now = 0 WHERE users_now <> 0");
            dbClient.RunFastQuery(
                "UPDATE `server_status` SET status = '1', users_online = '0', rooms_loaded = '0', server_ver = 'Yupi Emulator', stamp = '" +
                Yupi.GetUnixTimeStamp() + "' LIMIT 1;");
        }

        /// <summary>
        ///     Gets the client manager.
        /// </summary>
        /// <returns>GameClientManager.</returns>
        internal GameClientManager GetClientManager() => _clientManager;

        /// <summary>
        ///     Gets the ban manager.
        /// </summary>
        /// <returns>ModerationBanManager.</returns>
        internal ModerationBanManager GetBanManager() => _banManager;

        /// <summary>
        ///     Gets the role manager.
        /// </summary>
        /// <returns>RoleManager.</returns>
        internal RoleManager GetRoleManager() => _roleManager;

        /// <summary>
        ///     Gets the catalog.
        /// </summary>
        /// <returns>Catalog.</returns>
        internal CatalogManager GetCatalogManager() => _catalog;

        /// <summary>
        ///     Gets the room events.
        /// </summary>
        /// <returns>RoomEvents.</returns>
        internal RoomEvents GetRoomEvents() => _events;

        /// <summary>
        ///     Gets the guide manager.
        /// </summary>
        /// <returns>GuideManager.</returns>
        internal GuideManager GetGuideManager() => _guideManager;

        /// <summary>
        ///     Gets the navigator.
        /// </summary>
        /// <returns>NavigatorManager.</returns>
        internal HotelBrowserManager GetNavigator() => _navigatorManager;

        /// <summary>
        ///     Gets the item manager.
        /// </summary>
        /// <returns>ItemManager.</returns>
        internal ItemManager GetItemManager() => _itemManager;

        /// <summary>
        ///     Gets the room manager.
        /// </summary>
        /// <returns>RoomManager.</returns>
        internal RoomManager GetRoomManager() => _roomManager;

        /// <summary>
        ///     Gets the pixel manager.
        /// </summary>
        /// <returns>CoinsManager.</returns>
        internal ExchangeManager GetPixelManager() => _pixelManager;

        /// <summary>
        ///     Gets the hotel view.
        /// </summary>
        /// <returns>HotelView.</returns>
        internal HotelLandingManager GetHotelView() => _hotelView;

        internal HallOfFame GetHallOfFame() => _hallOfFame;

        internal TargetedOfferManager GetTargetedOfferManager() => _targetedOfferManager;

        /// <summary>
        ///     Gets the achievement manager.
        /// </summary>
        /// <returns>AchievementManager.</returns>
        internal AchievementManager GetAchievementManager() => _achievementManager;

        /// <summary>
        ///     Gets the moderation tool.
        /// </summary>
        /// <returns>ModerationTool.</returns>
        internal ModerationTool GetModerationTool() => _moderationTool;

        /// <summary>
        ///     Gets the bot manager.
        /// </summary>
        /// <returns>BotManager.</returns>
        internal BotManager GetBotManager() => _botManager;

        /// <summary>
        ///     Gets the group manager.
        /// </summary>
        /// <returns>GroupManager.</returns>
        internal GroupManager GetGroupManager() => _groupManager;

        /// <summary>
        ///     Gets the talent manager.
        /// </summary>
        /// <returns>TalentManager.</returns>
        internal TalentManager GetTalentManager() => _talentManager;

        /// <summary>
        ///     Gets the pinata handler.
        /// </summary>
        /// <returns>PinataHandler.</returns>
        internal PinataHandler GetPinataHandler() => _pinataHandler;

        internal CrackableEggHandler GetCrackableEggHandler() => _crackableEggHandler;

        /// <summary>
        ///     Gets the poll manager.
        /// </summary>
        /// <returns>PollManager.</returns>
        internal PollManager GetPollManager() => _pollManager;

        /// <summary>
        ///     Gets the clothing manager.
        /// </summary>
        /// <returns>ClothesManagerManager.</returns>
        internal ClothingManager GetClothingManager() => _clothingManager;

        /// <summary>
        ///     Starts the game loop.
        /// </summary>
        internal void StartGameLoop()
        {
            GameLoopActiveExt = true;

            _gameLoop = new Task(MainGameLoop);

            _gameLoop.Start();
        }

        /// <summary>
        ///     Stops the game loop.
        /// </summary>
        internal void StopGameLoop()
        {
            GameLoopActiveExt = false;

            while (!RoomManagerCycleEnded || !ClientManagerCycleEnded)
                Thread.Sleep(25);
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        internal void Destroy()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                DatabaseCleanup(queryReactor);

            GetClientManager();

            YupiWriterManager.WriteLine("Database YupiDatabaseManager destroyed", "Yupi.Game", ConsoleColor.DarkYellow);
        }

        /// <summary>
        ///     Reloaditemses this instance.
        /// </summary>
        internal void ReloadItems()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                GetItemManager().LoadItems(queryReactor);
            }
        }

        /// <summary>
        ///     Mains the game loop.
        /// </summary>
        private void MainGameLoop()
        {
            while (GameLoopActiveExt)
            {
                try
                {
                    ServerStatusUpdater.Process();

                    RoomManagerCycleEnded = false;
                    ClientManagerCycleEnded = false;

                    GetRoomManager().OnCycle();
                    GetClientManager().OnCycle();
                }
                catch (Exception ex)
                {
                    YupiLogManager.LogCriticalException(ex, "Registered HabboHotel Loop Exception.", "Yupi.User");
                }

                Thread.Sleep(GameLoopSleepTimeExt);
            }
        }
    }
}