// ---------------------------------------------------------------------------------
// <copyright file="PetBreedMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    public class PetBreedMessageComposer : Yupi.Messages.Contracts.PetBreedMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint furniId, PetEntity pet1, PetEntity pet2)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(furniId);
                message.AppendInteger(pet1.Info.Id);
                message.AppendString(pet1.Info.Name);
                message.AppendInteger(pet1.Info.Level);
                message.AppendString(pet1.Info.Look);
                message.AppendString(pet1.Info.Owner.Name);
                message.AppendInteger(pet2.Info.Id);
                message.AppendString(pet2.Info.Name);
                message.AppendInteger(pet2.Info.Level);
                message.AppendString(pet2.Info.Look);
                message.AppendString(pet2.Info.Owner.Name);
                message.AppendInteger(4);

                message.AppendInteger(1);
                /*
                switch (pet1.Type)
                {
                case "pet_terrier":
                    message.AppendInteger(PetBreeding.TerrierEpicRace.Length);

                    foreach (int value in PetBreeding.TerrierEpicRace)
                        message.AppendInteger(value);

                    break;

                case "pet_bear":
                    message.AppendInteger(PetBreeding.BearEpicRace.Length);

                    foreach (int value in PetBreeding.BearEpicRace)
                        message.AppendInteger(value);

                    break;
                }

                message.AppendInteger(2);

                switch (pet1.Type)
                {
                case "pet_terrier":
                    message.AppendInteger(PetBreeding.TerrierRareRace.Length);

                    foreach (int value in PetBreeding.TerrierRareRace)
                        message.AppendInteger(value);

                    break;

                case "pet_bear":
                    message.AppendInteger(PetBreeding.BearRareRace.Length);

                    foreach (int value in PetBreeding.BearRareRace)
                        message.AppendInteger(value);

                    break;
                }

                message.AppendInteger(3);

                switch (pet1.Type)
                {
                case "pet_terrier":
                    message.AppendInteger(PetBreeding.TerrierNoRareRace.Length);

                    foreach (int value in PetBreeding.TerrierNoRareRace)
                        message.AppendInteger(value);

                    break;

                case "pet_bear":
                    message.AppendInteger(PetBreeding.BearNoRareRace.Length);

                    foreach (int value in PetBreeding.BearNoRareRace)
                        message.AppendInteger(value);

                    break;
                }

                message.AppendInteger(94);

                switch (pet1.Type)
                {
                case "pet_terrier":
                    message.AppendInteger(PetBreeding.TerrierNormalRace.Length);

                    foreach (int value in PetBreeding.TerrierNormalRace)
                        message.AppendInteger(value);

                    break;

                case "pet_bear":
                    message.AppendInteger(PetBreeding.BearNormalRace.Length);

                    foreach (int value in PetBreeding.BearNormalRace)
                        message.AppendInteger(value);

                    break;
                }

                message.AppendInteger(pet1.Type == "pet_terrier"
                    ? PetTypeManager.GetPetRaceIdByType("pet_terrierbaby")
                    : PetTypeManager.GetPetRaceIdByType("pet_bearbaby"));
                session.Send (message);
                */
                throw new NotImplementedException();
            }
        }

        #endregion Methods
    }
}