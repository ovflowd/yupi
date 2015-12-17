using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Yupi.Core.Io;
using Yupi.Data.Collections;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pathfinding;
using Yupi.Game.Rooms.Items.Games.Teams.Enums;
using Yupi.Game.Rooms.Items.Games.Types.Soccer.Enums;
using Yupi.Game.Rooms.User;
using Yupi.Game.Rooms.User.Path;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Rooms.Items.Games.Types.Soccer
{
    internal class Soccer
    {
        private QueuedDictionary<uint, RoomItem> _balls;
        private RoomItem[] _gates;
        private Room _room;

        public Soccer(Room room)
        {
            _room = room;
            _gates = new RoomItem[4];
            _balls = new QueuedDictionary<uint, RoomItem>();
        }

        public static int GetThreadTime(int i)
        {
            switch (i)
            {
                case 1:
                    return 75;

                case 2:
                    return 100;

                case 3:
                    return 125;

                case 4:
                    return 150;

                default:
                    if (i != 5)
                        return i != 6 ? 200 : 350;
                    return 200;
            }
        }

        internal void AddBall(RoomItem item)
        {
            _balls.Add(item.Id, item);
        }

        internal void Destroy()
        {
            Array.Clear(_gates, 0, _gates.Length);
            _gates = null;
            _room = null;
            _balls.Destroy();
            _balls = null;
        }

        internal void OnCycle()
        {
            _balls.OnCycle();
        }

        internal void OnGateRemove(RoomItem item)
        {
            switch (item.GetBaseItem().InteractionType)
            {
                case Interaction.FootballGoalGreen:
                case Interaction.FootballCounterGreen:
                    _room.GetGameManager().RemoveFurnitureFromTeam(item, Team.Green);
                    return;

                case Interaction.FootballGoalYellow:
                case Interaction.FootballCounterYellow:
                    _room.GetGameManager().RemoveFurnitureFromTeam(item, Team.Yellow);
                    return;

                case Interaction.FootballGoalBlue:
                case Interaction.FootballCounterBlue:
                    _room.GetGameManager().RemoveFurnitureFromTeam(item, Team.Blue);
                    return;

                case Interaction.FootballGoalRed:
                case Interaction.FootballCounterRed:
                    _room.GetGameManager().RemoveFurnitureFromTeam(item, Team.Red);
                    return;

                default:
                    return;
            }
        }

        private IEnumerable<RoomItem> GetFootballItemsForAllTeams()
        {
            List<RoomItem> items = _room.GetGameManager().GetItems(Team.Red).Values.ToList();
            items.AddRange(_room.GetGameManager().GetItems(Team.Green).Values);

            items.AddRange(_room.GetGameManager().GetItems(Team.Blue).Values);

            items.AddRange(_room.GetGameManager().GetItems(Team.Yellow).Values);

            return items;
        }

        private bool GameItemOverlaps(RoomItem gameItem)
        {
            Point gameItemCoord = gameItem.Coordinate;
            return
                GetFootballItemsForAllTeams()
                    .Any(
                        item =>
                            item.AffectedTiles.Values.Any(
                                tile => tile.X == gameItemCoord.X && tile.Y == gameItemCoord.Y));
        }

        internal void OnUserWalk(RoomUser user)
        {
            if (user == null)
                return;

            foreach (RoomItem ball in _balls.Values)
            {
                if (user.SetX == ball.X && user.SetY == ball.Y && user.GoalX == ball.X && user.GoalY == ball.Y &&
                    user.HandelingBallStatus == 0) // super chute.
                {
                    Point userPoint = new Point(user.X, user.Y);
                    ball.ExtraData = "55";
                    ball.BallIsMoving = true;
                    ball.BallValue = 1;
                    MoveBall(ball, user.GetClient(), userPoint);
                }
                else if (user.X == ball.X && user.Y == ball.Y && user.HandelingBallStatus == 0)
                {
                    Point userPoint = new Point(user.SetX, user.SetY);
                    ball.ExtraData = "55";
                    ball.BallIsMoving = true;
                    ball.BallValue = 1;
                    MoveBall(ball, user.GetClient(), userPoint);
                }
                else
                {
                    if (user.HandelingBallStatus == 0 && user.GoalX == ball.X && user.GoalY == ball.Y)
                        return;

                    if (user.SetX != ball.X || user.SetY != ball.Y || !user.IsWalking ||
                        (user.X == user.GoalX && user.Y == user.GoalY))
                        return;
                    user.HandelingBallStatus = 1;
                    IComeDirection comeDirection = ComeDirection.GetComeDirection(new Point(user.X, user.Y), ball.Coordinate);
                    if (comeDirection == IComeDirection.Null)
                        return;

                    int newX = user.SetX;
                    int newY = user.SetY;

                    if (!ball.GetRoom().GetGameMap().ValidTile2(user.SetX, user.SetY) ||
                        !ball.GetRoom().GetGameMap().ItemCanBePlacedHere(user.SetX, user.SetY))
                    {
                        comeDirection = ComeDirection.InverseDirections(_room, comeDirection, user.X, user.Y);
                        newX = ball.X;
                        newY = ball.Y;
                    }

                    ComeDirection.GetNewCoords(comeDirection, ref newX, ref newY);
                    ball.ExtraData = "11";
                    MoveBall(ball, user.GetClient(), newX, newY);
                }
            }
        }

        internal bool MoveBall(RoomItem item, GameClient mover, int newX, int newY)
        {
            if (item == null || item.GetBaseItem() == null /*|| mover == null || mover.GetHabbo() == null*/)
                return false;

            if (!_room.GetGameMap().ItemCanBePlacedHere(newX, newY))
                return false;

            Point oldRoomCoord = item.Coordinate;
            bool itemIsOnGameItem = GameItemOverlaps(item);
            double newZ = _room.GetGameMap().Model.SqFloorHeight[newX][newY];

            ServerMessage mMessage = new ServerMessage();
            mMessage.Init(LibraryParser.OutgoingRequest("ItemAnimationMessageComposer")); // Cf
            mMessage.AppendInteger(item.Coordinate.X);
            mMessage.AppendInteger(item.Coordinate.Y);
            mMessage.AppendInteger(newX);
            mMessage.AppendInteger(newY);
            mMessage.AppendInteger(1);
            mMessage.AppendInteger(item.Id);
            mMessage.AppendString(ServerUserChatTextHandler.GetString(item.Z));
            mMessage.AppendString(ServerUserChatTextHandler.GetString(newZ));
            mMessage.AppendInteger(item.Id);
            _room.SendMessage(mMessage);

            if (oldRoomCoord.X == newX && oldRoomCoord.Y == newY)
                return false;

            item.SetState(newX, newY, item.Z,
                Gamemap.GetAffectedTiles(item.GetBaseItem().Length, item.GetBaseItem().Width, newX, newY, item.Rot));

            if (itemIsOnGameItem || mover == null || mover.GetHabbo() == null)
                return false;
            HandleFootballGameItems(new Point(newX, newY),
                _room.GetRoomUserManager().GetRoomUserByHabbo(mover.GetHabbo().Id));
            return false;
        }

        internal void MoveBall(RoomItem item, GameClient client, Point user)
        {
            try
            {
                item.ComeDirection = ComeDirection.GetComeDirection(user, item.Coordinate);

                if (item.ComeDirection != IComeDirection.Null)
                    // item.ballMover = client;
                    new TaskFactory().StartNew(() => MoveBallProcess(item, client));
            }
            catch
            {
            }
        }

        internal async void MoveBallProcess(RoomItem item, GameClient client)
        {
            int tryes = 0;
            int newX = item.Coordinate.X;
            int newY = item.Coordinate.Y;

            //while (tryes < 3)
            {
                if (_room == null || _room.GetGameMap() == null)
                    return;

                int total = item.ExtraData == "55" ? 6 : 1;
                for (int i = 0; i != total; i++)
                {
                    if (item.ComeDirection == IComeDirection.Null)
                    {
                        item.BallIsMoving = false;
                        break;
                    }

                    int resetX = newX;
                    int resetY = newY;

                    ComeDirection.GetNewCoords(item.ComeDirection, ref newX, ref newY);

                    bool ignoreUsers = false;

                    if (_room.GetGameMap().SquareHasUsers(newX, newY))
                    {
                        if (item.ExtraData != "55" && item.ExtraData != "44")
                        {
                            item.BallIsMoving = false;
                            break;
                        }
                        ignoreUsers = true;
                    }

                    if (ignoreUsers == false)
                        if (!_room.GetGameMap().ItemCanBePlacedHere(newX, newY))
                        {
                            item.ComeDirection = ComeDirection.InverseDirections(_room, item.ComeDirection, newX, newY);
                            newX = resetX;
                            newY = resetY;
                            tryes++;
                            if (tryes > 2)
                                item.BallIsMoving = false;
                            continue;
                        }

                    if (MoveBall(item, client, newX, newY))
                    {
                        item.BallIsMoving = false;
                        break;
                    }

                    int number;
                    int.TryParse(item.ExtraData, out number);
                    if (number > 11)
                        item.ExtraData = (int.Parse(item.ExtraData) - 11).ToString();

                    await Task.Delay(90);
                }

                item.BallValue++;

                if (item.BallValue <= 6)
                    return;
                item.BallIsMoving = false;
                item.BallValue = 1;
                //break;
            }
        }

        internal void RegisterGate(RoomItem item)
        {
            if (_gates[0] == null)
            {
                item.Team = Team.Blue;
                _gates[0] = item;
                return;
            }
            if (_gates[1] == null)
            {
                item.Team = Team.Red;
                _gates[1] = item;
                return;
            }
            if (_gates[2] == null)
            {
                item.Team = Team.Green;
                _gates[2] = item;
                return;
            }
            if (_gates[3] != null)
                return;
            item.Team = Team.Yellow;
            _gates[3] = item;
        }

        internal void RemoveBall(uint itemId)
        {
            _balls.Remove(itemId);
        }

        internal void UnRegisterGate(RoomItem item)
        {
            switch (item.Team)
            {
                case Team.Red:
                    _gates[1] = null;
                    return;

                case Team.Green:
                    _gates[2] = null;
                    return;

                case Team.Blue:
                    _gates[0] = null;
                    return;

                case Team.Yellow:
                    _gates[3] = null;
                    return;

                default:
                    return;
            }
        }

        private static bool Veryficball(RoomUser user, int actualx, int nexx, int actualy, int nexy)
        {
            return PathFinder.CalculateRotation(user.X, user.Y, actualx, actualy) == user.RotBody;
        }

        private void HandleFootballGameItems(Point ballItemCoord, RoomUser user)
        {
            if (user == null || _room == null || _room.GetGameManager() == null) return;
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RoomUserActionMessageComposer"));

            if (_room.GetGameManager()
                .GetItems(Team.Red)
                .Values.SelectMany(current => current.AffectedTiles.Values)
                .Any(current2 => current2.X == ballItemCoord.X && current2.Y == ballItemCoord.Y))
                _room.GetGameManager().AddPointToTeam(Team.Red, user);
            if (
                _room.GetGameManager()
                    .GetItems(Team.Green)
                    .Values.SelectMany(current3 => current3.AffectedTiles.Values)
                    .Any(current4 => current4.X == ballItemCoord.X && current4.Y == ballItemCoord.Y))
                _room.GetGameManager().AddPointToTeam(Team.Green, user);
            if (
                _room.GetGameManager()
                    .GetItems(Team.Blue)
                    .Values.SelectMany(current5 => current5.AffectedTiles.Values)
                    .Any(current6 => current6.X == ballItemCoord.X && current6.Y == ballItemCoord.Y))
                _room.GetGameManager().AddPointToTeam(Team.Blue, user);
            if (!_room.GetGameManager()
                .GetItems(Team.Yellow)
                .Values.SelectMany(current7 => current7.AffectedTiles.Values)
                .Any(current8 => current8.X == ballItemCoord.X && current8.Y == ballItemCoord.Y))
                _room.GetGameManager().AddPointToTeam(Team.Yellow, user);

            serverMessage.AppendInteger(user.VirtualId);
            serverMessage.AppendInteger(0);
            user.GetClient().GetHabbo().CurrentRoom.SendMessage(serverMessage);
        }
    }
}