using System;

namespace Yupi.Messages.Pets
{
	public class GetSellablePetBreedsMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string type = request.GetString();
			router.GetComposer<SellablePetBreedsMessageComposer> ().Compose (session, type);
		}
	}
}

