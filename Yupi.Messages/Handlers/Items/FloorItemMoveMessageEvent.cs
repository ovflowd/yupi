// ---------------------------------------------------------------------------------
// <copyright file="FloorItemMoveMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Collections.Generic;
    using System.Drawing;

    public class FloorItemMoveMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint id = request.GetUInt32();
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;

            if (!room.CheckRights(session, false, true))
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(id);

            if (item == null)
                return;

            int x = request.GetInteger();
            int y = request.GetInteger();
            int rot = request.GetInteger();
            // TODO Unused
            request.GetInteger();

            bool flag = item.GetBaseItem().InteractionType == Interaction.Teleport ||
                item.GetBaseItem().InteractionType == Interaction.Hopper ||
                item.GetBaseItem().InteractionType == Interaction.QuickTeleport;

            List<Point> oldCoords = item.GetCoords;

            if (!room.GetRoomItemHandler().SetFloorItem(session, item, x, y, rot, false, false, true, true, false))
            {
                router.GetComposer<UpdateRoomItemMessageComposer> ().Compose (room, item);
                return;
            }

            if (item.GetBaseItem().InteractionType == Interaction.BreedingTerrier ||
                item.GetBaseItem().InteractionType == Interaction.BreedingBear)
            {
                foreach (Pet pet in item.PetsList)
                {
                    pet.WaitingForBreading = 0;
                    pet.BreadingTile = new Point();

                    RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);

                    if (user == null)
                        continue;

                    user.Freezed = false;
                    room.GetGameMap().AddUserToMap(user, user.Coordinate);

                    Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                    user.MoveTo(nextCoord.X, nextCoord.Y);
                }

                item.PetsList.Clear();
            }

            List<Point> newcoords = item.GetCoords;
            room.GetRoomItemHandler().OnHeightMapUpdate(oldCoords, newcoords);

            if (!flag)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                room.GetRoomItemHandler().SaveFurniture(queryReactor);
                */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}