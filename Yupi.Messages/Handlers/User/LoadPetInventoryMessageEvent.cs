using System;

namespace Yupi.Messages.User
{
	public class LoadPetInventoryMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			session.Router.GetComposer<PetInventoryMessageComposer>().Compose(session, session.Info.Inventory.Pets);
		}
	}
}

