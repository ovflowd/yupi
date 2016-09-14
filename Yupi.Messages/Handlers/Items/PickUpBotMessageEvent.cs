// ---------------------------------------------------------------------------------
// <copyright file="PickUpBotMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class PickUpBotMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint botId = request.GetUInt32();
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
            RoomUser bot = room.GetRoomUserManager().GetBot(botId);

            if (session?.GetHabbo() == null || session.GetHabbo().GetInventoryComponent() == null || bot == null ||
                !room.CheckRights(session, true))
                return;

            session.GetHabbo().GetInventoryComponent().AddBot(bot.BotData);

            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor()) {
                queryreactor2.SetQuery ("UPDATE bots_data SET room_id = '0' WHERE id = @id");
                queryreactor2.AddParameter ("id", botId);
                queryreactor2.RunQuery ();
            }
            room.GetRoomUserManager().RemoveBot(bot.VirtualId, false);
            bot.BotData.WasPicked = true;

            router.GetComposer<BotInventoryMessageComposer> ().Compose (session, session.GetHabbo ().GetInventoryComponent ()._inventoryBots);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}