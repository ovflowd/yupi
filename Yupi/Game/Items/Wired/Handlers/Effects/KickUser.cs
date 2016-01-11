using System;
using System.Collections.Generic;
using System.Timers;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Wired.Handlers.Effects
{
    public class KickUser : IWiredItem
    {
        private readonly List<Interaction> _mBanned;
        private readonly List<RoomUser> _mUsers;
        private Timer _mTimer;

        public KickUser(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            OtherString = string.Empty;
            _mUsers = new List<RoomUser>();
            _mBanned = new List<Interaction>
            {
                Interaction.TriggerRepeater,
                Interaction.TriggerRoomEnter
            };
        }

        public Interaction Type => Interaction.ActionKickUser;

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

            if (roomUser?.GetClient() != null && roomUser.GetClient().GetHabbo() != null &&
                !string.IsNullOrWhiteSpace(OtherString))
            {
                if (roomUser.GetClient().GetHabbo().HasFuse("fuse_mod") || Room.RoomData.Owner == roomUser.GetUserName())
                    return false;

                roomUser.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(4, false);
                roomUser.GetClient().SendWhisper(OtherString);
                _mUsers.Add(roomUser);
            }

            if (_mTimer == null)
                _mTimer = new Timer(2000);

            _mTimer.Elapsed += ExecuteKick;
            _mTimer.Enabled = true;

            return true;
        }

        private void ExecuteKick(object source, ElapsedEventArgs eea)
        {
            try
            {
                _mTimer?.Stop();

                lock (_mUsers)
                {
                    foreach (RoomUser user in _mUsers)
                        Room.GetRoomUserManager().RemoveUserFromRoom(user.GetClient(), true, false);
                }

                _mUsers.Clear();
                _mTimer = null;
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}