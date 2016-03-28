using System.Collections.Generic;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Items.Wired.Handlers.Effects
{
    public class ShowMessage : IWiredItem
    {
        private readonly List<Interaction> _mBanned;

        public ShowMessage(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            OtherString = string.Empty;
            _mBanned = new List<Interaction>
            {
                Interaction.TriggerRepeater,
                Interaction.TriggerLongRepeater
            };
        }

        public Interaction Type => Interaction.ActionShowMessage;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items
        {
            get { return new List<RoomItem>(); }
            set { }
        }

        public int Delay
        {
            get { return 0; }
            set { }
        }

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

        public bool Execute(params object[] stuff)
        {
            if (stuff[0] == null)
                return false;

            RoomUser roomUser = (RoomUser) stuff[0];
            Interaction item = (Interaction) stuff[1];

            if (_mBanned.Contains(item))
                return false;

            if (roomUser?.GetClient() != null && !string.IsNullOrEmpty(OtherString))
                roomUser.GetClient().SendWhisper(OtherString, true);

            return true;
        }
    }
}