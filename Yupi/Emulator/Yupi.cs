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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Timers;
using log4net;
using MySql.Data.MySqlClient;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Settings;
using Yupi.Emulator.Core.Util.Math;
using Yupi.Emulator.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Data.Base.Managers;
using Yupi.Emulator.Data.Base.Managers.Interfaces;
using Yupi.Emulator.Data.Interfaces;
using Yupi.Emulator.Game;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Game.Users.Data.Models;
using Yupi.Emulator.Game.Users.Factories;
using Yupi.Emulator.Game.Users.Messenger.Structs;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Factorys;
using Yupi.Net;
using Yupi.Net.SuperSocketImpl;

namespace Yupi.Emulator
{
    /// <summary>
    ///     Class Yupi.
    /// </summary>
    public static class Yupi
    {
        /// <summary>
        ///     Server Language
        /// </summary>
        public static string ServerLanguage = "english";

        /// <summary>
        ///     The build of the server
        /// </summary>
        public static readonly string ServerBuild = "300";

        /// <summary>
        ///    Server Version
        /// </summary>
        public static readonly string ServerVersion = "1.0";

        /// <summary>
        ///     The live currency type
        /// </summary>
        public static int LiveCurrencyType = 105;

        /// <summary>
        ///     Console Clear Interval
        /// </summary>
        public static int ConsoleCleanTimeInterval = 2000;

        /// <summary>
        ///     Server is Started
        /// </summary>
        public static bool IsLive;

        /// <summary>
        ///     Server is Ready
        /// </summary>
        public static bool IsReady;

        /// <summary>
        ///     Multi Thread in Client
        /// </summary>
        public static bool SeparatedTasksInGameClientManager;

        /// <summary>
        ///     Multi Thread in Game
        /// </summary>
        public static bool SeparatedTasksInMainLoops;

        /// <summary>
        ///     Debug Packets
        /// </summary>
        public static bool PacketDebugMode;

        /// <summary>
        ///     Github Update String URI
        /// </summary>
        public static readonly string GithubUpdateFile = "https://raw.githubusercontent.com/sant0ro/Yupi/nio/UPDATES.json";

        /// <summary>
        ///     Github Update String URI
        /// </summary>
        public static readonly string GithubCommitApi = "https://api.github.com/repos/sant0ro/Yupi/commits/HEAD";

        /// <summary>
        ///     Console Clean Timer
        /// </summary>
        public static bool ConsoleTimerOn;

        /// <summary>
        ///     The staff alert minimum rank
        /// </summary>
        public static uint StaffAlertMinRank = 4;
		// TODO Should these variables be readonly???
        /// <summary>
        ///     Max Friends Requests
        /// </summary>
        public static uint FriendRequestLimit = 1000;

        /// <summary>
        ///     Bobba Filter Muted Users by Filter
        /// </summary>
        public static Dictionary<uint, uint> MutedUsersByFilter;

        /// <summary>
        ///     The manager
        /// </summary>
        public static IDatabaseManager YupiDatabaseManager;

        /// <summary>
        ///     The configuration data
        /// </summary>
        public static ServerDatabaseSettings DatabaseSettings;

        /// <summary>
        ///     The server started
        /// </summary>
        public static DateTime YupiServerStartDateTime;

        /// <summary>
        ///     The offline messages
        /// </summary>
        public static Dictionary<uint, List<OfflineMessage>> OfflineMessages;

        /// <summary>
        ///     The timer
        /// </summary>
        public static Timer ConsoleRefreshTimer;

        /// <summary>
        ///     The culture information
        /// </summary>
        public static CultureInfo CultureInfo;

        /// <summary>
        ///     The _plugins
        /// </summary>
        public static Dictionary<string, IPlugin> Plugins;

        /// <summary>
        ///     The users cached
        /// </summary>
        public static readonly ConcurrentDictionary<uint, Habbo> UsersCached = new ConcurrentDictionary<uint, Habbo>();

        /// <summary>
        ///     The Server Default Encoding
        /// </summary>
        public static Encoding YupiServerTextEncoding;

        /// <summary>
        ///     The GameServer
        /// </summary>
        public static HabboHotel GameServer;

		private static IServer TCPServer;

        /// <summary>
        ///     The ServerLanguageVariables
        /// </summary>
        public static ServerLanguageSettings ServerLanguageVariables;

