using System;



namespace Yupi.Messages.Items
{
	public class ReedemExchangeItemMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			if (session?.GetHabbo() == null)
				return;

			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			if (Yupi.GetDbConfig().DbData["exchange_enabled"] != "1")
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("exchange_is_disabled"));
				return;
			}

			RoomItem item = room.GetRoomItemHandler().GetItem(request.GetUInt32());

			if (item == null)
				return;

			if (!item.GetBaseItem().Name.StartsWith("CF_") && !item.GetBaseItem().Name.StartsWith("CFC_"))
				return;

			// TODO Refactor
			string[] array = item.GetBaseItem().Name.Split('_');

			uint amount;

			if (array[1] == "diamond")
			{
				uint.TryParse(array[2], out amount);

				session.GetHabbo().Diamonds += amount;
				session.GetHabbo().UpdateSeasonalCurrencyBalance();
			}
			else
			{
				uint.TryParse(array[1], out amount);

				session.GetHabbo().Credits += amount;
				session.GetHabbo().UpdateCreditsBalance();
			}

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
				queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE id={item.Id} LIMIT 1;");

			room.GetRoomItemHandler().RemoveFurniture(null, item.Id, false);

			session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);

			router.GetComposer<UpdateInventoryMessageComposer> ().Compose (session);
			*/
			throw new NotImplementedException ();
		}
	}
}

