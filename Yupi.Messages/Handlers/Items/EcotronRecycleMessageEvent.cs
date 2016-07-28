using System;



namespace Yupi.Messages.Items
{
	public class EcotronRecycleMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (!session.GetHabbo().InRoom)
				return;

			int slots = request.GetInteger();

			if (slots != Convert.ToUInt32(Yupi.GetDbConfig().DbData["recycler.number_of_slots"]))
				return;

			for(int i = 0; i < slots; ++i) {
				UserItem item = session.GetHabbo().GetInventoryComponent().GetItem(request.GetUInt32());

				if (item == null || !item.BaseItem.AllowRecycle)
					return;

				session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);

				using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
					queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE id={item.Id} LIMIT 1");
			}

			EcotronReward randomEcotronReward = Yupi.GetGame().GetCatalogManager().GetRandomEcotronReward();

			uint insertId;

			using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryreactor2.SetQuery(
					"INSERT INTO items_rooms (user_id,item_name,extra_data) VALUES (@userid, @baseName, @timestamp)");

				queryreactor2.AddParameter("userid", (int) session.GetHabbo().Id);
				queryreactor2.AddParameter("timestamp", DateTime.Now.ToLongDateString());
				queryreactor2.AddParameter("baseName", Yupi.GetDbConfig().DbData["recycler.box_name"]);

				insertId = (uint) queryreactor2.InsertQuery();

				queryreactor2.SetQuery ("INSERT INTO users_gifts (gift_id,item_id,gift_sprite,extradata) VALUES (@gift_id, @item_id, @gift_sprite, @extradata)");
				queryreactor2.AddParameter ("gift_id", insertId);
				queryreactor2.AddParameter ("item_id", randomEcotronReward.BaseId);
				queryreactor2.AddParameter ("gift_sprite", randomEcotronReward.DisplayId);
				queryreactor2.AddParameter ("extradata", "");
				queryreactor2.RunQuery ();
			}

			session.GetHabbo().GetInventoryComponent().UpdateItems(true);

			router.GetComposer<RecyclingStateMessageComposer> ().Compose (session, insertId);
		}
	}
}

