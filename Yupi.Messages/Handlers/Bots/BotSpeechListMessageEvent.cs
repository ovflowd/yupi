using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Messages.Bots
{
	public class BotSpeechListMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint botId = request.GetUInt32();
			int num = request.GetInteger(); // TODO meaning?

			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			// TODO introduce entity classes and proper inheritance structure
			RoomUser bot = room?.GetRoomUserManager().GetBot(botId);

			if (bot == null || !bot.IsBot)
				return;

			router.GetComposer<BotSpeechListMessageComposer> ().Compose (session, num, bot.BotData);
		}
	}
}

