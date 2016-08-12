using System;
using Yupi.Model.Domain;

namespace Yupi.Messages.Guides
{
	public class GuideInviteToRoom : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			Room room = session.UserData.Room;

			if (session.UserData.Room == null || session.UserData.GuideOtherUser == null) {
				return;
			}

			router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer> ().Compose (session.UserData.GuideOtherUser, room.Data.Id, room.Data.Name);

			// TODO Is this really required
			router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer> ().Compose (session, room.Data.Id, room.Data.Name);
		}
	}
}

