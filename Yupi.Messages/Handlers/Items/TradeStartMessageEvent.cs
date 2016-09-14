// ---------------------------------------------------------------------------------
// <copyright file="TradeStartMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class TradeStartMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;

            if (room.RoomData.TradeState == 0)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled"));
                return;
            }

            if (room.RoomData.TradeState == 1 && !room.CheckRights(session))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled_no_rights"));
                return;
            }

            if (Yupi.GetDbConfig().DbData["trading_enabled"] != "1")
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled_hotel"));
                return;
            }

            if (!session.GetHabbo().CheckTrading())
                session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled_mod"));

            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            RoomUser roomUserByVirtualId = room.GetRoomUserManager().GetRoomUserByVirtualId(request.GetInteger());

            if (roomUserByVirtualId?.GetClient() == null || roomUserByVirtualId.GetClient().GetHabbo() == null)
                return;

            room.TryStartTrade(roomUserByHabbo, roomUserByVirtualId);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}