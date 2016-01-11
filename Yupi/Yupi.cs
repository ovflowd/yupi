using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Timers;
using MySql.Data.MySqlClient;
using Yupi.Core.Encryption;
using Yupi.Core.Io;
using Yupi.Core.Security;
using Yupi.Core.Settings;
using Yupi.Core.Util.Math;
using Yupi.Data;
using Yupi.Data.Base;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Data.Base.Managers;
using Yupi.Data.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Groups.Structs;
using Yupi.Game.Pets;
using Yupi.Game.Users;
using Yupi.Game.Users.Data.Models;
using Yupi.Game.Users.Factories;
using Yupi.Game.Users.Messenger.Structs;
using Yupi.Messages;
using Yupi.Messages.Factorys;
using Yupi.Messages.Parsers;
using Yupi.Net.Connection;

namespace Yupi
{
    /// <summary>
    ///     Class Yupi.
    /// </summary>
    public static class Yupi
    {
        /// <summary>
        ///     Yupi Environment: Main Thread of Yupi Emulator, SetUp's the Emulator
        ///     Contains Initialize: Responsible of the Emulator Loadings
        /// </summary>
        internal static string ServerLanguage = "english";

        /// <summary>
        ///     The build of the server
        /// </summary>
        internal static readonly string Build = "200", Version = "1.0";

        /// <summary>
        ///     The live currency type
        /// </summary>
        internal static int LiveCurrencyType = 105, ConsoleTimer = 2000;

        /// <summary>
        ///     The is live
        /// </summary>
        internal static bool IsLive,
            SeparatedTasksInGameClientManager,
            SeparatedTasksInMainLoops,
            PacketDebugMode,
            ConsoleTimerOn;

        /// <summary>
        ///     The staff alert minimum rank
        /// </summary>
        internal static uint StaffAlertMinRank = 4, FriendRequestLimit = 1000;

        /// <summary>
        ///     Bobba Filter Muted Users by Filter
        /// </summary>
        internal static Dictionary<uint, uint> MutedUsersByFilter;

        /// <summary>
        ///     The manager
        /// </summary>
        internal static DatabaseManager Manager;

        /// <summary>
        ///     The configuration data
        /// </summary>
        internal static ServerDatabaseSettings ConfigData;

        /// <summary>
        ///     The server started
        /// </summary>
        internal static DateTime ServerStarted;

        /// <summary>
        ///     The offline messages
        /// </summary>
        internal static Dictionary<uint, List<OfflineMessage>> OfflineMessages;

        /// <summary>
        ///     The timer
        /// </summary>
        internal static Timer Timer;

        /// <summary>
        ///     The culture information
        /// </summary>
        internal static CultureInfo CultureInfo;

        /// <summary>
        ///     The _plugins
        /// </summary>
        public static Dictionary<string, IPlugin> Plugins;

        /// <summary>
        ///     The users cached
        /// </summary>
        public static readonly ConcurrentDictionary<uint, Habbo> UsersCached = new ConcurrentDictionary<uint, Habbo>();

        /// <summary>
        ///     The _connection manager
        /// </summary>
        private static ConnectionHandler _connectionManager;

        /// <summary>
        ///     The _default encoding
        /// </summary>
        private static Encoding _defaultEncoding;

        /// <summary>
        ///     The _game
        /// </summary>
        private static Game.Game _game;

        /// <summary>
        ///     The _languages
        /// </summary>
        private static ServerLanguageSettings _languages;

        /// <summary>
        ///     The allowed special chars
        /// </summary>
        private static readonly HashSet<char> AllowedSpecialChars = new HashSet<char>(new[]
        {
            '-', '.', ' ', 'Ã', '©', '¡', '­', 'º', '³', 'Ã', '‰', '_'
        });

        internal static string YupiVariablesDirectory = string.Empty;

        internal static string YupiRootDirectory = string.Empty;

        /// <summary>
        ///     Check's if the Shutdown Has Started
        /// </summary>
        /// <value><c>true</c> if [shutdown started]; otherwise, <c>false</c>.</value>
        internal static bool ShutdownStarted { get; set; }

