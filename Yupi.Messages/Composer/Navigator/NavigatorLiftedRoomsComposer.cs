using System;
using System.Collections.Generic;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
	public class NavigatorLiftedRoomsComposer : AbstractComposer<List<NavigatorHeader>>
	{
		public override void Compose (Yupi.Protocol.ISender session, List<NavigatorHeader> headers)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(headers.Count);

				foreach (NavigatorHeader navHeader in headers)
				{
					message.AppendInteger(navHeader.RoomId);
					message.AppendInteger(0);
					message.AppendString(navHeader.Image);
					message.AppendString(navHeader.Caption);
				}
				session.Send (message);
			}
		}
	}
}

