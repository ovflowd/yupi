// ---------------------------------------------------------------------------------
// <copyright file="EcotronRecycleMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class EcotronRecycleMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            if (!session.GetHabbo().InRoom)
                return;

            int slots = request.GetInteger();

            if (slots != Convert.ToUInt32(Yupi.GetDbConfig().DbData["recycler.number_of_slots"]))
                return;

            for(int i = 0; i < slots; ++i) {
                UserItem item = session.GetHabbo().GetInventoryComponent().GetItem(request.GetUInt32());

                if (item == null || !item.BaseItem.AllowRecycle)
                    return;

                session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE id={item.Id} LIMIT 1");
            }

            EcotronReward randomEcotronReward = Yupi.GetGame().GetCatalogManager().GetRandomEcotronReward();

            uint insertId;

            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryreactor2.SetQuery(
                    "INSERT INTO items_rooms (user_id,item_name,extra_data) VALUES (@userid, @baseName, @timestamp)");

                queryreactor2.AddParameter("userid", (int) session.GetHabbo().Id);
                queryreactor2.AddParameter("timestamp", DateTime.Now.ToLongDateString());
                queryreactor2.AddParameter("baseName", Yupi.GetDbConfig().DbData["recycler.box_name"]);

                insertId = (uint) queryreactor2.InsertQuery();

                queryreactor2.SetQuery ("INSERT INTO users_gifts (gift_id,item_id,gift_sprite,extradata) VALUES (@gift_id, @item_id, @gift_sprite, @extradata)");
                queryreactor2.AddParameter ("gift_id", insertId);
                queryreactor2.AddParameter ("item_id", randomEcotronReward.BaseId);
                queryreactor2.AddParameter ("gift_sprite", randomEcotronReward.DisplayId);
                queryreactor2.AddParameter ("extradata", "");
                queryreactor2.RunQuery ();
            }

            session.GetHabbo().GetInventoryComponent().UpdateItems(true);

            router.GetComposer<RecyclingStateMessageComposer> ().Compose (session, insertId);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}