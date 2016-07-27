using System;

using Yupi.Messages.User;

namespace Yupi.Messages.Guides
{
	public class VisitRoomGuides : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo().GuideOtherUser == null)
				return;

			GameClient requester = session.GetHabbo().GuideOtherUser;

			router.GetComposer<RoomForwardMessageComposer> ().Compose (session, requester.GetHabbo ().CurrentRoomId);
		}
	}
}

