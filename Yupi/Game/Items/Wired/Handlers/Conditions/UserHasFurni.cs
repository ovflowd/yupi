using System.Collections.Generic;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Wired.Handlers.Conditions
{
    internal class UserHasFurni : IWiredItem
    {
        public UserHasFurni(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Items = new List<RoomItem>();
            OtherString = string.Empty;
        }

        public Interaction Type => Interaction.ConditionUserHasFurni;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

        public string OtherString { get; set; }

        public string OtherExtraString
        {
            get { return string.Empty; }
            set { }
        }

        public string OtherExtraString2
        {
            get { return string.Empty; }
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
            RoomUser roomUser = stuff?[0] as RoomUser;

            if ((roomUser?.IsBot ?? true) || roomUser.GetClient() == null || roomUser.GetClient().GetHabbo() == null ||
                roomUser.GetClient().GetHabbo().GetInventoryComponent() == null || string.IsNullOrEmpty(OtherString))
                return false;

            string[] itemsIdsArray = OtherString.Split(';');

            foreach (string itemIdStr in itemsIdsArray)
            {
                uint itemId;

                if (!uint.TryParse(itemIdStr, out itemId))
                    continue;

                if (roomUser.GetClient().GetHabbo().GetInventoryComponent().HasBaseItem(itemId))
                    return true;
            }

            return false;
        }
    }
}