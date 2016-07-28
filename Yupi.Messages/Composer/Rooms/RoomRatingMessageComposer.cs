using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomRatingMessageComposer : AbstractComposer<int, bool>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int rating, bool canVote)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (rating);
				message.AppendBool (canVote);
				session.Send (message);
			}
		}	
	}
}