        public static bool ContainsAny(this string haystack, params string[] needles) => needles.Any(haystack.Contains);

        /// <summary>
        ///     Start the Plugin System
        /// </summary>
        /// <returns>ICollection&lt;IPlugin&gt;.</returns>
        public static ICollection<IPlugin> LoadPlugins()
        {
            string path = Path.Combine(YupiVariablesDirectory, "Plugins");

            if (!Directory.Exists(path))
                return null;

            string[] files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

            if (files.Length == 0)
                return null;

            List<Assembly> assemblies =
                files.Select(AssemblyName.GetAssemblyName)
                    .Select(Assembly.Load)
                    .Where(assembly => assembly != null)
                    .ToList();

            Type pluginType = typeof (IPlugin);

            List<Type> pluginTypes = new List<Type>();

            foreach (Type[] types in from assembly in assemblies where assembly != null select assembly.GetTypes())
                pluginTypes.AddRange(
                    types.Where(type => type != null && !type.IsInterface && !type.IsAbstract)
                        .Where(type => type.GetInterface(pluginType.FullName) != null));

            List<IPlugin> plugins = new List<IPlugin>(pluginTypes.Count);

            plugins.AddRange(
                pluginTypes.Select(type => (IPlugin) Activator.CreateInstance(type)).Where(plugin => plugin != null));

            return plugins;
        }

        /// <summary>
        ///     Get's Habbo By The User Id
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Habbo.</returns>
        /// Table: users.id
        internal static Habbo GetHabboById(uint userId)
        {
            GameClient clientByUserId = GetGame().GetClientManager().GetClientByUserId(userId);

            if (clientByUserId != null)
            {
                Habbo habbo = clientByUserId.GetHabbo();

                if (habbo != null && habbo.Id > 0)
                {
                    UsersCached.AddOrUpdate(userId, habbo, (key, value) => habbo);

                    return habbo;
                }
            }
            else
            {
                if (UsersCached.ContainsKey(userId))
                    return UsersCached[userId];

                UserData userData = UserDataFactory.GetUserData((int) userId);

                if (UsersCached.ContainsKey(userId))
                    return UsersCached[userId];

                if (userData?.User == null)
                    return null;

                UsersCached.TryAdd(userId, userData.User);
                userData.User.InitInformation(userData);

                return userData.User;
            }

            return null;
        }

        /// <summary>
        ///     Console Clear Thread
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs" /> instance containing the event data.</param>
        internal static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.Clear();
            Console.WriteLine();

            Writer.WriteLine($"Console Cleared in: {DateTime.Now} Next Time on: {ConsoleTimer} Seconds ", "Yupi.Boot",
                ConsoleColor.DarkGreen);

            Console.WriteLine();
            GC.Collect();

            Timer.Start();
        }

