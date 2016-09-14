// ---------------------------------------------------------------------------------
// <copyright file="PlacePetMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class PlacePetMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(session, true)) ||
                !room.CheckRights(session, true))
                return;

            uint petId = request.GetUInt32();

            Pet pet = session.GetHabbo().GetInventoryComponent().GetPet(petId);

            if (pet == null || pet.PlacedInRoom)
                return;

            int x = request.GetInteger();
            int y = request.GetInteger();

            if (!room.GetGameMap().CanWalk(x, y, false))
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"UPDATE pets_data SET room_id = {room.RoomId}, x = {x}, y = {y} WHERE id = {petId}");

            pet.PlacedInRoom = true;
            pet.RoomId = room.RoomId;

            room.GetRoomUserManager()
                .DeployBot(
                    new RoomBot(pet.PetId, Convert.ToUInt32(pet.OwnerId), pet.RoomId, AiType.Pet, "freeroam", pet.Name,
                        string.Empty, pet.Look, x, y, 0.0, 4, null, null, string.Empty, 0, string.Empty), pet);

            session.GetHabbo().GetInventoryComponent().MovePetToRoom(pet.PetId);

            if (pet.DbState != DatabaseUpdateState.NeedsInsert)
                pet.DbState = DatabaseUpdateState.NeedsUpdate;

            using (IQueryAdapter queryreactor = Yupi.GetDatabaseManager().GetQueryReactor())
                room.GetRoomUserManager().SavePets(queryreactor);

            session.Send(session.GetHabbo().GetInventoryComponent().SerializePetInventory());
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}