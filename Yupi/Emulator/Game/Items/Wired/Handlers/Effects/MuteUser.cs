using System;
using System.Collections.Generic;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Items.Wired.Handlers.Effects
{
    public class MuteUser : IWiredItem
    {
        public MuteUser(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            OtherString = string.Empty;
            OtherExtraString = string.Empty;
            OtherExtraString2 = string.Empty;
            Delay = 0;
        }

        public Interaction Type => Interaction.ActionMuteUser;

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
            RoomUser roomUser = (RoomUser) stuff[0];

            if (roomUser == null || roomUser.IsBot || roomUser.GetClient() == null ||
                roomUser.GetClient().GetHabbo() == null)
                return false;

            if (roomUser.GetClient().GetHabbo().Rank > 3)
                return false;

            if (Delay == 0)
                return false;

            int minutes = Delay/500;

            uint userId = roomUser.GetClient().GetHabbo().Id;

            if (Room.MutedUsers.ContainsKey(userId))
                Room.MutedUsers.Remove(userId);

            Room.MutedUsers.Add(userId, Convert.ToUInt32(Yupi.GetUnixTimeStamp() + minutes*60));

            if (!string.IsNullOrEmpty(OtherString))
                roomUser.GetClient().SendWhisper(OtherString);

            return true;
        }
    }
}