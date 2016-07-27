using System;
using System.Data;


namespace Yupi.Messages.User
{
	public class WardrobeMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadWardrobeMessageComposer> ().Compose (session);
		}
	}
}

