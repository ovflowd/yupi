using System;

namespace Yupi.Model.Domain.Components
{
	public class RoomChatSettings
	{
		// TODO Enumerations!
		public virtual int Balloon { get; set; }
		public virtual int Speed { get; set; }
		public virtual int MaxDistance { get; set; }
		public virtual int FloodProtection { get; set; }
		public virtual int Type { get; set; }
	}
}

