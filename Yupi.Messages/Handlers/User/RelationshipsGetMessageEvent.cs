using System;

using System.Linq;

namespace Yupi.Messages.User
{
	public class RelationshipsGetMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			uint userId = message.GetUInt32();

			Habbo habbo = Yupi.GetHabboById(userId);

			if (habbo == null)
				return;

			router.GetComposer<RelationshipMessageComposer> ().Compose (session, habbo);
		}
	}
}

