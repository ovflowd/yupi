using System;

namespace Yupi.Messages.User
{
	public class HotelViewCountdownMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string time = message.GetString();
			router.GetComposer<HotelViewCountdownMessageComposer> ().Compose (session, time);
		}
	}
}

