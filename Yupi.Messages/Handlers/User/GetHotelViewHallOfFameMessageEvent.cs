using System;

namespace Yupi.Messages.User
{
	public class GetHotelViewHallOfFameMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string code = message.GetString();
		}
	}
}

