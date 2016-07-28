using System;
using Yupi.Messages.Bots;

namespace Yupi.Messages.User
{
	public class LoadBotInventoryMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<BotInventoryMessageComposer> ().Compose (session, session.GetHabbo ().GetInventoryComponent ()._inventoryBots);
		}
	}
}

