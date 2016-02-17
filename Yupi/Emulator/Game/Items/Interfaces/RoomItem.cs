using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Yupi.Emulator.Core.Io;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Util.Math;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Items.Datas;
using Yupi.Emulator.Game.Items.Handlers;
using Yupi.Emulator.Game.Items.Interactions;
using Yupi.Emulator.Game.Items.Interactions.Controllers;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interactions.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Handlers;
using Yupi.Emulator.Game.Pathfinding.Vectors;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Items;
using Yupi.Emulator.Game.Rooms.Items.Games.Teams.Enums;
using Yupi.Emulator.Game.Rooms.Items.Games.Types.Freeze.Enum;
using Yupi.Emulator.Game.Rooms.Items.Games.Types.Soccer.Enums;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Rooms.User.Path;
using Yupi.Emulator.Game.SoundMachine;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Items.Interfaces
{
    /// <summary>
    ///     Class RoomItem.
    /// </summary>
    public class RoomItem : IEquatable<RoomItem>
    {
        /// <summary>
        ///     The _m base item
        /// </summary>
        private Item _mBaseItem;

        /// <summary>
        ///     The _m room
        /// </summary>
        private Room _mRoom;

        /// <summary>
        ///     The _update needed
        /// </summary>
        private bool _updateNeeded;

        /// <summary>
        ///     The ball is moving
        /// </summary>
        internal bool BallIsMoving;

        /// <summary>
        ///     The ball value
        /// </summary>
        internal int BallValue;

        /// <summary>
        ///     The base name
        /// </summary>
        internal string BaseName;

        /// <summary>
        ///     The come direction
        /// </summary>
        internal IComeDirection ComeDirection;

        /// <summary>
        ///     The extra data
        /// </summary>
        internal string ExtraData = string.Empty;

        /// <summary>
        ///     The freeze power up
        /// </summary>
        internal FreezePowerUp FreezePowerUp;

        /// <summary>
        ///     The group data
        /// </summary>
        internal string GroupData;

        /// <summary>
        ///     The group identifier
        /// </summary>
        internal uint GroupId;

        /// <summary>
        ///     The highscore data
        /// </summary>
        internal HighscoreData HighscoreData;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal uint Id;

        /// <summary>
        ///     The interacting ball user
        /// </summary>
        internal uint InteractingBallUser;

        /// <summary>
        ///     The interacting user
        /// </summary>
        internal uint InteractingUser;

        /// <summary>
        ///     The interacting user2
        /// </summary>
        internal uint InteractingUser2;

        /// <summary>
        ///     The interaction count
        /// </summary>
        public byte InteractionCount;

        /// <summary>
        ///     The interaction count helper
        /// </summary>
        internal byte InteractionCountHelper;

        /// <summary>
        ///     The is builder
        /// </summary>
        internal bool IsBuilder;

        /// <summary>
        ///     The is trans
        /// </summary>
        internal bool IsTrans;

        /// <summary>
        ///     The limited no
        /// </summary>
        internal int LimitedNo;

        /// <summary>
        ///     The limited tot
        /// </summary>
        internal int LimitedTot;

        /// <summary>
        ///     The magic remove
        /// </summary>
        internal bool MagicRemove;

        internal MovementDirection MoveToDirMovement = MovementDirection.None;

        /// <summary>
        ///     The on cannon acting
        /// </summary>
        internal bool OnCannonActing = false;

        /// <summary>
        ///     The pending reset
        /// </summary>
        internal bool PendingReset;

        /// <summary>
        ///     The pets list
        /// </summary>
        internal List<Pet> PetsList = new List<Pet>(2);

        /// <summary>
        ///     The room identifier
        /// </summary>
        internal uint RoomId;

        /// <summary>
        ///     The rot
        /// </summary>
        internal int Rot;

        /// <summary>
        ///     The song code
        /// </summary>
        internal string SongCode;

        /// <summary>
        ///     The team
        /// </summary>
        internal Team Team;

        /// <summary>
        ///     The update counter
        /// </summary>
        internal int UpdateCounter;

        /// <summary>
        ///     The user identifier
        /// </summary>
        internal uint UserId;

        /// <summary>
        ///     The value
        /// </summary>
        internal int Value;

        /// <summary>
        ///     The viking cotie burning
        /// </summary>
        internal bool VikingCotieBurning;

        /// <summary>
        ///     The wall coord
        /// </summary>
        internal WallCoordinate WallCoord;

        /// <summary>
        ///     Gets a value indicating whether this instance is wired.
        /// </summary>
        /// <value><c>true</c> if this instance is wired; otherwise, <c>false</c>.</value>
        public bool IsWired => InteractionTypes.AreFamiliar(GlobalInteractions.Wired, GetBaseItem().InteractionType);

        /// <summary>
        ///     Gets the affected tiles.
        /// </summary>
        /// <value>The affected tiles.</value>
        internal Dictionary<int, ThreeDCoord> AffectedTiles { get; private set; }

        /// <summary>
        ///     Gets the x.
        /// </summary>
        /// <value>The x.</value>
        internal int X { get; private set; }

        /// <summary>
        ///     Gets the y.
        /// </summary>
        /// <value>The y.</value>
        internal int Y { get; private set; }

        /// <summary>
        ///     Gets the Z Coordinate
        /// </summary>
        /// <value>The Private Z Coordinate.</value>
        private double _z;

        /// <summary>
        ///     Gets the Z Coordinate
        /// </summary>
        /// <value>The Z Coordinate</value>
        internal double Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = !double.IsInfinity(value) ? value : 0;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [update needed].
        /// </summary>
        /// <value><c>true</c> if [update needed]; otherwise, <c>false</c>.</value>
        internal bool UpdateNeeded
        {
            get { return _updateNeeded; }
            set
            {
                if (value)
                    GetRoom().GetRoomItemHandler().QueueRoomItemUpdate(this);

                _updateNeeded = value;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is roller.
        /// </summary>
        /// <value><c>true</c> if this instance is roller; otherwise, <c>false</c>.</value>
        internal bool IsRoller { get; private set; }

        /// <summary>
        ///     Gets the coordinate.
        /// </summary>
        /// <value>The coordinate.</value>
        internal Point Coordinate => new Point(X, Y);

        /// <summary>
        ///     Gets the get coords.
        /// </summary>
        /// <value>The get coords.</value>
        internal List<Point> GetCoords
        {
            get
            {
                List<Point> list = new List<Point> { Coordinate };

                list.AddRange(AffectedTiles.Values.Select(current => new Point(current.X, current.Y)));

                return list;
            }
        }

        internal double Height
        {
            get
            {
                if (GetBaseItem() == null)
                    return 0;

                if (!GetBaseItem().StackMultipler)
                    return GetBaseItem().Height;

                if (string.IsNullOrEmpty(ExtraData))
                    ExtraData = "0";

                int stackMultiple;

                if (!int.TryParse(ExtraData, out stackMultiple))
                    return 0;

                return GetBaseItem().ToggleHeight[stackMultiple];
            }
        }

        /// <summary>
        ///     Gets the total height.
        /// </summary>
        /// <value>The total height.</value>
        internal double TotalHeight
        {
            get
            {
                if (GetBaseItem() == null)
                    return Z;

                if (!GetBaseItem().StackMultipler)
                    return Z + GetBaseItem().Height;

                if (string.IsNullOrEmpty(ExtraData))
                    ExtraData = "0";

                int stackMultiple;

                if (!int.TryParse(ExtraData, out stackMultiple))
                    return Z;

                return Z + GetBaseItem().ToggleHeight[stackMultiple];
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is wall item.
        /// </summary>
        /// <value><c>true</c> if this instance is wall item; otherwise, <c>false</c>.</value>
        internal bool IsWallItem { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is floor item.
        /// </summary>
        /// <value><c>true</c> if this instance is floor item; otherwise, <c>false</c>.</value>
        internal bool IsFloorItem { get; set; }

        /// <summary>
        ///     Gets the square in front.
        /// </summary>
        /// <value>The square in front.</value>
        internal Point SquareInFront
        {
            get
            {
                Point result = new Point(X, Y);
                {
                    switch (Rot)
                    {
                        case 0:
                            result.Y--;
                            break;

                        case 2:
                            result.X++;
                            break;

                        case 4:
                            result.Y++;
                            break;

                        case 6:
                            result.X--;
                            break;
                    }

                    return result;
                }
            }
        }

        /// <summary>
        ///     Gets the square behind.
        /// </summary>
        /// <value>The square behind.</value>
        internal Point SquareBehind
        {
            get
            {
                Point result = new Point(X, Y);
                {
                    switch (Rot)
                    {
                        case 0:
                            result.Y++;
                            break;

                        case 2:
                            result.X--;
                            break;

                        case 4:
                            result.Y--;
                            break;

                        case 6:
                            result.X++;
                            break;
                    }

                    return result;
                }
            }
        }

        /// <summary>
        ///     Gets the interactor.
        /// </summary>
        /// <value>The interactor.</value>
        internal IFurniInteractor Interactor
        {
            get
            {
                if (IsWired)
                    return new InteractorWired();

                Interaction interactionType = GetBaseItem().InteractionType;

                switch (interactionType)
                {
                    case Interaction.Gate:
                        return new InteractorGate();

                    case Interaction.GuildGate:
                        return new InteractorGroupGate();

                    case Interaction.ScoreBoard:
                        return new InteractorScoreboard();

                    case Interaction.VendingMachine:
                        return new InteractorVendor();

                    case Interaction.Alert:
                        return new InteractorAlert();

                    case Interaction.OneWayGate:
                        return new InteractorOneWayGate();

                    case Interaction.LoveShuffler:
                        return new InteractorLoveShuffler();

                    case Interaction.HabboWheel:
                        return new InteractorHabboWheel();

                    case Interaction.Dice:
                        return new InteractorDice();

                    case Interaction.Bottle:
                        return new InteractorSpinningBottle();

                    case Interaction.Hopper:
                        return new InteractorHopper();

                    case Interaction.Teleport:
                        return new InteractorTeleport();

                    case Interaction.Football:
                        return new InteractorFootball();

                    case Interaction.FootballCounterGreen:
                    case Interaction.FootballCounterYellow:
                    case Interaction.FootballCounterBlue:
                    case Interaction.FootballCounterRed:
                        return new InteractorScoreCounter();

                    case Interaction.BanzaiScoreBlue:
                    case Interaction.BanzaiScoreRed:
                    case Interaction.BanzaiScoreYellow:
                    case Interaction.BanzaiScoreGreen:
                        return new InteractorBanzaiScoreCounter();

                    case Interaction.BanzaiCounter:
                        return new InteractorBanzaiTimer();

                    case Interaction.FreezeTimer:
                        return new InteractorFreezeTimer();

                    case Interaction.FreezeYellowCounter:
                    case Interaction.FreezeRedCounter:
                    case Interaction.FreezeBlueCounter:
                    case Interaction.FreezeGreenCounter:
                        return new InteractorFreezeScoreCounter();

                    case Interaction.FreezeTileBlock:
                    case Interaction.FreezeTile:
                        return new InteractorFreezeTile();

                    case Interaction.JukeBox:
                        return new InteractorJukebox();

                    case Interaction.PuzzleBox:
                        return new InteractorPuzzleBox();

                    case Interaction.Mannequin:
                        return new InteractorMannequin();

                    case Interaction.Fireworks:
                        return new InteractorFireworks();

                    case Interaction.GroupForumTerminal:
                        return new InteractorGroupForumTerminal();

                    case Interaction.VikingCotie:
                        return new InteractorVikingCotie();

                    case Interaction.Cannon:
                        return new InteractorCannon();

                    case Interaction.FxBox:
                        return new InteractorFxBox();

                    case Interaction.HcGate:
                        return new InteractorHcGate();

                    case Interaction.QuickTeleport:
                        return new InteractorQuickTeleport();

                    case Interaction.CrackableEgg:
                        return new InteractorCrackableEgg();

                    case Interaction.FloorSwitch1:
                    case Interaction.FloorSwitch2:
                        return new InteractorSwitch();

                    case Interaction.WalkInternalLink:
                        return new InteractorWalkInternalLink();

                    default:
                        return new InteractorGenericSwitch();
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomItem" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="baseName"></param>
        /// <param name="extraData">The extra data.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="rot">The rot.</param>
        /// <param name="pRoom">The p room.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="eGroup">The group.</param>
        /// <param name="songCode">The song code.</param>
        /// <param name="isBuilder">if set to <c>true</c> [is builder].</param>
        internal RoomItem(uint id, uint roomId, string baseName, string extraData, int x, int y, double z, int rot, Room pRoom, uint userid, uint eGroup, string songCode, bool isBuilder)
        {
            Id = id;
            RoomId = roomId;
            BaseName = baseName;
            ExtraData = extraData;
            GroupId = eGroup;

            X = x;
            Y = y;
            Z = z;

            Rot = rot;

            UpdateNeeded = false;
            IsTrans = false;

            InteractingUser = 0u;
            InteractingUser2 = 0u;
            InteractingBallUser = 0u;

            InteractionCount = 0;
            UpdateCounter = 0;
            Value = 0;

            UserId = userid;
            SongCode = songCode;
            IsBuilder = isBuilder;

            _mBaseItem = Yupi.GetGame().GetItemManager().GetItemByName(baseName);

            _mRoom = pRoom;

            if (GetBaseItem() == null)
                YupiLogManager.LogMessage($"Unknown Furniture Item: {baseName} (Id: #{id}).", "Yupi.Items");

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery($"SELECT * FROM items_limited WHERE item_id = {id} LIMIT 1");

                DataRow row = queryReactor.GetRow();

                LimitedNo = 0;
                LimitedTot = 0;

                if (row != null)
                {
                    LimitedNo = (int) row["limited_number"];
                    LimitedTot = (int) row["limited_total"];
                }
            }

            if (GetBaseItem().Name.ContainsAny("guild_", "grp", "gld_"))
            {
                GroupData = extraData;
                ExtraData = GroupData.Split(';')[0];

                if (GroupData.Contains(";;"))
                {
                    GroupData = GroupData.Replace(";;", ";");

                    _mRoom.GetRoomItemHandler().AddOrUpdateItem(Id);
                }
            }

            switch (GetBaseItem().InteractionType)
            {
                case Interaction.PressurePadBed:
                case Interaction.Bed:
                    pRoom.ContainsBeds++;
                    break;

                case Interaction.Hopper:
                    IsTrans = true;
                    ReqUpdate(0, true);
                    break;

                case Interaction.Teleport:
                case Interaction.QuickTeleport:
                    IsTrans = true;
                    ReqUpdate(0, true);
                    break;

                case Interaction.Roller:
                    IsRoller = true;
                    pRoom.GetRoomItemHandler().GotRollers = true;
                    break;

                case Interaction.FootballCounterGreen:
                case Interaction.BanzaiGateGreen:
                case Interaction.BanzaiScoreGreen:
                case Interaction.FreezeGreenCounter:
                case Interaction.FreezeGreenGate:
                    Team = Team.Green;
                    break;

                case Interaction.FootballCounterYellow:
                case Interaction.BanzaiGateYellow:
                case Interaction.BanzaiScoreYellow:
                case Interaction.FreezeYellowCounter:
                case Interaction.FreezeYellowGate:
                    Team = Team.Yellow;
                    break;

                case Interaction.FootballCounterBlue:
                case Interaction.BanzaiGateBlue:
                case Interaction.BanzaiScoreBlue:
                case Interaction.FreezeBlueCounter:
                case Interaction.FreezeBlueGate:
                    Team = Team.Blue;
                    break;

                case Interaction.FootballCounterRed:
                case Interaction.BanzaiGateRed:
                case Interaction.BanzaiScoreRed:
                case Interaction.FreezeRedCounter:
                case Interaction.FreezeRedGate:
                    Team = Team.Red;
                    break;

                case Interaction.BanzaiFloor:
                case Interaction.BanzaiCounter:
                case Interaction.BanzaiPuck:
                case Interaction.BanzaiPyramid:
                case Interaction.FreezeTimer:
                case Interaction.FreezeExit:
                    break;

                case Interaction.BanzaiTele:
                    ExtraData = string.Empty;
                    break;

                case Interaction.BreedingTerrier:
                    if (!pRoom.GetRoomItemHandler().BreedingTerrier.ContainsKey(Id))
                        pRoom.GetRoomItemHandler().BreedingTerrier.Add(Id, this);
                    break;

                case Interaction.BreedingBear:
                    if (!pRoom.GetRoomItemHandler().BreedingBear.ContainsKey(Id))
                        pRoom.GetRoomItemHandler().BreedingBear.Add(Id, this);
                    break;

                case Interaction.VikingCotie:
                    int num = Convert.ToInt32(extraData);

                    VikingCotieBurning = num >= 1 && num < 5;
                    break;
            }

            IsWallItem = GetBaseItem().Type.ToString().ToLower() == "i";
            IsFloorItem = GetBaseItem().Type.ToString().ToLower() == "s";

            AffectedTiles = Gamemap.GetAffectedTiles(GetBaseItem().Length, GetBaseItem().Width, X, Y, rot);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomItem" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="baseName"></param>
        /// <param name="extraData">The extra data.</param>
        /// <param name="wallCoord">The wall coord.</param>
        /// <param name="pRoom">The p room.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="eGroup">The group.</param>
        /// <param name="isBuilder">if set to <c>true</c> [is builder].</param>
        internal RoomItem(uint id, uint roomId, string baseName, string extraData, WallCoordinate wallCoord, Room pRoom, uint userid, uint eGroup, bool isBuilder)
        {
            BaseName = baseName;

            _mBaseItem = Yupi.GetGame().GetItemManager().GetItemByName(baseName);

            _mRoom = pRoom;

            if (GetBaseItem() == null)
                YupiLogManager.LogMessage($"Unknown Furniture Item: {baseName} (Id: #{id}).", "Yupi.Items");

            Id = id;
            RoomId = roomId;
            ExtraData = extraData;
            GroupId = eGroup;

            X = 0;
            Y = 0;
            Z = 0.0;

            UpdateNeeded = false;
            IsTrans = false;
            IsWallItem = true;
            IsFloorItem = false;

            InteractingUser = 0u;
            InteractingUser2 = 0u;
            InteractingBallUser = 0u;

            UpdateCounter = 0;
            InteractionCount = 0;
            Value = 0;

            WallCoord = wallCoord;
            UserId = userid;
            IsBuilder = isBuilder;

            AffectedTiles = new Dictionary<int, ThreeDCoord>();

            SongCode = string.Empty;
        }

        /// <summary>
        ///     Equalses the specified compared item.
        /// </summary>
        /// <param name="comparedItem">The compared item.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(RoomItem comparedItem) => comparedItem.Id == Id;

        internal event OnItemTrigger ItemTriggerEventHandler;

        internal event UserWalksFurniDelegate OnUserWalksOffFurni;

        internal event UserWalksFurniDelegate OnUserWalksOnFurni;

        internal void SetState(int x, int y, double z) => SetState(x, y, z, Gamemap.GetAffectedTiles(GetBaseItem().Length, GetBaseItem().Width, x, y, Rot));

        /// <summary>
        ///     Sets the state.
        /// </summary>
        /// <param name="x">The p x.</param>
        /// <param name="y">The p y.</param>
        /// <param name="z">The p z.</param>
        /// <param name="tiles">The tiles.</param>
        internal void SetState(int x, int y, double z, Dictionary<int, ThreeDCoord> tiles)
        {
            X = x;
            Y = y;
            Z = z;

            AffectedTiles = tiles;
        }

        /// <summary>
        ///     Called when [trigger].
        /// </summary>
        /// <param name="user">The user.</param>
        internal void OnTrigger(RoomUser user) => ItemTriggerEventHandler?.Invoke(null, new ItemTriggeredArgs(user, this));

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        internal void Destroy()
        {
            _mRoom = null;

            AffectedTiles.Clear();

            ItemTriggerEventHandler = null;
            OnUserWalksOffFurni = null;
            OnUserWalksOnFurni = null;
        }

        /// <summary>
        ///     Processes the updates.
        /// </summary>
        internal void ProcessUpdates()
        {
            UpdateCounter--;

            if (UpdateCounter <= 0 || IsTrans)
            {
                UpdateNeeded = false;
                UpdateCounter = 0;

                Interaction interactionType = GetBaseItem().InteractionType;

                switch (interactionType)
                {
                    case Interaction.ScoreBoard:
                        {
                            if (string.IsNullOrEmpty(ExtraData))
                                break;

                            int num;

                            int.TryParse(ExtraData, out num);

                            if (num > 0)
                            {
                                if (InteractionCountHelper == 1)
                                {
                                    num--;

                                    InteractionCountHelper = 0;

                                    ExtraData = num.ToString();

                                    UpdateState();
                                }
                                else
                                    InteractionCountHelper += 1;

                                UpdateCounter = 1;

                                break;
                            }

                            UpdateCounter = 0;

                            break;
                        }
                    case Interaction.VendingMachine:
                        {
                            if (ExtraData == "1")
                            {
                                RoomUser user = GetRoom().GetRoomUserManager().GetRoomUserByHabbo(InteractingUser);

                                if (user != null)
                                {
                                    user.UnlockWalking();

                                    int drink = GetBaseItem().VendingIds[Yupi.GetRandomNumber(0, GetBaseItem().VendingIds.Count - 1)];

                                    user.CarryItem(drink);
                                }

                                InteractingUser = 0u;

                                ExtraData = "0";

                                UpdateState(false, true);
                            }

                            break;
                        }
                    case Interaction.Alert:
                        {
                            if (ExtraData == "1")
                            {
                                ExtraData = "0";

                                UpdateState(false, true);
                            }

                            break;
                        }
                    case Interaction.OneWayGate:
                        {
                            RoomUser roomUser3 = null;

                            if (InteractingUser > 0u)
                                roomUser3 = GetRoom().GetRoomUserManager().GetRoomUserByHabbo(InteractingUser);

                            if (roomUser3 != null && roomUser3.X == X && roomUser3.Y == Y)
                            {
                                ExtraData = "1";

                                roomUser3.MoveTo(SquareBehind);
                                roomUser3.InteractingGate = false;
                                roomUser3.GateId = 0u;

                                ReqUpdate(1, false);

                                UpdateState(false, true);
                            }
                            else if (roomUser3 != null && roomUser3.Coordinate == SquareBehind)
                            {
                                roomUser3.UnlockWalking();

                                ExtraData = "0";
                                InteractingUser = 0u;
                                roomUser3.InteractingGate = false;
                                roomUser3.GateId = 0u;

                                UpdateState(false, true);
                            }
                            else if (ExtraData == "1")
                            {
                                ExtraData = "0";

                                UpdateState(false, true);
                            }

                            if (roomUser3 == null)
                                InteractingUser = 0u;

                            break;
                        }
                    case Interaction.LoveShuffler:
                        {
                            ExtraData = ExtraData == "0" ? RandomNumberGenerator.Get(1, 4).ToString() : "-1";

                            ReqUpdate(20, false);

                            UpdateState(false, true);

                            break;
                        }
                    case Interaction.HabboWheel:
                        {
                            ExtraData = RandomNumberGenerator.Get(1, 10).ToString();

                            UpdateState();

                            break;
                        }
                    case Interaction.Dice:
                        {
                            ExtraData = RandomNumberGenerator.Get(1, 7).ToString();

                            UpdateState();

                            break;
                        }
                    case Interaction.Bottle:
                        {
                            ExtraData = RandomNumberGenerator.Get(0, 7).ToString();

                            UpdateState();

                            break;
                        }
                    case Interaction.Teleport:
                    case Interaction.QuickTeleport:
                        {
                            bool keepDoorOpen = false, showTeleEffect = false;

                            #region TeleportItem
                            if (InteractingUser > 0)
                            {
                                RoomUser user = GetRoom().GetRoomUserManager().GetRoomUserByHabbo(InteractingUser);

                                if (user != null)
                                {
                                    if (user.Coordinate == Coordinate)
                                    {
                                        user.AllowOverride = false;

                                        if (TeleHandler.IsTeleLinked(Id, _mRoom))
                                        {
                                            showTeleEffect = true;

                                            uint linkedTele = TeleHandler.GetLinkedTele(Id, _mRoom);
                                            uint teleRoomId = TeleHandler.GetTeleRoomId(linkedTele, _mRoom);

                                            if (teleRoomId == RoomId)
                                            {
                                                RoomItem item = GetRoom().GetRoomItemHandler().GetItem(linkedTele);

                                                if (item == null)
                                                    user.UnlockWalking();
                                                else
                                                {
                                                    user.SetPos(item.X, item.Y, item.Z);
                                                    user.SetRot(item.Rot, false);
                                                    item.ExtraData = "2";
                                                    item.UpdateState(false, true);
                                                    item.InteractingUser2 = InteractingUser;
                                                }
                                            }
                                            else
                                            {
                                                if (!user.IsBot && user.GetClient() != null && user.GetClient().GetHabbo() != null && user.GetClient().GetMessageHandler() != null)
                                                {
                                                    user.GetClient().GetHabbo().IsTeleporting = true;
                                                    user.GetClient().GetHabbo().TeleportingRoomId = teleRoomId;
                                                    user.GetClient().GetHabbo().TeleporterId = linkedTele;
                                                    user.GetClient().GetMessageHandler().PrepareRoomForUser(teleRoomId, string.Empty);
                                                }

                                                InteractingUser = 0u;
                                            }
                                        }
                                        else
                                        {
                                            user.UnlockWalking();
                                            InteractingUser = 0;
                                        }
                                    }
                                    else if (user.Coordinate == SquareInFront)
                                    {
                                        user.AllowOverride = true;
                                        keepDoorOpen = true;

                                        if (user.IsWalking && (user.GoalX != X || user.GoalY != Y))
                                            user.ClearMovement();

                                        user.CanWalk = false;
                                        user.AllowOverride = true;

                                        user.MoveTo(Coordinate.X, Coordinate.Y, true);
                                    }
                                    else
                                        InteractingUser = 0;
                                }
                                else
                                    InteractingUser = 0;
                            }
                            #endregion

                            if (InteractingUser2 > 0)
                            {
                                RoomUser user2 = GetRoom().GetRoomUserManager().GetRoomUserByHabbo(InteractingUser2);

                                if (user2 != null)
                                {
                                    keepDoorOpen = true;

                                    user2.UnlockWalking();
                                    user2.MoveTo(SquareInFront);
                                }

                                InteractingUser2 = 0;
                            }

                            if (keepDoorOpen)
                            {
                                if (ExtraData != "1")
                                {
                                    ExtraData = "1";

                                    UpdateState(false, true);
                                }
                            }
                            else if (showTeleEffect)
                            {
                                if (ExtraData != "2")
                                {
                                    ExtraData = "2";
                                    UpdateState(false, true);
                                }
                            }
                            else if (ExtraData != "0")
                            {
                                ExtraData = "0";

                                UpdateState(false, true);
                            }

                            ReqUpdate(1, false);

                            break;
                        }
                    case Interaction.BanzaiFloor:
                        {
                            if (Value == 3)
                            {
                                if (InteractionCountHelper == 1)
                                {
                                    InteractionCountHelper = 0;

                                    switch (Team)
                                    {
                                        case Team.Red:
                                            ExtraData = "5";
                                            break;

                                        case Team.Green:
                                            ExtraData = "8";
                                            break;

                                        case Team.Blue:
                                            ExtraData = "11";
                                            break;

                                        case Team.Yellow:
                                            ExtraData = "14";
                                            break;
                                    }
                                }
                                else
                                {
                                    ExtraData = string.Empty;

                                    InteractionCountHelper += 1;
                                }

                                UpdateState();

                                InteractionCount += 1;

                                if (InteractionCount < 16)
                                {
                                    UpdateCounter = 1;

                                    break;
                                }

                                UpdateCounter = 0;
                            }

                            break;
                        }
                    case Interaction.BanzaiScoreBlue:
                    case Interaction.BanzaiScoreRed:
                    case Interaction.BanzaiScoreYellow:
                    case Interaction.BanzaiScoreGreen:
                    case Interaction.BanzaiPyramid:
                    case Interaction.FreezeExit:
                    case Interaction.FreezeRedCounter:
                    case Interaction.FreezeBlueCounter:
                    case Interaction.FreezeYellowCounter:
                    case Interaction.FreezeGreenCounter:
                    case Interaction.FreezeYellowGate:
                    case Interaction.FreezeRedGate:
                    case Interaction.FreezeGreenGate:
                    case Interaction.FreezeBlueGate:
                    case Interaction.FreezeTileBlock:
                    case Interaction.JukeBox:
                    case Interaction.MusicDisc:
                    case Interaction.PuzzleBox:
                    case Interaction.RoomBg:
                    case Interaction.ActionKickUser:
                    case Interaction.ActionGiveReward:
                    case Interaction.ArrowPlate:
                        break;

                    case Interaction.BanzaiCounter:
                        {
                            if (string.IsNullOrEmpty(ExtraData))
                                break;

                            int num4;
                            int.TryParse(ExtraData, out num4);

                            if (num4 > 0)
                            {
                                if (InteractionCountHelper == 1)
                                {
                                    num4--;

                                    InteractionCountHelper = 0;

                                    if (!GetRoom().GetBanzai().IsBanzaiActive)
                                        break;

                                    ExtraData = num4.ToString();
                                    UpdateState();
                                }
                                else
                                    InteractionCountHelper += 1;

                                UpdateCounter = 1;

                                break;
                            }

                            UpdateCounter = 0;
                            GetRoom().GetBanzai().BanzaiEnd();

                            break;
                        }
                    case Interaction.BanzaiTele:
                        {
                            ExtraData = string.Empty;
                            UpdateState();

                            break;
                        }
                    case Interaction.BanzaiPuck:
                        {
                            if (InteractionCount > 4)
                            {
                                InteractionCount += 1;
                                UpdateCounter = 1;

                                break;
                            }

                            InteractionCount = 0;
                            UpdateCounter = 0;

                            break;
                        }
                    case Interaction.FreezeTimer:
                        {
                            if (string.IsNullOrEmpty(ExtraData))
                                break;

                            int num5;

                            int.TryParse(ExtraData, out num5);

                            if (num5 > 0)
                            {
                                if (InteractionCountHelper == 1)
                                {
                                    num5--;

                                    InteractionCountHelper = 0;

                                    if (!GetRoom().GetFreeze().GameStarted)
                                        break;

                                    ExtraData = num5.ToString();
                                    UpdateState();
                                }
                                else
                                    InteractionCountHelper += 1;

                                UpdateCounter = 1;

                                break;
                            }

                            UpdateNeeded = false;
                            GetRoom().GetFreeze().StopGame();

                            break;
                        }
                    case Interaction.FreezeTile:
                        {
                            if (InteractingUser > 0u)
                            {
                                ExtraData = "11000";

                                UpdateState(false, true);

                                GetRoom().GetFreeze().OnFreezeTiles(this, FreezePowerUp, InteractingUser);

                                InteractingUser = 0u;
                                InteractionCountHelper = 0;

                            }

                            break;
                        }
                    case Interaction.WearItem:
                        {
                            ExtraData = "1";
                            UpdateState();

                            string text = string.Empty;

                            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(InteractingUser);

                            if (clientByUserId == null)
                                break;

                            if (!clientByUserId.GetHabbo().Look.Contains("ha"))
                                text = $"{clientByUserId.GetHabbo().Look}.ha-1006-1326";
                            else
                            {
                                string[] array = clientByUserId.GetHabbo().Look.Split('.');

                                string[] array2 = array;

                                foreach (string text2 in array2)
                                {
                                    string str = text2;

                                    if (text2.Contains("ha"))
                                        str = "ha-1006-1326";

                                    text = $"{text}{str}.";
                                }
                            }

                            if (text.EndsWith("."))
                                text = text.TrimEnd('.');

                            clientByUserId.GetHabbo().Look = text;

                            clientByUserId.GetMessageHandler().GetResponse().Init(PacketLibraryManager.OutgoingRequest("UpdateUserDataMessageComposer"));
                            clientByUserId.GetMessageHandler().GetResponse().AppendInteger(-1);
                            clientByUserId.GetMessageHandler().GetResponse().AppendString(clientByUserId.GetHabbo().Look);
                            clientByUserId.GetMessageHandler().GetResponse().AppendString(clientByUserId.GetHabbo().Gender.ToLower());
                            clientByUserId.GetMessageHandler().GetResponse().AppendString(clientByUserId.GetHabbo().Motto);
                            clientByUserId.GetMessageHandler().GetResponse().AppendInteger(clientByUserId.GetHabbo().AchievementPoints);
                            clientByUserId.GetMessageHandler().SendResponse();

                            ServerMessage serverMessage = new ServerMessage(PacketLibraryManager.OutgoingRequest("UpdateUserDataMessageComposer"));
                            serverMessage.AppendInteger(InteractingUser2);
                            serverMessage.AppendString(clientByUserId.GetHabbo().Look);
                            serverMessage.AppendString(clientByUserId.GetHabbo().Gender.ToLower());
                            serverMessage.AppendString(clientByUserId.GetHabbo().Motto);
                            serverMessage.AppendInteger(clientByUserId.GetHabbo().AchievementPoints);

                            GetRoom().SendMessage(serverMessage);

                            break;
                        }
                    case Interaction.TriggerTimer:
                    case Interaction.TriggerRoomEnter:
                    case Interaction.TriggerGameEnd:
                    case Interaction.TriggerGameStart:
                    case Interaction.TriggerRepeater:
                    case Interaction.TriggerLongRepeater:
                    case Interaction.TriggerOnUserSay:
                    case Interaction.TriggerScoreAchieved:
                    case Interaction.TriggerStateChanged:
                    case Interaction.TriggerWalkOnFurni:
                    case Interaction.TriggerWalkOffFurni:
                    case Interaction.TriggerCollision:
                    case Interaction.ActionGiveScore:
                    case Interaction.ActionPosReset:
                    case Interaction.ActionMoveRotate:
                    case Interaction.ActionMoveToDir:
                    case Interaction.ActionResetTimer:
                    case Interaction.ActionShowMessage:
                    case Interaction.ActionEffectUser:
                    case Interaction.ActionTeleportTo:
                    case Interaction.ActionToggleState:
                    case Interaction.ActionChase:
                    case Interaction.ConditionFurnisHaveUsers:
                    case Interaction.ConditionStatePos:
                    case Interaction.ConditionTimeLessThan:
                    case Interaction.ConditionTimeMoreThan:
                    case Interaction.ConditionTriggerOnFurni:
                    case Interaction.ConditionFurniHasFurni:
                    case Interaction.ConditionItemsMatches:
                    case Interaction.ConditionGroupMember:
                    case Interaction.ConditionFurniTypeMatches:
                    case Interaction.ConditionHowManyUsersInRoom:
                    case Interaction.ConditionTriggererNotOnFurni:
                    case Interaction.ConditionFurniHasNotFurni:
                    case Interaction.ConditionFurnisHaveNotUsers:
                    case Interaction.ConditionItemsDontMatch:
                    case Interaction.ConditionFurniTypeDontMatch:
                    case Interaction.ConditionNotGroupMember:
                    case Interaction.ConditionUserWearingEffect:
                    case Interaction.ConditionUserWearingBadge:
                    case Interaction.ConditionUserNotWearingEffect:
                    case Interaction.ConditionUserNotWearingBadge:
                    case Interaction.ConditionDateRangeActive:
                    case Interaction.ConditionUserHasFurni:
                        {
                            ExtraData = "0";

                            UpdateState(false, true);

                            break;
                        }
                    case Interaction.PressurePadBed:
                    case Interaction.PressurePad:
                        {
                            ExtraData = "1";

                            UpdateState();

                            break;
                        }
                }
            }
        }

        /// <summary>
        ///     Reqs the update.
        /// </summary>
        /// <param name="cycles">The cycles.</param>
        /// <param name="setUpdate">if set to <c>true</c> [set update].</param>
        internal void ReqUpdate(int cycles, bool setUpdate)
        {
            UpdateCounter = cycles;

            if (setUpdate)
                UpdateNeeded = true;
        }

        /// <summary>
        ///     Updates the state.
        /// </summary>
        internal void UpdateState() => UpdateState(true, true);

        /// <summary>
        ///     Updates the state.
        /// </summary>
        /// <param name="inDb">if set to <c>true</c> [in database].</param>
        /// <param name="inRoom">if set to <c>true</c> [in room].</param>
        internal void UpdateState(bool inDb, bool inRoom)
        {
            if (GetRoom() == null)
                return;

            if (inDb)
                GetRoom().GetRoomItemHandler().AddOrUpdateItem(Id);

            if (!inRoom)
                return;

            ServerMessage serverMessage = new ServerMessage(0);

            if (IsFloorItem)
            {
                serverMessage.Init(PacketLibraryManager.OutgoingRequest("UpdateFloorItemExtraDataMessageComposer"));
                serverMessage.AppendString(Id.ToString());

                switch (GetBaseItem().InteractionType)
                {
                    case Interaction.MysteryBox:
                        {
                            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                            {
                                queryReactor.SetQuery($"SELECT extra_data FROM items_rooms WHERE id = {Id} LIMIT 1");

                                ExtraData = queryReactor.GetString();
                            }

                            if (ExtraData.Contains('\u0005'.ToString()))
                            {
                                string[] mysteryBoxData = ExtraData.Split('\u0005');

                                int num = int.Parse(mysteryBoxData[0]);
                                int num2 = int.Parse(mysteryBoxData[1]);

                                ExtraData = (3 * num - num2).ToString();
                            }

                            break;
                        }              
                    case Interaction.Mannequin:
                        {
                            serverMessage.AppendInteger(1);
                            serverMessage.AppendInteger(3);

                            if (ExtraData.Contains('\u0005'.ToString()))
                            {
                                string[] mannequinData = ExtraData.Split('\u0005');

                                serverMessage.AppendString("GENDER");
                                serverMessage.AppendString(mannequinData[0]);
                                serverMessage.AppendString("FIGURE");
                                serverMessage.AppendString(mannequinData[1]);
                                serverMessage.AppendString("OUTFIT_NAME");
                                serverMessage.AppendString(mannequinData[2]);

                                break;
                            }

                            serverMessage.AppendString("GENDER");
                            serverMessage.AppendString(string.Empty);
                            serverMessage.AppendString("FIGURE");
                            serverMessage.AppendString(string.Empty);
                            serverMessage.AppendString("OUTFIT_NAME");
                            serverMessage.AppendString(string.Empty);

                            break;
                        }
                    case Interaction.Pinata:
                        {
                            serverMessage.AppendInteger(7);

                            if (ExtraData.Length <= 0)
                            {
                                serverMessage.AppendString("6");
                                serverMessage.AppendInteger(0);
                                serverMessage.AppendInteger(100);

                                break;
                            }

                            serverMessage.AppendString(int.Parse(ExtraData) == 100 ? "8" : "6");
                            serverMessage.AppendInteger(int.Parse(ExtraData));
                            serverMessage.AppendInteger(100);

                            break;
                        }
                    case Interaction.WiredHighscore:
                        {
                            if (HighscoreData == null)
                                HighscoreData = new HighscoreData(this);

                            HighscoreData.GenerateExtraData(this, serverMessage);

                            break;
                        }
                    case Interaction.CrackableEgg:
                        {
                            Yupi.GetGame().GetCrackableEggHandler().GetServerMessage(serverMessage, this);

                            break;
                        }
                    case Interaction.YoutubeTv:
                        {
                            serverMessage.AppendInteger(1);
                            serverMessage.AppendInteger(1);
                            serverMessage.AppendString("THUMBNAIL_URL");
                            serverMessage.AppendString(ExtraData);

                            break;
                        }
                    default:
                        {
                            serverMessage.AppendInteger(0);
                            serverMessage.AppendString(ExtraData);

                            break;
                        }  
                }
            }

            if (IsWallItem)
            {
                serverMessage.Init(PacketLibraryManager.OutgoingRequest("UpdateRoomWallItemMessageComposer"));

                Serialize(serverMessage);
            }

            GetRoom().SendMessage(serverMessage);
        }

        /// <summary>
        ///     Serializes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void Serialize(ServerMessage message)
        {
            if (IsFloorItem)
            {
                message.AppendInteger(Id);
                message.AppendInteger(GetBaseItem().SpriteId);
                message.AppendInteger(X);
                message.AppendInteger(Y);
                message.AppendInteger(Rot);
                message.AppendString(ServerUserChatTextHandler.GetString(Z));
                message.AppendString(ServerUserChatTextHandler.GetString(GetBaseItem().Height));

                switch (GetBaseItem().InteractionType)
                {
                    case Interaction.GuildGate:
                    case Interaction.GuildItem:
                    case Interaction.GroupForumTerminal:
                    case Interaction.GuildForum:
                        {
                            Group itemGroup = Yupi.GetGame().GetGroupManager().GetGroup(GroupId);

                            if (itemGroup == null)
                            {
                                message.AppendInteger(1);
                                message.AppendInteger(0);
                                message.AppendString(ExtraData);

                                break;
                            }

                            message.AppendInteger(0);
                            message.AppendInteger(2);
                            message.AppendInteger(5);
                            message.AppendString(ExtraData);
                            message.AppendString(GroupId.ToString());
                            message.AppendString(itemGroup.Badge);
                            message.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(itemGroup.Colour1, true));
                            message.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(itemGroup.Colour2, false));

                            break;
                        }
                    case Interaction.YoutubeTv:
                        {
                            message.AppendInteger(0);

                            if (string.IsNullOrEmpty(ExtraData))
                            {
                                message.AppendInteger(0);
                                message.AppendString(string.Empty);

                                break;
                            }

                            message.AppendInteger(1);
                            message.AppendInteger(1);
                            message.AppendString("THUMBNAIL_URL");
                            message.AppendString(ExtraData);

                            break;
                        }
                    case Interaction.MusicDisc:
                        {
                            message.AppendInteger(SoundMachineSongManager.GetSongId(SongCode));
                            message.AppendInteger(0);
                            message.AppendString(ExtraData);

                            break;
                        }
                    case Interaction.Background:
                    case Interaction.WalkInternalLink:
                        {
                            message.AppendInteger(0);
                            message.AppendInteger(1);

                            if (!string.IsNullOrEmpty(ExtraData))
                            {
                                message.AppendInteger(ExtraData.Split('\t').Length / 2);

                                for (int i = 0; i <= ExtraData.Split('\t').Length - 1; i++)
                                    message.AppendString(ExtraData.Split('\t')[i]);

                                break;
                            }

                            message.AppendInteger(0);

                            break;
                        }
                    case Interaction.Gift:
                        {
                            string[] giftData = ExtraData.Split('\t');

                            string giftMessage = string.Empty, giverName = string.Empty, giverLook = string.Empty, product = string.Empty;

                            int giftRibbon = 1;
                            int giftColor = 2;

                            bool showGiver = false;

                            if (giftData.Length >= 6)
                            {
                                giftMessage = giftData[1];
                                giverName = giftData[5];
                                giverLook = giftData[6];
                                product = giftData[7];

                                giftRibbon = int.Parse(giftData[2]);
                                giftColor = int.Parse(giftData[3]);

                                showGiver = Yupi.EnumToBool(giftData[4]);
                            }

                            int ribbonAndColor = giftRibbon * 1000 + giftColor;

                            message.AppendInteger(ribbonAndColor);
                            message.AppendInteger(1);
                            message.AppendInteger(showGiver ? 6 : 4);
                            message.AppendString("EXTRA_PARAM");
                            message.AppendString(string.Empty);
                            message.AppendString("MESSAGE");
                            message.AppendString(giftMessage);

                            if (showGiver)
                            {
                                message.AppendString("PURCHASER_NAME");
                                message.AppendString(giverName);
                                message.AppendString("PURCHASER_FIGURE");
                                message.AppendString(giverLook);
                            }

                            message.AppendString("PRODUCT_CODE");
                            message.AppendString(product);
                            message.AppendString("state");
                            message.AppendString(MagicRemove ? "1" : "0");

                            break;
                        }
                    case Interaction.Pinata:
                        {
                            message.AppendInteger(0);
                            message.AppendInteger(7);
                            message.AppendString(ExtraData == "100" ? "8" : "6");

                            if (ExtraData.Length <= 0)
                            {
                                message.AppendInteger(0);
                                message.AppendInteger(100);

                                break;
                            }

                            message.AppendInteger(int.Parse(ExtraData));
                            message.AppendInteger(100);

                            break;
                        }
                    case Interaction.Mannequin:
                        {
                            message.AppendInteger(0);
                            message.AppendInteger(1);
                            message.AppendInteger(3);

                            if (ExtraData.Contains('\u0005'.ToString()))
                            {
                                string[] mannequinData = ExtraData.Split('\u0005');

                                message.AppendString("GENDER");
                                message.AppendString(mannequinData[0]);
                                message.AppendString("FIGURE");
                                message.AppendString(mannequinData[1]);
                                message.AppendString("OUTFIT_NAME");
                                message.AppendString(mannequinData[2]);

                                break;
                            }

                            message.AppendString("GENDER");
                            message.AppendString(string.Empty);
                            message.AppendString("FIGURE");
                            message.AppendString(string.Empty);
                            message.AppendString("OUTFIT_NAME");
                            message.AppendString(string.Empty);

                            break;
                        }
                    case Interaction.BadgeDisplay:
                        {
                            string[] badgeData = ExtraData.Split('|');

                            message.AppendInteger(0);
                            message.AppendInteger(2);
                            message.AppendInteger(4);
                            message.AppendString("0");
                            message.AppendString(badgeData[0]);
                            message.AppendString(badgeData.Length > 1 ? badgeData[1] : string.Empty);
                            message.AppendString(badgeData.Length > 1 ? badgeData[2] : string.Empty);

                            break;
                        }
                    case Interaction.LoveLock:
                        {
                            string[] data = ExtraData.Split('\u0005');

                            message.AppendInteger(0);
                            message.AppendInteger(2);
                            message.AppendInteger(data.Length);

                            foreach (string datak in data)
                                message.AppendString(datak);

                            break;
                        }
                    case Interaction.Moplaseed:
                    case Interaction.RareMoplaSeed:
                        {
                            message.AppendInteger(0);
                            message.AppendInteger(1);
                            message.AppendInteger(1);
                            message.AppendString("rarity");
                            message.AppendString(ExtraData);

                            break;
                        }
                    case Interaction.RoomBg:
                        {
                            if (_mRoom.TonerData == null)
                                _mRoom.TonerData = new TonerData(Id);

                            _mRoom.TonerData.GenerateExtraData(message);

                            break;
                        }
                    case Interaction.AdsMpu:
                        {
                            message.AppendInteger(0);
                            message.AppendInteger(1);

                            if (!string.IsNullOrEmpty(ExtraData) && ExtraData.Contains('\t'))
                            {
                                string[] backgroundData = ExtraData.Split('\t');

                                message.AppendInteger(backgroundData.Length / 2);

                                foreach (string dataStr in backgroundData)
                                    message.AppendString(dataStr);

                                break;
                            }

                            message.AppendInteger(0);

                            break;
                        }
                    case Interaction.MysteryBox:
                        {
                            message.AppendInteger(0);
                            message.AppendInteger(0);

                            if (ExtraData.Contains('\u0005'.ToString()))
                            {
                                string[] mysterBoxData = ExtraData.Split('\u0005');

                                int firstProbability = int.Parse(mysterBoxData[0]);

                                int secondProbability = int.Parse(mysterBoxData[1]);

                                message.AppendString((3 * firstProbability - secondProbability).ToString());

                                break;
                            }

                            ExtraData = $"0{'\u0005'}0";

                            message.AppendString("0");

                            break;
                        }
                    case Interaction.WiredHighscore:
                        {
                            if (HighscoreData == null)
                                HighscoreData = new HighscoreData(this);

                            message.AppendInteger(0);

                            HighscoreData.GenerateExtraData(this, message);

                            break;
                        }
                    case Interaction.CrackableEgg:
                        {
                            message.AppendInteger(0);

                            Yupi.GetGame().GetCrackableEggHandler().GetServerMessage(message, this);

                            break;
                        }
                    default:
                        {
                            if (LimitedNo > 0)
                            {
                                message.AppendInteger(1);
                                message.AppendInteger(256);
                                message.AppendString(ExtraData);
                                message.AppendInteger(LimitedNo);
                                message.AppendInteger(LimitedTot);

                                break;
                            }

                            message.AppendInteger(GetBaseItem().InteractionType == Interaction.TileStackMagic ? 0 : 1);

                            message.AppendInteger(0);
                            message.AppendString(ExtraData);

                            break;
                        }
                }

                message.AppendInteger(-1);

                switch (GetBaseItem().InteractionType)
                {
                    case Interaction.MysteryBox:
                    case Interaction.YoutubeTv:
                    case Interaction.Background:
                        {
                            message.AppendInteger(2);

                            break;
                        }
                    case Interaction.Moplaseed:
                    case Interaction.RareMoplaSeed:
                        {
                            message.AppendInteger(1);

                            break;
                        }
                    default:
                        {
                            message.AppendInteger(GetBaseItem().Modes > 1 ? 1 : 0);

                            break;
                        }
                }

                message.AppendInteger(IsBuilder ? -12345678 : Convert.ToInt32(UserId));

                return;
            }

            if (!IsWallItem)
                return;

            message.AppendString($"{Id}{string.Empty}");
            message.AppendInteger(GetBaseItem().SpriteId);
            message.AppendString(WallCoord?.ToString() ?? string.Empty);

            Interaction interactionType = GetBaseItem().InteractionType;

            message.AppendString(interactionType == Interaction.PostIt ? ExtraData.Split(' ')[0] : ExtraData);
            message.AppendInteger(-1);
            message.AppendInteger(GetBaseItem().Modes > 1 ? 1 : 0);
            message.AppendInteger(IsBuilder ? -12345678 : Convert.ToInt32(UserId));
        }

        /// <summary>
        ///     Refreshes the item.
        /// </summary>
        internal void RefreshItem() => _mBaseItem = null;

        /// <summary>
        ///     Gets the base item.
        /// </summary>
        /// <returns>Item.</returns>
        internal Item GetBaseItem() => _mBaseItem ?? (_mBaseItem = Yupi.GetGame().GetItemManager().GetItemByName(BaseName));

        /// <summary>
        ///     Gets the room.
        /// </summary>
        /// <returns>Room.</returns>
        internal Room GetRoom() => _mRoom ?? (_mRoom = Yupi.GetGame().GetRoomManager().GetRoom(RoomId));

        /// <summary>
        ///     Users the walks on furni.
        /// </summary>
        /// <param name="user">The user.</param>
        internal void UserWalksOnFurni(RoomUser user)
        {
            OnUserWalksOnFurni?.Invoke(this, new UserWalksOnArgs(user));

            GetRoom().GetWiredHandler().ExecuteWired(Interaction.TriggerWalkOnFurni, user, this);

            user.LastItem = Id;
        }

        /// <summary>
        ///     Users the walks off furni.
        /// </summary>
        /// <param name="user">The user.</param>
        internal void UserWalksOffFurni(RoomUser user)
        {
            OnUserWalksOffFurni?.Invoke(this, new UserWalksOnArgs(user));

            GetRoom().GetWiredHandler().ExecuteWired(Interaction.TriggerWalkOffFurni, user, this);
        }
    }
}