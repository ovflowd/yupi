using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.Items.Games.Teams;
using Yupi.Game.Rooms.Items.Games.Teams.Enums;
using Yupi.Game.Rooms.Items.Games.Types.Freeze.Enum;
using Yupi.Game.Rooms.User;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Rooms.Items.Games.Types.Freeze
{
    internal class Freeze
    {
        private ConcurrentDictionary<uint, RoomItem> _freezeTiles, _freezeBlocks;
        private Random _rnd;
        private Room _room;

        public Freeze(Room room)
        {
            _room = room;
            _freezeTiles = new ConcurrentDictionary<uint, RoomItem>();
            _freezeBlocks = new ConcurrentDictionary<uint, RoomItem>();
            ExitTeleport = null;
            _rnd = new Random();
            GameStarted = false;
        }

        public bool IsFreezeActive => GameStarted;

        internal bool GameStarted { get; private set; }

        internal RoomItem ExitTeleport { get; set; }

        public static void CycleUser(RoomUser user)
        {
            if (user.Freezed)
            {
                {
                    ++user.FreezeCounter;
                }
                if (user.FreezeCounter > 10)
                {
                    user.Freezed = false;
                    user.FreezeCounter = 0;
                    ActivateShield(user);
                }
            }
            if (!user.ShieldActive) return;

            {
                ++user.ShieldCounter;
            }
            if (user.ShieldCounter <= 10) return;
            user.ShieldActive = false;
            user.ShieldCounter = 10;
            user.ApplyEffect((int) (user.Team + 39));
        }

        internal static void OnCycle()
        {
        }

        internal void StartGame()
        {
            GameStarted = true;
            CountTeamPoints();
            ResetGame();
            _room.GetGameManager().LockGates();
            _room.GetGameManager().StartGame();

            if (ExitTeleport == null) return;

            foreach (
                RoomUser user in
                    _freezeTiles.Values.Select(
                        tile => _room.GetGameMap().GetRoomUsers(new Point(tile.X, tile.Y)))
                        .SelectMany(users => users.Where(user => user != null && user.Team == Team.None)))
                _room.GetGameMap().TeleportToItem(user, ExitTeleport);
        }

        internal void StopGame()
        {
            GameStarted = false;
            _room.GetGameManager().UnlockGates();
            _room.GetGameManager().StopGame();
            Team winningTeam = _room.GetGameManager().GetWinningTeam();
            foreach (RoomUser avatar in _room.GetRoomUserManager().UserList.Values)
            {
                avatar.FreezeLives = 0;
                if (avatar.Team != winningTeam) continue;
                avatar.UnIdle();
                avatar.DanceId = 0;
                ServerMessage waveAtWin = new ServerMessage(LibraryParser.OutgoingRequest("RoomUserActionMessageComposer"));
                waveAtWin.AppendInteger(avatar.VirtualId);
                waveAtWin.AppendInteger(1);
                _room.SendMessage(waveAtWin);
            }
        }

        internal void ResetGame()
        {
            foreach (
                RoomItem roomItem in
                    _freezeBlocks.Values.Where(
                        roomItem =>
                            !string.IsNullOrEmpty(roomItem.ExtraData) &&
                            !roomItem.GetBaseItem().InteractionType.Equals(Interaction.FreezeBlueGate) &&
                            !roomItem.GetBaseItem().InteractionType.Equals(Interaction.FreezeRedGate) &&
                            !roomItem.GetBaseItem().InteractionType.Equals(Interaction.FreezeGreenGate) &&
                            !roomItem.GetBaseItem().InteractionType.Equals(Interaction.FreezeYellowGate)))
            {
                roomItem.ExtraData = string.Empty;
                roomItem.UpdateState(false, true);
                _room.GetGameMap().AddItemToMap(roomItem, false);
            }
        }

        internal void OnUserWalk(RoomUser user)
        {
            if (!GameStarted || user == null || user.Team == Team.None) return;

            if (user.X == user.GoalX && user.GoalY == user.Y && user.ThrowBallAtGoal)
            {
                foreach (
                    RoomItem roomItem in
                        _freezeTiles.Values.Where(
                            roomItem =>
                                roomItem.InteractionCountHelper == 0 && roomItem.X == user.X && roomItem.Y == user.Y))
                {
                    roomItem.InteractionCountHelper = 1;
                    roomItem.ExtraData = "1000";
                    roomItem.UpdateState();
                    roomItem.InteractingUser = user.UserId;
                    roomItem.FreezePowerUp = user.BanzaiPowerUp;
                    roomItem.ReqUpdate(4, true);
                    switch (user.BanzaiPowerUp)
                    {
                        case FreezePowerUp.GreenArrow:
                        case FreezePowerUp.OrangeSnowball:
                            user.BanzaiPowerUp = FreezePowerUp.None;
                            break;
                    }
                }
            }
            foreach (
                RoomItem roomItem in
                    _freezeBlocks.Values.Where(
                        roomItem =>
                            roomItem != null && user.X == roomItem.X && user.Y == roomItem.Y &&
                            roomItem.FreezePowerUp != FreezePowerUp.None))
                PickUpPowerUp(roomItem, user);
        }

        internal void OnFreezeTiles(RoomItem item, FreezePowerUp powerUp, uint userId)
        {
            if (_room.GetRoomUserManager().GetRoomUserByHabbo(userId) == null || item == null) return;

            List<RoomItem> items;
            switch (powerUp)
            {
                case FreezePowerUp.BlueArrow:
                    items = GetVerticalItems(item.X, item.Y, 5);
                    break;

                case FreezePowerUp.GreenArrow:
                    items = GetDiagonalItems(item.X, item.Y, 5);
                    break;

                case FreezePowerUp.OrangeSnowball:
                    items = GetVerticalItems(item.X, item.Y, 5);
                    items.AddRange(GetDiagonalItems(item.X, item.Y, 5));
                    break;

                default:
                    items = GetVerticalItems(item.X, item.Y, 3);
                    break;
            }
            HandleBanzaiFreezeItems(items);
        }

        internal void AddFreezeTile(RoomItem item)
        {
            _freezeTiles.AddOrUpdate(item.Id, item, (k, v) => item);
        }

        internal void RemoveFreezeTile(uint itemId)
        {
            RoomItem junk;
            _freezeTiles.TryRemove(itemId, out junk);
        }

        internal void AddFreezeBlock(RoomItem item)
        {
            _freezeBlocks.AddOrUpdate(item.Id, item, (k, v) => item);
        }

        internal void RemoveFreezeBlock(uint itemId)
        {
            RoomItem junk;
            _freezeBlocks.TryRemove(itemId, out junk);
        }

        internal void Destroy()
        {
            _freezeBlocks.Clear();
            _freezeTiles.Clear();
            _room = null;
            _freezeTiles = null;
            _freezeBlocks = null;
            ExitTeleport = null;
            _rnd = null;
        }

        internal void FreezeStart()
        {
            throw new NotImplementedException();
        }

        internal void FreezeEnd()
        {
            throw new NotImplementedException();
        }

        private static void ActivateShield(RoomUser user)
        {
            user.ApplyEffect((int) (user.Team + 48));
            user.ShieldActive = true;
            user.ShieldCounter = 0;
        }

        private static bool SquareGotFreezeTile(IEnumerable<RoomItem> items)
        {
            return
                items.Any(
                    roomItem => roomItem != null && roomItem.GetBaseItem().InteractionType == Interaction.FreezeTile);
        }

        private static bool SquareGotFreezeBlock(IEnumerable<RoomItem> items)
        {
            return
                items.Any(
                    roomItem =>
                        roomItem != null && roomItem.GetBaseItem().InteractionType == Interaction.FreezeTileBlock);
        }

        private void CountTeamPoints()
        {
            _room.GetGameManager().Reset();
            foreach (
                RoomUser roomUser in
                    _room.GetRoomUserManager()
                        .UserList.Values.Where(
                            roomUser => !roomUser.IsBot && roomUser.Team != Team.None && roomUser.GetClient() != null))
            {
                roomUser.BanzaiPowerUp = FreezePowerUp.None;
                roomUser.FreezeLives = 3;
                roomUser.ShieldActive = false;
                roomUser.ShieldCounter = 11;
                _room.GetGameManager().AddPointToTeam(roomUser.Team, 30, null);
                ServerMessage serverMessage = new ServerMessage();
                serverMessage.Init(LibraryParser.OutgoingRequest("UpdateFreezeLivesMessageComposer"));
                serverMessage.AppendInteger(roomUser.InternalRoomId);
                serverMessage.AppendInteger(roomUser.FreezeLives);
                roomUser.GetClient().SendMessage(serverMessage);
            }
        }

        private void HandleBanzaiFreezeItems(IEnumerable<RoomItem> items)
        {
            foreach (RoomItem roomItem in items)
            {
                switch (roomItem.GetBaseItem().InteractionType)
                {
                    case Interaction.FreezeTileBlock:
                        SetRandomPowerUp(roomItem);
                        roomItem.UpdateState(false, true);
                        continue;
                    case Interaction.FreezeTile:
                        roomItem.ExtraData = "11000";
                        roomItem.UpdateState(false, true);
                        continue;
                    default:
                        continue;
                }
            }
        }

        private void SetRandomPowerUp(RoomItem item)
        {
            if (!string.IsNullOrEmpty(item.ExtraData)) return;
            switch (_rnd.Next(1, 14))
            {
                case 2:
                    item.ExtraData = "2000";
                    item.FreezePowerUp = FreezePowerUp.BlueArrow;
                    break;

                case 3:
                    item.ExtraData = "3000";
                    item.FreezePowerUp = FreezePowerUp.Snowballs;
                    break;

                case 4:
                    item.ExtraData = "4000";
                    item.FreezePowerUp = FreezePowerUp.GreenArrow;
                    break;

                case 5:
                    item.ExtraData = "5000";
                    item.FreezePowerUp = FreezePowerUp.OrangeSnowball;
                    break;

                case 6:
                    item.ExtraData = "6000";
                    item.FreezePowerUp = FreezePowerUp.Heart;
                    break;

                case 7:
                    item.ExtraData = "7000";
                    item.FreezePowerUp = FreezePowerUp.Shield;
                    break;

                default:
                    item.ExtraData = "1000";
                    item.FreezePowerUp = FreezePowerUp.None;
                    break;
            }
            _room.GetGameMap().RemoveFromMap(item, false);
            item.UpdateState(false, true);
        }

        private void PickUpPowerUp(RoomItem item, RoomUser user)
        {
            switch (item.FreezePowerUp)
            {
                case FreezePowerUp.BlueArrow:
                case FreezePowerUp.GreenArrow:
                case FreezePowerUp.OrangeSnowball:
                    user.BanzaiPowerUp = item.FreezePowerUp;
                    break;

                case FreezePowerUp.Shield:
                    ActivateShield(user);
                    break;

                case FreezePowerUp.Heart:
                    if (user.FreezeLives < 5)
                    {
                        {
                            ++user.FreezeLives;
                        }
                        _room.GetGameManager().AddPointToTeam(user.Team, 10, user);
                    }
                    ServerMessage serverMessage = new ServerMessage();
                    serverMessage.Init(LibraryParser.OutgoingRequest("UpdateFreezeLivesMessageComposer"));
                    serverMessage.AppendInteger(user.InternalRoomId);
                    serverMessage.AppendInteger(user.FreezeLives);
                    user.GetClient().SendMessage(serverMessage);
                    break;
            }
            item.FreezePowerUp = FreezePowerUp.None;
            item.ExtraData = $"1{item.ExtraData}";
            item.UpdateState(false, true);
        }

        private void HandleUserFreeze(Point point)
        {
            RoomUser user = _room.GetGameMap().GetRoomUsers(point).FirstOrDefault();

            if (user == null || user.IsWalking && user.SetX != point.X && user.SetY != point.Y)
                return;

            FreezeUser(user);
        }

        private void FreezeUser(RoomUser user)
        {
            if (user.IsBot || user.ShieldActive || user.Team == Team.None || user.Freezed)
                return;

            user.Freezed = true;
            user.FreezeCounter = 0;
            --user.FreezeLives;

            if (user.FreezeLives <= 0)
            {
                ServerMessage serverMessage = new ServerMessage();
                serverMessage.Init(LibraryParser.OutgoingRequest("UpdateFreezeLivesMessageComposer"));
                serverMessage.AppendInteger(user.InternalRoomId);
                serverMessage.AppendInteger(user.FreezeLives);
                user.GetClient().SendMessage(serverMessage);
                user.ApplyEffect(-1);
                _room.GetGameManager().AddPointToTeam(user.Team, -10, user);
                TeamManager managerForFreeze = _room.GetTeamManagerForFreeze();
                managerForFreeze.OnUserLeave(user);
                user.Team = Team.None;
                if (ExitTeleport != null) _room.GetGameMap().TeleportToItem(user, ExitTeleport);
                user.Freezed = false;
                user.SetStep = false;
                user.IsWalking = false;
                user.UpdateNeeded = true;
                if (!managerForFreeze.BlueTeam.Any() && !managerForFreeze.RedTeam.Any() &&
                    !managerForFreeze.GreenTeam.Any() && managerForFreeze.YellowTeam.Any())
                    StopGame();
                else if (managerForFreeze.BlueTeam.Any() && !managerForFreeze.RedTeam.Any() &&
                         !managerForFreeze.GreenTeam.Any() && !managerForFreeze.YellowTeam.Any())
                    StopGame();
                else if (!managerForFreeze.BlueTeam.Any() && managerForFreeze.RedTeam.Any() &&
                         !managerForFreeze.GreenTeam.Any() && !managerForFreeze.YellowTeam.Any())
                    StopGame();
                else
                {
                    if (managerForFreeze.BlueTeam.Any() || managerForFreeze.RedTeam.Any() ||
                        !managerForFreeze.GreenTeam.Any() || managerForFreeze.YellowTeam.Any())
                        return;
                    StopGame();
                }
            }
            else
            {
                _room.GetGameManager().AddPointToTeam(user.Team, -10, user);
                user.ApplyEffect(12);
                ServerMessage serverMessage = new ServerMessage();
                serverMessage.Init(LibraryParser.OutgoingRequest("UpdateFreezeLivesMessageComposer"));
                serverMessage.AppendInteger(user.InternalRoomId);
                serverMessage.AppendInteger(user.FreezeLives);
                user.GetClient().SendMessage(serverMessage);
            }
        }

        private List<RoomItem> GetVerticalItems(int x, int y, int length)
        {
            List<RoomItem> list = new List<RoomItem>();
            int num1 = 0;
            Point point;
            while (num1 < length)
            {
                point = new Point(x + num1, y);

                List<RoomItem> itemsForSquare = GetItemsForSquare(point);
                if (SquareGotFreezeTile(itemsForSquare))
                {
                    HandleUserFreeze(point);
                    list.AddRange(itemsForSquare);
                    if (!SquareGotFreezeBlock(itemsForSquare)) ++num1;
                    else break;
                }
                else break;
            }
            int num2 = 1;
            while (num2 < length)
            {
                point = new Point(x, y + num2);

                List<RoomItem> itemsForSquare = GetItemsForSquare(point);
                if (SquareGotFreezeTile(itemsForSquare))
                {
                    HandleUserFreeze(point);
                    list.AddRange(itemsForSquare);
                    if (!SquareGotFreezeBlock(itemsForSquare)) ++num2;
                    else break;
                }
                else break;
            }
            int num3 = 1;
            while (num3 < length)
            {
                point = new Point(x - num3, y);

                List<RoomItem> itemsForSquare = GetItemsForSquare(point);
                if (SquareGotFreezeTile(itemsForSquare))
                {
                    HandleUserFreeze(point);
                    list.AddRange(itemsForSquare);
                    if (!SquareGotFreezeBlock(itemsForSquare)) ++num3;
                    else break;
                }
                else break;
            }
            int num4 = 1;
            while (num4 < length)
            {
                point = new Point(x, y - num4);

                List<RoomItem> itemsForSquare = GetItemsForSquare(point);
                if (SquareGotFreezeTile(itemsForSquare))
                {
                    HandleUserFreeze(point);
                    list.AddRange(itemsForSquare);
                    if (!SquareGotFreezeBlock(itemsForSquare)) ++num4;
                    else break;
                }
                else break;
            }
            return list;
        }

        private List<RoomItem> GetDiagonalItems(int x, int y, int length)
        {
            List<RoomItem> list = new List<RoomItem>();
            int num1 = 0;
            Point point;
            while (num1 < length)
            {
                point = new Point(x + num1, checked(y + num1));

                List<RoomItem> itemsForSquare = GetItemsForSquare(point);
                if (SquareGotFreezeTile(itemsForSquare))
                {
                    HandleUserFreeze(point);
                    list.AddRange(itemsForSquare);
                    if (!SquareGotFreezeBlock(itemsForSquare)) ++num1;
                    else break;
                }
                else break;
            }
            int num2 = 0;
            while (num2 < length)
            {
                point = new Point(x - num2, checked(y - num2));

                List<RoomItem> itemsForSquare = GetItemsForSquare(point);
                if (SquareGotFreezeTile(itemsForSquare))
                {
                    HandleUserFreeze(point);
                    list.AddRange(itemsForSquare);
                    if (!SquareGotFreezeBlock(itemsForSquare)) ++num2;
                    else break;
                }
                else break;
            }
            int num3 = 0;
            while (num3 < length)
            {
                point = new Point(x - num3, checked(y + num3));

                List<RoomItem> itemsForSquare = GetItemsForSquare(point);
                if (SquareGotFreezeTile(itemsForSquare))
                {
                    HandleUserFreeze(point);
                    list.AddRange(itemsForSquare);
                    if (!SquareGotFreezeBlock(itemsForSquare)) ++num3;
                    else break;
                }
                else break;
            }
            int num4 = 0;
            while (num4 < length)
            {
                point = new Point(x + num4, checked(y - num4));

                List<RoomItem> itemsForSquare = GetItemsForSquare(point);
                if (SquareGotFreezeTile(itemsForSquare))
                {
                    HandleUserFreeze(point);
                    list.AddRange(itemsForSquare);
                    if (!SquareGotFreezeBlock(itemsForSquare)) ++num4;
                    else break;
                }
                else break;
            }
            return list;
        }

        private List<RoomItem> GetItemsForSquare(Point point)
        {
            return _room.GetGameMap().GetCoordinatedItems(point);
        }
    }
}