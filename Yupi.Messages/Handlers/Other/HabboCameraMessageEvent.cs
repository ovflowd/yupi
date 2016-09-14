// ---------------------------------------------------------------------------------
// <copyright file="HabboCameraMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Other
{
    using System;
    using System.Data;

    public class HabboCameraMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            throw new NotImplementedException();
            /*
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery($"SELECT * FROM cms_stories_photos_preview WHERE user_id = {session.GetHabbo().Id} AND type = 'PHOTO' ORDER BY id DESC LIMIT 1");

                DataTable table = queryReactor.GetTable();

                foreach (DataRow dataRow in table.Rows)
                {
                    object date = dataRow["date"];
                    object room = dataRow["room_id"];
                    object photo = dataRow["id"];
                    object image = dataRow["image_url"];

                    using (IQueryAdapter queryReactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor2.SetQuery(
                            "INSERT INTO cms_stories_photos (user_id,user_name,room_id,image_preview_url,image_url,type,date,tags) VALUES (@user_id,@user_name,@room_id,@image_url,@image_url,@type,@date,@tags)");
                        queryReactor2.AddParameter("user_id", session.GetHabbo().Id);
                        queryReactor2.AddParameter("user_name", session.GetHabbo().Name);
                        queryReactor2.AddParameter("room_id", room);
                        queryReactor2.AddParameter("image_url", image);
                        queryReactor2.AddParameter("type", "PHOTO");
                        queryReactor2.AddParameter("date", date);
                        queryReactor2.AddParameter("tags", "");
                        queryReactor2.RunQuery();

                        string newPhotoData = "{\"t\":" + date + ",\"u\":\"" + photo + "\",\"m\":\"\",\"s\":" + room +
                            ",\"w\":\"" + image + "\"}";

                        UserItem item = session.GetHabbo()
                            .GetInventoryComponent()
                            .AddNewItem(0, "external_image_wallitem_poster", newPhotoData, 0, true, false, 0, 0);

                        session.GetHabbo().GetInventoryComponent().UpdateItems(false);
                        session.GetHabbo().Credits -= 2;
                        session.GetHabbo().UpdateCreditsBalance();
                        session.GetHabbo().GetInventoryComponent().SendNewItems(item.Id);
                    }
                }
            }
            */
            router.GetComposer<CameraPurchaseOkComposer>().Compose(session);
        }

        #endregion Methods
    }
}