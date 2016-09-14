// ---------------------------------------------------------------------------------
// <copyright file="MannequinUpdateDataMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class MannequinUpdateDataMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint pId = request.GetUInt32();

            /*
            RoomItem item = session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(pId);

            if (item == null)
                return;

            if (!item.ExtraData.Contains(Convert.ToChar(5)))
                return;

            if (!session.GetHabbo().CurrentRoom.CheckRights(session, true))
                return;

            string[] array = item.ExtraData.Split(Convert.ToChar(5));

            array[0] = session.GetHabbo().Gender.ToLower();
            array[1] = string.Empty;

            string[] array2 = session.GetHabbo().Look.Split('.');
            // TODO Use String.Join??? (need more knowlege about figure strings
            foreach (
                string text in
                array2.Where(
                    text =>
                    !text.Contains("hr") && !text.Contains("hd") && !text.Contains("he") && !text.Contains("ea") &&
                    !text.Contains("ha")))
            {
                array[1] += text + ".";
            }

            array[1] = array[1].TrimEnd('.');
            item.ExtraData = String.Join(Convert.ToChar(5).ToString(), array);
            item.UpdateNeeded = true;
            item.UpdateState(true, true);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
                queryReactor.AddParameter("extraData", item.ExtraData);
                queryReactor.AddParameter("id", item.Id);
                queryReactor.RunQuery();
            }
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}