// ---------------------------------------------------------------------------------
// <copyright file="CompostMonsterplantMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class CompostMonsterplantMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_1"));
                return;
            }

            uint moplaId = request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(moplaId);

            if (pet == null || !pet.IsPet || pet.PetData.Type != "pet_monster" || pet.PetData.MoplaBreed == null)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_2"));
                return;
            }

            if (pet.PetData.MoplaBreed.LiveState != MoplaState.Dead)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_3"));
                return;
            }

            Item compostItem = Yupi.GetGame().GetItemManager().GetItemByName("mnstr_compost");

            if (compostItem == null)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_4"));
                return;
            }

            int x = pet.X;
            int y = pet.Y;
            double z = pet.Z;

            room.GetRoomUserManager().RemoveBot(pet.VirtualId, false);

            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery(
                    "INSERT INTO items_rooms (user_id, room_id, item_name, extra_data, x, y, z) VALUES (@uid, @rid, @bit, '0', @ex, @wai, @zed)");
                dbClient.AddParameter("uid", session.GetHabbo().Id);
                dbClient.AddParameter("rid", room.RoomId);
                dbClient.AddParameter("bit", compostItem.Name);
                dbClient.AddParameter("ex", x);
                dbClient.AddParameter("wai", y);
                dbClient.AddParameter("zed", z);

                uint itemId = (uint) dbClient.InsertQuery();

                RoomItem roomItem = new RoomItem(itemId, room.RoomId, compostItem.Name, "0", x, y, z, 0, room,
                    session.GetHabbo().Id, 0, string.Empty, false);

                if (!room.GetRoomItemHandler().SetFloorItem(session, roomItem, x, y, 0, true, false, true))
                {
                    session.GetHabbo().GetInventoryComponent().AddItem(roomItem);
                    session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_5"));
                }

                dbClient.RunFastQuery($"DELETE FROM pets_data WHERE id = {moplaId};");
                dbClient.RunFastQuery($"DELETE FROM pets_plants WHERE pet_id = {moplaId};");
            }
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}