using System;

namespace Yupi.Messages.Contracts
{
	public class HotelClosedMessageComposer : AbstractComposer<int, int, bool>
	{
		public override void Compose (Yupi.Protocol.ISender session, int openHour, int openMinute, bool userThrownOut)
		{
			
		}
	}
}

