using System;

namespace Yupi.Model.Domain
{
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

		public override void Send (Yupi.Protocol.Buffers.ServerMessage message)
		{
			
		}

		public override void OnRoomExit ()
		{
			
		}
	}
}

