using System;
using Yupi.Model.Domain;

namespace Yupi.Messages.Guides
{
	public class GuideInviteToRoom : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			Room room = session.Room;

			if (session.Room == null || session.GuideOtherUser == null) {
				return;
			}

			router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer> ().Compose (session.GuideOtherUser, room.Data.Id, room.Data.Name);

			// TODO Is this really required
			router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer> ().Compose (session, room.Data.Id, room.Data.Name);
		}
	}
}