        /// <summary>
        ///     The allowed special chars
        /// </summary>
        private static readonly HashSet<char> AllowedSpecialChars = new HashSet<char>(new[]
        {
            '-', '.', ' ', 'Ã', '©', '¡', '­', 'º', '³', 'Ã', '‰', '_'
        });

        /// <summary>
        ///    Yupi Variable Directory
        /// </summary>
        public static string YupiVariablesDirectory = string.Empty;

        /// <summary>
        ///     Yupi Root Directory
        /// </summary>
        public static string YupiRootDirectory = string.Empty;

        /// <summary>
        ///     Max Recommended MySQL Connection Amount
        /// </summary>
        public static uint MaxRecommendedMySqlConnections = 50;

        /// <summary>
        ///     Check's if the Shutdown Has Started
        /// </summary>
        /// <value><c>true</c> if [shutdown started]; otherwise, <c>false</c>.</value>
        public static bool ShutdownStarted { get; set; }

        /// <summary>
        ///    Contains Any
        /// </summary>
        public static bool ContainsAny(this string haystack, params string[] needles) => needles.Any(haystack.Contains);

        /// <summary>
        ///     Get Log Manager
        /// </summary>
        /// <returns>ILog</returns>
        public static ILog GetLogManager() => YupiLogManager.GetLogManager();

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

            List<Assembly> assemblies = files.Select(AssemblyName.GetAssemblyName).Select(Assembly.Load).Where(assembly => assembly != null).ToList();

            Type pluginType = typeof (IPlugin);

            List<Type> pluginTypes = new List<Type>();

            foreach (Type[] types in from assembly in assemblies where assembly != null select assembly.GetTypes())
                pluginTypes.AddRange(types.Where(type => type != null && !type.IsInterface && !type.IsAbstract).Where(type => type.GetInterface(pluginType.FullName) != null));

            List<IPlugin> plugins = new List<IPlugin>(pluginTypes.Count);

            plugins.AddRange(pluginTypes.Select(type => (IPlugin) Activator.CreateInstance(type)).Where(plugin => plugin != null));

            return plugins;
        }

        /// <summary>
        ///     Reload Plugins Data
        /// </summary>
        /// <returns>ILog</returns>
        public static void ReloadPlugins()
        {
            Plugins = new Dictionary<string, IPlugin>();

            ICollection<IPlugin> plugins = LoadPlugins();

            if (plugins != null)
            {
                foreach (IPlugin item in plugins.Where(item => item != null))
                {
                    Plugins.Add(item.PluginName, item);

                    YupiWriterManager.WriteLine("Loaded Plugin: " + item.PluginName + " ServerVersion: " + item.PluginVersion, "Yupi.Adon", ConsoleColor.DarkBlue);
                }
            }

            if (plugins != null)
            {
                foreach (IPlugin plugin in plugins)
                    plugin?.message_void();
            }
        }

        /// <summary>
        ///     Get's Habbo By The User Id
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Habbo.</returns>
        /// Table: users.id
        public static Habbo GetHabboById(uint userId)
        {
            if (userId == 0)
                return null;

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

                if (userData == null)
                    return null;

                if (UsersCached.ContainsKey(userId))
                    return UsersCached[userId];

                if (userData.User == null)
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
        public static void ConsoleRefreshTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.Clear();
            Console.WriteLine();

            YupiWriterManager.WriteLine($"Console Cleared in: {DateTime.Now} Next Time on: {ConsoleCleanTimeInterval} Seconds ", "Yupi.Boot", ConsoleColor.DarkGreen);

            Console.WriteLine();
            GC.Collect();

            ConsoleRefreshTimer.Start();
        }

