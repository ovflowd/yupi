using System;




using Yupi.Messages.Notification;

namespace Yupi.Messages.Items
{
	public class UsePurchasableClothingMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint furniId = request.GetUInt32();

			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
			RoomItem item = room?.GetRoomItemHandler().GetItem(furniId);

			if (item?.GetBaseItem().InteractionType != Interaction.Clothing)
				return;
			ClothingItem clothes = Yupi.GetGame().GetClothingManager().GetClothesInFurni(item.GetBaseItem().Name);

			if (clothes == null)
				return;

			if (session.GetHabbo().ClothesManagerManager.Clothing.Contains(clothes.ItemName))
				return;

			session.GetHabbo().ClothesManagerManager.Add(clothes.ItemName);

			GetResponse().Init(PacketLibraryManager.OutgoingHandler("FigureSetIdsMessageComposer"));
			session.GetHabbo().ClothesManagerManager.Serialize(GetResponse());

			SendResponse();

			room.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);
		
			session.Router.GetComposer<SuperNotificationMessageComposer>()
				.Compose(session, "${notification.figureset.redeemed.success.title}", "${notification.figureset.redeemed.success.messageBuffer}", 
					"event:avatareditor/open", "${notification.figureset.redeemed.success.linkTitle}");

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
				queryReactor.SetQuery("DELETE FROM items_rooms WHERE id = @id");
				queryReactor.AddParameter("id", item.Id);
				queryReactor.RunQuery();
			}
		}
	}
}

