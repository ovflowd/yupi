using System;

namespace Yupi.Messages.Groups
{
	public class GetGroupPurchasingInfoMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<GroupPurchasePartsMessageComposer> ().Compose (session);
		}
	}
}

