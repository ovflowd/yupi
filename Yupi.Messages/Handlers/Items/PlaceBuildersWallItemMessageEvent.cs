// ---------------------------------------------------------------------------------
// <copyright file="PlaceBuildersWallItemMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Items
{
    using System;

    public class PlaceBuildersWallItemMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            request.GetInteger(); // pageId
            uint itemId = request.GetUInt32();
            string extradata = request.GetString();
            string wallcoords = request.GetString();

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

        #endregion Methods
    }
}