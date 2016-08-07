using System;

namespace Yupi.Messages.Groups
{
	public class GetGroupPurchasingInfoMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<GroupPurchasePartsMessageComposer> ().Compose (session);
		}
	}
}

