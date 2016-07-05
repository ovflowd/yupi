using System;

namespace Yupi.Messages.Other
{
	public class UniqueIDMessageEvent : AbstractHandler
	{
		public override bool RequireUser {
			get { 
				return false; 
			}
		}

		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			request.GetString(); // TODO unused
			session.MachineId = request.GetString();
		}
	}
}

