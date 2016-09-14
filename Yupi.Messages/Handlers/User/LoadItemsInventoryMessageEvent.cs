using System;

using Yupi.Messages.Items;

using Yupi.Messages.Notification;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.User
{
	public class LoadItemsInventoryMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadInventoryMessageComposer> ().Compose (session, session.Info.Inventory);
		}
	}
}

