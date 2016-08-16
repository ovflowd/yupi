using System;
using Yupi.Model.Domain;



namespace Yupi.Messages.Rooms
{
	public class StopTypingMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Room room = session.UserData.Room;

			if (room == null) {
				return;
			}

			room.Router.GetComposer<TypingStatusMessageComposer> ().Compose (room, session.UserData.RoomEntity.Id, false);
		}
	}
}

