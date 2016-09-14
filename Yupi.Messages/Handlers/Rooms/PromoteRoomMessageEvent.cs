// ---------------------------------------------------------------------------------
// <copyright file="PromoteRoomMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Messages.Catalog;

    public class PromoteRoomMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint pageId = request.GetUInt32();
            uint itemId = request.GetUInt32();

            /*
            CatalogPage page = Yupi.GetGame().GetCatalogManager().GetPage(pageId);
            CatalogItem catalogItem = page?.GetItem(itemId);

            if (catalogItem == null)
                return;

            // TODO num?
            uint num = request.GetUInt32();
            string text = request.GetString();

            request.GetBool(); // TODO Unused!

            string text2 = request.GetString();

            int category = request.GetInteger();

            // TODO Bail on error and don't create a new room instance!
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(num) ?? new Room();

            room.Start(Yupi.GetGame().GetRoomManager().GenerateNullableRoomData(num), true);

            if (!room.CheckRights(session, true))
                return;

            // TODO Why do we need to check this? Should be the responsibility of a setter...
            if (catalogItem.CreditsCost > 0)
            {
                if (catalogItem.CreditsCost > session.GetHabbo().Credits)
                    return;

                session.GetHabbo().Credits -= catalogItem.CreditsCost;
                session.GetHabbo().UpdateCreditsBalance();
            }

            if (catalogItem.DucketsCost > 0)
            {
                if (catalogItem.DucketsCost > session.GetHabbo().Duckets)
                    return;

                session.GetHabbo().Duckets -= catalogItem.DucketsCost;
                session.GetHabbo().UpdateActivityPointsBalance();
            }

            if (catalogItem.DiamondsCost > 0)
            {
                if (catalogItem.DiamondsCost > session.GetHabbo().Diamonds)
                    return;

                session.GetHabbo().Diamonds -= catalogItem.DiamondsCost;
                session.GetHabbo().UpdateSeasonalCurrencyBalance();
            }

            session.Router.GetComposer<PurchaseOKMessageComposer> ().Compose (session, catalogItem, catalogItem.Items);

            if (room.RoomData.Event != null && !room.RoomData.Event.HasExpired)
            {
                room.RoomData.Event.Time = Yupi.GetUnixTimeStamp();

                Yupi.GetGame().GetRoomEvents().SerializeEventInfo(room.RoomId);
            }
            else
            {
                Yupi.GetGame().GetRoomEvents().AddNewEvent(room.RoomId, text, text2, session, 7200, category);
                Yupi.GetGame().GetRoomEvents().SerializeEventInfo(room.RoomId);
            }

            // TODO Use Enum for Badges!
            session.GetHabbo().GetBadgeComponent().GiveBadge("RADZZ", true, session);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}