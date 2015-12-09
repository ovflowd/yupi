using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Yupi.Data.Collections;
using Yupi.Game.Items.Interactions;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Rooms.Items.Games.Teams;
using Yupi.Game.Rooms.Items.Games.Teams.Enums;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Rooms.Items.Games
{
    internal class GameManager
    {
        private QueuedDictionary<uint, RoomItem> _blueTeamItems;
        private QueuedDictionary<uint, RoomItem> _greenTeamItems;
        private QueuedDictionary<uint, RoomItem> _redTeamItems;
        private Room _room;
        private QueuedDictionary<uint, RoomItem> _yellowTeamItems;
        internal int[] TeamPoints;

        public GameManager(Room room)
        {
            TeamPoints = new int[5];
            _redTeamItems = new QueuedDictionary<uint, RoomItem>();
            _blueTeamItems = new QueuedDictionary<uint, RoomItem>();
            _greenTeamItems = new QueuedDictionary<uint, RoomItem>();
            _yellowTeamItems = new QueuedDictionary<uint, RoomItem>();
            _room = room;
        }

        internal int[] Points
        {
            get { return TeamPoints; }
            set { TeamPoints = value; }
        }

        internal event TeamScoreChangedDelegate OnScoreChanged;

        internal event RoomEventDelegate OnGameStart;

        internal event RoomEventDelegate OnGameEnd;

        internal void OnCycle()
        {
            _redTeamItems.OnCycle();
            _blueTeamItems.OnCycle();
            _greenTeamItems.OnCycle();
            _yellowTeamItems.OnCycle();
        }

        internal QueuedDictionary<uint, RoomItem> GetItems(Team team)
        {
            switch (team)
            {
                case Team.Red:
                    return _redTeamItems;

                case Team.Green:
                    return _greenTeamItems;

                case Team.Blue:
                    return _blueTeamItems;

                case Team.Yellow:
                    return _yellowTeamItems;

                default:
                    return new QueuedDictionary<uint, RoomItem>();
            }
        }

        internal Team GetWinningTeam()
        {
            var result = 1;
            var num = 0;
            for (var i = 1; i < 5; i++)
            {
                if (TeamPoints[i] <= num) continue;
                num = TeamPoints[i];
                result = i;
            }
            return (Team) result;
        }

        internal void AddPointToTeam(Team team, RoomUser user)
        {
            AddPointToTeam(team, 1, user);
        }

        internal void AddPointToTeam(Team team, int points, RoomUser user)
        {
            var num = (TeamPoints[(int) team] += points);

            if (num < 0) num = 0;

            TeamPoints[(int) team] = num;
            if (OnScoreChanged != null) OnScoreChanged(null, new TeamScoreChangedArgs(num, team, user));
            foreach (
                var current in
                    GetFurniItems(team).Values.Where(current => !IsSoccerGoal(current.GetBaseItem().InteractionType)))
            {
                current.ExtraData = TeamPoints[(int) team].ToString();
                current.UpdateState();
            }

            _room.GetWiredHandler().ExecuteWired(Interaction.TriggerScoreAchieved, user);
        }

        internal void Reset()
        {
            {
                AddPointToTeam(Team.Blue, GetScoreForTeam(Team.Blue)*-1, null);
                AddPointToTeam(Team.Green, GetScoreForTeam(Team.Green)*-1, null);
                AddPointToTeam(Team.Red, GetScoreForTeam(Team.Red)*-1, null);
                AddPointToTeam(Team.Yellow, GetScoreForTeam(Team.Yellow)*-1, null);
            }
        }

        internal void AddFurnitureToTeam(RoomItem item, Team team)
        {
            switch (team)
            {
                case Team.Red:
                    _redTeamItems.Add(item.Id, item);
                    return;

                case Team.Green:
                    _greenTeamItems.Add(item.Id, item);
                    return;

                case Team.Blue:
                    _blueTeamItems.Add(item.Id, item);
                    return;

                case Team.Yellow:
                    _yellowTeamItems.Add(item.Id, item);
                    return;

                default:
                    return;
            }
        }

        internal void RemoveFurnitureFromTeam(RoomItem item, Team team)
        {
            switch (team)
            {
                case Team.Red:
                    _redTeamItems.Remove(item.Id);
                    return;

                case Team.Green:
                    _greenTeamItems.Remove(item.Id);
                    return;

                case Team.Blue:
                    _blueTeamItems.Remove(item.Id);
                    return;

                case Team.Yellow:
                    _yellowTeamItems.Remove(item.Id);
                    return;

                default:
                    return;
            }
        }

        internal RoomItem GetFirstScoreBoard(Team team)
        {
            switch (team)
            {
                case Team.Red:
                    goto IL_BF;
                case Team.Green:
                    break;

                case Team.Blue:
                    using (var enumerator = _blueTeamItems.Values.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            var current = enumerator.Current;
                            if (current.GetBaseItem().InteractionType != Interaction.FreezeBlueCounter) continue;
                            var result = current;
                            return result;
                        }
                        goto IL_151;
                    }
                case Team.Yellow:
                    goto IL_108;
                default:
                    goto IL_151;
            }
            using (var enumerator2 = _greenTeamItems.Values.GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    var current2 = enumerator2.Current;
                    if (current2.GetBaseItem().InteractionType != Interaction.FreezeGreenCounter) continue;
                    var result = current2;
                    return result;
                }
                goto IL_151;
            }
            IL_BF:
            using (var enumerator3 = _redTeamItems.Values.GetEnumerator())
            {
                while (enumerator3.MoveNext())
                {
                    var current3 = enumerator3.Current;
                    if (current3.GetBaseItem().InteractionType != Interaction.FreezeRedCounter) continue;
                    var result = current3;
                    return result;
                }
                goto IL_151;
            }
            IL_108:
            foreach (
                var result in
                    _yellowTeamItems.Values.Where(
                        current4 => current4.GetBaseItem().InteractionType == Interaction.FreezeYellowCounter))
                return result;
            IL_151:
            return null;
        }

        internal void UnlockGates()
        {
            foreach (var current in _redTeamItems.Values) UnlockGate(current);
            foreach (var current2 in _greenTeamItems.Values) UnlockGate(current2);
            foreach (var current3 in _blueTeamItems.Values) UnlockGate(current3);
            foreach (var current4 in _yellowTeamItems.Values) UnlockGate(current4);
        }

        internal void LockGates()
        {
            foreach (var current in _redTeamItems.Values) LockGate(current);
            foreach (var current2 in _greenTeamItems.Values) LockGate(current2);
            foreach (var current3 in _blueTeamItems.Values) LockGate(current3);
            foreach (var current4 in _yellowTeamItems.Values) LockGate(current4);
        }

        internal void StopGame()
        {
            var team = GetWinningTeam();
            var item = GetFirstHighscore();
            if (item == null || _room == null) return;
            var winners = new List<RoomUser>();
            switch (team)
            {
                case Team.Blue:
                    winners = GetRoom().GetTeamManagerForFreeze().BlueTeam;
                    break;

                case Team.Red:
                    winners = GetRoom().GetTeamManagerForFreeze().RedTeam;
                    break;

                case Team.Yellow:
                    winners = GetRoom().GetTeamManagerForFreeze().YellowTeam;
                    break;

                case Team.Green:
                    winners = GetRoom().GetTeamManagerForFreeze().GreenTeam;
                    break;
            }
            var score = GetScoreForTeam(team);
            foreach (var winner in winners) item.HighscoreData.AddUserScore(item, winner.GetUserName(), score);
            item.UpdateState(false, true);
            if (OnGameEnd != null) OnGameEnd(null, null);
        }

        internal void StartGame()
        {
            if (OnGameStart != null) OnGameStart(null, null);
            GetRoom().GetWiredHandler().ResetExtraString(Interaction.ActionGiveScore);
        }

        internal Room GetRoom()
        {
            return _room;
        }

        internal void Destroy()
        {
            Array.Clear(TeamPoints, 0, TeamPoints.Length);
            _redTeamItems.Destroy();
            _blueTeamItems.Destroy();
            _greenTeamItems.Destroy();
            _yellowTeamItems.Destroy();
            TeamPoints = null;
            OnScoreChanged = null;
            OnGameStart = null;
            OnGameEnd = null;
            _redTeamItems = null;
            _blueTeamItems = null;
            _greenTeamItems = null;
            _yellowTeamItems = null;
            _room = null;
        }

        private static bool IsSoccerGoal(Interaction type)
        {
            return type == Interaction.FootballGoalBlue || type == Interaction.FootballGoalGreen ||
                   type == Interaction.FootballGoalRed || type == Interaction.FootballGoalYellow;
        }

        private int GetScoreForTeam(Team team)
        {
            return TeamPoints[(int) team];
        }

        private QueuedDictionary<uint, RoomItem> GetFurniItems(Team team)
        {
            switch (team)
            {
                case Team.Red:
                    return _redTeamItems;

                case Team.Green:
                    return _greenTeamItems;

                case Team.Blue:
                    return _blueTeamItems;

                case Team.Yellow:
                    return _yellowTeamItems;

                default:
                    return new QueuedDictionary<uint, RoomItem>();
            }
        }

        private void LockGate(RoomItem item)
        {
            var interactionType = item.GetBaseItem().InteractionType;
            if (!InteractionTypes.AreFamiliar(GlobalInteractions.GameGate, interactionType)) return;
            foreach (var current in _room.GetGameMap().GetRoomUsers(new Point(item.X, item.Y))) current.SqState = 0;
            _room.GetGameMap().GameMap[item.X, item.Y] = 0;
        }

        private void UnlockGate(RoomItem item)
        {
            var interactionType = item.GetBaseItem().InteractionType;
            if (!InteractionTypes.AreFamiliar(GlobalInteractions.GameGate, interactionType)) return;

            foreach (var current in _room.GetGameMap().GetRoomUsers(new Point(item.X, item.Y))) current.SqState = 1;
            _room.GetGameMap().GameMap[item.X, item.Y] = 1;
        }

        internal RoomItem GetFirstHighscore()
        {
            using (var enumerator = _room.GetRoomItemHandler().FloorItems.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var current2 = enumerator.Current;
                    if (current2.GetBaseItem().InteractionType != Interaction.WiredHighscore) continue;
                    var result = current2;
                    return result;
                }
            }
            return null;
        }
    }
}