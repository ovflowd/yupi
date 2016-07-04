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


namespace Yupi.Emulator.Game.Items.Interfaces
{
    public class RoomItem : IEquatable<RoomItem>
    {
        private Item _mBaseItem;
        private Room _mRoom;
        private bool _updateNeeded;
     public bool BallIsMoving;
     public int BallValue;
     public string BaseName;
     public IComeDirection ComeDirection;
     public string ExtraData = string.Empty;
     public FreezePowerUp FreezePowerUp;
     public string GroupData;
     public uint GroupId;
     public HighscoreData HighscoreData;
     public uint Id;
     public uint InteractingBallUser;
     public uint InteractingUser;
     public uint InteractingUser2;
     public byte InteractionCount;
     public byte InteractionCountHelper;
     public bool IsBuilder;
     public bool IsTrans;
     public int LimitedNo;
     public int LimitedTot;
     public bool MagicRemove;
     public MovementDirection MoveToDirMovement = MovementDirection.None;
     public bool OnCannonActing = false;
     public bool PendingReset;
     public List<Pet> PetsList = new List<Pet>(2);
     public uint RoomId;
     public int Rot;
     public string SongCode;
     public Team Team;
     public int UpdateCounter;
     public uint UserId;
     public int Value;
     public bool VikingCotieBurning;
     public WallCoordinate WallCoord;
	
     public bool IsWired => InteractionTypes.AreFamiliar(GlobalInteraction.Wired, GetBaseItem().InteractionType);

     public Dictionary<int, ThreeDCoord> AffectedTiles { get; private set; }

     public int X { get; private set; }

     public int Y { get; private set; }

     private double _z;

     public double Z
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

     public bool UpdateNeeded
        {
            get { return _updateNeeded; }
            set
            {
                if (value)
                    GetRoom().GetRoomItemHandler().QueueRoomItemUpdate(this);

                _updateNeeded = value;
            }
        }

     public bool IsRoller { get; private set; }

     public Point Coordinate => new Point(X, Y);

     public List<Point> GetCoords
        {
            get
            {
                List<Point> list = new List<Point> { Coordinate };

                list.AddRange(AffectedTiles.Values.Select(current => new Point(current.X, current.Y)));

                return list;
            }
        }

     public double Height
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

     public double TotalHeight
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

     public bool IsWallItem { get; set; }

     public bool IsFloorItem { get; set; }

     public Point SquareInFront
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

     public Point SquareBehind
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

		public readonly IFurniInteractor Interactor;

