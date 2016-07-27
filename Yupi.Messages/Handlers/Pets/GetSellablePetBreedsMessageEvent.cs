using System;

namespace Yupi.Messages.Pets
{
	public class GetSellablePetBreedsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string type = request.GetString();
			router.GetComposer<SellablePetBreedsMessageComposer> ().Compose (type);
		}
	}
}

