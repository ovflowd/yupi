using System;

namespace Yupi.Messages.User
{
	public class GetHotelViewHallOfFameMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string code = message.GetString();
		}
	}
}

