using System;

namespace Yupi.Messages.Messenger
{
	public class RequestFriendMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo().GetMessenger() == null)
				return;

			session.GetHabbo().GetMessenger().RequestBuddy(request.GetString());
		}
	}
}

