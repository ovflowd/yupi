using System;


namespace Yupi.Messages.User
{
	public class LoadBadgeInventoryMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Refactor
			session.Send(session.GetHabbo().GetBadgeComponent().Serialize());
		}
	}
}

