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
    public class FurniStateToggled : IWiredItem, IWiredCycler
    {
        private readonly List<RoomUser> _mUsers;

        private long _mNext;

        public FurniStateToggled(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Items = new List<RoomItem>();
            Delay = 0;
            _mUsers = new List<RoomUser>();
        }

        public Queue ToWork
        {
            get { return null; }
            set { }
        }

        public ConcurrentQueue<RoomUser> ToWorkConcurrentQueue { get; set; }

        public bool OnCycle()
        {
            long num = Yupi.Now();

            if (_mNext >= num)
                return false;

            List<IWiredItem> conditions = Room.GetWiredHandler().GetConditions(this);
            List<IWiredItem> effects = Room.GetWiredHandler().GetEffects(this);

            foreach (RoomUser current in _mUsers)
            {
                if (conditions.Any())
                {
                    RoomUser current3 = current;

                    foreach (IWiredItem current2 in conditions.Where(current2 => current2.Execute(current3)))
                        WiredHandler.OnEvent(current2);
                }

                if (!effects.Any())
                    continue;

                RoomUser current1 = current;

                foreach (IWiredItem current3 in effects.Where(current3 => current3.Execute(current1, Type)))
                    WiredHandler.OnEvent(current3);
            }

            WiredHandler.OnEvent(this);
            _mNext = 0L;

            return true;
        }

        public Interaction Type => Interaction.TriggerStateChanged;

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

            if (roomUser == null || roomItem == null)
                return false;

            if (!Items.Contains(roomItem))
                return false;

            _mUsers.Add(roomUser);

            if (Delay == 0)
            {
                WiredHandler.OnEvent(this);
                OnCycle();
            }
            else
            {
                if (_mNext == 0L || _mNext < Yupi.Now())
                    _mNext = Yupi.Now() + Delay;

                Room.GetWiredHandler().EnqueueCycle(this);
            }

            return true;
        }
    }
}