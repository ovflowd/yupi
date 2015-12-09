using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Items.Wired.Handlers.Effects
{
    internal class Chase : IWiredItem
    {
        public Chase(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Items = new List<RoomItem>();
            Delay = 0;
        }

        public Interaction Type => Interaction.ActionChase;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

        public string OtherString
        {
            get { return string.Empty; }
            set { }
        }

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

        public int Delay { get; set; }

        public bool Execute(params object[] stuff)
        {
            if (Room == null) return false;
            var toRemove = new Queue<RoomItem>();

            if (Items.Any())
            {
                foreach (var item in Items)
                {
                    if (item == null || Room.GetRoomItemHandler().GetItem(item.Id) == null)
                    {
                        toRemove.Enqueue(item);
                        continue;
                    }

                    HandleMovement(item);
                }
            }


            while (toRemove.Count > 0)
            {
                var itemToRemove = toRemove.Dequeue();

                if (Items.Contains(itemToRemove))
                    Items.Remove(itemToRemove);
            }

            return true;
        }

        private void HandleMovement(RoomItem item)
        {
            var movement = Room.GetGameMap().GetChasingMovement(item.X, item.Y);

            if (movement == MovementState.None)
                return;

            var newPoint = Movement.HandleMovement(item.Coordinate, movement, item.Rot);

            if (newPoint == item.Coordinate)
                return;

            if (Room.GetGameMap().SquareHasUsers(newPoint.X, newPoint.Y))
            {
                var user = Room.GetRoomUserManager().GetUserForSquare(newPoint.X, newPoint.Y);

                if (user == null || user.IsBot || user.IsPet)
                    return;

                Room.GetWiredHandler().ExecuteWired(Interaction.TriggerCollision, user);
            }
            else if (Room.GetGameMap().SquareIsOpen(newPoint.X, newPoint.Y, false))
                Room.GetRoomItemHandler().SetFloorItem(null, item, newPoint.X, newPoint.Y, item.Rot, false, false, true, true, true);
        }
    }
}