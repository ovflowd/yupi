using System;


using System.Data;
using System.Collections.Generic;

namespace Yupi.Messages.Items
{
	public class OpenGiftMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			if (currentRoom == null)
			{
				session.SendWhisper(Yupi.GetLanguage().GetVar("gift_two"));
				return;
			}

			if (!currentRoom.CheckRights(session, true))
			{
				session.SendWhisper(Yupi.GetLanguage().GetVar("gift_three"));
				return;
			}

			uint itemId = request.GetUInt32();

			RoomItem item = currentRoom.GetRoomItemHandler().GetItem(itemId);

			if (item == null)
			{
				session.SendWhisper(Yupi.GetLanguage().GetVar("gift_four"));
				return;
			}

			item.MagicRemove = true;

			router.GetComposer<UpdateRoomItemMessageComposer> ().Compose (currentRoom, item);

			session.GetHabbo().LastGiftOpenTime = DateTime.Now;
			IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor();

			queryReactor.SetQuery("SELECT * FROM users_gifts WHERE gift_id = @gift_id");
			queryReactor.AddParameter ("gift_id", item.Id);
			DataRow row = queryReactor.GetRow();

			if (row == null)
			{
				currentRoom.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);
				return;
			}

			Item item2 = Yupi.GetGame().GetItemManager().GetItem(Convert.ToUInt32(row["item_id"]));

			if (item2 == null)
			{
				currentRoom.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);
				return;
			}

			if (item2.Type.Equals('s'))
			{
				currentRoom.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);

				string extraData = row["extradata"].ToString();
				string itemName = row["item_name"].ToString();

				queryReactor.RunFastQuery($"UPDATE items_rooms SET item_name='{itemName}' WHERE id='{item.Id}'");

				queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
				queryReactor.AddParameter("id", item.Id);
				queryReactor.AddParameter("extraData", extraData);
				queryReactor.RunQuery();

				queryReactor.SetQuery("DELETE FROM users_gifts WHERE gift_id=@id");
				queryReactor.AddParameter("id", item.Id);
				queryReactor.RunQuery();

				item.BaseName = itemName;
				item.RefreshItem();
				item.ExtraData = extraData;

				if (!currentRoom.GetRoomItemHandler().SetFloorItem(item, item.X, item.Y, item.Z, item.Rot, true))
					session.SendNotif(Yupi.GetLanguage().GetVar("error_creating_gift"));
				else
				{
					router.GetComposer<OpenGiftMessageComposer> ().Compose (session, item2, extraData);

					router.GetComposer<AddFloorItemMessageComposer> ().Compose (currentRoom, item);
					currentRoom.GetRoomItemHandler().SetFloorItem(session, item, item.X, item.Y, 0, true, false, true);
				}
			}
			else
			{
				currentRoom.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);

				queryReactor.SetQuery("DELETE FROM users_gifts WHERE gift_id = @id");
				queryReactor.AddParameter ("id", item.Id);
				queryReactor.RunQuery ();

	
				List<UserItem> list = Yupi.GetGame()
					.GetCatalogManager()
					.DeliverItems(session, item2, 1, (string) row["extradata"], 0, 0, string.Empty);

				router.GetComposer<NewInventoryObjectMessageComposer> ().Compose (session, item2, list);
				session.GetHabbo().GetInventoryComponent().UpdateItems(true);
			}
			router.GetComposer<UpdateInventoryMessageComposer> ().Compose (session);
			*/
			throw new NotImplementedException ();
		}
	}
}

