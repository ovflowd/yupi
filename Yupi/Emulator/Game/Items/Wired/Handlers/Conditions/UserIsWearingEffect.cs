using System.Collections.Generic;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Items.Wired.Handlers.Conditions
{
     class UserIsWearingEffect : IWiredItem
    {
        public UserIsWearingEffect(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Items = new List<RoomItem>();
            OtherString = "0";
        }

        public Interaction Type => Interaction.ConditionUserWearingEffect;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

        public string OtherString { get; set; }

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

        public int Delay
        {
            get { return 0; }
            set { }
        }

        public bool Execute(params object[] stuff)
        {
            if (!(stuff[0] is RoomUser))
                return false;

            RoomUser roomUser = (RoomUser) stuff[0];

            int effect;

            if (!int.TryParse(OtherString, out effect))
                return false;

            if (roomUser.IsBot || roomUser.GetClient() == null)
                return false;

            return roomUser.CurrentEffect == effect;
        }
    }
}