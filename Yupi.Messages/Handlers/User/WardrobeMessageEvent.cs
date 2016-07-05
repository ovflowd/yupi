using System;
using System.Data;


namespace Yupi.Messages.User
{
	public class WardrobeMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadWardrobeMessageComposer> ().Compose (session);
		}
	}
}

