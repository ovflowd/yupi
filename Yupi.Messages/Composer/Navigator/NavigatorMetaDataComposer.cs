using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
	public class NavigatorMetaDataComposer : Yupi.Messages.Contracts.NavigatorMetaDataComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(4);
				message.AppendString("official_view");
				message.AppendInteger(0);
				message.AppendString("hotel_view");
				message.AppendInteger(0);
				message.AppendString("roomads_view");
				message.AppendInteger(0);
				message.AppendString("myworld_view");
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

