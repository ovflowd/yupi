using System.Collections.Generic;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Items.Wired.Handlers.Conditions
{
     class UserHasHanditem : IWiredItem
    {
        public UserHasHanditem(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Items = new List<RoomItem>();
        }

        public Interaction Type => Interaction.ConditionUserHasHanditem;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

        public string OtherString
        {
            get { return ""; }
            set { }
        }

        public string OtherExtraString
        {
            get { return ""; }
            set { }
        }

        public string OtherExtraString2
        {
            get { return ""; }
            set { }
        }

        public bool OtherBool
        {
            get { return true; }
            set { }
        }

        public int Delay { get; set; }

        public bool Execute(params object[] stuff)
        {
            RoomUser roomUser = (RoomUser) stuff[0];
            int handitem = Delay/500;

            if (handitem < 0)
                return false;

            if (roomUser.CarryItemId == handitem)
                return true;

            return false;
        }
    }
}