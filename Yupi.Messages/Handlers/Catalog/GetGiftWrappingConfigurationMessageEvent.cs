using System;

namespace Yupi.Messages.Catalog
{
	public class GetGiftWrappingConfigurationMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<GiftWrappingConfigurationMessageComposer> ().Compose (session);
		}
	}
}

