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
    internal class LongRepeater : IWiredItem, IWiredCycler
    {
        private long _mNext;

        public LongRepeater(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Delay = 10000;
            Room.GetWiredHandler().EnqueueCycle(this);

            if (_mNext == 0L || _mNext < Yupi.Now())
                _mNext = Yupi.Now() + Delay;
        }

        public Queue ToWork
        {
            get { return new Queue(); }
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

            if (conditions.Any())
            {
                foreach (IWiredItem current in conditions)
                {
                    if (!current.Execute(null))
                        return false;

                    WiredHandler.OnEvent(current);
                }
            }

            if (effects.Any())
            {
                foreach (IWiredItem current2 in effects)
                {
                    if (current2.Execute(null, Type))
                        WiredHandler.OnEvent(current2);
                }
            }

            _mNext = Yupi.Now() + Delay;
            return false;
        }

        public Interaction Type => Interaction.TriggerLongRepeater;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items
        {
            get { return new List<RoomItem>(); }
            set { }
        }

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
            if (_mNext == 0L || _mNext < Yupi.Now())
                _mNext = Yupi.Now() + Delay;

            if (!Room.GetWiredHandler().IsCycleQueued(this))
                Room.GetWiredHandler().EnqueueCycle(this);

            return true;
        }
    }
}