using System;

namespace Yupi.Model.Domain
{
	public class RoomMute
	{
		public virtual int Id { get; protected set; }
		public virtual RoomEntity Entity { get; set; }
		public virtual DateTime ExpiresAt { get; set; }

		// TODO Clean up mutes from time to time
		public virtual bool HasExpired() {
			return DateTime.Now > ExpiresAt;
		}
	}
}

