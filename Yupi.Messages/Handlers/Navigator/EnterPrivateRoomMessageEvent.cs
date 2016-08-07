using System;

namespace Yupi.Messages.Navigator
{
	public class EnterPrivateRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint roomId = request.GetUInt32();

			string pWd = request.GetString();

			session.PrepareRoomForUser(roomId, pWd);
		}
	}
}

