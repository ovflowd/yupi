using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class SendRoomCampaignFurnitureMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (0);
				session.Send (message);
			}
		}
	}
}

