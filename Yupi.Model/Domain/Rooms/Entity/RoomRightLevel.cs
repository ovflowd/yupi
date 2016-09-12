using System;
using Headspring;

namespace Yupi.Model
{
	/// <summary>
	/// Room right level
	/// </summary>
	public class RoomRightLevel : Enumeration<RoomRightLevel>, IStatusString
	{
		/// <summary>
		/// No Rights
		/// </summary>
		public static readonly RoomRightLevel None = new RoomRightLevel(0, "None");

		/// <summary>
		/// Rights given by room owner
		/// </summary>
		public static readonly RoomRightLevel Rights = new RoomRightLevel(1, "Rights");

		/// <summary>
		/// Rights through group
		/// </summary>
		public static readonly RoomRightLevel Group_Rights = new RoomRightLevel(2, "Group_Rights");

		/// <summary>
		/// Rights as group admin
		/// </summary>
		public static readonly RoomRightLevel Group_Admin = new RoomRightLevel(3, "Group_Admin");

		/// <summary>
		/// Room Owner rights
		/// </summary>
		public static readonly RoomRightLevel Owner = new RoomRightLevel(4, "Owner");

		/// <summary>
		/// Moderator rights
		/// </summary>
		public static readonly RoomRightLevel Moderator = new RoomRightLevel(5, "Moderator");


		protected RoomRightLevel (int value, string displayName) : base(value, displayName)
		{
			
		}

		public string ToStatusString ()
		{
			return "flatctrl " + Value;
		}
	}
}

