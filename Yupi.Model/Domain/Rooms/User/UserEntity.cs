using System;

namespace Yupi.Model.Domain
{
	public class UserEntity : RoomEntity
	{
		public readonly Habbo UserInfo;

		public override EntityType Type {
			get {
				return EntityType.User;
			}
		}
	}
}

