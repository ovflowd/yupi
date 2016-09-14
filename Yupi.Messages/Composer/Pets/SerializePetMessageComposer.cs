// ---------------------------------------------------------------------------------
// <copyright file="SerializePetMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    public class SerializePetMessageComposer : Yupi.Messages.Contracts.SerializePetMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, PetEntity pet)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(pet.Id);
                message.AppendInteger(pet.Info.Id);
                message.AppendInteger(pet.Info.RaceId);
                message.AppendInteger(pet.Info.Race);
                message.AppendString(pet.Info.Color.ToLower());
                if (pet.Info.HaveSaddle)
                {
                    message.AppendInteger(2);
                    message.AppendInteger(3);
                    message.AppendInteger(4);
                    message.AppendInteger(9);
                    message.AppendInteger(0);
                    message.AppendInteger(3);
                    message.AppendInteger(pet.Info.Hair);
                    message.AppendInteger(pet.Info.HairDye);
                    message.AppendInteger(3);
                    message.AppendInteger(pet.Info.Hair);
                    message.AppendInteger(pet.Info.HairDye);
                }
                else
                {
                    message.AppendInteger(1);
                    message.AppendInteger(2);
                    message.AppendInteger(2);
                    message.AppendInteger(pet.Info.Hair);
                    message.AppendInteger(pet.Info.HairDye);
                    message.AppendInteger(3);
                    message.AppendInteger(pet.Info.Hair);
                    message.AppendInteger(pet.Info.HairDye);
                }
                message.AppendBool(pet.Info.HaveSaddle);
                throw new NotImplementedException();
                //message.AppendBool(pet.RidingHorse);
                room.Send(message);
            }
        }

        #endregion Methods
    }
}