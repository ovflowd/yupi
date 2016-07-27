using System;

namespace Yupi.Messages.Catalog
{
	public class GetRecyclerRewardsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<RecyclerRewardsMessageComposer> ().Compose (session);
		}
	}
}

