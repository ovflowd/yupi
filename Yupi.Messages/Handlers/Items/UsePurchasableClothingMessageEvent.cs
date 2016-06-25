using System;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Messages.Enums;

namespace Yupi.Messages.Items
{
	public class UsePurchasableClothingMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint furniId = request.GetUInt32();

			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
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
			session.SendMessage(StaticMessage.FiguresetRedeemed);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
				queryReactor.SetQuery("DELETE FROM items_rooms WHERE id = @id");
				queryReactor.AddParameter("id", item.Id);
				queryReactor.RunQuery();
			}
		}
	}
}

