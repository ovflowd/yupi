using System;

namespace Yupi.Messages.User
{
	public class GetSubscriptionDataMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<Yupi.Emulator.Game.GameClients.Interfaces.GameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			// TODO Refactor
			session.UserData.GetHabbo().SerializeClub();
		}
	}
}

