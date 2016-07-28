using System;

namespace Yupi.Messages.Catalog
{
	public class GetRecyclerRewardsMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<RecyclerRewardsMessageComposer> ().Compose (session);
		}
	}
}