        /// <summary>
        ///     Main Void, Initializes the Emulator.
        /// </summary>
        internal static void Initialize()
        {
            Console.Title = "Yupi Emulator | Starting [...]";

            ServerStarted = DateTime.Now;
            _defaultEncoding = Encoding.Default;
            MutedUsersByFilter = new Dictionary<uint, uint>();

            ChatEmotions.Initialize();

            CultureInfo = CultureInfo.CreateSpecificCulture("en-GB");

            YupiRootDirectory = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName;

            YupiVariablesDirectory = Path.Combine(YupiRootDirectory, "Variables");

            try
            {
                ServerConfigurationSettings.Load(Path.Combine(YupiVariablesDirectory, "Settings/main.ini"));
                ServerConfigurationSettings.Load(Path.Combine(YupiVariablesDirectory, "Settings/Welcome/settings.ini"),
                    true);

                MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
                {
                    Server = ServerConfigurationSettings.Data["db.hostname"],
                    Port = uint.Parse(ServerConfigurationSettings.Data["db.port"]),
                    UserID = ServerConfigurationSettings.Data["db.username"],
                    Password = ServerConfigurationSettings.Data["db.password"],
                    Database = ServerConfigurationSettings.Data["db.name"],
                    MinimumPoolSize = uint.Parse(ServerConfigurationSettings.Data["db.pool.minsize"]),
                    MaximumPoolSize = uint.Parse(ServerConfigurationSettings.Data["db.pool.maxsize"]),
                    Pooling = true,
                    AllowZeroDateTime = true,
                    ConvertZeroDateTime = true,
                    DefaultCommandTimeout = 300u,
                    ConnectionTimeout = 10u
                };

                Manager = new DatabaseManager(mySqlConnectionStringBuilder.ToString());

                using (IQueryAdapter commitableQueryReactor = GetDatabaseManager().GetQueryReactor())
                {
                    ConfigData = new ServerDatabaseSettings(commitableQueryReactor);
                    PetCommandHandler.Init(commitableQueryReactor);
                    PetLocale.Init(commitableQueryReactor);
                    OfflineMessages = new Dictionary<uint, List<OfflineMessage>>();
                    OfflineMessage.InitOfflineMessages(commitableQueryReactor);
                }

                ConsoleTimer = int.Parse(ServerConfigurationSettings.Data["console.clear.time"]);
                ConsoleTimerOn = bool.Parse(ServerConfigurationSettings.Data["console.clear.enabled"]);
                FriendRequestLimit = (uint) int.Parse(ServerConfigurationSettings.Data["client.maxrequests"]);

                LibraryParser.Incoming = new Dictionary<int, LibraryParser.StaticRequestHandler>();
                LibraryParser.Library = new Dictionary<string, string>();
                LibraryParser.Outgoing = new Dictionary<string, int>();
                LibraryParser.Config = new Dictionary<string, string>();

                if (ServerConfigurationSettings.Data.ContainsKey("client.build"))
                    LibraryParser.ReleaseName = ServerConfigurationSettings.Data["client.build"];
                else
                    throw new Exception("Unable to Continue if No Release is configured to the Emulator Handle.");

                LibraryParser.RegisterLibrary();
                LibraryParser.RegisterOutgoing();
                LibraryParser.RegisterIncoming();
                LibraryParser.RegisterConfig();

                Plugins = new Dictionary<string, IPlugin>();

                ICollection<IPlugin> plugins = LoadPlugins();

                if (plugins != null)
                {
                    foreach (IPlugin item in plugins.Where(item => item != null))
                    {
                        Plugins.Add(item.PluginName, item);

                        Writer.WriteLine("Loaded Plugin: " + item.PluginName + " Version: " + item.PluginVersion,
                            "Yupi.Plugins", ConsoleColor.DarkBlue);
                    }
                }

                ServerExtraSettings.RunExtraSettings();
                FurnitureDataManager.SetCache();
                CrossDomainSettings.Set();

                _game = new Game.Game(int.Parse(ServerConfigurationSettings.Data["game.tcp.conlimit"]));

                _game.GetNavigator().LoadNewPublicRooms();
                _game.ContinueLoading();

                FurnitureDataManager.Clear();

                if (ServerConfigurationSettings.Data.ContainsKey("server.lang"))
                    ServerLanguage = Convert.ToString(ServerConfigurationSettings.Data["server.lang"]);

                _languages = new ServerLanguageSettings(ServerLanguage);

                Writer.WriteLine("Loaded " + _languages.Count() + " Languages Vars", "Yupi.Interpreters");

                if (plugins != null)
                    foreach (IPlugin itemTwo in plugins)
                        itemTwo?.message_void();

                if (ConsoleTimerOn)
                    Writer.WriteLine("Console Clear Timer is Enabled, with " + ConsoleTimer + " Seconds.", "Yupi.Boot");

                ClientMessageFactory.Init();

                Writer.WriteLine(
                    "Game server started at port " + int.Parse(ServerConfigurationSettings.Data["game.tcp.port"]),
                    "Server.Game");

                _connectionManager = new ConnectionHandler(int.Parse(ServerConfigurationSettings.Data["game.tcp.port"]),
                    int.Parse(ServerConfigurationSettings.Data["game.tcp.conlimit"]),
                    int.Parse(ServerConfigurationSettings.Data["game.tcp.conperip"]),
                    ServerConfigurationSettings.Data["game.tcp.antiddos"].ToLower() == "true",
                    ServerConfigurationSettings.Data["game.tcp.enablenagles"].ToLower() == "true");

                if (LibraryParser.Config["Crypto.Enabled"] == "true")
                {
                    Handler.Initialize(LibraryParser.Config["Crypto.RSA.N"], LibraryParser.Config["Crypto.RSA.D"],
                        LibraryParser.Config["Crypto.RSA.E"]);

                    Writer.WriteLine("Started RSA crypto service", "Yupi.Crypto");
                }
                else
                    Writer.WriteLine("The encryption system is disabled.", "Yupi.Crypto", ConsoleColor.DarkYellow);

                LibraryParser.Initialize();

                if (ConsoleTimerOn)
                {
                    Timer = new Timer {Interval = ConsoleTimer};
                    Timer.Elapsed += TimerElapsed;
                    Timer.Start();
                }

                if (ServerConfigurationSettings.Data.ContainsKey("StaffAlert.MinLevel"))
                    StaffAlertMinRank = uint.Parse(ServerConfigurationSettings.Data["StaffAlert.MinLevel"]);

                if (ServerConfigurationSettings.Data.ContainsKey("game.multithread.enabled"))
                    SeparatedTasksInMainLoops = ServerConfigurationSettings.Data["game.multithread.enabled"] == "true";

                if (ServerConfigurationSettings.Data.ContainsKey("client.multithread.enabled"))
                    SeparatedTasksInGameClientManager =
                        ServerConfigurationSettings.Data["client.multithread.enabled"] == "true";

                if (ServerConfigurationSettings.Data.ContainsKey("debug.packet"))
                    if (ServerConfigurationSettings.Data["debug.packet"] == "true")
                        PacketDebugMode = true;

                Writer.WriteLine("Yupi Emulator ready. Status: idle", "Yupi.Boot");

                IsLive = true;
            }
            catch (Exception e)
            {
                Writer.WriteLine("Error When Starting Yupi Environment!" + Environment.NewLine + e.Message, "Yupi.Boot",
                    ConsoleColor.Red);
                Writer.WriteLine("Please press Y to get more details or press other Key to Exit", "Yupi.Boot",
                    ConsoleColor.Red);
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    Writer.WriteLine(
                        Environment.NewLine + "[Message] Error Details: " + Environment.NewLine + e.StackTrace +
                        Environment.NewLine + e.InnerException + Environment.NewLine + e.TargetSite +
                        Environment.NewLine + "[Message] Press Any Key To Exit", "Yupi.Boot", ConsoleColor.Red);
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else
                    Environment.Exit(1);
            }
        }

