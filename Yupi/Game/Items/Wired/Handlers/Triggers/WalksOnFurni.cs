using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Wired.Handlers.Triggers
{
    internal class WalksOnFurni : IWiredItem, IWiredCycler
    {
        private long _mNext;

        public WalksOnFurni(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            ToWork = new Queue();
            Items = new List<RoomItem>();
        }

        public Queue ToWork { get; set; }

        public ConcurrentQueue<RoomUser> ToWorkConcurrentQueue { get; set; }

        public bool OnCycle()
        {
            long num = Yupi.Now();

            if (num <= _mNext)
                return false;

            lock (ToWork.SyncRoot)
            {
                while (ToWork.Count > 0)
                {
                    RoomUser roomUser = (RoomUser) ToWork.Dequeue();
                    List<IWiredItem> conditions = Room.GetWiredHandler().GetConditions(this);
                    List<IWiredItem> effects = Room.GetWiredHandler().GetEffects(this);

                    if (conditions.Any())
                    {
                        foreach (IWiredItem current in conditions)
                        {
                            if (!current.Execute(roomUser))
                                return false;

                            WiredHandler.OnEvent(current);
                        }
                    }

                    if (!effects.Any())
                        continue;

                    foreach (IWiredItem current2 in effects.Where(current2 => current2.Execute(roomUser, Type)))
                        WiredHandler.OnEvent(current2);
                }
            }

            _mNext = 0L;
            WiredHandler.OnEvent(this);
            return true;
        }

        public Interaction Type => Interaction.TriggerWalkOnFurni;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

        public int Delay { get; set; }

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

        public bool Execute(params object[] stuff)
        {
            RoomUser roomUser = (RoomUser) stuff[0];
            RoomItem roomItem = (RoomItem) stuff[1];

            int userPosition = roomUser.X;
            int lastUserPosition = roomUser.CopyX;

            if (!Items.Contains(roomItem) ||
                (roomUser.LastItem != 0 && roomUser.LastItem == roomItem.Id && userPosition == lastUserPosition))
                return false;

            if (roomItem.GetRoom() == null || roomItem.GetRoom().GetRoomItemHandler() == null ||
                roomItem.GetRoom()
                    .GetRoomItemHandler()
                    .FloorItems.Values.Any(i => i.X == roomItem.X && i.Y == roomItem.Y && i.Z > roomItem.Z))
                return false;

            ToWork.Enqueue(roomUser);

            if (Delay == 0)
                OnCycle();
            else
            {
                _mNext = Yupi.Now() + Delay;

                Room.GetWiredHandler().EnqueueCycle(this);
            }

            return true;
        }
    }
}