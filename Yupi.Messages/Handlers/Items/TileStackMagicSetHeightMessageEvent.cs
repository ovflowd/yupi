// ---------------------------------------------------------------------------------
// <copyright file="TileStackMagicSetHeightMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class TileStackMagicSetHeightMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;

            uint itemId = request.GetUInt32();

            RoomItem item = room.GetRoomItemHandler().GetItem(itemId);

            if (item == null || item.GetBaseItem().InteractionType != Interaction.TileStackMagic)
                return;

            int heightToSet = request.GetInteger();
            double totalZ;

            if (heightToSet < 0)
            {
                totalZ = room.GetGameMap().SqAbsoluteHeight(item.X, item.Y);

                router.GetComposer<UpdateTileStackMagicHeightComposer> ().Compose (item.Id, totalZ);
            }
            else
            {
                if (heightToSet > 10000)
                    heightToSet = 10000;

                totalZ = heightToSet/100.0;

                if (totalZ < room.RoomData.Model.SqFloorHeight[item.X][item.Y])
                {
                    totalZ = room.RoomData.Model.SqFloorHeight[item.X][item.Y];
                    router.GetComposer<UpdateTileStackMagicHeightComposer> ().Compose (item.Id, totalZ);
                }
            }

            room.GetRoomItemHandler().SetFloorItem(item, item.X, item.Y, totalZ, item.Rot, true);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}