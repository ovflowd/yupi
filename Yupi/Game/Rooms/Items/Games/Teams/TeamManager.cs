using System.Collections.Generic;
using System.Drawing;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.Items.Games.Teams.Enums;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Rooms.Items.Games.Teams
{
    public class TeamManager
    {
        public List<RoomUser> BlueTeam;
        public string Game;
        public List<RoomUser> GreenTeam;
        public List<RoomUser> RedTeam;
        public List<RoomUser> YellowTeam;

        public static TeamManager CreateTeamforGame(string game)
        {
            return new TeamManager
            {
                Game = game,
                BlueTeam = new List<RoomUser>(),
                RedTeam = new List<RoomUser>(),
                GreenTeam = new List<RoomUser>(),
                YellowTeam = new List<RoomUser>()
            };
        }

        public bool CanEnterOnTeam(Team t)
        {
            if (t.Equals(Team.Blue)) return BlueTeam.Count < 5;
            if (t.Equals(Team.Red)) return RedTeam.Count < 5;
            if (t.Equals(Team.Yellow)) return YellowTeam.Count < 5;
            return t.Equals(Team.Green) && GreenTeam.Count < 5;
        }

        public void AddUser(RoomUser user)
        {
            if (user == null || user.GetClient() == null) return;
            if (user.Team.Equals(Team.Blue)) BlueTeam.Add(user);
            else
            {
                if (user.Team.Equals(Team.Red)) RedTeam.Add(user);
                else
                {
                    if (user.Team.Equals(Team.Yellow)) YellowTeam.Add(user);
                    else if (user.Team.Equals(Team.Green)) GreenTeam.Add(user);
                }
            }

            if (string.IsNullOrEmpty(Game)) return;
            switch (Game.ToLower())
            {
                case "banzai":
                    Room currentRoom = user.GetClient().GetHabbo().CurrentRoom;
                    using (IEnumerator<RoomItem> enumerator = currentRoom.GetRoomItemHandler().FloorItems.Values.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            RoomItem current = enumerator.Current;
                            if (current.GetBaseItem().InteractionType.Equals(Interaction.BanzaiGateBlue))
                            {
                                current.ExtraData = BlueTeam.Count.ToString();
                                current.UpdateState();
                                if (BlueTeam.Count != 5) continue;
                                foreach (
                                    RoomUser current2 in
                                        currentRoom.GetGameMap().GetRoomUsers(new Point(current.X, current.Y)))
                                    current2.SqState = 0;
                                currentRoom.GetGameMap().GameMap[current.X, current.Y] = 0;
                            }
                            else
                            {
                                if (current.GetBaseItem().InteractionType.Equals(Interaction.BanzaiGateRed))
                                {
                                    current.ExtraData = RedTeam.Count.ToString();
                                    current.UpdateState();
                                    if (RedTeam.Count != 5) continue;
                                    foreach (
                                        RoomUser current3 in
                                            currentRoom.GetGameMap().GetRoomUsers(new Point(current.X, current.Y)))
                                        current3.SqState = 0;
                                    currentRoom.GetGameMap().GameMap[current.X, current.Y] = 0;
                                }
                                else
                                {
                                    if (current.GetBaseItem().InteractionType.Equals(Interaction.BanzaiGateGreen))
                                    {
                                        current.ExtraData = GreenTeam.Count.ToString();
                                        current.UpdateState();
                                        if (GreenTeam.Count != 5) continue;
                                        foreach (
                                            RoomUser current4 in
                                                currentRoom.GetGameMap().GetRoomUsers(new Point(current.X, current.Y)))
                                            current4.SqState = 0;
                                        currentRoom.GetGameMap().GameMap[current.X, current.Y] = 0;
                                    }
                                    else
                                    {
                                        if (!current.GetBaseItem().InteractionType.Equals(Interaction.BanzaiGateYellow))
                                            continue;
                                        current.ExtraData = YellowTeam.Count.ToString();
                                        current.UpdateState();
                                        if (YellowTeam.Count != 5) continue;
                                        foreach (
                                            RoomUser current5 in
                                                currentRoom.GetGameMap().GetRoomUsers(new Point(current.X, current.Y)))
                                            current5.SqState = 0;
                                        currentRoom.GetGameMap().GameMap[current.X, current.Y] = 0;
                                    }
                                }
                            }
                        }
                    }
                    break;

                case "freeze":
                    Room currentRoom2 = user.GetClient().GetHabbo().CurrentRoom;
                    foreach (RoomItem current6 in currentRoom2.GetRoomItemHandler().FloorItems.Values)
                    {
                        switch (current6.GetBaseItem().InteractionType)
                        {
                            case Interaction.FreezeBlueGate:
                                current6.ExtraData = BlueTeam.Count.ToString();
                                current6.UpdateState();
                                break;

                            case Interaction.FreezeRedGate:
                                current6.ExtraData = RedTeam.Count.ToString();
                                current6.UpdateState();
                                break;

                            case Interaction.FreezeGreenGate:
                                current6.ExtraData = GreenTeam.Count.ToString();
                                current6.UpdateState();
                                break;

                            case Interaction.FreezeYellowGate:
                                current6.ExtraData = YellowTeam.Count.ToString();
                                current6.UpdateState();
                                break;
                        }
                    }
                    break;
            }
        }

        public void OnUserLeave(RoomUser user)
        {
            if (user == null) return;
            if (user.Team.Equals(Team.Blue)) BlueTeam.Remove(user);
            else
            {
                if (user.Team.Equals(Team.Red)) RedTeam.Remove(user);
                else
                {
                    if (user.Team.Equals(Team.Yellow)) YellowTeam.Remove(user);
                    else if (user.Team.Equals(Team.Green)) GreenTeam.Remove(user);
                }
            }
            if (string.IsNullOrEmpty(Game)) return;

            Room currentRoom = user.GetClient().GetHabbo().CurrentRoom;
            if (currentRoom == null) return;

            switch (Game.ToLower())
            {
                case "banzai":
                    using (IEnumerator<RoomItem> enumerator = currentRoom.GetRoomItemHandler().FloorItems.Values.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            RoomItem current = enumerator.Current;
                            if (current.GetBaseItem().InteractionType.Equals(Interaction.BanzaiGateBlue))
                            {
                                current.ExtraData = BlueTeam.Count.ToString();
                                current.UpdateState();
                                if (currentRoom.GetGameMap().GameMap[current.X, current.Y] != 0) continue;
                                foreach (
                                    RoomUser current2 in
                                        currentRoom.GetGameMap().GetRoomUsers(new Point(current.X, current.Y)))
                                    current2.SqState = 1;
                                currentRoom.GetGameMap().GameMap[current.X, current.Y] = 1;
                            }
                            else
                            {
                                if (current.GetBaseItem().InteractionType.Equals(Interaction.BanzaiGateRed))
                                {
                                    current.ExtraData = RedTeam.Count.ToString();
                                    current.UpdateState();
                                    if (currentRoom.GetGameMap().GameMap[current.X, current.Y] != 0) continue;
                                    foreach (
                                        RoomUser current3 in
                                            currentRoom.GetGameMap().GetRoomUsers(new Point(current.X, current.Y)))
                                        current3.SqState = 1;
                                    currentRoom.GetGameMap().GameMap[current.X, current.Y] = 1;
                                }
                                else
                                {
                                    if (current.GetBaseItem().InteractionType.Equals(Interaction.BanzaiGateGreen))
                                    {
                                        current.ExtraData = GreenTeam.Count.ToString();
                                        current.UpdateState();
                                        if (currentRoom.GetGameMap().GameMap[current.X, current.Y] != 0) continue;
                                        foreach (
                                            RoomUser current4 in
                                                currentRoom.GetGameMap().GetRoomUsers(new Point(current.X, current.Y)))
                                            current4.SqState = 1;
                                        currentRoom.GetGameMap().GameMap[current.X, current.Y] = 1;
                                    }
                                    else
                                    {
                                        if (!current.GetBaseItem().InteractionType.Equals(Interaction.BanzaiGateYellow))
                                            continue;
                                        current.ExtraData = YellowTeam.Count.ToString();
                                        current.UpdateState();
                                        if (currentRoom.GetGameMap().GameMap[current.X, current.Y] != 0) continue;
                                        foreach (
                                            RoomUser current5 in
                                                currentRoom.GetGameMap().GetRoomUsers(new Point(current.X, current.Y)))
                                            current5.SqState = 1;
                                        currentRoom.GetGameMap().GameMap[current.X, current.Y] = 1;
                                    }
                                }
                            }
                        }
                    }
                    break;

                case "freeze":
                    foreach (RoomItem current6 in currentRoom.GetRoomItemHandler().FloorItems.Values)
                    {
                        switch (current6.GetBaseItem().InteractionType)
                        {
                            case Interaction.FreezeBlueGate:
                                current6.ExtraData = BlueTeam.Count.ToString();
                                current6.UpdateState();
                                break;

                            case Interaction.FreezeRedGate:
                                current6.ExtraData = RedTeam.Count.ToString();
                                current6.UpdateState();
                                break;

                            case Interaction.FreezeGreenGate:
                                current6.ExtraData = GreenTeam.Count.ToString();
                                current6.UpdateState();
                                break;

                            case Interaction.FreezeYellowGate:
                                current6.ExtraData = YellowTeam.Count.ToString();
                                current6.UpdateState();
                                break;
                        }
                    }
                    break;
            }
        }
    }
}