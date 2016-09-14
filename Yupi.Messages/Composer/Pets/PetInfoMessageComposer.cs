// ---------------------------------------------------------------------------------
// <copyright file="PetInfoMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class PetInfoMessageComposer : Yupi.Messages.Contracts.PetInfoMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, PetInfo pet)
        {
            /*
            using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
                message.AppendInteger(pet.Id);
                message.AppendString(pet.Name);
                message.AppendInteger(pet.Level);
                message.AppendInteger(20);
                message.AppendInteger(pet.Experience);
                message.AppendInteger(pet.ExperienceGoal);
                message.AppendInteger(pet.Energy);
                message.AppendInteger(100);
                message.AppendInteger(pet.Nutrition);
                message.AppendInteger(150);
                message.AppendInteger(pet.Respect);
                message.AppendInteger(pet.Owner.Id);
                message.AppendInteger(pet.Age);
                message.AppendString(pet.Owner.Name);
                message.AppendInteger(1);
                message.AppendBool(pet.HaveSaddle);
                message.AppendBool(
                    Yupi.GetGame()
                    .GetRoomManager()
                    .GetRoom(pet.RoomId)
                    .GetRoomUserManager()
                    .GetRoomUserByVirtualId(pet.VirtualId)
                    .RidingHorse);
                message.AppendInteger(0);
                message.AppendInteger(pet.AnyoneCanRide);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendString(string.Empty);
                message.AppendBool(false);
                message.AppendInteger(-1);
                message.AppendInteger(-1);
                message.AppendInteger(-1);
                message.AppendBool(false);
                room.Send (message);
            }*/
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}