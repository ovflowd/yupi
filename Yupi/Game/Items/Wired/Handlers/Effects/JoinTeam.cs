using System.Collections.Generic;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.Items.Games.Teams;
using Yupi.Game.Rooms.Items.Games.Teams.Enums;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Wired.Handlers.Effects
{
    public class JoinTeam : IWiredItem
    {
        public JoinTeam(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Delay = 0;
        }

        public Interaction Type => Interaction.ActionJoinTeam;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items
        {
            get { return new List<RoomItem>(); }
            set { }
        }

        public int Delay { get; set; }

        public string OtherString { get; set; }

        public string OtherExtraString { get; set; }

        public string OtherExtraString2 { get; set; }

        public bool OtherBool { get; set; }

        public bool Execute(params object[] stuff)
        {
            if (stuff[0] == null)
                return false;

            RoomUser roomUser = (RoomUser) stuff[0];
            int team = Delay/500;
            TeamManager t = roomUser.GetClient().GetHabbo().CurrentRoom.GetTeamManagerForFreeze();

            if (roomUser.Team != Team.None)
            {
                t.OnUserLeave(roomUser);
                roomUser.Team = Team.None;
            }

            switch (team)
            {
                case 1:
                    roomUser.Team = Team.Red;
                    break;

                case 2:
                    roomUser.Team = Team.Green;
                    break;

                case 3:
                    roomUser.Team = Team.Blue;
                    break;

                case 4:
                    roomUser.Team = Team.Yellow;
                    break;
            }

            t.AddUser(roomUser);
            roomUser.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(Delay + 39);

            return true;
        }
    }
}