        /// <summary>
        ///     Main Void, Initializes the Emulator.
        /// </summary>
        public static void Initialize()
        {
            Console.Title = "Yupi Emulator | Starting [...]";

            YupiServerStartDateTime = DateTime.Now;

            YupiServerTextEncoding = Encoding.Default;

            CultureInfo = CultureInfo.CreateSpecificCulture("en-GB");

            MutedUsersByFilter = new Dictionary<uint, uint>();

            OfflineMessages = new Dictionary<uint, List<OfflineMessage>>();

            YupiRootDirectory = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName;

            YupiVariablesDirectory = Path.Combine(YupiRootDirectory, "Variables");

			#if !DEBUG
            try
            {
			#endif
                ServerConfigurationSettings.Load(Path.Combine(YupiVariablesDirectory, "Settings" ,"main.ini"));

                ServerConfigurationSettings.Load(Path.Combine(YupiVariablesDirectory, "Settings", "Welcome", "settings.ini"), true);

                if (ServerConfigurationSettings.Data.ContainsKey("console.clear.time"))
                    ConsoleCleanTimeInterval = int.Parse(ServerConfigurationSettings.Data["console.clear.time"]);

                if (ServerConfigurationSettings.Data.ContainsKey("console.clear.enabled"))
                    ConsoleTimerOn = bool.Parse(ServerConfigurationSettings.Data["console.clear.enabled"]);

                if (ServerConfigurationSettings.Data.ContainsKey("client.maxfriend.requests"))
                    FriendRequestLimit = uint.Parse(ServerConfigurationSettings.Data["client.maxfriend.requests"]);

                if (ServerConfigurationSettings.Data.ContainsKey("server.lang"))
                    ServerLanguage = ServerConfigurationSettings.Data["server.lang"];

                if (ServerConfigurationSettings.Data.ContainsKey("game.multithread.enabled"))
                    SeparatedTasksInMainLoops = ServerConfigurationSettings.Data["game.multithread.enabled"] == "true";

                if (ServerConfigurationSettings.Data.ContainsKey("client.multithread.enabled"))
                    SeparatedTasksInGameClientManager = ServerConfigurationSettings.Data["client.multithread.enabled"] == "true";

                if (ServerConfigurationSettings.Data.ContainsKey("debug.packet"))
                    PacketDebugMode = ServerConfigurationSettings.Data["debug.packet"] == "true";

                YupiDatabaseManager = new BasicDatabaseManager(new MySqlConnectionStringBuilder
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
                    DefaultCommandTimeout = 300,
                    ConnectionTimeout = 10
                });

                ServerExtraSettings.RunExtraSettings();

                YupiLogManager.Init(MethodBase.GetCurrentMethod().DeclaringType);
                
                using (IQueryAdapter queryReactor = GetDatabaseManager().GetQueryReactor())
                    DatabaseSettings = new ServerDatabaseSettings(queryReactor);

                GameServer = new HabboHotel();    
			// TODO Refactor (+rename)
                GameServer.Init();

                if (uint.Parse(ServerConfigurationSettings.Data["db.pool.maxsize"]) > MaxRecommendedMySqlConnections)
                    YupiLogManager.LogWarning($"MySQL Max Pool Size is High: {ServerConfigurationSettings.Data["db.pool.maxsize"]}, Recommended Value: {MaxRecommendedMySqlConnections}.", "Yupi.Data", false);

                ServerLanguageVariables = new ServerLanguageSettings(ServerLanguage);

                YupiWriterManager.WriteLine($"Loaded {ServerLanguageVariables.Count()} Languages Vars", "Yupi.Boot");
                YupiWriterManager.WriteLine($"Loaded {PacketLibraryManager.ReleasesCount} Habbo Releases", "Yupi.Data");
                YupiWriterManager.WriteLine($"Loaded {PacketLibraryManager.Incoming.Count} Event Controllers", "Yupi.Data");

                ReloadPlugins();

                if (ConsoleTimerOn)
                    YupiWriterManager.WriteLine("Console Automatic Clear is Enabled, with " + ConsoleCleanTimeInterval + " Seconds.", "Yupi.Boot");

			// TODO Cleanup
             
              //  ConnectionManager.DataParser = new ServerPacketParser();
			int port = int.Parse(ServerConfigurationSettings.Data["game.tcp.port"]);
			TCPServer = ServerFactory.CreateServer(port);

			TCPServer.OnConnectionOpened += GetGame().GetClientManager().AddClient; // TODO Connection security!
			TCPServer.OnConnectionClosed += GetGame().GetClientManager().RemoveClient;
			TCPServer.OnMessageReceived += (ISession session, byte[] body) => {
				//using(global::Yupi.Emulator.Messages.Buffers.SimpleClientMessageBuffer message = ClientMessageFactory.GetClientMessage()) {
				// TODO When using message pool the SimpleClientMessageBuffer becomes invalid (after several messages) -> DEBUG
				global::Yupi.Emulator.Messages.Buffers.SimpleClientMessageBuffer message = new global::Yupi.Emulator.Messages.Buffers.SimpleClientMessageBuffer();
				    message.Setup(body);
					GetGame().GetClientManager().GetClient(session).GetMessageHandler().HandleRequest(message);
				//}
			};

			TCPServer.Start();

                YupiWriterManager.WriteLine("Server Started at Port " + ServerConfigurationSettings.Data["game.tcp.port"] + " and Address " + ServerConfigurationSettings.Data["game.tcp.bindip"], "Yupi.Boot", ConsoleColor.DarkCyan);

                if (ConsoleTimerOn)
                {
                    ConsoleRefreshTimer = new Timer {Interval = ConsoleCleanTimeInterval};
                    ConsoleRefreshTimer.Elapsed += ConsoleRefreshTimerElapsed;
                    ConsoleRefreshTimer.Start();
                }

                YupiWriterManager.WriteLine("The Encryption System is Removed from Yupi.", "Yupi.Info", ConsoleColor.DarkYellow);

                YupiWriterManager.WriteLine("Yupi Emulator Ready.", "Yupi.Boot", ConsoleColor.DarkGreen);

                IsLive = true;
                IsReady = true;
			#if !DEBUG
            }
            catch (Exception e)
			{
                YupiWriterManager.WriteLine("Error When Starting Yupi Environment!" + Environment.NewLine + e.Message, "Yupi.Boot", ConsoleColor.Red);
                YupiWriterManager.WriteLine("Please press Y to get more details or press other Key to Exit", "Yupi.Boot", ConsoleColor.Red);

                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.Y)
                {
                    Console.WriteLine();

                    YupiWriterManager.WriteLine(
                        Environment.NewLine + "[Message] Error Details: " + Environment.NewLine + e.StackTrace +
                        Environment.NewLine + e.InnerException + Environment.NewLine + e.TargetSite +
                        Environment.NewLine + "[Message] Press Any Key To Exit", "Yupi.Boot", ConsoleColor.Red);

                    Console.ReadKey();
                }

