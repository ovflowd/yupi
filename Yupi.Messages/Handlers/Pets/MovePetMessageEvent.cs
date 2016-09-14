// ---------------------------------------------------------------------------------
// <copyright file="MovePetMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Drawing;

    public class MovePetMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if ((room == null) || !room.CheckRights(session))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_6"));
                return;
            }

            uint petId = request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet == null || !pet.IsPet || pet.PetData.Type != "pet_monster")
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_7"));
                return;
            }

            int x = request.GetInteger();
            int y = request.GetInteger();
            int rot = request.GetInteger();
            int oldX = pet.X;
            int oldY = pet.Y;

            if ((x != oldX) && (y != oldY))
            {
                if (!room.GetGameMap().CanWalk(x, y, false))
                {
                    session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_8"));
                    return;
                }
            }

            if ((rot < 0) || (rot > 6) || (rot%2 != 0))
                rot = pet.RotBody;

            pet.PetData.X = x;
            pet.PetData.Y = y;
            pet.X = x;
            pet.Y = y;
            pet.RotBody = rot;
            pet.RotHead = rot;

            if (pet.PetData.DbState != DatabaseUpdateState.NeedsInsert)
                pet.PetData.DbState = DatabaseUpdateState.NeedsUpdate;

            pet.UpdateNeeded = true;
            room.GetGameMap().UpdateUserMovement(new Point(oldX, oldY), new Point(x, y), pet);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}