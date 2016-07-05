using System;

using System.Linq;

namespace Yupi.Messages.User
{
	public class RelationshipsGetMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			uint userId = message.GetUInt32();

			Habbo habbo = Yupi.GetHabboById(userId);

			if (habbo == null)
				return;

			router.GetComposer<RelationshipMessageComposer> ().Compose (session, habbo);
		}
	}
}

