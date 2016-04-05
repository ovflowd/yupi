using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Yupi.Emulator.Core.Io;
using Yupi.Emulator.Core.Security;
using Yupi.Emulator.Core.Security.BlackWords;
using Yupi.Emulator.Core.Security.BlackWords.Enums;
using Yupi.Emulator.Core.Security.BlackWords.Structs;
using Yupi.Emulator.Game.Commands;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Pathfinding.Vectors;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.RoomBots;
using Yupi.Emulator.Game.RoomBots.Enumerators;
using Yupi.Emulator.Game.RoomBots.Interfaces;
using Yupi.Emulator.Game.Rooms.Items.Enums;
using Yupi.Emulator.Game.Rooms.Items.Games.Teams.Enums;
using Yupi.Emulator.Game.Rooms.Items.Games.Types.Freeze.Enum;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Group = Yupi.Emulator.Game.Groups.Structs.Group;

namespace Yupi.Emulator.Game.Rooms.User
{
    /// <summary>
    ///     Class RoomUser.
    /// </summary>
    public class RoomUser : IEquatable<RoomUser>
    {
        /// <summary>
        ///     The _flood count
        /// </summary>
        private int _floodCount;

        /// <summary>
        ///     The _m client
        /// </summary>
        private GameClient _mClient;

        /// <summary>
        ///     The _m room
        /// </summary>
        private Room _mRoom;

        /// <summary>
        ///     The allow override
        /// </summary>
         bool AllowOverride;

        /// <summary>
        ///     The banzai power up
        /// </summary>
         FreezePowerUp BanzaiPowerUp;

        /// <summary>
        ///     The bot ai
        /// </summary>
         BotAi BotAi;

        /// <summary>
        ///     The bot data
        /// </summary>
         RoomBot BotData;

        /// <summary>
        ///     The can walk
        /// </summary>
         bool CanWalk;

        /// <summary>
        ///     The carry item identifier
        /// </summary>
         int CarryItemId;

        /// <summary>
        ///     The carry timer
        /// </summary>
         int CarryTimer;

        /// <summary>
        ///     The current item effect
        /// </summary>
         ItemEffectType CurrentItemEffect;

        /// <summary>
        ///     The dance identifier
        /// </summary>
         uint DanceId;

        /// <summary>
        ///     The fast walking
        /// </summary>
         bool FastWalking;

        /// <summary>
        ///     The flood expiry time
        /// </summary>
         int FloodExpiryTime;

        /// <summary>
        ///     The following owner
        /// </summary>
         RoomUser FollowingOwner;

        /// <summary>
        ///     The freeze counter
        /// </summary>
         int FreezeCounter;

        /// <summary>
        ///     The freezed
        /// </summary>
         bool Freezed; //En el freeze

        /// <summary>
        ///     The freeze lives
        /// </summary>
         int FreezeLives;

        /// <summary>
        ///     The frozen
        /// </summary>
         bool Frozen; //por comando

        /// <summary>
        ///     The gate identifier
        /// </summary>
         uint GateId;

        /// <summary>
        ///     The goal x
        /// </summary>
         int GoalX;

        /// <summary>
        ///     The goal y
        /// </summary>
         int GoalY;

        /// <summary>
        ///     The habbo identifier
        /// </summary>
         uint HabboId;

        /// <summary>
        ///     The handeling ball status
        /// </summary>
         int HandelingBallStatus = 0;

        /// <summary>
        ///     The has path blocked
        /// </summary>
         bool HasPathBlocked;

        /// <summary>
        ///     The horse identifier
        /// </summary>
         uint HorseId;

        /// <summary>
        ///     The idle time
        /// </summary>
         int IdleTime;

        /// <summary>
        ///     The interacting gate
        /// </summary>
         bool InteractingGate;

        /// <summary>
        ///     The  room identifier
        /// </summary>
         int InternalRoomId;

        /// <summary>
        ///     The is asleep
        /// </summary>
         bool IsAsleep;

        /// <summary>
        ///     The is flooded
        /// </summary>
         bool IsFlooded;

        /// <summary>
        ///     The is lying down
        /// </summary>
         bool IsLyingDown;

        /// <summary>
        ///     The is moonwalking
        /// </summary>
         bool IsMoonwalking;

        /// <summary>
        ///     The is sitting
        /// </summary>
         bool IsSitting;

        /// <summary>
        ///     The is spectator
        /// </summary>
         bool IsSpectator;

