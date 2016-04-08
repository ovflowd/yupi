using System;

namespace Yupi.Messages.Other
{
	public class PongMessageEvent : AbstractHandler
	{
		public bool RequireUser {
			get { 
				return false; 
			}
		}

		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			session.TimePingedReceived = DateTime.Now;
		}
	}
}

