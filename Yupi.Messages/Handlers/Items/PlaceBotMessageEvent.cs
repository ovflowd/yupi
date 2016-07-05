using System;


using Yupi.Messages.Bots;

namespace Yupi.Messages.Items
{
	public class PlaceBotMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			uint botId = request.GetUInt32();

			RoomBot bot = session.GetHabbo().GetInventoryComponent().GetBot(botId);

			if (bot == null)
				return;

			int x = request.GetInteger(); // coords
			int y = request.GetInteger();

			if (!room.GetGameMap().CanWalk(x, y, false) || !room.GetGameMap().ValidTile(x, y))
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("bot_error_1"));
				return;
			}

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("UPDATE bots_data SET room_id = @room, x = @x, y = @y WHERE id = @id");
				queryReactor.AddParameter ("room", room.RoomId);
				queryReactor.AddParameter ("x", x);
				queryReactor.AddParameter ("y", y);
				queryReactor.AddParameter ("id", botId);
				queryReactor.RunQuery ();
			}

			bot.RoomId = room.RoomId;

			bot.X = x;
			bot.Y = y;

			room.GetRoomUserManager().DeployBot(bot, null);
			bot.WasPicked = false;

			session.GetHabbo().GetInventoryComponent().MoveBotToRoom(botId);
			router.GetComposer<BotInventoryMessageComposer> ().Compose (session, session.GetHabbo ().GetInventoryComponent ()._inventoryBots);
		}
	}
}

