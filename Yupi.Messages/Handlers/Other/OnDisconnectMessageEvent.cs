using System;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.Other
{
	public class OnDisconnectMessageEvent : AbstractHandler
	{
		public override bool RequireUser {
			get { 
				return false; 
			}
		}

		private ClientManager ClientManager;

		public OnDisconnectMessageEvent ()
		{
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{

			ClientManager.Disconnect(session, "User disconnected");
		}
	}
}

