namespace Yupi.Model.Domain.Components
{
    using System;

    public class ModerationSettings
    {
        #region Constructors

        public ModerationSettings(RoomData room)
        {
            this.WhoCanMute = RoomModerationRight.None;
            this.WhoCanKick = RoomModerationRight.None;
            this.WhoCanBan = RoomModerationRight.None;
            this.Room = room;
        }

        protected ModerationSettings()
        {
        }

        #endregion Constructors

        #region Properties

        public virtual RoomModerationRight WhoCanBan
        {
            get; set;
        }

        public virtual RoomModerationRight WhoCanKick
        {
            get; set;
        }

        public virtual RoomModerationRight WhoCanMute
        {
            get; set;
        }

        protected virtual RoomData Room
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public bool CanBan(UserInfo info)
        {
            if (this.WhoCanBan == RoomModerationRight.None)
            {
                return this.Room.HasOwnerRights(info);
            }
            else
            {
                return this.Room.HasRights(info);
            }
        }

        public bool CanKick(UserInfo info)
        {
            if (this.WhoCanKick == RoomModerationRight.None)
            {
                return this.Room.HasOwnerRights(info);
            }
            else if (this.WhoCanKick == RoomModerationRight.UsersWithRights)
            {
                return this.Room.HasRights(info);
            }
            else
            {
                return true;
            }
        }

        public bool CanMute(UserInfo info)
        {
            if (this.WhoCanMute == RoomModerationRight.None)
            {
                return this.Room.HasOwnerRights(info);
            }
            else
            {
                return this.Room.HasRights(info);
            }
        }

        #endregion Methods
    }
}