     public RoomItem(uint id, uint roomId, string baseName, string extraData, int x, int y, double z, int rot, Room pRoom, uint userid, uint eGroup, string songCode, bool isBuilder)
        {
            Id = id;
            RoomId = roomId;
            BaseName = baseName;
            ExtraData = extraData;
            GroupId = eGroup;

			if (IsWired)
				Interactor = new InteractorWired();
			else 
				Interactor = InteractionFactory.Create (GetBaseItem().InteractionType);

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
                YupiLogManager.LogMessage($"Unknown Furniture Item: {baseName} (Id: #{id}).", "Yupi.Mobi");

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

     public RoomItem(uint id, uint roomId, string baseName, string extraData, WallCoordinate wallCoord, Room pRoom, uint userid, uint eGroup, bool isBuilder)
        {
            BaseName = baseName;

            _mBaseItem = Yupi.GetGame().GetItemManager().GetItemByName(baseName);

            _mRoom = pRoom;

            if (GetBaseItem() == null)
                YupiLogManager.LogMessage($"Unknown Furniture Item: {baseName} (Id: #{id}).", "Yupi.Mobi");

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

        public bool Equals(RoomItem comparedItem) => comparedItem.Id == Id;

		 event EventHandler<ItemTriggeredArgs> ItemTriggerEventHandler;

		 event EventHandler<UserWalksOnArgs> OnUserWalksOffFurni;

		 event EventHandler<UserWalksOnArgs> OnUserWalksOnFurni;

     public void SetState(int x, int y, double z) => SetState(x, y, z, Gamemap.GetAffectedTiles(GetBaseItem().Length, GetBaseItem().Width, x, y, Rot));

     public void SetState(int x, int y, double z, Dictionary<int, ThreeDCoord> tiles)
        {
            X = x;
            Y = y;
            Z = z;

            AffectedTiles = tiles;
        }

     public void OnTrigger(RoomUser user) => ItemTriggerEventHandler?.Invoke(null, new ItemTriggeredArgs(user, this));

     public void Destroy()
        {
            _mRoom = null;

            AffectedTiles.Clear();

            ItemTriggerEventHandler = null;
            OnUserWalksOffFurni = null;
            OnUserWalksOnFurni = null;
        }

     public void ProcessUpdates()
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
                                                if (!user.IsBot && user.GetClient() != null && user.GetClient().GetHabbo() != null)
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

                            clientByUserId.GetMessageHandler().GetResponse().Init(PacketLibraryManager.OutgoingHandler("UpdateUserDataMessageComposer"));
                            clientByUserId.GetMessageHandler().GetResponse().AppendInteger(-1);
                            clientByUserId.GetMessageHandler().GetResponse().AppendString(clientByUserId.GetHabbo().Look);
                            clientByUserId.GetMessageHandler().GetResponse().AppendString(clientByUserId.GetHabbo().Gender.ToLower());
                            clientByUserId.GetMessageHandler().GetResponse().AppendString(clientByUserId.GetHabbo().Motto);
                            clientByUserId.GetMessageHandler().GetResponse().AppendInteger(clientByUserId.GetHabbo().AchievementPoints);
                            clientByUserId.GetMessageHandler().SendResponse();

                            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateUserDataMessageComposer"));
                            simpleServerMessageBuffer.AppendInteger(InteractingUser2);
                            simpleServerMessageBuffer.AppendString(clientByUserId.GetHabbo().Look);
                            simpleServerMessageBuffer.AppendString(clientByUserId.GetHabbo().Gender.ToLower());
                            simpleServerMessageBuffer.AppendString(clientByUserId.GetHabbo().Motto);
                            simpleServerMessageBuffer.AppendInteger(clientByUserId.GetHabbo().AchievementPoints);

                            GetRoom().SendMessage(simpleServerMessageBuffer);

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

     public void ReqUpdate(int cycles, bool setUpdate)
        {
            UpdateCounter = cycles;

            if (setUpdate)
                UpdateNeeded = true;
        }

     public void UpdateState() => UpdateState(true, true);

     public void UpdateState(bool inDb, bool inRoom)
        {
            if (GetRoom() == null)
                return;

            if (inDb)
                GetRoom().GetRoomItemHandler().AddOrUpdateItem(Id);

            if (!inRoom)
                return;

            if (IsFloorItem)
            {
				router.GetComposer<UpdateFloorItemExtraDataMessageComposer> ().Compose (GetRoom (), this);
            }

            if (IsWallItem)
            {
				router.GetComposer<UpdateRoomWallItemMessageComposer> ().Compose (GetRoom (), this);
            }
        }

		// TODO Must be refactored later on
		/*
     public void Serialize(SimpleServerMessageBuffer messageBuffer)
        {
            if (IsFloorItem)
            {
                messageBuffer.AppendInteger(Id);
                messageBuffer.AppendInteger(GetBaseItem().SpriteId);
                messageBuffer.AppendInteger(X);
                messageBuffer.AppendInteger(Y);
                messageBuffer.AppendInteger(Rot);
                messageBuffer.AppendString(ServerUserChatTextHandler.GetString(Z));
                messageBuffer.AppendString(ServerUserChatTextHandler.GetString(GetBaseItem().Height));

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
                                messageBuffer.AppendInteger(1);
                                messageBuffer.AppendInteger(0);
                                messageBuffer.AppendString(ExtraData);

                                break;
                            }

                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendInteger(2);
                            messageBuffer.AppendInteger(5);
                            messageBuffer.AppendString(ExtraData);
                            messageBuffer.AppendString(GroupId.ToString());
                            messageBuffer.AppendString(itemGroup.Badge);
                            messageBuffer.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(itemGroup.Colour1, true));
                            messageBuffer.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(itemGroup.Colour2, false));

                            break;
                        }
                    case Interaction.YoutubeTv:
                        {
                            messageBuffer.AppendInteger(0);

                            if (string.IsNullOrEmpty(ExtraData))
                            {
                                messageBuffer.AppendInteger(0);
                                messageBuffer.AppendString(string.Empty);

                                break;
                            }

                            messageBuffer.AppendInteger(1);
                            messageBuffer.AppendInteger(1);
                            messageBuffer.AppendString("THUMBNAIL_URL");
                            messageBuffer.AppendString(ExtraData);

                            break;
                        }
                    case Interaction.MusicDisc:
                        {
                            messageBuffer.AppendInteger(SoundMachineSongManager.GetSongId(SongCode));
                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendString(ExtraData);

                            break;
                        }
                    case Interaction.Background:
                    case Interaction.WalkInternalLink:
                        {
                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendInteger(1);

                            if (!string.IsNullOrEmpty(ExtraData))
                            {
                                messageBuffer.AppendInteger(ExtraData.Split('\t').Length / 2);

                                for (int i = 0; i <= ExtraData.Split('\t').Length - 1; i++)
                                    messageBuffer.AppendString(ExtraData.Split('\t')[i]);

                                break;
                            }

                            messageBuffer.AppendInteger(0);

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

                            messageBuffer.AppendInteger(ribbonAndColor);
                            messageBuffer.AppendInteger(1);
                            messageBuffer.AppendInteger(showGiver ? 6 : 4);
                            messageBuffer.AppendString("EXTRA_PARAM");
                            messageBuffer.AppendString(string.Empty);
                            messageBuffer.AppendString("MESSAGE");
                            messageBuffer.AppendString(giftMessage);

                            if (showGiver)
                            {
                                messageBuffer.AppendString("PURCHASER_NAME");
                                messageBuffer.AppendString(giverName);
                                messageBuffer.AppendString("PURCHASER_FIGURE");
                                messageBuffer.AppendString(giverLook);
                            }

                            messageBuffer.AppendString("PRODUCT_CODE");
                            messageBuffer.AppendString(product);
                            messageBuffer.AppendString("state");
                            messageBuffer.AppendString(MagicRemove ? "1" : "0");

                            break;
                        }
                    case Interaction.Pinata:
                        {
                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendInteger(7);
                            messageBuffer.AppendString(ExtraData == "100" ? "8" : "6");

                            if (ExtraData.Length <= 0)
                            {
                                messageBuffer.AppendInteger(0);
                                messageBuffer.AppendInteger(100);

                                break;
                            }

                            messageBuffer.AppendInteger(int.Parse(ExtraData));
                            messageBuffer.AppendInteger(100);

                            break;
                        }
                    case Interaction.Mannequin:
                        {
                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendInteger(1);
                            messageBuffer.AppendInteger(3);

                            if (ExtraData.Contains('\u0005'.ToString()))
                            {
                                string[] mannequinData = ExtraData.Split('\u0005');

                                messageBuffer.AppendString("GENDER");
                                messageBuffer.AppendString(mannequinData[0]);
                                messageBuffer.AppendString("FIGURE");
                                messageBuffer.AppendString(mannequinData[1]);
                                messageBuffer.AppendString("OUTFIT_NAME");
                                messageBuffer.AppendString(mannequinData[2]);

                                break;
                            }

                            messageBuffer.AppendString("GENDER");
                            messageBuffer.AppendString(string.Empty);
                            messageBuffer.AppendString("FIGURE");
                            messageBuffer.AppendString(string.Empty);
                            messageBuffer.AppendString("OUTFIT_NAME");
                            messageBuffer.AppendString(string.Empty);

                            break;
                        }
                    case Interaction.BadgeDisplay:
                        {
                            string[] badgeData = ExtraData.Split('|');

                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendInteger(2);
                            messageBuffer.AppendInteger(4);
                            messageBuffer.AppendString("0");
                            messageBuffer.AppendString(badgeData[0]);
                            messageBuffer.AppendString(badgeData.Length > 1 ? badgeData[1] : string.Empty);
                            messageBuffer.AppendString(badgeData.Length > 1 ? badgeData[2] : string.Empty);

                            break;
                        }
                    case Interaction.LoveLock:
                        {
                            string[] data = ExtraData.Split('\u0005');

                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendInteger(2);
                            messageBuffer.AppendInteger(data.Length);

                            foreach (string datak in data)
                                messageBuffer.AppendString(datak);

                            break;
                        }
                    case Interaction.Moplaseed:
                    case Interaction.RareMoplaSeed:
                        {
                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendInteger(1);
                            messageBuffer.AppendInteger(1);
                            messageBuffer.AppendString("rarity");
                            messageBuffer.AppendString(ExtraData);

                            break;
                        }
                    case Interaction.RoomBg:
                        {
                            if (_mRoom.TonerData == null)
                                _mRoom.TonerData = new TonerData(Id);

			messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(5);
            messageBuffer.AppendInteger(4);
            messageBuffer.AppendInteger(_mRoom.TonerData.Enabled);
            messageBuffer.AppendInteger(_mRoom.TonerData.Data1);
            messageBuffer.AppendInteger(_mRoom.TonerData.Data2);
            messageBuffer.AppendInteger(_mRoom.TonerData.Data3);
                            break;
                        }
                    case Interaction.AdsMpu:
                        {
                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendInteger(1);

                            if (!string.IsNullOrEmpty(ExtraData) && ExtraData.Contains('\t'))
                            {
                                string[] backgroundData = ExtraData.Split('\t');

                                messageBuffer.AppendInteger(backgroundData.Length / 2);

                                foreach (string dataStr in backgroundData)
                                    messageBuffer.AppendString(dataStr);

                                break;
                            }

                            messageBuffer.AppendInteger(0);

                            break;
                        }
                    case Interaction.MysteryBox:
                        {
                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendInteger(0);

                            if (ExtraData.Contains('\u0005'.ToString()))
                            {
                                string[] mysterBoxData = ExtraData.Split('\u0005');

                                int firstProbability = int.Parse(mysterBoxData[0]);

                                int secondProbability = int.Parse(mysterBoxData[1]);

                                messageBuffer.AppendString((3 * firstProbability - secondProbability).ToString());

                                break;
                            }

                            ExtraData = $"0{'\u0005'}0";

                            messageBuffer.AppendString("0");

                            break;
                        }
                    case Interaction.WiredHighscore:
                        {
                            if (HighscoreData == null)
                                HighscoreData = new HighscoreData(this);

                            messageBuffer.AppendInteger(0);

RoomItem item = this;
				messageBuffer.AppendInteger(6);
           		 messageBuffer.AppendString(item.ExtraData); //Ouvert/fermé

            if (item.GetBaseItem().Name.StartsWith("highscore_classic"))
                messageBuffer.AppendInteger(2);
            else if (item.GetBaseItem().Name.StartsWith("highscore_mostwin"))
                messageBuffer.AppendInteger(1);
            else if (item.GetBaseItem().Name.StartsWith("highscore_perteam"))
                messageBuffer.AppendInteger(0);

            messageBuffer.AppendInteger(0); //Time : ["alltime", "daily", "weekly", "monthly"]
            messageBuffer.AppendInteger(HighscoreData.Lines.Count); //Count

            foreach (KeyValuePair<int, HighScoreLine> line in HighscoreData.Lines)
            {
                messageBuffer.AppendInteger(line.Value.Score);
                messageBuffer.AppendInteger(1);
                messageBuffer.AppendString(line.Value.Username);
            }

                            break;
                        }
                    case Interaction.CrackableEgg:
                        {
                            messageBuffer.AppendInteger(0);

                            Yupi.GetGame().GetCrackableEggHandler().GetServerMessage(messageBuffer, this);

                            break;
                        }
                    default:
                        {
                            if (LimitedNo > 0)
                            {
                                messageBuffer.AppendInteger(1);
                                messageBuffer.AppendInteger(256);
                                messageBuffer.AppendString(ExtraData);
                                messageBuffer.AppendInteger(LimitedNo);
                                messageBuffer.AppendInteger(LimitedTot);

                                break;
                            }

                            messageBuffer.AppendInteger(GetBaseItem().InteractionType == Interaction.TileStackMagic ? 0 : 1);

                            messageBuffer.AppendInteger(0);
                            messageBuffer.AppendString(ExtraData);

                            break;
                        }
                }

                messageBuffer.AppendInteger(-1);

                switch (GetBaseItem().InteractionType)
                {
                    case Interaction.MysteryBox:
                    case Interaction.YoutubeTv:
                    case Interaction.Background:
                        {
                            messageBuffer.AppendInteger(2);

                            break;
                        }
                    case Interaction.Moplaseed:
                    case Interaction.RareMoplaSeed:
                        {
                            messageBuffer.AppendInteger(1);

                            break;
                        }
                    default:
                        {
                            messageBuffer.AppendInteger(GetBaseItem().Modes > 1 ? 1 : 0);

                            break;
                        }
                }

                messageBuffer.AppendInteger(IsBuilder ? -12345678 : Convert.ToInt32(UserId));

                return;
            }

            if (!IsWallItem)
                return;

            messageBuffer.AppendString($"{Id}{string.Empty}");
            messageBuffer.AppendInteger(GetBaseItem().SpriteId);
            messageBuffer.AppendString(WallCoord?.ToString() ?? string.Empty);

            Interaction interactionType = GetBaseItem().InteractionType;

            messageBuffer.AppendString(interactionType == Interaction.PostIt ? ExtraData.Split(' ')[0] : ExtraData);
            messageBuffer.AppendInteger(-1);
            messageBuffer.AppendInteger(GetBaseItem().Modes > 1 ? 1 : 0);
            messageBuffer.AppendInteger(IsBuilder ? -12345678 : Convert.ToInt32(UserId));
        }
*/
     public void RefreshItem() => _mBaseItem = null;

     public Item GetBaseItem() => _mBaseItem ?? (_mBaseItem = Yupi.GetGame().GetItemManager().GetItemByName(BaseName));

     public Room GetRoom() => _mRoom ?? (_mRoom = Yupi.GetGame().GetRoomManager().GetRoom(RoomId));

     public void UserWalksOnFurni(RoomUser user)
        {
            OnUserWalksOnFurni?.Invoke(this, new UserWalksOnArgs(user));

            GetRoom().GetWiredHandler().ExecuteWired(Interaction.TriggerWalkOnFurni, user, this);

            user.LastItem = Id;
        }

     public void UserWalksOffFurni(RoomUser user)
        {
            OnUserWalksOffFurni?.Invoke(this, new UserWalksOnArgs(user));

            GetRoom().GetWiredHandler().ExecuteWired(Interaction.TriggerWalkOffFurni, user, this);
        }
    }
}