        /// <summary>
        ///     The is walking
        /// </summary>
         bool IsWalking;

        /// <summary>
        ///     The last bubble
        /// </summary>
         int LastBubble = 0;

        /// <summary>
        ///     The last interaction
        /// </summary>
         int LastInteraction;

        /// <summary>
        ///     The last item
        /// </summary>
         uint LastItem;

         int LastSelectedX;
         int LastSelectedY;

        /// <summary>
        ///     The locked tiles count
        /// </summary>
         uint LockedTilesCount;

        /// <summary>
        ///     The love lock partner
        /// </summary>
         uint LoveLockPartner;

        /// <summary>
        ///     The on camping tent
        /// </summary>
         bool OnCampingTent;

        /// <summary>
        ///     The path
        /// </summary>
         List<Vector2D> Path = new List<Vector2D>();

        /// <summary>
        ///     The path recalc needed
        /// </summary>
         bool PathRecalcNeeded;

        /// <summary>
        ///     The path step
        /// </summary>
         int PathStep = 1;

        /// <summary>
        ///     The pet data
        /// </summary>
         Pet PetData;

        /// <summary>
        ///     The riding horse
        /// </summary>
         bool RidingHorse;

        /// <summary>
        ///     The room identifier
        /// </summary>
         uint RoomId;

        /// <summary>
        ///     The rot body
        /// </summary>
         int RotBody;

        /// <summary>
        ///     The rot head
        /// </summary>
         int RotHead;

        /// <summary>
        ///     The set step
        /// </summary>
         bool SetStep;

        /// <summary>
        ///     The set x
        /// </summary>
         int SetX;

        /// <summary>
        ///     The set y
        /// </summary>
         int SetY;

        /// <summary>
        ///     The set z
        /// </summary>
         double SetZ;

        /// <summary>
        ///     The shield active
        /// </summary>
         bool ShieldActive;

        /// <summary>
        ///     The shield counter
        /// </summary>
         int ShieldCounter;

        /// <summary>
        ///     The sign time
        /// </summary>
         int SignTime;

        /// <summary>
        ///     The sq state
        /// </summary>
         byte SqState;

        /// <summary>
        ///     The statusses
        /// </summary>
         Dictionary<string, string> Statusses;

        /// <summary>
        ///     The team
        /// </summary>
         Team Team;

        /// <summary>
        ///     The tele delay
        /// </summary>
         int TeleDelay;

        /// <summary>
        ///     The teleport enabled
        /// </summary>
         bool TeleportEnabled;

        /// <summary>
        ///     The throw ball at goal
        /// </summary>
         bool ThrowBallAtGoal;

        /// <summary>
        ///     The update needed
        /// </summary>
         bool UpdateNeeded;

        /// <summary>
        ///     The user identifier
        /// </summary>
         uint UserId;

         int UserTimeInCurrentRoom;

        /// <summary>
        ///     The virtual identifier
        /// </summary>
         int VirtualId;

        /// <summary>
        ///     The x
        /// </summary>
         int X;

        /// <summary>
        ///     The y
        /// </summary>
         int Y;

