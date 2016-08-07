using System;

namespace Yupi.Messages.Support
{
	public class ModerationToolRoomChatlogMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.GetHabbo().HasFuse("fuse_chatlogs"))
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("help_information_error_rank_low"));
				return;
			}

			message.GetInteger(); // TODO Unused
			uint roomId = message.GetUInt32();

			router.GetComposer<ModerationToolRoomChatlogMessageComposer> ().Compose (session, roomId);
		}
	}
}

