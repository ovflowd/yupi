using System;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Items
{
	public class SaveRoomBackgroundTonerMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			RoomItem item = room.GetRoomItemHandler().GetItem(room.TonerData.ItemId);

			if (item == null || item.GetBaseItem().InteractionType != Interaction.RoomBg)
				return;

			// TODO Unused
			request.GetInteger();

			int data1 = request.GetInteger();
			int data2 = request.GetInteger();
			int data3 = request.GetInteger();

			if (data1 > 255 || data2 > 255 || data3 > 255)
				return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
				queryReactor.SetQuery ("UPDATE items_toners SET enabled = @enabled, data1 = @data1, data2 = @data2, data3 = @data3 WHERE id = @id");
				queryReactor.AddParameter ("enabled", 1);
				queryReactor.AddParameter ("data1", data1);
				queryReactor.AddParameter ("data2", data2);
				queryReactor.AddParameter ("data3", data3);
				queryReactor.AddParameter ("id", item.Id);
			}

			room.TonerData.Data1 = data1;
			room.TonerData.Data2 = data2;
			room.TonerData.Data3 = data3;

			// TODO Enabled is int?!
			room.TonerData.Enabled = 1;

			router.GetComposer<UpdateRoomItemMessageComposer> ().Compose (room, item);
			item.UpdateState();
		}
	}
}

