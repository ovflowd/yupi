// ---------------------------------------------------------------------------------
// <copyright file="HorseRemoveSaddleMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Pets
{
    using System;

    using Yupi.Messages.Rooms;

    public class HorseRemoveSaddleMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(session, true)))
                return;

            uint petId = request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet?.PetData == null || pet.PetData.OwnerId != session.GetHabbo().Id)
                return;

            pet.PetData.HaveSaddle = false;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.RunFastQuery(
                    $"UPDATE pets_data SET have_saddle = '0' WHERE id = {pet.PetData.PetId}");

                queryReactor.RunFastQuery(
                    $"INSERT INTO items_rooms (user_id, item_name) VALUES ({session.GetHabbo().Id}, 'horse_saddle1');");
            }

            session.GetHabbo().GetInventoryComponent().UpdateItems(true);

            router.GetComposer<SetRoomUserMessageComposer> ().Compose (room, pet);
            router.GetComposer<SerializePetMessageComposer> ().Compose (room, pet);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}