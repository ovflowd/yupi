using System;
using System.Collections.Generic;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class ModerationTool
	{
		public IList<SupportTicket> Tickets { get; private set; }
		public IList<ModerationTemplate> Templates { get; private set; }
		public IList<string> UserMessagePresets { get; private set; }
		public IList<string> RoomMessagePresets { get; private set; }

		public ModerationTool ()
		{
		}
	}
}

