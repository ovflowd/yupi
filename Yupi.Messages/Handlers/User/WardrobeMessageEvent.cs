using System;
using System.Data;


namespace Yupi.Messages.User
{
	public class WardrobeMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadWardrobeMessageComposer> ().Compose (session);
		}
	}
}

