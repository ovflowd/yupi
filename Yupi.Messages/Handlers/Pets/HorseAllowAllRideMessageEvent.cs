// ---------------------------------------------------------------------------------
// <copyright file="HorseAllowAllRideMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class HorseAllowAllRideMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            uint petId = request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet.PetData.AnyoneCanRide == 1)
            {
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery($"UPDATE pets_data SET anyone_ride = '0' WHERE id={num} LIMIT 1");

                pet.PetData.AnyoneCanRide = 0;
            }
            else
            {
                using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryreactor2.RunFastQuery($"UPDATE pets_data SET anyone_ride = '1' WHERE id = {num} LIMIT 1");

                pet.PetData.AnyoneCanRide = 1;
            }

            router.GetComposer<PetInfoMessageComposer> ().Compose (room, pet.PetData);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}