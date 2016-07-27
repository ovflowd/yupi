using System;

namespace Yupi.Messages.Other
{
	public class GenerateSecretKeyMessageEvent : AbstractHandler
	{
		public override bool RequireUser {
			get { 
				return false; 
			}
		}

		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			request.GetString(); // TODO unused

			router.GetComposer<SecretKeyMessageComposer> ().Compose (session);
		}
	}
}

