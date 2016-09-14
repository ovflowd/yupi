// ---------------------------------------------------------------------------------
// <copyright file="PickUpItemMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Drawing;

    public class PickUpItemMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            // TODO Unused
            request.GetInteger();

            /*

            Yupi.Messages.Rooms room = session.GetHabbo().CurrentRoom;

            if (room?.GetRoomItemHandler() == null || session.GetHabbo() == null)
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(request.GetUInt32());

            if (item == null || item.GetBaseItem().InteractionType == Interaction.PostIt)
                return;

            if (item.UserId != session.GetHabbo().Id && !room.CheckRights(session, true))
                return;

            switch (item.GetBaseItem().InteractionType)
            {
            case Interaction.BreedingTerrier:

                if (room.GetRoomItemHandler().BreedingTerrier.ContainsKey(item.Id))
                    room.GetRoomItemHandler().BreedingTerrier.Remove(item.Id);

                foreach (Pet pet in item.PetsList)
                {
                    pet.WaitingForBreading = 0;
                    pet.BreadingTile = new Point();

                    RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);

                    if (user == null)
                        continue;

                    user.Freezed = false;
                    room.GetGameMap().AddUserToMap(user, user.Coordinate);

                    Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                    user.MoveTo(nextCoord.X, nextCoord.Y);
                }

                item.PetsList.Clear();
                break;

            case Interaction.BreedingBear:

                if (room.GetRoomItemHandler().BreedingBear.ContainsKey(item.Id))
                    room.GetRoomItemHandler().BreedingBear.Remove(item.Id);

                foreach (Pet pet in item.PetsList)
                {
                    pet.WaitingForBreading = 0;
                    pet.BreadingTile = new Point();

                    RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);

                    if (user == null)
                        continue;

                    user.Freezed = false;
                    room.GetGameMap().AddUserToMap(user, user.Coordinate);

                    Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                    user.MoveTo(nextCoord.X, nextCoord.Y);
                }

                item.PetsList.Clear();
                break;
            }
            if (item.IsBuilder)
            {
                using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    room.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);
                    session.GetHabbo().BuildersItemsUsed--;
                    router.GetComposer<BuildersClubUpdateFurniCountMessageComposer> ().Compose (session, session.GetHabbo ().BuildersItemsUsed);

                    adapter.SetQuery("DELETE FROM items_rooms WHERE id = @item_id");
                    adapter.AddParameter("item_id", item.Id);
                    adapter.RunQuery ();
                }
            }
            else
            {
                room.GetRoomItemHandler().RemoveFurniture(session, item.Id);

                session.GetHabbo()
                    .GetInventoryComponent()
                    .AddNewItem(item.Id, item.BaseName, item.ExtraData, item.GroupId, true, true, 0, 0);

                session.GetHabbo().GetInventoryComponent().UpdateItems(false);
            }*/
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}