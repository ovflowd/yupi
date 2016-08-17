using System;
using Yupi.Model.Domain;



namespace Yupi.Messages.Rooms
{
	public class StartTypingMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Room room = session.Room;

			if (room == null) {
				return;
			}

			room.Router.GetComposer<TypingStatusMessageComposer> ().Compose (room, session.RoomEntity.Id, true);
		}
	}
}