        /// <summary>
        ///     The z
        /// </summary>
         double Z;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomUser" /> class.
        /// </summary>
        /// <param name="habboId">The habbo identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="virtualId">The virtual identifier.</param>
        /// <param name="room">The room.</param>
        /// <param name="isSpectator">if set to <c>true</c> [is spectator].</param>
         RoomUser(uint habboId, uint roomId, int virtualId, Room room, bool isSpectator)
        {
            Freezed = false;
            HabboId = habboId;
            RoomId = roomId;
            VirtualId = virtualId;
            IdleTime = 0;
            X = 0;
            Y = 0;
            Z = 0.0;
            UserTimeInCurrentRoom = 0;
            RotHead = 0;
            RotBody = 0;
            UpdateNeeded = true;
            Statusses = new Dictionary<string, string>();
            TeleDelay = -1;
            _mRoom = room;
            AllowOverride = false;
            CanWalk = true;
            IsSpectator = isSpectator;
            SqState = 3;
            InternalRoomId = 0;
            CurrentItemEffect = ItemEffectType.None;
            Events1 = new Queue();
            FreezeLives = 0;
            InteractingGate = false;
            GateId = 0u;
            LastInteraction = 0;
            LockedTilesCount = 0;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomUser" /> class.
        /// </summary>
        /// <param name="habboId">The habbo identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="virtualId">The virtual identifier.</param>
        /// <param name="pClient">The p client.</param>
        /// <param name="room">The room.</param>
         RoomUser(uint habboId, uint roomId, int virtualId, GameClient pClient, Room room)
        {
            _mClient = pClient;
            Freezed = false;
            HabboId = habboId;
            RoomId = roomId;
            VirtualId = virtualId;
            IdleTime = 0;
            X = 0;
            Y = 0;
            Z = 0.0;
            RotHead = 0;
            RotBody = 0;
            UserTimeInCurrentRoom = 0;
            UpdateNeeded = true;
            Statusses = new Dictionary<string, string>();
            TeleDelay = -1;
            LastInteraction = 0;
            AllowOverride = false;
            CanWalk = true;
            IsSpectator = GetClient().GetHabbo().SpectatorMode;
            SqState = 3;
            InternalRoomId = 0;
            CurrentItemEffect = ItemEffectType.None;
            _mRoom = room;
            Events1 = new Queue();
            InteractingGate = false;
            GateId = 0u;
            LockedTilesCount = 0;
        }

        /// <summary>
        ///     The _events
        /// </summary>
        public Queue Events1 { get; }

        /// <summary>
        ///     Gets the coordinate.
        /// </summary>
        /// <value>The coordinate.</value>
         Point Coordinate => new Point(X, Y);

        /// <summary>
        ///     Gets the square behind.
        /// </summary>
        /// <value>The square behind.</value>
         Point SquareBehind
        {
            get
            {
                int x = X;
                int y = Y;

                switch (RotBody)
                {
                    case 0:
                        y++;
                        break;

                    case 1:
                        x--;
                        y++;
                        break;

                    case 2:
                        x--;
                        break;

                    case 3:
                        x--;
                        y--;
                        break;

                    case 4:
                        y--;
                        break;

                    case 5:
                        x++;
                        y--;
                        break;

                    case 6:
                        x++;
                        break;

                    case 7:
                        x++;
                        y++;
                        break;
                }

                return new Point(x, y);
            }
        }

        /// <summary>
        ///     Gets the square in front.
        /// </summary>
        /// <value>The square in front.</value>
         Point SquareInFront
        {
            get
            {
                {
                    int x = X + 1;
                    int y = 0;
                    switch (RotBody)
                    {
                        case 0:
                            x = X;
                            y = Y - 1;
                            break;

                        case 1:
                            x = X + 1;
                            y = Y - 1;
                            break;

                        case 2:
                            x = X + 1;
                            y = Y;
                            break;

                        case 3:
                            x = X + 1;
                            y = Y + 1;
                            break;

                        case 4:
                            x = X;
                            y = Y + 1;
                            break;

                        case 5:
                            x = X - 1;
                            y = Y + 1;
                            break;

                        case 6:
                            x = X - 1;
                            y = Y;
                            break;

                        case 7:
                            x = X - 1;
                            y = Y - 1;
                            break;
                    }
                    return new Point(x, y);
                }
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is pet.
        /// </summary>
        /// <value><c>true</c> if this instance is pet; otherwise, <c>false</c>.</value>
         bool IsPet => IsBot && BotData.IsPet;

        /// <summary>
        ///     Gets the current effect.
        /// </summary>
        /// <value>The current effect.</value>
         int CurrentEffect
        {
            get
            {
                if (GetClient() == null || GetClient().GetHabbo() == null)
                    return 0;

                return GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().CurrentEffect;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is dancing.
        /// </summary>
        /// <value><c>true</c> if this instance is dancing; otherwise, <c>false</c>.</value>
         bool IsDancing => DanceId >= 1;

        /// <summary>
        ///     Gets a value indicating whether [needs autokick].
        /// </summary>
        /// <value><c>true</c> if [needs autokick]; otherwise, <c>false</c>.</value>
         bool NeedsAutokick => !IsBot &&
                                       (GetClient() == null || GetClient().GetHabbo() == null ||
                                        (GetClient().GetHabbo().Rank <= 6u && IdleTime >= 1800));

        /// <summary>
        ///     Gets a value indicating whether this instance is trading.
        /// </summary>
        /// <value><c>true</c> if this instance is trading; otherwise, <c>false</c>.</value>
         bool IsTrading => !IsBot && Statusses.ContainsKey("trd");

        /// <summary>
        ///     Gets a value indicating whether this instance is bot.
        /// </summary>
        /// <value><c>true</c> if this instance is bot; otherwise, <c>false</c>.</value>
         bool IsBot => BotData != null;

        /// <summary>
        ///     Equalses the specified compared user.
        /// </summary>
        /// <param name="comparedUser">The compared user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(RoomUser comparedUser)
        {
            return comparedUser.HabboId == HabboId;
        }

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        /// <returns>System.String.</returns>
         string GetUserName()
        {
            if (!IsBot)
                return GetClient() != null ? GetClient().GetHabbo().UserName : string.Empty;
            if (!IsPet)
                return BotData == null ? string.Empty : BotData.Name;
            return PetData.Name;
        }

        /// <summary>
        ///     Determines whether this instance is owner.
        /// </summary>
        /// <returns><c>true</c> if this instance is owner; otherwise, <c>false</c>.</returns>
         bool IsOwner()
        {
            Room currentRoom = GetRoom();
            return !IsBot && currentRoom != null && GetUserName() == currentRoom.RoomData.Owner;
        }

        /// <summary>
        ///     Uns the idle.
        /// </summary>
         void UnIdle()
        {
            IdleTime = 0;
            if (!IsAsleep)
                return;
            IsAsleep = false;
            SimpleServerMessageBuffer sleep = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomUserIdleMessageComposer"));
            sleep.AppendInteger(VirtualId);
            sleep.AppendBool(false);
            GetRoom().SendMessage(sleep);
        }

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
         void Dispose()
        {
            Statusses.Clear();
            _mRoom = null;
            _mClient = null;
        }

        /// <summary>
        ///     Chats the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="shout">if set to <c>true</c> [shout].</param>
        /// <param name="count">The count.</param>
        /// <param name="textColor">Color of the text.</param>
         void Chat(GameClient session, string msg, bool shout, int count, int textColor = 0)
        {
            if (session == null)
                return;

            if (IsPet || IsBot)
            {
                SimpleServerMessageBuffer botChatmsg = new SimpleServerMessageBuffer();

                botChatmsg.Init(shout ? PacketLibraryManager.OutgoingHandler("ShoutMessageComposer") : PacketLibraryManager.OutgoingHandler("ChatMessageComposer"));
                botChatmsg.AppendInteger(VirtualId);
                botChatmsg.AppendString(msg);
                botChatmsg.AppendInteger(0);
                botChatmsg.AppendInteger(!IsPet ? 2 : textColor);
                botChatmsg.AppendInteger(0);
                botChatmsg.AppendInteger(count);

                GetRoom().SendMessage(botChatmsg);

                return;
            }

            if (msg.Length > 100)
                return;

            if (!ServerSecurityChatFilter.CanTalk(session, msg))
                return;

            if (session.GetHabbo() == null)
                return;

            BlackWord word;

            if ((!msg.StartsWith(":deleteblackword ") || !msg.StartsWith(":addblackword ")) && session.GetHabbo().Rank < 5 && BlackWordsManager.Check(msg, BlackWordType.Hotel, out word))
            {
                BlackWordTypeSettings settings = word.TypeSettings;

                session.HandlePublicist(word.Word, msg, "CHAT", settings);

                if (settings.ShowMessage)
                {
                    GetClient().SendModeratorMessage("A mensagem contém a palavra: " + word.Word + " que não é permitida, você poderá ser banido!");

                    return;
                }

                return;
            }

            if (!IsBot && IsFlooded && FloodExpiryTime <= Yupi.GetUnixTimeStamp())
                IsFlooded = false;
            else if (!IsBot && IsFlooded)
                return; 

            if (session.GetHabbo().Rank < 4 && GetRoom().CheckMute(session))
                return;

            UnIdle();

            if (!IsPet && !IsBot)
            {
                if (msg.StartsWith(":") && CommandsManager.TryExecute(msg.Substring(1), session))
                    return;

                Habbo habbo = GetClient().GetHabbo();

                if (habbo == null)
                    return;

                if (GetRoom() == null)
                    return;

                if (GetRoom().GetWiredHandler().ExecuteWired(Interaction.TriggerOnUserSay, this, msg))
                    return;

                GetRoom().AddChatlog(session.GetHabbo().Id, msg, true);

                uint rank = habbo.Rank;

                msg = GetRoom().WordFilter.Aggregate(msg, (current1, current) => Regex.Replace(current1, current, "bobba", RegexOptions.IgnoreCase));

                if (rank < 4)
                {
                    TimeSpan span = DateTime.Now - habbo.SpamFloodTime;

                    if ((span.TotalSeconds > habbo.SpamProtectionTime) && habbo.SpamProtectionBol)
                    {
                        _floodCount = 0;

                        habbo.SpamProtectionBol = false;
                        habbo.SpamProtectionAbuse = 0;
                    }
                    else if (span.TotalSeconds > 4.0)
                        _floodCount = 0;

                    SimpleServerMessageBuffer messageBuffer;

                    if ((span.TotalSeconds < habbo.SpamProtectionTime) && habbo.SpamProtectionBol)
                    {
                        messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("FloodFilterMessageComposer"));

                        int i = habbo.SpamProtectionTime - span.Seconds;

                        messageBuffer.AppendInteger(i);

                        IsFlooded = true;

                        FloodExpiryTime = Yupi.GetUnixTimeStamp() + i;

                        GetClient()?.SendMessage(messageBuffer);

                        return;
                    }

                    if ((span.TotalSeconds < 4.0) && (_floodCount > 5) && (rank < 5))
                    {
                        messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("FloodFilterMessageComposer"));

                        habbo.SpamProtectionCount++;

                        if (habbo.SpamProtectionCount%2 == 0)
                            habbo.SpamProtectionTime = 10*habbo.SpamProtectionCount;
                        else
                            habbo.SpamProtectionTime = 10*(habbo.SpamProtectionCount - 1);

                        habbo.SpamProtectionBol = true;

                        int j = habbo.SpamProtectionTime - span.Seconds;

                        messageBuffer.AppendInteger(j);

                        IsFlooded = true;

                        FloodExpiryTime = Yupi.GetUnixTimeStamp() + j;

                        GetClient()?.SendMessage(messageBuffer);

                        return;
                    }

                    habbo.SpamFloodTime = DateTime.Now;

                    _floodCount++;
                }

                if (habbo.Preferences.ChatColor != textColor)
                {
                    habbo.Preferences.ChatColor = textColor;
                    habbo.Preferences.Save();
                }
            }
            else if (!IsPet)
                textColor = 2;

            SimpleServerMessageBuffer chatMsg = new SimpleServerMessageBuffer(shout ? PacketLibraryManager.OutgoingHandler("ShoutMessageComposer") : PacketLibraryManager.OutgoingHandler("ChatMessageComposer"));

            chatMsg.AppendInteger(VirtualId);
            chatMsg.AppendString(msg);
            chatMsg.AppendInteger(ChatEmotions.GetEmotionsForText(msg));
            chatMsg.AppendInteger(textColor);
            chatMsg.AppendInteger(0); 
            chatMsg.AppendInteger(count);

            GetRoom()?.BroadcastChatMessage(chatMsg, this, session.GetHabbo().Id);

            GetRoom()?.OnUserSay(this, msg, shout);

            GetRoom()?.GetRoomUserManager()?.TurnHeads(X, Y, HabboId);
        }

        /// <summary>
        ///     Increments the and check flood.
        /// </summary>
        /// <param name="muteTime">The mute time.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
         bool IncrementAndCheckFlood(out int muteTime)
        {
            muteTime = 20;
            TimeSpan timeSpan = DateTime.Now - GetClient().GetHabbo().SpamFloodTime;
            if (timeSpan.TotalSeconds > GetClient().GetHabbo().SpamProtectionTime &&
                GetClient().GetHabbo().SpamProtectionBol)
            {
                _floodCount = 0;
                GetClient().GetHabbo().SpamProtectionBol = false;
                GetClient().GetHabbo().SpamProtectionAbuse = 0;
            }
            else if (timeSpan.TotalSeconds > 2.0)
                _floodCount = 0;

            {
                if (timeSpan.TotalSeconds < 2.0 && _floodCount > 6 && GetClient().GetHabbo().Rank < 5u)
                {
                    muteTime = GetClient().GetHabbo().SpamProtectionTime - timeSpan.Seconds + 30;
                    return true;
                }
                GetClient().GetHabbo().SpamFloodTime = DateTime.Now;
                _floodCount++;
                return false;
            }
        }

        /// <summary>
        ///     Clears the movement.
        /// </summary>
         void ClearMovement()
        {
            IsWalking = false;
            GoalX = 0;
            GoalY = 0;
            SetStep = false;

            if (GetRoom().GetRoomUserManager().ToSet.ContainsKey(new Point(SetX, SetY)))
                GetRoom().GetRoomUserManager().ToSet.Remove(new Point(SetX, SetY));

            SetX = 0;
            SetY = 0;
            SetZ = 0.0;

            if (!Statusses.ContainsKey("mv"))
                return;

            Statusses.Remove("mv");
            UpdateNeeded = true;
        }

        /// <summary>
        ///     Moves to.
        /// </summary>
        /// <param name="c">The c.</param>
         void MoveTo(Point c)
        {
            MoveTo(c.X, c.Y);
        }

        /// <summary>
        ///     Moves to.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="pOverride"></param>
         void MoveTo(Point c, bool pOverride)
        {
            MoveTo(c.X, c.Y, pOverride);
        }

        /// <summary>
        ///     Moves to.
        /// </summary>
        /// <param name="x">The p x.</param>
        /// <param name="y">The p y.</param>
        /// <param name="pOverride">if set to <c>true</c> [p override].</param>
         void MoveTo(int x, int y, bool pOverride)
        {
            if (TeleportEnabled)
            {
                UnIdle();

                GetRoom()
                    .SendMessage(GetRoom()
                        .GetRoomItemHandler()
                        .UpdateUserOnRoller(this, new Point(x, y), 0u,
                            GetRoom().GetGameMap().SqAbsoluteHeight(GoalX, GoalY)));

                if (Statusses.ContainsKey("sit"))
                    Z -= 0.35;

                UpdateNeeded = true;

                GetRoom().GetRoomUserManager().UpdateUserStatus(this, false);

                return;
            }

            if (GetRoom().GetGameMap().SquareHasUsers(x, y) && !pOverride)
                return;

            if (Frozen)
                return;

            Point coord = new Point(x, y);

            List<RoomItem> allRoomItemForSquare = GetRoom().GetGameMap().GetCoordinatedHeighestItems(coord);

            if ((RidingHorse && !IsBot && allRoomItemForSquare.Any()) || (IsPet && allRoomItemForSquare.Any()))
                if (
                    allRoomItemForSquare.Any(
                        current =>
                            current.GetBaseItem().IsSeat ||
                            current.GetBaseItem().InteractionType == Interaction.LowPool ||
                            current.GetBaseItem().InteractionType == Interaction.Pool ||
                            current.GetBaseItem().InteractionType == Interaction.HaloweenPool ||
                            current.GetBaseItem().InteractionType == Interaction.Bed ||
                            current.GetBaseItem().InteractionType == Interaction.PressurePadBed ||
                            current.GetBaseItem().InteractionType == Interaction.Guillotine))
                    return;

            if (IsPet &&
                allRoomItemForSquare.Any(
                    p => InteractionTypes.AreFamiliar(GlobalInteraction.PetBreeding, p.GetBaseItem().InteractionType)))
            {
                RoomItem s =
                    allRoomItemForSquare.FirstOrDefault(
                        p =>
                            InteractionTypes.AreFamiliar(GlobalInteraction.PetBreeding, p.GetBaseItem().InteractionType));
                Z -= s.GetBaseItem().Height;
            }

            UnIdle();
            GoalX = x;
            GoalY = y;
            LastSelectedX = x;
            LastSelectedY = y;

            PathRecalcNeeded = true;
            ThrowBallAtGoal = false;
        }

        /// <summary>
        ///     Moves to.
        /// </summary>
        /// <param name="pX">The p x.</param>
        /// <param name="pY">The p y.</param>
         void MoveTo(int pX, int pY)
        {
            MoveTo(pX, pY, false);
        }

        /// <summary>
        ///     Unlocks the walking.
        /// </summary>
         void UnlockWalking()
        {
            AllowOverride = false;
            CanWalk = true;
        }

        /// <summary>
        ///     Sets the position.
        /// </summary>
        /// <param name="pX">The p x.</param>
        /// <param name="pY">The p y.</param>
        /// <param name="pZ">The p z.</param>
         void SetPos(int pX, int pY, double pZ)
        {
            X = pX;
            Y = pY;
            Z = pZ;
        }

        /// <summary>
        ///     Carries the item.
        /// </summary>
        /// <param name="item">The item.</param>
         void CarryItem(int item)
        {
            CarryItemId = item;
            CarryTimer = item > 0 ? 240 : 0;
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ApplyHanditemMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(VirtualId);
            simpleServerMessageBuffer.AppendInteger(item);
            GetRoom().SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Sets the rot.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
         void SetRot(int rotation)
        {
            SetRot(rotation, false);
        }

        /// <summary>
        ///     Sets the rot.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        /// <param name="headOnly">if set to <c>true</c> [head only].</param>
         void SetRot(int rotation, bool headOnly)
        {
            if (Statusses.ContainsKey("lay") || IsWalking) return;
            int num = RotBody - rotation;
            RotHead = RotBody;
            if (Statusses.ContainsKey("sit") || headOnly)
                switch (RotBody)
                {
                    case 4:
                    case 2:
                        if (num > 0) RotHead = RotBody - 1;
                        else if (num < 0) RotHead = RotBody + 1;
                        break;

                    case 6:
                    case 0:
                        if (num > 0) RotHead = RotBody - 1;
                        else if (num < 0) RotHead = RotBody + 1;
                        break;
                }
            else if (num <= -2 || num >= 2)
            {
                RotHead = rotation;
                RotBody = rotation;
            }
            else
                RotHead = rotation;
            UpdateNeeded = true;
        }

        /// <summary>
        ///     Adds the status.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
         void AddStatus(string key, string value)
        {
            if (!Statusses.ContainsKey(key))
                Statusses.Add(key, value);
            else
                Statusses[key] = value;
        }

        /// <summary>
        ///     Removes the status.
        /// </summary>
        /// <param name="key">The key.</param>
         void RemoveStatus(string key)
        {
            if (Statusses.ContainsKey(key))
                Statusses.Remove(key);
        }

        /// <summary>
        ///     Applies the effect.
        /// </summary>
        /// <param name="effectId">The effect identifier.</param>
         void ApplyEffect(int effectId)
        {
            if (IsBot || GetClient() == null || GetClient().GetHabbo() == null ||
                GetClient().GetHabbo().GetAvatarEffectsInventoryComponent() == null)
                return;
            GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(effectId);
        }

        /// <summary>
        ///     Serializes the specified messageBuffer.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="gotPublicRoom">if set to <c>true</c> [got public room].</param>
         void Serialize(SimpleServerMessageBuffer messageBuffer, bool gotPublicRoom)
        {
            if (messageBuffer == null)
                return;
            if (IsSpectator)
                return;
            if (!IsBot)
            {
                if (GetClient() == null || GetClient().GetHabbo() == null)
                    return;
                Group group = Yupi.GetGame().GetGroupManager().GetGroup(GetClient().GetHabbo().FavouriteGroup);
                if (GetClient() == null || GetClient().GetHabbo() == null)
                    return;
                Habbo habbo = GetClient().GetHabbo();

                if (habbo == null)
                    return;

                messageBuffer.AppendInteger(habbo.Id);
                messageBuffer.AppendString(habbo.UserName);
                messageBuffer.AppendString(habbo.Motto);
                messageBuffer.AppendString(habbo.Look);
                messageBuffer.AppendInteger(VirtualId);
                messageBuffer.AppendInteger(X);
                messageBuffer.AppendInteger(Y);
                messageBuffer.AppendString(ServerUserChatTextHandler.GetString(Z));
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendInteger(1);
                messageBuffer.AppendString(habbo.Gender.ToLower());
                if (@group != null)
                {
                    messageBuffer.AppendInteger(@group.Id);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendString(@group.Name);
                }
                else
                {
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendString("");
                }
                messageBuffer.AppendString("");
                messageBuffer.AppendInteger(habbo.AchievementPoints);
                messageBuffer.AppendBool(false);
                return;
            }

            if (BotAi == null || BotData == null)
                throw new NullReferenceException("BotAI or BotData is undefined");

            messageBuffer.AppendInteger(BotAi.BaseId);
            messageBuffer.AppendString(BotData.Name);
            messageBuffer.AppendString(BotData.Motto);
            if (BotData.AiType == AiType.Pet)
                if (PetData.Type == "pet_monster")
                    messageBuffer.AppendString(PetData.MoplaBreed.PlantData);
                else if (PetData.HaveSaddle == Convert.ToBoolean(2))
                    messageBuffer.AppendString(string.Concat(BotData.Look.ToLower(), " 3 4 10 0 2 ", PetData.PetHair, " ",
                        PetData.HairDye, " 3 ", PetData.PetHair, " ", PetData.HairDye));
                else if (PetData.HaveSaddle == Convert.ToBoolean(1))
                    messageBuffer.AppendString(string.Concat(BotData.Look.ToLower(), " 3 2 ", PetData.PetHair, " ",
                        PetData.HairDye, " 3 ", PetData.PetHair, " ", PetData.HairDye, " 4 9 0"));
                else
                    messageBuffer.AppendString(string.Concat(BotData.Look.ToLower(), " 2 2 ", PetData.PetHair, " ",
                        PetData.HairDye, " 3 ", PetData.PetHair, " ", PetData.HairDye));
            else
                messageBuffer.AppendString(BotData.Look.ToLower());
            messageBuffer.AppendInteger(VirtualId);
            messageBuffer.AppendInteger(X);
            messageBuffer.AppendInteger(Y);
            messageBuffer.AppendString(ServerUserChatTextHandler.GetString(Z));
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(BotData.AiType == AiType.Generic ? 4 : 2);
            if (BotData.AiType == AiType.Pet)
            {
                messageBuffer.AppendInteger(PetData.RaceId);
                messageBuffer.AppendInteger(PetData.OwnerId);
                messageBuffer.AppendString(PetData.OwnerName);
                messageBuffer.AppendInteger(PetData.Type == "pet_monster" ? 0 : 1);
                messageBuffer.AppendBool(PetData.HaveSaddle);
                messageBuffer.AppendBool(RidingHorse);
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendInteger(PetData.Type == "pet_monster" ? 1 : 0);
                messageBuffer.AppendString(PetData.Type == "pet_monster" ? PetData.MoplaBreed.GrowStatus : "");
                return;
            }
            messageBuffer.AppendString(BotData.Gender.ToLower());
            messageBuffer.AppendInteger(BotData.OwnerId);
            messageBuffer.AppendString(Yupi.GetGame().GetClientManager().GetUserNameByUserId(BotData.OwnerId));
            messageBuffer.AppendInteger(5);
            messageBuffer.AppendShort(1);
            messageBuffer.AppendShort(2);
            messageBuffer.AppendShort(3);
            messageBuffer.AppendShort(4);
            messageBuffer.AppendShort(5);
        }

        /// <summary>
        ///     Serializes the status.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
         void SerializeStatus(SimpleServerMessageBuffer messageBuffer)
        {
            messageBuffer.AppendInteger(VirtualId);
            messageBuffer.AppendInteger(X);
            messageBuffer.AppendInteger(Y);
            messageBuffer.AppendString(ServerUserChatTextHandler.GetString(Z));
            messageBuffer.AppendInteger(RotHead);
            messageBuffer.AppendInteger(RotBody);

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("/");

            if (IsPet && PetData.Type == "pet_monster")
                stringBuilder.AppendFormat("/{0}{1}", PetData.MoplaBreed.GrowStatus, Statusses.Count >= 1 ? "/" : "");

            lock (Statusses)
            {
                foreach (KeyValuePair<string, string> current in Statusses)
                {
                    stringBuilder.Append(current.Key);

                    if (!string.IsNullOrEmpty(current.Value))
                    {
                        stringBuilder.Append(" ");
                        stringBuilder.Append(current.Value);
                    }

                    stringBuilder.Append("/");
                }
            }

            stringBuilder.Append("/");

            messageBuffer.AppendString(stringBuilder.ToString());

            if (!Statusses.ContainsKey("sign"))
                return;

            RemoveStatus("sign");
            UpdateNeeded = true;
        }

        /// <summary>
        ///     Serializes the status.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="status">The status.</param>
         void SerializeStatus(SimpleServerMessageBuffer messageBuffer, string status)
        {
            if (IsSpectator)
                return;
            messageBuffer.AppendInteger(VirtualId);
            messageBuffer.AppendInteger(X);
            messageBuffer.AppendInteger(Y);
            messageBuffer.AppendString(ServerUserChatTextHandler.GetString(SetZ));
            messageBuffer.AppendInteger(RotHead);
            messageBuffer.AppendInteger(RotBody);
            messageBuffer.AppendString(status);
        }

        /// <summary>
        ///     Gets the client.
        /// </summary>
        /// <returns>GameClient.</returns>
         GameClient GetClient()
        {
            if (IsBot)
                return null;

            if (_mClient != null)
                return _mClient;

            return _mClient = Yupi.GetGame().GetClientManager().GetClientByUserId(HabboId);
        }

        /// <summary>
        ///     Sends the messageBuffer.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
         void SendMessage(byte[] message)
        {
            if (GetClient() == null || GetClient().GetConnection() == null)
                return;

            GetClient().GetConnection().Send(message);
        }

        /// <summary>
        ///     Gets the room.
        /// </summary>
        /// <returns>Room.</returns>
        private Room GetRoom() => _mRoom ?? (_mRoom = Yupi.GetGame().GetRoomManager().GetRoom(RoomId));
    }
}