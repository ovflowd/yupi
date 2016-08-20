using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
	public class HotelClosedMessageComposer : Contracts.HotelClosedMessageComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, int openHour, int openMinute, bool userThrownOut)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (openHour);
				message.AppendInteger (openMinute);
				message.AppendBool (userThrownOut);
				session.Send (message);
			}
		}
	}
}

