using System;

namespace Yupi.Messages.Rooms
{
	public class RoomOnLoadMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<SendRoomCampaignFurnitureMessageComposer> ().Compose (session);
		}
	}
}

