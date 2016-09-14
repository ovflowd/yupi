using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class PlaceBuildersWallItemMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            request.GetInteger(); // pageId
            var itemId = request.GetUInt32();
            var extradata = request.GetString();
            var wallcoords = request.GetString();

            /*
            Yupi.Messages.Rooms actualRoom = session.GetHabbo().CurrentRoom;
            CatalogItem item = Yupi.GetGame().GetCatalogManager().GetItem(itemId);
            if (actualRoom == null || item == null) return;

            session.GetHabbo().BuildersItemsUsed++;
            router.GetComposer<BuildersClubUpdateFurniCountMessageComposer> ().Compose (session, session.GetHabbo ().BuildersItemsUsed);

            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery(
                    "INSERT INTO items_rooms (user_id,room_id,item_name,wall_pos,builders) VALUES (@userId,@roomId,@baseName,@wallpos,'1')");
                adapter.AddParameter("userId", session.GetHabbo().Id);
                adapter.AddParameter("roomId", actualRoom.RoomId);
                adapter.AddParameter("baseName", item.BaseName);
                adapter.AddParameter("wallpos", wallcoords);

                uint insertId = (uint) adapter.InsertQuery();

                RoomItem newItem = new RoomItem(insertId, actualRoom.RoomId, item.BaseName, extradata,
                    new WallCoordinate(wallcoords), actualRoom, session.GetHabbo().Id, 0, true);

                actualRoom.GetRoomItemHandler().WallItems.TryAdd(newItem.Id, newItem);

                router.GetComposer<AddWallItemMessageComposer> ().Compose (session, newItem);
                actualRoom.GetGameMap().AddItemToMap(newItem);
            }
            */
            throw new NotImplementedException();
        }
    }
}