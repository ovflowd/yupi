using System;
using Yupi.Model.Domain;
using System.Collections.Generic;



namespace Yupi.Messages.Rooms
{
	public class UserSignMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.RoomEntity == null)
				return;

			session.RoomEntity.Wake();

			int value = request.GetInteger();

			Sign sign;

			if (Sign.TryFromInt32 (value, out sign)) {
				session.RoomEntity.Status.Sign (sign);
			}
		}
	}
}

