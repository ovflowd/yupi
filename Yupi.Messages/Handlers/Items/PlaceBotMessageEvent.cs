// ---------------------------------------------------------------------------------
// <copyright file="PlaceBotMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Messages.Bots;

    public class PlaceBotMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            uint botId = request.GetUInt32();

            RoomBot bot = session.GetHabbo().GetInventoryComponent().GetBot(botId);

            if (bot == null)
                return;

            int x = request.GetInteger(); // coords
            int y = request.GetInteger();

            if (!room.GetGameMap().CanWalk(x, y, false) || !room.GetGameMap().ValidTile(x, y))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("bot_error_1"));
                return;
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                queryReactor.SetQuery ("UPDATE bots_data SET room_id = @room, x = @x, y = @y WHERE id = @id");
                queryReactor.AddParameter ("room", room.RoomId);
                queryReactor.AddParameter ("x", x);
                queryReactor.AddParameter ("y", y);
                queryReactor.AddParameter ("id", botId);
                queryReactor.RunQuery ();
            }

            bot.RoomId = room.RoomId;

            bot.X = x;
            bot.Y = y;

            room.GetRoomUserManager().DeployBot(bot, null);
            bot.WasPicked = false;

            session.GetHabbo().GetInventoryComponent().MoveBotToRoom(botId);
            router.GetComposer<BotInventoryMessageComposer> ().Compose (session, session.GetHabbo ().GetInventoryComponent ()._inventoryBots);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}