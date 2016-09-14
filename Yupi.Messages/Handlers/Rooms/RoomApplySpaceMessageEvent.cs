// ---------------------------------------------------------------------------------
// <copyright file="RoomApplySpaceMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Messages.Rooms
{
    using System;

    public class RoomApplySpaceMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
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

        #endregion Methods
    }
}