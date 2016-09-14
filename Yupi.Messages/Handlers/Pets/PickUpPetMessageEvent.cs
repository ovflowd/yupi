// ---------------------------------------------------------------------------------
// <copyright file="PickUpPetMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class PickUpPetMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (session?.GetHabbo() == null || session.GetHabbo().GetInventoryComponent() == null)
                return;

            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(session, true)))
                return;

            uint petId = request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet == null)
                return;

            if (pet.RidingHorse)
            {
                RoomUser roomUserByVirtualId = room.GetRoomUserManager().GetRoomUserByVirtualId(Convert.ToInt32(pet.HorseId));

                if (roomUserByVirtualId != null)
                {
                    roomUserByVirtualId.RidingHorse = false;
                    roomUserByVirtualId.ApplyEffect(-1);
                    roomUserByVirtualId.MoveTo(new Point(roomUserByVirtualId.X + 1, roomUserByVirtualId.Y + 1));
                }
            }

            if (pet.PetData.DbState != DatabaseUpdateState.NeedsInsert)
                pet.PetData.DbState = DatabaseUpdateState.NeedsUpdate;

            pet.PetData.RoomId = 0;

            session.GetHabbo().GetInventoryComponent().AddPet(pet.PetData);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                room.GetRoomUserManager().SavePets(queryReactor);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"UPDATE pets_data SET room_id = 0, x = 0, y = 0 WHERE id = {petId}");

            room.GetRoomUserManager().RemoveBot(pet.VirtualId, false);

            session.Send(session.GetHabbo().GetInventoryComponent().SerializePetInventory());
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}