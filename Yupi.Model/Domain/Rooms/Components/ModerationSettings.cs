namespace Yupi.Model.Domain.Components
{
    public class ModerationSettings
    {
        protected ModerationSettings()
        {
        }

        public ModerationSettings(RoomData room)
        {
            WhoCanMute = RoomModerationRight.None;
            WhoCanKick = RoomModerationRight.None;
            WhoCanBan = RoomModerationRight.None;
            Room = room;
        }

        public virtual RoomModerationRight WhoCanMute { get; set; }
        public virtual RoomModerationRight WhoCanKick { get; set; }
        public virtual RoomModerationRight WhoCanBan { get; set; }

        protected virtual RoomData Room { get; set; }

        public bool CanMute(UserInfo info)
        {
            if (WhoCanMute == RoomModerationRight.None) return Room.HasOwnerRights(info);
            return Room.HasRights(info);
        }

        public bool CanKick(UserInfo info)
        {
            if (WhoCanKick == RoomModerationRight.None) return Room.HasOwnerRights(info);
            if (WhoCanKick == RoomModerationRight.UsersWithRights) return Room.HasRights(info);
            return true;
        }

        public bool CanBan(UserInfo info)
        {
            if (WhoCanBan == RoomModerationRight.None) return Room.HasOwnerRights(info);
            return Room.HasRights(info);
        }
    }
}