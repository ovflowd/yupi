using System;



using Yupi.Messages.Catalog;

namespace Yupi.Messages.Rooms
{
	public class PromoteRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint pageId = request.GetUInt32();
			uint itemId = request.GetUInt32();

			CatalogPage page = Yupi.GetGame().GetCatalogManager().GetPage(pageId);
			CatalogItem catalogItem = page?.GetItem(itemId);

			if (catalogItem == null)
				return;

			// TODO num?
			uint num = request.GetUInt32();
			string text = request.GetString();

			request.GetBool(); // TODO Unused!

			string text2 = request.GetString();

			int category = request.GetInteger();

			// TODO Bail on error and don't create a new room instance!
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(num) ?? new Room();

			room.Start(Yupi.GetGame().GetRoomManager().GenerateNullableRoomData(num), true);

			if (!room.CheckRights(session, true))
				return;
			
			// TODO Why do we need to check this? Should be the responsibility of a setter...
			if (catalogItem.CreditsCost > 0)
			{
				if (catalogItem.CreditsCost > session.GetHabbo().Credits)
					return;

				session.GetHabbo().Credits -= catalogItem.CreditsCost;
				session.GetHabbo().UpdateCreditsBalance();
			}

			if (catalogItem.DucketsCost > 0)
			{
				if (catalogItem.DucketsCost > session.GetHabbo().Duckets)
					return;

				session.GetHabbo().Duckets -= catalogItem.DucketsCost;
				session.GetHabbo().UpdateActivityPointsBalance();
			}

			if (catalogItem.DiamondsCost > 0)
			{
				if (catalogItem.DiamondsCost > session.GetHabbo().Diamonds)
					return;

				session.GetHabbo().Diamonds -= catalogItem.DiamondsCost;
				session.GetHabbo().UpdateSeasonalCurrencyBalance();
			}

			session.Router.GetComposer<PurchaseOKMessageComposer> ().Compose (session, catalogItem, catalogItem.Items);

			if (room.RoomData.Event != null && !room.RoomData.Event.HasExpired)
			{
				room.RoomData.Event.Time = Yupi.GetUnixTimeStamp();

				Yupi.GetGame().GetRoomEvents().SerializeEventInfo(room.RoomId);
			}
			else
			{
				Yupi.GetGame().GetRoomEvents().AddNewEvent(room.RoomId, text, text2, session, 7200, category);
				Yupi.GetGame().GetRoomEvents().SerializeEventInfo(room.RoomId);
			}

			// TODO Use Enum for Badges!
			session.GetHabbo().GetBadgeComponent().GiveBadge("RADZZ", true, session);
		}
	}
}

