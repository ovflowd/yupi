using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Wired.Handlers.Effects
{
    internal class TeleportToFurni : IWiredItem, IWiredCycler
    {
        private readonly List<Interaction> _mBanned;

        private long _mNext;

        public TeleportToFurni(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            ToWorkConcurrentQueue = new ConcurrentQueue<RoomUser>();
            Items = new List<RoomItem>();
            Delay = 0;
            _mNext = 0L;
            _mBanned = new List<Interaction>
            {
                Interaction.TriggerRepeater,
                Interaction.TriggerLongRepeater
            };
        }

        public Queue ToWork { get; set; }

        public ConcurrentQueue<RoomUser> ToWorkConcurrentQueue { get; set; }

        public bool OnCycle()
        {
            if (!ToWorkConcurrentQueue.Any())
                return true;

            if (Room?.GetRoomItemHandler() == null || Room.GetRoomItemHandler().FloorItems == null)
                return false;

            long num = Yupi.Now();
            List<RoomUser> toAdd = new List<RoomUser>();
            RoomUser roomUser;

            while (ToWorkConcurrentQueue.TryDequeue(out roomUser))
            {
                if (roomUser?.GetClient() == null)
                    continue;

                if (_mNext <= num)
                {
                    if (Teleport(roomUser))
                        continue;

                    return false;
                }

                if (_mNext - num < 500L && roomUser.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent() != null)
                    roomUser.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(4);

                toAdd.Add(roomUser);
            }

            foreach (RoomUser roomUserToAdd in toAdd.Where(roomUserToAdd => !ToWorkConcurrentQueue.Contains(roomUserToAdd)))
                ToWorkConcurrentQueue.Enqueue(roomUserToAdd);

            toAdd.Clear();

            if (_mNext >= num)
                return false;

            _mNext = 0L;
            return true;
        }

        public Interaction Type => Interaction.ActionTeleportTo;

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

        public int Delay { get; set; }

        public bool Execute(params object[] stuff)
        {
            if (stuff[0] == null)
                return false;

            RoomUser roomUser = (RoomUser) stuff[0];
            Interaction item = (Interaction) stuff[1];

            if (_mBanned.Contains(item))
                return false;

            if (!Items.Any())
                return false;

            if (!ToWorkConcurrentQueue.Contains(roomUser))
                ToWorkConcurrentQueue.Enqueue(roomUser);

            if (Delay < 500)
                Delay = 500;

            if (Room.GetWiredHandler().IsCycleQueued(this))
                return false;

            if (_mNext == 0L || _mNext < Yupi.Now())
                _mNext = Yupi.Now() + Delay;

            Room.GetWiredHandler().EnqueueCycle(this);

            return true;
        }

        private bool Teleport(RoomUser user)
        {
            if (!Items.Any())
                return true;

            if (user?.GetClient() == null || user.GetClient().GetHabbo() == null)
                return true;

            Random rnd = new Random();

            Items = (from x in Items orderby rnd.Next() select x).ToList();

            RoomItem roomItem = null;

            foreach (
                RoomItem current in
                    Items.Where(
                        current => current != null && Room.GetRoomItemHandler().FloorItems.ContainsKey(current.Id)))
                roomItem = current;

            if (roomItem == null)
            {
                user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(0);
                return false;
            }

            Room.GetGameMap().TeleportToItem(user, roomItem);
            Room.GetRoomUserManager().OnUserUpdateStatus();
            user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(0);

            return true;
        }
    }
}