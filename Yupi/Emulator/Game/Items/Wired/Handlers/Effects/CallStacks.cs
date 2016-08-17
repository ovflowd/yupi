using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Items.Wired.Handlers.Effects
{
    public class CallStacks : IWiredItem
    {
        public CallStacks(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Items = new List<RoomItem>();
        }

        public Interaction Type => Interaction.ActionCallStacks;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

        public int Delay { get; set; }

        public string OtherString { get; set; }

        public string OtherExtraString { get; set; }

        public string OtherExtraString2 { get; set; }

        public bool OtherBool { get; set; }

        public bool Execute(params object[] stuff)
        {
            RoomUser roomUser = (RoomUser) stuff[0];

            foreach (
                IWiredItem wired in
                    Items.Where(item => item.IsWired)
                        .Select(item => Room.GetWiredHandler().GetWired(item))
                        .Where(wired => wired != null))
            {
                WiredHandler.OnEvent(wired);
                wired.Execute(roomUser, Type);
            }

            return true;
        }
    }
}