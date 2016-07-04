using System;

namespace Yupi.Messages.Navigator
{
	public class EnterPrivateRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint roomId = request.GetUInt32();

			string pWd = request.GetString();

			session.PrepareRoomForUser(roomId, pWd);
		}
	}
}

