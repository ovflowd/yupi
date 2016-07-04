using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class SendRoomCampaignFurnitureMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (0);
				session.Send (message);
			}
		}
	}
}

