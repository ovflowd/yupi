// ---------------------------------------------------------------------------------
// <copyright file="SaveFootballGateOutfitMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class SaveFootballGateOutfitMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint pId = request.GetUInt32();
            string gender = request.GetString();
            string look = request.GetString();

            /*

            Yupi.Messages.Rooms room = session.GetHabbo().CurrentRoom;

            RoomItem item = room?.GetRoomItemHandler().GetItem(pId);

            if (item == null || item.GetBaseItem().InteractionType != Interaction.FootballGate)
                return;

            string[] figures = item.ExtraData.Split(',');
            string[] newFigures = new string[2];

            switch (gender.ToUpper())
            {
            case "M":
                {
                    newFigures[0] = look;

                    newFigures[1] = figures.Length > 1 ? figures[1] : "hd-99999-99999.ch-630-62.lg-695-62";

                    item.ExtraData = string.Join(",", newFigures);
                }
                break;

            case "F":
                {
                    newFigures[0] = !string.IsNullOrWhiteSpace(figures[0]) ? figures[0] : "hd-99999-99999.lg-270-62";

                    newFigures[1] = look;

                    item.ExtraData = string.Join(",", newFigures);
                }
                break;
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
                queryReactor.AddParameter("extraData", item.ExtraData);
                queryReactor.AddParameter("id", item.Id);
                queryReactor.RunQuery();
            }

            router.GetComposer<UpdateRoomItemMessageComposer> ().Compose (session, item);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}