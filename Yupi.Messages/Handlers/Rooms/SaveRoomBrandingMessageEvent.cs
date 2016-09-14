// ---------------------------------------------------------------------------------
// <copyright file="SaveRoomBrandingMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class SaveRoomBrandingMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint itemId = request.GetUInt32();
            uint count = request.GetUInt32();
            /*
            if (session?.GetHabbo() == null)
                return;
            Room room = session.GetHabbo().CurrentRoom;

            if (room == null || !room.CheckRights(session, true))
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(itemId);

            if (item == null)
                return;

            string extraData = "state"+Convert.ToChar(9)+"0";

            for (uint i = 1; i <= count; i++)
                extraData = extraData + Convert.ToChar(9) + request.GetString();

            item.ExtraData = extraData;

            room.GetRoomItemHandler().SetFloorItem(session, item, item.X, item.Y, item.Rot, false, false, true);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}