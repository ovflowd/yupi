// ---------------------------------------------------------------------------------
// <copyright file="OpenGiftMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Data;

    public class OpenGiftMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            if (currentRoom == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("gift_two"));
                return;
            }

            if (!currentRoom.CheckRights(session, true))
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("gift_three"));
                return;
            }

            uint itemId = request.GetUInt32();

            RoomItem item = currentRoom.GetRoomItemHandler().GetItem(itemId);

            if (item == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("gift_four"));
                return;
            }

            item.MagicRemove = true;

            router.GetComposer<UpdateRoomItemMessageComposer> ().Compose (currentRoom, item);

            session.GetHabbo().LastGiftOpenTime = DateTime.Now;
            IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor();

            queryReactor.SetQuery("SELECT * FROM users_gifts WHERE gift_id = @gift_id");
            queryReactor.AddParameter ("gift_id", item.Id);
            DataRow row = queryReactor.GetRow();

            if (row == null)
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);
                return;
            }

            Item item2 = Yupi.GetGame().GetItemManager().GetItem(Convert.ToUInt32(row["item_id"]));

            if (item2 == null)
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);
                return;
            }

            if (item2.Type.Equals('s'))
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);

                string extraData = row["extradata"].ToString();
                string itemName = row["item_name"].ToString();

                queryReactor.RunFastQuery($"UPDATE items_rooms SET item_name='{itemName}' WHERE id='{item.Id}'");

                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
                queryReactor.AddParameter("id", item.Id);
                queryReactor.AddParameter("extraData", extraData);
                queryReactor.RunQuery();

                queryReactor.SetQuery("DELETE FROM users_gifts WHERE gift_id=@id");
                queryReactor.AddParameter("id", item.Id);
                queryReactor.RunQuery();

                item.BaseName = itemName;
                item.RefreshItem();
                item.ExtraData = extraData;

                if (!currentRoom.GetRoomItemHandler().SetFloorItem(item, item.X, item.Y, item.Z, item.Rot, true))
                    session.SendNotif(Yupi.GetLanguage().GetVar("error_creating_gift"));
                else
                {
                    router.GetComposer<OpenGiftMessageComposer> ().Compose (session, item2, extraData);

                    router.GetComposer<AddFloorItemMessageComposer> ().Compose (currentRoom, item);
                    currentRoom.GetRoomItemHandler().SetFloorItem(session, item, item.X, item.Y, 0, true, false, true);
                }
            }
            else
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);

                queryReactor.SetQuery("DELETE FROM users_gifts WHERE gift_id = @id");
                queryReactor.AddParameter ("id", item.Id);
                queryReactor.RunQuery ();

                List<UserItem> list = Yupi.GetGame()
                    .GetCatalogManager()
                    .DeliverItems(session, item2, 1, (string) row["extradata"], 0, 0, string.Empty);

                router.GetComposer<NewInventoryObjectMessageComposer> ().Compose (session, item2, list);
                session.GetHabbo().GetInventoryComponent().UpdateItems(true);
            }
            router.GetComposer<UpdateInventoryMessageComposer> ().Compose (session);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}