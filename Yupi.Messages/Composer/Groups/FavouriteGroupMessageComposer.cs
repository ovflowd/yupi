using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
	public class FavouriteGroupMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int userId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (userId);
				session.Send (message);
			}
		}
	}
}

