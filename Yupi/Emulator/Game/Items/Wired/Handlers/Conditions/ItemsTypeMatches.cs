using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Items.Wired.Handlers.Conditions
{
    internal class ItemsTypeMatches : IWiredItem
    {
        public ItemsTypeMatches(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Items = new List<RoomItem>();
        }

        public Interaction Type => Interaction.ConditionFurniTypeMatches;

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

        public int Delay
        {
            get { return 0; }
            set { }
        }

        public bool Execute(params object[] stuff)
        {
            if (!Items.Any())
                return true;

            RoomItem lastitem = null;

            foreach (RoomItem current in Items)
            {
                if (lastitem == null)
                {
                    lastitem = current;
                    continue;
                }

                if (current.GetBaseItem().InteractionType == Interaction.None ||
                    lastitem.GetBaseItem().InteractionType == Interaction.None)
                {
                    if (current.GetBaseItem().SpriteId != lastitem.GetBaseItem().SpriteId)
                        return false;
                }
                else
                {
                    if (current.GetBaseItem().InteractionType.ToString().StartsWith("banzai") &&
                        lastitem.GetBaseItem().InteractionType.ToString().StartsWith("banzai"))
                        continue;
                    if (current.GetBaseItem().InteractionType.ToString().StartsWith("football") &&
                        lastitem.GetBaseItem().InteractionType.ToString().StartsWith("football"))
                        continue;
                    if (current.GetBaseItem().InteractionType.ToString().StartsWith("freeze") &&
                        lastitem.GetBaseItem().InteractionType.ToString().StartsWith("freeze"))
                        continue;
                    if (current.GetBaseItem().InteractionType != lastitem.GetBaseItem().InteractionType)
                        return false;
                }
            }

            return true;
        }
    }
}