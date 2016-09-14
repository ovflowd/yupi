// ---------------------------------------------------------------------------------
// <copyright file="ReedemExchangeItemMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class ReedemExchangeItemMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            if (session?.GetHabbo() == null)
                return;

            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            if (Yupi.GetDbConfig().DbData["exchange_enabled"] != "1")
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("exchange_is_disabled"));
                return;
            }

            RoomItem item = room.GetRoomItemHandler().GetItem(request.GetUInt32());

            if (item == null)
                return;

            if (!item.GetBaseItem().Name.StartsWith("CF_") && !item.GetBaseItem().Name.StartsWith("CFC_"))
                return;

            // TODO Refactor
            string[] array = item.GetBaseItem().Name.Split('_');

            uint amount;

            if (array[1] == "diamond")
            {
                uint.TryParse(array[2], out amount);

                session.GetHabbo().Diamonds += amount;
                session.GetHabbo().UpdateSeasonalCurrencyBalance();
            }
            else
            {
                uint.TryParse(array[1], out amount);

                session.GetHabbo().Credits += amount;
                session.GetHabbo().UpdateCreditsBalance();
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE id={item.Id} LIMIT 1;");

            room.GetRoomItemHandler().RemoveFurniture(null, item.Id, false);

            session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);

            router.GetComposer<UpdateInventoryMessageComposer> ().Compose (session);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}