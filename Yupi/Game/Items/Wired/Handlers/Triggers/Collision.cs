using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Wired.Handlers.Triggers
{
    public class Collision : IWiredItem
    {
        private readonly WiredHandler _handler;

        public Collision(RoomItem item, WiredHandler handler, Room room)
        {
            Item = item;
            Room = room;
            OtherString = string.Empty;
            OtherBool = false;
            _handler = handler;
        }

        public Interaction Type => Interaction.TriggerCollision;

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
            get { return string.Empty; }
            set { }
        }

        public string OtherExtraString2
        {
            get { return string.Empty; }
            set { }
        }

        public bool OtherBool { get; set; }

        public bool Execute(params object[] stuff)
        {
            RoomUser roomUser = (RoomUser) stuff[0];

            List<IWiredItem> conditions = Room.GetWiredHandler().GetConditions(this);
            List<IWiredItem> effects = Room.GetWiredHandler().GetEffects(this);

            if (conditions.Any())
            {
                foreach (IWiredItem current in conditions)
                {
                    WiredHandler.OnEvent(current);

                    if (!current.Execute(roomUser))
                        return true;
                }
            }

            if (!effects.Any())
                return true;

            foreach (
                IWiredItem wiredItem in
                    effects.Where(
                        wiredItem =>
                            wiredItem != null && wiredItem.Type != Interaction.ActionChase &&
                            wiredItem.Execute(roomUser, Type)))
                WiredHandler.OnEvent(wiredItem);

            return true;
        }
    }
}