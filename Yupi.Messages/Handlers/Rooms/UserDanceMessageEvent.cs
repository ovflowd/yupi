using System;
using Yupi.Model.Domain;



namespace Yupi.Messages.Rooms
{
	public class UserDanceMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.RoomEntity == null)
				return;

			session.RoomEntity.Wake();

			int danceId = request.GetInteger();

			Dance dance;

			if (Dance.TryFromInt32 (danceId, out dance)) {
				// TODO Remove Item from hand
				session.RoomEntity.SetDance(dance);
			}
		}
	}
}

