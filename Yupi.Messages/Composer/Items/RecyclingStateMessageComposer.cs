using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class RecyclingStateMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int insertId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendInteger(insertId);
				session.Send (message);
			}
		}
	}
}

