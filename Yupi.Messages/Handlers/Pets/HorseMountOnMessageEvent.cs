// ---------------------------------------------------------------------------------
// <copyright file="HorseMountOnMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class HorseMountOnMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            uint petId = request.GetUInt32();
            bool flag = request.GetBool();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet?.PetData == null)
                return;

            if (pet.PetData.AnyoneCanRide == 0 && pet.PetData.OwnerId != roomUserByHabbo.UserId)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("horse_error_1"));
                return;
            }

            if (flag)
            {
                if (pet.RidingHorse)
                {
                    string[] value = PetLocale.GetValue("pet.alreadymounted");

                    Random random = new Random();

                    pet.Chat(null, value[random.Next(0, value.Length - 1)], false, 0);
                }
                else if (!roomUserByHabbo.RidingHorse)
                {
                    pet.Statusses.Remove("sit");
                    pet.Statusses.Remove("lay");
                    pet.Statusses.Remove("snf");
                    pet.Statusses.Remove("eat");
                    pet.Statusses.Remove("ded");
                    pet.Statusses.Remove("jmp");

                    int x = roomUserByHabbo.X, y = roomUserByHabbo.Y;

                    room.Send(room.GetRoomItemHandler()
                        .UpdateUserOnRoller(pet, new Point(x, y), 0u, room.GetGameMap().SqAbsoluteHeight(x, y)));
                    room.GetRoomUserManager().UpdateUserStatus(pet, false);
                    room.Send(room.GetRoomItemHandler()
                        .UpdateUserOnRoller(roomUserByHabbo, new Point(x, y), 0u,
                            room.GetGameMap().SqAbsoluteHeight(x, y) + 1.0));
                    room.GetRoomUserManager().UpdateUserStatus(roomUserByHabbo, false);
                    pet.ClearMovement();
                    roomUserByHabbo.RidingHorse = true;
                    pet.RidingHorse = true;
                    pet.HorseId = (uint) roomUserByHabbo.VirtualId;
                    roomUserByHabbo.HorseId = Convert.ToUInt32(pet.VirtualId);
                    roomUserByHabbo.ApplyEffect(77);
                    roomUserByHabbo.Z += 1.0;
                    roomUserByHabbo.UpdateNeeded = true;
                    pet.UpdateNeeded = true;
                }
            }
            else if (roomUserByHabbo.VirtualId == pet.HorseId)
            {
                pet.Statusses.Remove("sit");
                pet.Statusses.Remove("lay");
                pet.Statusses.Remove("snf");
                pet.Statusses.Remove("eat");
                pet.Statusses.Remove("ded");
                pet.Statusses.Remove("jmp");
                roomUserByHabbo.RidingHorse = false;
                roomUserByHabbo.HorseId = 0u;
                pet.RidingHorse = false;
                pet.HorseId = 0u;
                roomUserByHabbo.MoveTo(new Point(roomUserByHabbo.X + 2, roomUserByHabbo.Y + 2));

                roomUserByHabbo.ApplyEffect(-1);
                roomUserByHabbo.UpdateNeeded = true;
                pet.UpdateNeeded = true;
            }
            else
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("horse_error_2"));
                return;
            }

            if (session.GetHabbo().Id != pet.PetData.OwnerId)
            {
                    Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(session, "ACH_HorseRent", 1);
            }

            router.GetComposer<SerializePetMessageComposer> ().Compose (room, pet);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}