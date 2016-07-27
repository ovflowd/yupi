using System;

namespace Yupi.Messages.Messenger
{
	public class FriendListUpdateMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			session.GetHabbo().GetMessenger();
		}
	}
}

