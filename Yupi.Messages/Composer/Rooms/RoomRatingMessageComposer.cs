using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomRatingMessageComposer : Yupi.Messages.Contracts.RoomRatingMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, int rating, bool canVote)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (rating);
				message.AppendBool (canVote);
				session.Send (message);
			}
		}	
	}
}

