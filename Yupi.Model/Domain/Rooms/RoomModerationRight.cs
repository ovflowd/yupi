using System;
using Headspring;

namespace Yupi.Model
{
	public class RoomModerationRight : Enumeration<RoomModerationRight>
	{
		public static readonly RoomModerationRight None = new RoomModerationRight(0, "None"); 
		public static readonly RoomModerationRight UsersWithRights = new RoomModerationRight(1, "UsersWithRights"); 
		public static readonly RoomModerationRight Everybody = new RoomModerationRight(2, "Everybody"); 

		private RoomModerationRight (int value, string displayName) : base (value, displayName)
		{
		}
		
	}
}

