using System;

namespace Yupi.Model.Domain.Components
{
	public class ModerationSettings
	{
		public virtual RoomModerationRight WhoCanMute { get; set; }
		public virtual RoomModerationRight WhoCanKick { get; set; }
		public virtual RoomModerationRight WhoCanBan { get; set; }

		protected virtual RoomData Room { get; set; }

		protected ModerationSettings ()
		{
			
		}

		public ModerationSettings (RoomData room)
		{
			this.WhoCanMute = RoomModerationRight.None;
			this.WhoCanKick = RoomModerationRight.None;
			this.WhoCanBan = RoomModerationRight.None;
			this.Room = room;
		}

		public bool CanMute(UserInfo info) {
			if (this.WhoCanMute == RoomModerationRight.None) {
				return this.Room.HasOwnerRights(info);
			} else {
				return this.Room.HasRights (info);
			}
		}

		public bool CanKick(UserInfo info) {
			if (this.WhoCanKick == RoomModerationRight.None) {
				return this.Room.HasOwnerRights(info);
			} else if (this.WhoCanKick == RoomModerationRight.UsersWithRights) {
				return this.Room.HasRights (info);
			} else {
				return true;
			}
		}

		public bool CanBan(UserInfo info) {
			if (this.WhoCanBan == RoomModerationRight.None) {
				return this.Room.HasOwnerRights(info);
			} else {
				return this.Room.HasRights (info);
			}
		}
	}
}

