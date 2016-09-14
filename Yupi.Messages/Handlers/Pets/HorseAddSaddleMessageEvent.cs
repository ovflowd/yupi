// ---------------------------------------------------------------------------------
// <copyright file="HorseAddSaddleMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class HorseAddSaddleMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room =
                Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(session, true)))
                return;
            uint pId = request.GetUInt32();
            RoomItem item = room.GetRoomItemHandler().GetItem(pId);
            if (item == null)
                return;
            uint petId = request.GetUInt32();
            RoomUser pet = room.GetRoomUserManager().GetPet(petId);
            if (pet?.PetData == null || pet.PetData.OwnerId != session.GetHabbo().Id)
                return;
            bool isForHorse = true;
            {
                if (item.GetBaseItem().Name.Contains("horse_hairdye"))
                {
                    string s = item.GetBaseItem().Name.Split('_')[2];
                    int num = 48;
                    num += int.Parse(s);
                    pet.PetData.HairDye = num;
                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery ("UPDATE pets_data SET hairdye = @hairdye WHERE id = @id");
                        queryReactor.AddParameter ("hairdye", pet.PetData.HairDye);
                        queryReactor.AddParameter ("id", pet.PetData.PetId);
                        queryReactor.RunQuery ();
                    }
                }

                if (item.GetBaseItem().Name.Contains("horse_dye"))
                {
                    string s2 = item.GetBaseItem().Name.Split('_')[2];

                    uint num2 = uint.Parse(s2);
                    uint num3 = 2 + num2*4 - 4;

                    switch (num2)
                    {
                    case 13:
                        num3 = 61;
                        break;

                    case 14:
                        num3 = 65;
                        break;

                    case 15:
                        num3 = 69;
                        break;

                    case 16:
                        num3 = 73;
                        break;
                    }

                    pet.PetData.Race = num3;

                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery ("UPDATE pets_data Set race_id = @race WHERE id = @id");
                        queryReactor.AddParameter ("race", pet.PetData.Race);
                        queryReactor.AddParameter ("id", pet.PetData.PetId);
                        queryReactor.RunQuery();

                        queryReactor.SetQuery("DELETE FROM items_rooms WHERE id=@id");
                        queryReactor.AddParameter ("id", item.Id);
                        queryReactor.RunQuery();
                    }
                }
                if (item.GetBaseItem().Name.Contains("horse_hairstyle"))
                {
                    string s3 = item.GetBaseItem().Name.Split('_')[2];
                    int num4 = 100;
                    num4 += int.Parse(s3);
                    pet.PetData.PetHair = num4;
                    using (
                        IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery ("UPDATE pets_data SET pethair = @hair WHERE id = @id");
                        queryReactor.AddParameter ("hair", pet.PetData.PetHair);
                        queryReactor.AddParameter ("id", pet.PetData.PetId);
                        queryReactor.RunQuery ();

                        queryReactor.SetQuery ("DELETE FROM items_rooms WHERE id = @id");
                        queryReactor.AddParameter ("id", item.Id);
                        queryReactor.RunQuery ();
                    }
                }
                if (item.GetBaseItem().Name.Contains("saddle"))
                {
                    pet.PetData.HaveSaddle = true;
                    using (
                        IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery ("UPDATE pets_data SET have_saddle = '1' WHERE id = @id");
                        queryReactor.AddParameter ("id", pet.PetData.PetId);
                        queryReactor.RunQuery ();

                        queryReactor.SetQuery ("DELETE FROM items_rooms WHERE id=@id");
                        queryReactor.AddParameter ("id", item.Id);
                        queryReactor.RunQuery ();
                    }
                }
                if (item.GetBaseItem().Name == "mnstr_fert")
                {
                    if (pet.PetData.MoplaBreed.LiveState == MoplaState.Grown) return;
                    isForHorse = false;
                    pet.PetData.MoplaBreed.GrowingStatus = 7;
                    pet.PetData.MoplaBreed.LiveState = MoplaState.Grown;
                    pet.PetData.MoplaBreed.UpdateInDb();
                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery ("DELETE FROM items_rooms WHERE id=@id");
                        queryReactor.AddParameter ("id", item.Id);
                        queryReactor.RunQuery ();
                    }
                }

                room.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);

                router.GetComposer<SetRoomUserMessageComposer> ().Compose (room, pet);

                if (isForHorse)
                {
                    router.GetComposer<SerializePetMessageComposer> ().Compose (room, pet);
                }
            }
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}