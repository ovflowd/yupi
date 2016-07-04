using System;

namespace Yupi.Messages.Other
{
	public class UniqueIDMessageEvent : AbstractHandler
	{
		public bool RequireUser {
			get { 
				return false; 
			}
		}

		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			request.GetString(); // TODO unused
			session.MachineId = request.GetString();
		}
	}
}

