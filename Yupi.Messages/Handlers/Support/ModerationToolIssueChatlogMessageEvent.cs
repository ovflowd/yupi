using System;
using Yupi.Emulator.Game.Support;
using Yupi.Emulator.Game.Rooms.Data.Models;

namespace Yupi.Messages.Support
{
	public class ModerationToolIssueChatlogMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			if (!session.GetHabbo().HasFuse("fuse_mod"))
				return;

			SupportTicket ticket = Yupi.GetGame().GetModerationTool().GetTicket(message.GetUInt32());

			if (ticket == null)
				return;

			RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateNullableRoomData(ticket.RoomId);

			if (roomData == null)
				return;

			router.GetComposer<ModerationToolIssueChatlogMessageComposer> ().Compose (session, ticket, roomData);
		}
	}
}

