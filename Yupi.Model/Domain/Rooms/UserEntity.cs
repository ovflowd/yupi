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

		public Habbo User;

		public override EntityType Type {
			get {
				return EntityType.User;
			}
		}

		public UserEntity (Habbo user)
		{
			this.User = user;
		}

		public override void Send (Yupi.Protocol.Buffers.ServerMessage message)
		{
			
		}

		public override void OnRoomExit ()
		{
			
		}
	}
}