                Environment.Exit(1);
            }
			#endif
        }

		// TODO Enum != Boolean !!! Where is this even used ??? Also Enums aren't strings!!!
        /// <summary>
        ///     Convert's Enum to Boolean
        /// </summary>
        /// <param name="theEnum">The theEnum.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool EnumToBool(string theEnum) => theEnum == "1";
		// TODO Remove useless? methods
        /// <summary>
        ///     Convert's Boolean to Integer
        /// </summary>
        /// <param name="theBool">if set to <c>true</c> [theBool].</param>
        /// <returns>System.Int32.</returns>
        public static int BoolToInteger(bool theBool) => theBool ? 1 : 0;

        /// <summary>
        ///     Convert's Boolean to Enum
        /// </summary>
        /// <param name="theBool">if set to <c>true</c> [theBool].</param>
        /// <returns>System.String.</returns>
        public static string BoolToEnum(bool theBool) => theBool ? "1" : "0";

        /// <summary>
        ///     Generates a Random Number in the Interval Min,Max
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>System.Int32.</returns>
        public static int GetRandomNumber(int min, int max) => RandomNumberGenerator.Get(min, max);

        /// <summary>
        ///     Get's the Actual Timestamp in Unix Format
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static int GetUnixTimeStamp()
            => (int) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
		// TODO Where is this used? Always prefer Database / Environment time
        /// <summary>
        ///     Convert's a Unix TimeStamp to DateTime
        /// </summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns>DateTime.</returns>
        public static DateTime UnixToDateTime(double unixTimeStamp)
            => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local).AddSeconds(unixTimeStamp).ToLocalTime();

        public static DateTime UnixToDateTime(int unixTimeStamp)
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
        public static int DateTimeToUnix(DateTime target) => Convert.ToInt32((target - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);

        /// <summary>
        ///     Get the Actual Time
        /// </summary>
        /// <returns>System.Int64.</returns>
        public static long Now() => (long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;

        public static int DifferenceInMilliSeconds(DateTime time, DateTime tFrom)
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
        public static string FilterFigure(string figure)
            =>
                figure.Any(character => !IsValid(character))
                    ? "lg-3023-1335.hr-828-45.sh-295-1332.hd-180-4.ea-3168-89.ca-1813-62.ch-235-1332"
                    : figure;

        /// <summary>
        ///     Check if is a Valid AlphaNumeric String
        /// </summary>
        /// <param name="inputStr">The input string.</param>
        /// <returns><c>true</c> if [is valid alpha numeric] [the specified input string]; otherwise, <c>false</c>.</returns>
        public static bool IsValidAlphaNumeric(string inputStr)
            => !string.IsNullOrEmpty(inputStr.ToLower()) && inputStr.ToLower().All(IsValid);

        /// <summary>
        ///     Get a Habbo With the Habbo's Username
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Habbo.</returns>
        /// Table: users.username
        public static Habbo GetHabboForName(string userName)
        {
            try
            {
                using (IQueryAdapter queryReactor = GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery("SELECT id FROM users WHERE username = @user");

                    queryReactor.AddParameter("user", userName);

                    int integer = queryReactor.GetInteger();

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
        public static bool IsNum(string theNum)
        {
            double num;
            
            return double.TryParse(theNum, out num);
        }

        /// <summary>
        ///     Get the Database Configuration Data
        /// </summary>
        /// <returns>DatabaseSettings.</returns>
        public static ServerDatabaseSettings GetDbConfig() => DatabaseSettings;

        /// <summary>
        ///     Get's the Default Emulator Encoding
        /// </summary>
        /// <returns>Encoding.</returns>
        public static Encoding GetDefaultEncoding() => YupiServerTextEncoding;

        /// <summary>
        ///     Get's the HabboHotel Environment Handler
        /// </summary>
        /// <returns>HabboHotel.</returns>
		public static HabboHotel GetGame() {
			return GameServer;
		}

        /// <summary>
        ///     Gets the language.
        /// </summary>
        /// <returns>Languages.</returns>
        public static ServerLanguageSettings GetLanguage() => ServerLanguageVariables;

		// TODO Check where this method is used and replace the queries with Prepared statements
        /// <summary>
        ///     Filter's SQL Injection Characters
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public static string FilterInjectionChars(string input)
        {
            input = input.Replace('\u0001', ' ');
            input = input.Replace('\u0002', ' ');
            input = input.Replace('\u0003', ' ');
            input = input.Replace('\t', ' ');

            return input;
        }

        /// <summary>
        ///     Get's the Database YupiDatabaseManager Handler
        /// </summary>
        /// <returns>ConnectionManager.</returns>
        public static IDatabaseManager GetDatabaseManager() => YupiDatabaseManager;

        /// <summary>
        ///     Perform's the Emulator Shutdown
        /// </summary>
        public static void PerformShutDown() => PerformShutDown(false);

        /// <summary>
        ///     Performs the restart.
        /// </summary>
        public static void PerformRestart() => PerformShutDown(true);

        /// <summary>
        ///     Shutdown the Emulator
        /// </summary>
        /// <param name="restart">if set to <c>true</c> [restart].</param>
        /// Set a Different Message in Hotel
        public static void PerformShutDown(bool restart)
        {
            IsReady = false;

            DateTime now = DateTime.Now;

            CacheManager.StopProcess();

            ShutdownStarted = true;

            Console.Title = "Yupi Emulator | Shutting down...";

            GetGame().StopGameLoop();

            GetGame().GetRoomManager().RemoveAllRooms();

            GetGame().GetClientManager().CloseAll();

			TCPServer.Stop();

            foreach (Group group in GetGame().GetGroupManager().Groups.Values)
                group.UpdateForum();

            using (IQueryAdapter queryReactor = YupiDatabaseManager.GetQueryReactor())
            {
                queryReactor.RunFastQuery("UPDATE users SET online = '0'");
                queryReactor.RunFastQuery("UPDATE rooms_data SET users_now = 0");
                queryReactor.RunFastQuery("TRUNCATE TABLE users_rooms_visits");
            }

            GetGame().Destroy();

            YupiDatabaseManager.Destroy();

            YupiLogManager.Stop();

            YupiWriterManager.WriteLine(" destroyed", "Yupi.Game", ConsoleColor.DarkYellow);

            TimeSpan span = DateTime.Now - now;

            YupiWriterManager.WriteLine("Elapsed " + TimeSpanToString(span) + "ms on Shutdown Proccess", "Yupi.Life", ConsoleColor.DarkYellow);

            IsLive = false;

            IsReady = true;

            Console.Title = "Yupi! | Waiting for Input.";

            if (restart)
                Program.StartEverything();
        }

        /// <summary>
        ///     Convert's a Unix TimeSpan to A String
        /// </summary>
        /// <param name="span">The span.</param>
        /// <returns>System.String.</returns>
        public static string TimeSpanToString(TimeSpan span)
            => string.Concat(span.Seconds, " s, ", span.Milliseconds, " ms");

        /// <summary>
        ///     Check's if Input Data is a Valid AlphaNumeric Character
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns><c>true</c> if the specified c is valid; otherwise, <c>false</c>.</returns>
        private static bool IsValid(char c) => char.IsLetterOrDigit(c) || AllowedSpecialChars.Contains(c);
    }
}