        /// <summary>
        ///     Convert's Enum to Boolean
        /// </summary>
        /// <param name="theEnum">The theEnum.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal static bool EnumToBool(string theEnum) => theEnum == "1";

        /// <summary>
        ///     Convert's Boolean to Integer
        /// </summary>
        /// <param name="theBool">if set to <c>true</c> [theBool].</param>
        /// <returns>System.Int32.</returns>
        internal static int BoolToInteger(bool theBool) => theBool ? 1 : 0;

        /// <summary>
        ///     Convert's Boolean to Enum
        /// </summary>
        /// <param name="theBool">if set to <c>true</c> [theBool].</param>
        /// <returns>System.String.</returns>
        internal static string BoolToEnum(bool theBool) => theBool ? "1" : "0";

        /// <summary>
        ///     Generates a Random Number in the Interval Min,Max
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>System.Int32.</returns>
        internal static int GetRandomNumber(int min, int max) => RandomNumberGenerator.Get(min, max);

        /// <summary>
        ///     Get's the Actual Timestamp in Unix Format
        /// </summary>
        /// <returns>System.Int32.</returns>
        internal static int GetUnixTimeStamp()
            => (int) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

        /// <summary>
        ///     Convert's a Unix TimeStamp to DateTime
        /// </summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns>DateTime.</returns>
        internal static DateTime UnixToDateTime(double unixTimeStamp)
            => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local).AddSeconds(unixTimeStamp).ToLocalTime();

        internal static DateTime UnixToDateTime(int unixTimeStamp)
            => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local).AddSeconds(unixTimeStamp).ToLocalTime();

        /// <summary>
        ///     Convert timestamp to GroupJoin String
        /// </summary>
        /// <param name="timeStamp">The target.</param>
        /// <returns>System.String.</returns>
        public static string GetGroupDateJoinString(long timeStamp)
        {
            string[] time = UnixToDateTime(timeStamp).ToString("MMMM/dd/yyyy", CultureInfo).Split('/');

            return $"{time[0].Substring(0, 3)} {time[1]}, {time[2]}";
        }

        /// <summary>
        ///     Convert's a DateTime to Unix TimeStamp
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>System.Int32.</returns>
        internal static int DateTimeToUnix(DateTime target)
            => Convert.ToInt32((target - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);

        /// <summary>
        ///     Get the Actual Time
        /// </summary>
        /// <returns>System.Int64.</returns>
        internal static long Now() => (long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;

        internal static int DifferenceInMilliSeconds(DateTime time, DateTime tFrom)
        {
            double time1 = tFrom.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            double time2 = time.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            if ((time1 >= double.MaxValue) || (time1 <= double.MinValue) || time1 <= 0.0)
                time1 = 0.0;

            if ((time2 >= double.MaxValue) || (time2 <= double.MinValue) || time2 <= 0.0)
                time2 = 0.0;

            return Convert.ToInt32(time1 - time2);
        }

        /// <summary>
        ///     Filter's the Habbo Avatars AvatarFigureParts
        /// </summary>
        /// <param name="figure">The figure.</param>
        /// <returns>System.String.</returns>
        internal static string FilterFigure(string figure)
            =>
                figure.Any(character => !IsValid(character))
                    ? "lg-3023-1335.hr-828-45.sh-295-1332.hd-180-4.ea-3168-89.ca-1813-62.ch-235-1332"
                    : figure;

        /// <summary>
        ///     Check if is a Valid AlphaNumeric String
        /// </summary>
        /// <param name="inputStr">The input string.</param>
        /// <returns><c>true</c> if [is valid alpha numeric] [the specified input string]; otherwise, <c>false</c>.</returns>
        internal static bool IsValidAlphaNumeric(string inputStr)
            => !string.IsNullOrEmpty(inputStr.ToLower()) && inputStr.ToLower().All(IsValid);

        /// <summary>
        ///     Get a Habbo With the Habbo's Username
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Habbo.</returns>
        /// Table: users.username
        internal static Habbo GetHabboForName(string userName)
        {
            try
            {
                using (IQueryAdapter commitableQueryReactor = GetDatabaseManager().GetQueryReactor())
                {
                    commitableQueryReactor.SetQuery("SELECT id FROM users WHERE username = @user");

                    commitableQueryReactor.AddParameter("user", userName);

                    int integer = commitableQueryReactor.GetInteger();

                    if (integer > 0)
                    {
                        Habbo result = GetHabboById((uint) integer);

                        return result;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return null;
        }

        /// <summary>
        ///     Check if the Input String is a Integer
        /// </summary>
        /// <param name="theNum">The theNum.</param>
        /// <returns><c>true</c> if the specified theNum is number; otherwise, <c>false</c>.</returns>
        internal static bool IsNum(string theNum)
        {
            double num;
            return double.TryParse(theNum, out num);
        }

        /// <summary>
        ///     Get the Database Configuration Data
        /// </summary>
        /// <returns>ConfigData.</returns>
        internal static ServerDatabaseSettings GetDbConfig() => ConfigData;

        /// <summary>
        ///     Get's the Default Emulator Encoding
        /// </summary>
        /// <returns>Encoding.</returns>
        internal static Encoding GetDefaultEncoding() => _defaultEncoding;

        /// <summary>
        ///     Get's the Game Connection Manager Handler
        /// </summary>
        /// <returns>ConnectionHandling.</returns>
        internal static ConnectionHandler GetConnectionManager() => _connectionManager;

        /// <summary>
        ///     Get's the Game Environment Handler
        /// </summary>
        /// <returns>Game.</returns>
        internal static Game.Game GetGame() => _game;

        /// <summary>
        ///     Gets the language.
        /// </summary>
        /// <returns>Languages.</returns>
        internal static ServerLanguageSettings GetLanguage() => _languages;

        /// <summary>
        ///     Filter's SQL Injection Characters
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        internal static string FilterInjectionChars(string input)
        {
            input = input.Replace('\u0001', ' ');
            input = input.Replace('\u0002', ' ');
            input = input.Replace('\u0003', ' ');
            input = input.Replace('\t', ' ');

            return input;
        }

        /// <summary>
        ///     Get's the Database Manager Handler
        /// </summary>
        /// <returns>ConnectionManager.</returns>
        internal static DatabaseManager GetDatabaseManager() => Manager;

        /// <summary>
        ///     Perform's the Emulator Shutdown
        /// </summary>
        internal static void PerformShutDown()
        {
            PerformShutDown(false);
        }

        /// <summary>
        ///     Performs the restart.
        /// </summary>
        internal static void PerformRestart()
        {
            PerformShutDown(true);
        }

        /// <summary>
        ///     Shutdown the Emulator
        /// </summary>
        /// <param name="restart">if set to <c>true</c> [restart].</param>
        /// Set a Different Message in Hotel
        internal static void PerformShutDown(bool restart)
        {
            DateTime now = DateTime.Now;

            CacheManager.StopProcess();

            ShutdownStarted = true;

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            serverMessage.AppendString("disconnection");
            serverMessage.AppendInteger(2);
            serverMessage.AppendString("title");
            serverMessage.AppendString("HEY EVERYONE!");
            serverMessage.AppendString("message");
            serverMessage.AppendString(
                restart
                    ? "<b>The hotel is shutting down for a break.<)/b>\nYou may come back later.\r\n<b>So long!</b>"
                    : "<b>The hotel is shutting down for a break.</b><br />You may come back soon. Don't worry, everything's going to be saved..<br /><b>So long!</b>\r\n~ This session was powered by Yupi");
            GetGame().GetClientManager().QueueBroadcaseMessage(serverMessage);
            Console.Title = "Yupi Emulator | Shutting down...";

            _game.StopGameLoop();
            _game.GetRoomManager().RemoveAllRooms();
            GetGame().GetClientManager().CloseAll();

            GetConnectionManager().Destroy();

            foreach (Group group in _game.GetGroupManager().Groups.Values) group.UpdateForum();

            using (IQueryAdapter commitableQueryReactor = Manager.GetQueryReactor())
            {
                commitableQueryReactor.RunFastQuery("UPDATE users SET online = '0'");
                commitableQueryReactor.RunFastQuery("UPDATE rooms_data SET users_now = 0");
                commitableQueryReactor.RunFastQuery("TRUNCATE TABLE users_rooms_visits");
            }

            _connectionManager.Destroy();
            _game.Destroy();


            Writer.WriteLine("Game Manager destroyed", "Yupi.Game", ConsoleColor.DarkYellow);

            TimeSpan span = DateTime.Now - now;

            Writer.WriteLine("Elapsed " + TimeSpanToString(span) + "ms on Shutdown Proccess", "Yupi.Life",
                ConsoleColor.DarkYellow);

            if (!restart)
                Writer.WriteLine("Shutdown Completed. Press Any Key to Continue...", string.Empty, ConsoleColor.DarkRed);

            if (!restart)
                Console.ReadKey();

            IsLive = false;

            if (restart)
                Process.Start(Assembly.GetEntryAssembly().Location);

            Console.WriteLine("Closing...");
            Environment.Exit(0);
        }

        /// <summary>
        ///     Convert's a Unix TimeSpan to A String
        /// </summary>
        /// <param name="span">The span.</param>
        /// <returns>System.String.</returns>
        internal static string TimeSpanToString(TimeSpan span)
            => string.Concat(span.Seconds, " s, ", span.Milliseconds, " ms");

        /// <summary>
        ///     Check's if Input Data is a Valid AlphaNumeric Character
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns><c>true</c> if the specified c is valid; otherwise, <c>false</c>.</returns>
        private static bool IsValid(char c) => char.IsLetterOrDigit(c) || AllowedSpecialChars.Contains(c);
    }
}