using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomApplySpaceMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            UserItem item = session.GetHabbo().GetInventoryComponent().GetItem(request.GetUInt32());

            if (item == null)
                return;

            // TODO Improve handling of type
            RoomSpacesMessageComposer.RoomSpacesType type = RoomSpacesMessageComposer.RoomSpacesType.FLOOR;

            if (item.BaseItem.Name.ToLower().Contains("wallpaper"))
                type = RoomSpacesMessageComposer.RoomSpacesType.WALLPAPER;
            else if (item.BaseItem.Name.ToLower().Contains("landscape"))
                type = RoomSpacesMessageComposer.RoomSpacesType.LANDSCAPE;

            switch (type)
            {
            case RoomSpacesMessageComposer.RoomSpacesType.FLOOR:

                room.RoomData.Floor = item.ExtraData;

                Yupi.GetGame()
                    .GetAchievementManager()
                    .ProgressUserAchievement(session, "ACH_RoomDecoFloor", 1);
                break;

            case RoomSpacesMessageComposer.RoomSpacesType.WALLPAPER:

                room.RoomData.WallPaper = item.ExtraData;

                Yupi.GetGame()
                    .GetAchievementManager()
                    .ProgressUserAchievement(session, "ACH_RoomDecoWallpaper", 1);
                break;

            case RoomSpacesMessageComposer.RoomSpacesType.LANDSCAPE:

                room.RoomData.LandScape = item.ExtraData;
                // TODO Handle Achivements eventbased?
                Yupi.GetGame()
                    .GetAchievementManager()
                    .ProgressUserAchievement(session, "ACH_RoomDecoLandscape", 1);
                break;
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE rooms_data SET " + type.ToString().ToLower() + " = @extradata WHERE id = @room");
                queryReactor.AddParameter("extradata", item.ExtraData);
                queryReactor.AddParameter("room", room.RoomId);
                queryReactor.RunQuery();

                queryReactor.SetQuery("DELETE FROM items_rooms WHERE id=@id");
                queryReactor.AddParameter("id", item.Id);
                queryReactor.RunQuery();
            }

            session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);

            router.GetComposer<RoomSpacesMessageComposer> ().Compose (room, type, room.RoomData);
            */
            throw new NotImplementedException();
        }
    }
}