// ---------------------------------------------------------------------------------
// <copyright file="PetBreedResultMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class PetBreedResultMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            uint itemId = request.GetUInt32();

            RoomItem item = room.GetRoomItemHandler().GetItem(itemId);

            if (item == null)
                return;

            if (item.GetBaseItem().InteractionType != Interaction.BreedingTerrier &&
                item.GetBaseItem().InteractionType != Interaction.BreedingBear)
                return;

            string petName = request.GetString();

            item.ExtraData = "1";
            item.UpdateState();

            int randomNmb = new Random().Next(101);
            int petType = 0;
            int randomResult = 3;

            // TODO Refactor
            switch (item.GetBaseItem().InteractionType)
            {
            case Interaction.BreedingTerrier:
                if (randomNmb == 1)
                {
                    petType = PetBreeding.TerrierEpicRace[new Random().Next(PetBreeding.TerrierEpicRace.Length - 1)];

                    randomResult = 0;
                }
                else if (randomNmb <= 3)
                {
                    petType = PetBreeding.TerrierRareRace[new Random().Next(PetBreeding.TerrierRareRace.Length - 1)];

                    randomResult = 1;
                }
                else if (randomNmb <= 6)
                {
                    petType = PetBreeding.TerrierNoRareRace[new Random().Next(PetBreeding.TerrierNoRareRace.Length - 1)];

                    randomResult = 2;
                }
                else
                {
                    petType = PetBreeding.TerrierNormalRace[new Random().Next(PetBreeding.TerrierNormalRace.Length - 1)];

                    randomResult = 3;
                }

                break;

            case Interaction.BreedingBear:
                if (randomNmb == 1)
                {
                    petType = PetBreeding.BearEpicRace[new Random().Next(PetBreeding.BearEpicRace.Length - 1)];

                    randomResult = 0;
                }
                else if (randomNmb <= 3)
                {
                    petType = PetBreeding.BearRareRace[new Random().Next(PetBreeding.BearRareRace.Length - 1)];

                    randomResult = 1;
                }
                else if (randomNmb <= 6)
                {
                    petType = PetBreeding.BearNoRareRace[new Random().Next(PetBreeding.BearNoRareRace.Length - 1)];

                    randomResult = 2;
                }
                else
                {
                    petType = PetBreeding.BearNormalRace[new Random().Next(PetBreeding.BearNormalRace.Length - 1)];
                    randomResult = 3;
                }

                break;
            }

            Pet pet = CatalogManager.CreatePet(session.GetHabbo().Id, petName, item.GetBaseItem().InteractionType == Interaction.BreedingTerrier ? "pet_terrierbaby" : "pet_bearbaby",  petType.ToString(), "ffffff");

            if (pet == null)
                return;

            RoomUser petUser =
                room.GetRoomUserManager()
                    .DeployBot(
                        new RoomBot(pet.PetId, pet.OwnerId, pet.RoomId, AiType.Pet, "freeroam", pet.Name, string.Empty,
                            pet.Look, item.X, item.Y, 0.0, 4, null, null, string.Empty, 0, string.Empty), pet);

            if (petUser == null)
                return;

            item.ExtraData = "2";
            item.UpdateState();

            room.GetRoomItemHandler().RemoveFurniture(session, item.Id);

            switch (item.GetBaseItem().InteractionType)
            {
            case Interaction.BreedingTerrier:
                if (room.GetRoomItemHandler().BreedingTerrier.ContainsKey(item.Id))
                    room.GetRoomItemHandler().BreedingTerrier.Remove(item.Id);
                Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_TerrierBreeder", 1);
                break;

            case Interaction.BreedingBear:
                if (room.GetRoomItemHandler().BreedingBear.ContainsKey(item.Id))
                    room.GetRoomItemHandler().BreedingBear.Remove(item.Id);
                Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_BearBreeder", 1);
                break;
            }

            router.GetComposer<PetBreedResultMessageComposer> ().Compose (session, pet.PetId, randomResult);

            pet.X = item.X;
            pet.Y = item.Y;
            pet.RoomId = room.RoomId;
            pet.PlacedInRoom = true;

            if (pet.DbState != DatabaseUpdateState.NeedsInsert)
                pet.DbState = DatabaseUpdateState.NeedsUpdate;

            foreach (Pet pet2 in item.PetsList)
            {
                pet2.WaitingForBreading = 0;
                pet2.BreadingTile = new Point();

                RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet2.VirtualId);
                user.Freezed = false;
                room.GetGameMap().AddUserToMap(user, user.Coordinate);

                Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                user.MoveTo(nextCoord.X, nextCoord.Y);
            }

            item.PetsList.Clear();
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}