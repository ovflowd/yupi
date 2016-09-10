using System;
using Yupi.Model.Domain;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class UserEntity : RoomEntity
	{
		public UserInfo UserInfo { 
			get { 
				return User.Info; 
			} 
		}

		public override BaseInfo BaseInfo {
			get {
				return UserInfo;
			}
		}

		public Habbo User;

		public override EntityType Type {
			get {
				return EntityType.User;
			}
		}

		public UserEntity (Habbo user, Room room, int id) : base(room, id)
		{
			this.User = user;
		}

		public override void HandleChatMessage (UserEntity user, Action<Habbo> sendTo)
		{
			if (!User.Info.MutedUsers.Contains (user.User.Info)) {
				base.HandleChatMessage (user, sendTo);
				sendTo (User);
			}
		}
			
		public override void OnRoomExit ()
		{
			
		}
	}
}

