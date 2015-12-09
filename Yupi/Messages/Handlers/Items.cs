using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Yupi.Core.Io;
using Yupi.Game.Catalogs;
using Yupi.Game.Items;
using Yupi.Game.Items.Datas;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired;
using Yupi.Game.Pets;
using Yupi.Game.Pets.Enums;
using Yupi.Game.Quests;
using Yupi.Game.RoomBots;
using Yupi.Game.RoomBots.Enumerators;
using Yupi.Game.Rooms.User;
using Yupi.Messages.Enums;
using Yupi.Messages.Parsers;

namespace Yupi.Messages.Handlers
{
    partial class GameClientMessageHandler
    {
        internal void PetBreedCancel()
        {
            if (Session?.GetHabbo() == null)
                return;

            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            var itemId = Request.GetUInteger();

            var item = room.GetRoomItemHandler().GetItem(itemId);

            if (item == null)
                return;

            if (item.GetBaseItem().InteractionType != Interaction.BreedingTerrier && item.GetBaseItem().InteractionType != Interaction.BreedingBear)
                return;

            foreach (var pet in item.PetsList)
            {
                pet.WaitingForBreading = 0;
                pet.BreadingTile = new Point();

                var user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);
                user.Freezed = false;
                room.GetGameMap().AddUserToMap(user, user.Coordinate);

                var nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                user.MoveTo(nextCoord.X, nextCoord.Y);
            }

            item.PetsList.Clear();
        }

        internal void PetBreedResult()
        {
            if (Session?.GetHabbo() == null)
                return;

            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            var itemId = Request.GetUInteger();

            var item = room.GetRoomItemHandler().GetItem(itemId);

            if (item == null)
                return;

            if (item.GetBaseItem().InteractionType != Interaction.BreedingTerrier && item.GetBaseItem().InteractionType != Interaction.BreedingBear)
                return;

            var petName = Request.GetString();
            // petid1
            // petid2

            item.ExtraData = "1";
            item.UpdateState();

            var randomNmb = new Random().Next(101);
            var petType = 0;
            var randomResult = 3;

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
                        petType =
                            PetBreeding.TerrierNoRareRace[new Random().Next(PetBreeding.TerrierNoRareRace.Length - 1)];
                        randomResult = 2;
                    }
                    else
                    {
                        petType =
                            PetBreeding.TerrierNormalRace[new Random().Next(PetBreeding.TerrierNormalRace.Length - 1)];
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

            var pet = CatalogManager.CreatePet(Session.GetHabbo().Id, petName,
                ((item.GetBaseItem().InteractionType == Interaction.BreedingTerrier) ? 25 : 24), petType.ToString(),
                "ffffff");
            if (pet == null)
                return;

            var petUser =
                room.GetRoomUserManager()
                    .DeployBot(
                        new RoomBot(pet.PetId, pet.OwnerId, pet.RoomId, AiType.Pet, "freeroam", pet.Name, "", pet.Look,
                            item.X, item.Y, 0.0, 4, null, null, "", 0, ""), pet);
            if (petUser == null)
                return;

            item.ExtraData = "2";
            item.UpdateState();

            room.GetRoomItemHandler().RemoveFurniture(Session, item.Id);

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

            /*Session.GetMessageHandler().GetResponse().Init(Outgoing.RemovePetBreedingPanel);
            Session.GetMessageHandler().GetResponse().AppendUInt(itemId);
            Session.GetMessageHandler().GetResponse().AppendInt32(0);
            Session.GetMessageHandler().SendResponse();*/

            Session.GetMessageHandler()
                .GetResponse()
                .Init(LibraryParser.OutgoingRequest("PetBreedResultMessageComposer"));
            Session.GetMessageHandler().GetResponse().AppendInteger(pet.PetId);
            Session.GetMessageHandler().GetResponse().AppendInteger(randomResult);
            Session.GetMessageHandler().SendResponse();

            pet.X = item.X;
            pet.Y = item.Y;
            pet.RoomId = room.RoomId;
            pet.PlacedInRoom = true;

            if (pet.DbState != DatabaseUpdateState.NeedsInsert)
                pet.DbState = DatabaseUpdateState.NeedsUpdate;

            foreach (var pet2 in item.PetsList)
            {
                pet2.WaitingForBreading = 0;
                pet2.BreadingTile = new Point();

                var user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet2.VirtualId);
                user.Freezed = false;
                room.GetGameMap().AddUserToMap(user, user.Coordinate);

                var nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                user.MoveTo(nextCoord.X, nextCoord.Y);
            }

            item.PetsList.Clear();
        }

        //PlacePetErrorMessageComposer struct : int(0-5)->errorCode room.error.pets.
        //PlaceBotErrorMessageComposer struct : int(0-4)->errorCode room.error.bots.

        internal void GetTrainerPanel()
        {
            var petId = Request.GetUInteger();
            var currentRoom = Session.GetHabbo().CurrentRoom;
            if (currentRoom == null)
                return;
            Pet petData;
            if ((petData = currentRoom.GetRoomUserManager().GetPet(petId).PetData) == null)
                return;
            //var arg_3F_0 = petData.Level;
            Response.Init(LibraryParser.OutgoingRequest("PetTrainerPanelMessageComposer"));
            Response.AppendInteger(petData.PetId);

            var availableCommands = new List<short>();

            Response.AppendInteger(petData.PetCommands.Count);
            foreach (short sh in petData.PetCommands.Keys)
            {
                Response.AppendInteger(sh);
                if (petData.PetCommands[sh])
                    availableCommands.Add(sh);
            }

            Response.AppendInteger(availableCommands.Count);
            foreach (short sh in availableCommands)
                Response.AppendInteger(sh);

            SendResponse();
        }

        internal void PlacePostIt()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session))
                return;
            var id = Request.GetUInteger();
            var locationData = Request.GetString();
            var item = Session.GetHabbo().GetInventoryComponent().GetItem(id);
            if (item == null)
                return;
            try
            {
                var wallCoord = new WallCoordinate(":" + locationData.Split(':')[1]);
                var item2 = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData, wallCoord, room,
                    Session.GetHabbo().Id, item.GroupId, item.BaseItem.FlatId, false);
                if (room.GetRoomItemHandler().SetWallItem(Session, item2))
                    Session.GetHabbo().GetInventoryComponent().RemoveItem(id, true);
            }
            catch
            {
            }
        }

        internal void PlaceItem()
        {
            if (Session?.GetHabbo() == null)
                return;

            try
            {
                var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

                if (room == null || Yupi.GetDbConfig().DbData["placing_enabled"] != "1")
                    return;

                if (!room.CheckRights(Session, false, true))
                {
                    Session.SendMessage(StaticMessage.ErrorCantSetNotOwner);
                    return;
                }

                var placementData = Request.GetString();
                var dataBits = placementData.Split(' ');
                var itemId = uint.Parse(dataBits[0].Replace("-", string.Empty));
                var item = Session.GetHabbo().GetInventoryComponent().GetItem(itemId);

                if (item == null)
                    return;

                var type = dataBits[1].StartsWith(":") ? "wall" : "floor";
                int x, y, rot;
                double z;

                switch (type)
                {
                    case "wall":
                        {
                            switch (item.BaseItem.InteractionType)
                            {
                                case Interaction.Dimmer:
                                    {
                                        if (room.MoodlightData != null && room.GetRoomItemHandler().GetItem(room.MoodlightData.ItemId) != null)
                                            Session.SendNotif(Yupi.GetLanguage().GetVar("room_moodlight_one_allowed"));

                                        goto PlaceWall;
                                    }
                                default:
                                    {
                                        goto PlaceWall;
                                    }
                            }
                        }
                    case "floor":
                        {
                            x = int.Parse(dataBits[1]);
                            y = int.Parse(dataBits[2]);
                            rot = int.Parse(dataBits[3]);
                            z = room.GetGameMap().SqAbsoluteHeight(x, y);

                            if (z >= 100)
                                goto CannotSetItem;

                            switch (item.BaseItem.InteractionType)
                            {
                                case Interaction.BreedingTerrier:
                                case Interaction.BreedingBear:
                                    {
                                        var roomItemBreed = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData,
                                            x, y, z, rot, room, Session.GetHabbo().Id, 0, 0, string.Empty, false);

                                        if (item.BaseItem.InteractionType == Interaction.BreedingTerrier)
                                            if (!room.GetRoomItemHandler().BreedingTerrier.ContainsKey(roomItemBreed.Id))
                                                room.GetRoomItemHandler().BreedingTerrier.Add(roomItemBreed.Id, roomItemBreed);
                                            else if (!room.GetRoomItemHandler().BreedingBear.ContainsKey(roomItemBreed.Id))
                                                room.GetRoomItemHandler().BreedingBear.Add(roomItemBreed.Id, roomItemBreed);
                                        goto PlaceFloor;
                                    }
                                case Interaction.Alert:
                                case Interaction.VendingMachine:
                                case Interaction.ScoreBoard:
                                case Interaction.Bed:
                                case Interaction.PressurePadBed:
                                case Interaction.Trophy:
                                case Interaction.RoomEffect:
                                case Interaction.PostIt:
                                case Interaction.Gate:
                                case Interaction.None:
                                case Interaction.HcGate:
                                case Interaction.Teleport:
                                case Interaction.QuickTeleport:
                                case Interaction.Guillotine:
                                    {
                                        goto PlaceFloor;
                                    }
                                case Interaction.Hopper:
                                    {
                                        if (room.GetRoomItemHandler().HopperCount > 0)
                                            return;
                                        goto PlaceFloor;
                                    }
                                case Interaction.FreezeTile:
                                    {
                                        if (!room.GetGameMap().SquareHasFurni(x, y, Interaction.FreezeTile))
                                            goto PlaceFloor;
                                        goto CannotSetItem;
                                    }
                                case Interaction.FreezeTileBlock:
                                    {
                                        if (!room.GetGameMap().SquareHasFurni(x, y, Interaction.FreezeTileBlock))
                                            goto PlaceFloor;
                                        goto CannotSetItem;
                                    }
                                case Interaction.Toner:
                                    {
                                        var tonerData = room.TonerData;
                                        if (tonerData != null && room.GetRoomItemHandler().GetItem(tonerData.ItemId) != null)
                                        {
                                            Session.SendNotif(Yupi.GetLanguage().GetVar("room_toner_one_allowed"));
                                            return;
                                        }
                                        goto PlaceFloor;
                                    }
                                default:
                                    {
                                        goto PlaceFloor;
                                    }
                            }
                        }
                }

                PlaceWall:
                var coordinate = new WallCoordinate(":" + placementData.Split(':')[1]);
                var roomItemWall = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData,
                    coordinate, room, Session.GetHabbo().Id, item.GroupId, 0, false);
                if (room.GetRoomItemHandler().SetWallItem(Session, roomItemWall))
                    Session.GetHabbo().GetInventoryComponent().RemoveItem(itemId, true);
                return;
                PlaceFloor:
                if (room.CheckRights(Session))
                    Yupi.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.FurniPlace);

                var roomItem = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData, x, y, z, rot,
                    room, Session.GetHabbo().Id, item.GroupId, item.BaseItem.FlatId, item.SongCode, false);

                if (room.GetRoomItemHandler().SetFloorItem(Session, roomItem, x, y, rot, true, false, true))
                {
                    Session.GetHabbo().GetInventoryComponent().RemoveItem(itemId, true);
                    if (roomItem.IsWired)
                    {
                        var item5 = room.GetWiredHandler().GenerateNewItem(roomItem);
                        room.GetWiredHandler().AddWired(item5);
                        WiredHandler.SaveWired(item5);
                    }
                }

                //Azure.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.FurniPlace, 0u);
                Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_RoomDecoFurniCount", 1, true);
                return;

                CannotSetItem:
                Session.SendMessage(StaticMessage.ErrorCantSetItem);
            }
            catch (Exception e)
            {
                Session.SendMessage(StaticMessage.ErrorCantSetItem);
                Writer.LogException(e.ToString());
            }
        }

        internal void TakeItem()
        {
            Request.GetInteger();
            var room = Session.GetHabbo().CurrentRoom;
            if (room == null || room.GetRoomItemHandler() == null || Session.GetHabbo() == null)
                return;
            var item = room.GetRoomItemHandler().GetItem(Request.GetUInteger());
            if (item == null || item.GetBaseItem().InteractionType == Interaction.PostIt)
                return;
            if (item.UserId != Session.GetHabbo().Id && !room.CheckRights(Session, true)) return;

            switch (item.GetBaseItem().InteractionType)
            {
                case Interaction.BreedingTerrier:
                    if (room.GetRoomItemHandler().BreedingTerrier.ContainsKey(item.Id))
                        room.GetRoomItemHandler().BreedingTerrier.Remove(item.Id);
                    foreach (var pet in item.PetsList)
                    {
                        pet.WaitingForBreading = 0;
                        pet.BreadingTile = new Point();
                        var user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);
                        if (user == null)
                            continue;
                        user.Freezed = false;
                        room.GetGameMap().AddUserToMap(user, user.Coordinate);

                        var nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                        user.MoveTo(nextCoord.X, nextCoord.Y);
                    }
                    item.PetsList.Clear();
                    break;

                case Interaction.BreedingBear:
                    if (room.GetRoomItemHandler().BreedingBear.ContainsKey(item.Id))
                        room.GetRoomItemHandler().BreedingBear.Remove(item.Id);
                    foreach (var pet in item.PetsList)
                    {
                        pet.WaitingForBreading = 0;
                        pet.BreadingTile = new Point();
                        var user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);
                        if (user == null)
                            continue;
                        user.Freezed = false;
                        room.GetGameMap().AddUserToMap(user, user.Coordinate);
                        var nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                        user.MoveTo(nextCoord.X, nextCoord.Y);
                    }
                    item.PetsList.Clear();
                    break;
            }
            if (item.IsBuilder)
                using (var adapter = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    room.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);
                    Session.GetHabbo().BuildersItemsUsed--;
                    BuildersClubUpdateFurniCount();

                    adapter.RunFastQuery("DELETE FROM items_rooms WHERE id = " + item.Id);
                }
            else
            {
                room.GetRoomItemHandler().RemoveFurniture(Session, item.Id);
                Session.GetHabbo()
                    .GetInventoryComponent()
                    .AddNewItem(item.Id, item.BaseName, item.ExtraData, item.GroupId, true, true, 0, 0);
                Session.GetHabbo().GetInventoryComponent().UpdateItems(false);
            }
        }

        internal void MoveItem()
        {
            var id = Convert.ToUInt32(Math.Abs(Request.GetInteger()));
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;

            if (!room.CheckRights(Session, false, true))
                return;

            var item = room.GetRoomItemHandler().GetItem(id);
            if (item == null)
                return;

            var x = Request.GetInteger();
            var y = Request.GetInteger();
            var rot = Request.GetInteger();
            Request.GetInteger();

            var flag = item.GetBaseItem().InteractionType == Interaction.Teleport ||
                       item.GetBaseItem().InteractionType == Interaction.Hopper || item.GetBaseItem().InteractionType == Interaction.QuickTeleport;
            if (x != item.X || y != item.Y)
                Yupi.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.FurniMove);
            if (rot != item.Rot)
                Yupi.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.FurniRotate);
            var oldCoords = item.GetCoords;

            if (!room.GetRoomItemHandler().SetFloorItem(Session, item, x, y, rot, false, false, true, true, false))
            {
                var message3 = new ServerMessage(LibraryParser.OutgoingRequest("UpdateRoomItemMessageComposer"));
                item.Serialize(message3);
                room.SendMessage(message3);
                return;
            }

            if (item.GetBaseItem().InteractionType == Interaction.BreedingTerrier ||
                item.GetBaseItem().InteractionType == Interaction.BreedingBear)
            {
                foreach (var pet in item.PetsList)
                {
                    pet.WaitingForBreading = 0;
                    pet.BreadingTile = new Point();
                    var user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);
                    if (user == null) continue;

                    user.Freezed = false;
                    room.GetGameMap().AddUserToMap(user, user.Coordinate);
                    var nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                    user.MoveTo(nextCoord.X, nextCoord.Y);
                }
                item.PetsList.Clear();
            }

            if (item.Z >= 0.1)
                Yupi.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.FurniStack);

            var newcoords = item.GetCoords;
            room.GetRoomItemHandler().OnHeightMapUpdate(oldCoords, newcoords);

            if (!flag)
                return;
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                room.GetRoomItemHandler().SaveFurniture(queryReactor);
        }

        internal void MoveWallItem()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session))
                return;
            var id = Request.GetUInteger();
            var locationData = Request.GetString();
            var item = room.GetRoomItemHandler().GetItem(id);
            if (item == null)
                return;
            try
            {
                var wallCoord = new WallCoordinate(":" + locationData.Split(':')[1]);
                item.WallCoord = wallCoord;
            }
            catch
            {
                return;
            }
            room.GetRoomItemHandler().AddOrUpdateItem(id);
            var message = new ServerMessage(LibraryParser.OutgoingRequest("UpdateRoomWallItemMessageComposer"));
            item.Serialize(message);
            room.SendMessage(message);
        }

        internal void TriggerItem()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var num = Request.GetInteger();
            if (num < 0)
                return;
            var pId = Convert.ToUInt32(num);
            var item = room.GetRoomItemHandler().GetItem(pId);
            if (item == null)
                return;
            var hasRightsOne = room.CheckRights(Session, false, true);
            var hasRightsTwo = room.CheckRights(Session, true);

            switch (item.GetBaseItem().InteractionType)
            {
                case Interaction.RoomBg:
                    {
                        if (!hasRightsTwo)
                            return;
                        room.TonerData.Enabled = room.TonerData.Enabled == 0 ? 1 : 0;
                        var message = new ServerMessage(LibraryParser.OutgoingRequest("UpdateRoomItemMessageComposer"));
                        item.Serialize(message);
                        room.SendMessage(message);
                        item.UpdateState();
                        using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                            queryReactor.RunFastQuery(string.Format("UPDATE items_toners SET enabled = '{0}' LIMIT 1",
                                room.TonerData.Enabled));
                        return;
                    }
                case Interaction.LoveLock:
                    {
                        if (!hasRightsOne)
                            return;
                        TriggerLoveLock(item);
                        return;
                    }
                case Interaction.Moplaseed:
                case Interaction.RareMoplaSeed:
                    {
                        if (!hasRightsOne)
                            return;
                        PlantMonsterplant(item, room);
                        return;
                    }
                case Interaction.LoveShuffler:
                    {
                        if (!hasRightsOne)
                            return;
                        TriggerLoveLock(item);
                        return;
                    }
            }
            item.Interactor.OnTrigger(Session, item, Request.GetInteger(), hasRightsOne);
            item.OnTrigger(room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id));
            //Azure.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.ExploreFindItem, item.GetBaseItem().itemId);
            foreach (var current in room.GetRoomUserManager().UserList.Values.Where(current => current != null))
                room.GetRoomUserManager().UpdateUserStatus(current, true);
        }

        internal void TriggerItemDiceSpecial()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var item = room.GetRoomItemHandler().GetItem(Request.GetUInteger());
            if (item == null)
                return;
            var hasRights = room.CheckRights(Session);
            item.Interactor.OnTrigger(Session, item, -1, hasRights);
            item.OnTrigger(room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id));
        }

        internal void OpenPostit()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var item = room.GetRoomItemHandler().GetItem(Request.GetUInteger());
            if (item == null || item.GetBaseItem().InteractionType != Interaction.PostIt)
                return;
            Response.Init(LibraryParser.OutgoingRequest("LoadPostItMessageComposer"));
            Response.AppendString(item.Id.ToString());
            Response.AppendString(item.ExtraData);
            SendResponse();
        }

        internal void SavePostit()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var item = room.GetRoomItemHandler().GetItem(Request.GetUInteger());
            if (item == null || item.GetBaseItem().InteractionType != Interaction.PostIt)
                return;
            var text = Request.GetString();
            var text2 = Request.GetString();
            if (!room.CheckRights(Session) && !text2.StartsWith(item.ExtraData))
                return;
            string a;
            if ((a = text) == null || (a != "FFFF33" && a != "FF9CFF" && a != "9CCEFF" && a != "9CFF9C"))
                return;
            item.ExtraData = string.Format("{0} {1}", text, text2);
            item.UpdateState(true, true);
        }

        internal void DeletePostit()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            var item = room.GetRoomItemHandler().GetItem(Request.GetUInteger());
            if (item == null || item.GetBaseItem().InteractionType != Interaction.PostIt)
                return;
            room.GetRoomItemHandler().RemoveFurniture(Session, item.Id);
        }

        internal void OpenGift()
        {
            if ((DateTime.Now - Session.GetHabbo().LastGiftOpenTime).TotalSeconds <= 15.0)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("gift_one"));
                return;
            }
            var currentRoom = Session.GetHabbo().CurrentRoom;
            if (currentRoom == null)
            {
                Session.SendWhisper(Yupi.GetLanguage().GetVar("gift_two"));
                return;
            }
            if (!currentRoom.CheckRights(Session, true))
            {
                Session.SendWhisper(Yupi.GetLanguage().GetVar("gift_three"));
                return;
            }
            var pId = Request.GetUInteger();
            var item = currentRoom.GetRoomItemHandler().GetItem(pId);
            if (item == null)
            {
                Session.SendWhisper(Yupi.GetLanguage().GetVar("gift_four"));
                return;
            }
            item.MagicRemove = true;

            var message = new ServerMessage(LibraryParser.OutgoingRequest("UpdateRoomItemMessageComposer"));
            item.Serialize(message);
            currentRoom.SendMessage(message);

            Session.GetHabbo().LastGiftOpenTime = DateTime.Now;
            var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor();

            queryReactor.SetQuery("SELECT * FROM users_gifts WHERE gift_id = " + item.Id);
            var row = queryReactor.GetRow();

            if (row == null)
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);
                return;
            }
            var item2 = Yupi.GetGame().GetItemManager().GetItem(Convert.ToUInt32(row["item_id"]));

            if (item2 == null)
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);
                return;
            }

            if (item2.Type.Equals('s'))
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);

                var extraData = row["extradata"].ToString();
                var itemName = row["item_name"].ToString();

                queryReactor.RunFastQuery($"UPDATE items_rooms SET item_name='{itemName}' WHERE id='{item.Id}'");

                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = " + item.Id);
                queryReactor.AddParameter("extraData", extraData);
                queryReactor.RunQuery();

                queryReactor.RunFastQuery($"DELETE FROM users_gifts WHERE gift_id='{item.Id}'");

                item.BaseName = itemName;
                item.RefreshItem();
                item.ExtraData = extraData;

                if (!currentRoom.GetRoomItemHandler().SetFloorItem(item, item.X, item.Y, item.Z, item.Rot, true))
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("error_creating_gift"));
                }
                else
                {
                    Response.Init(LibraryParser.OutgoingRequest("OpenGiftMessageComposer"));

                    Response.AppendString(item2.Type.ToString());
                    Response.AppendInteger(item2.SpriteId);
                    Response.AppendString(item2.Name);
                    Response.AppendInteger(item2.ItemId);
                    Response.AppendString(item2.Type.ToString());
                    Response.AppendBool(true);
                    Response.AppendString(extraData);
                    SendResponse();

                    var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("AddFloorItemMessageComposer"));

                    item.Serialize(serverMessage);
                    serverMessage.AppendString(currentRoom.RoomData.Owner);
                    currentRoom.SendMessage(serverMessage);

                    currentRoom.GetRoomItemHandler().SetFloorItem(Session, item, item.X, item.Y, 0, true, false, true);
                }
            }
            else
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);

                queryReactor.RunFastQuery("DELETE FROM users_gifts WHERE gift_id = " + item.Id);
                Response.Init(LibraryParser.OutgoingRequest("NewInventoryObjectMessageComposer"));
                Response.AppendInteger(1);

                var i = 2;

                if (item2.Type == 's')
                    i = item2.InteractionType == Interaction.Pet ? 3 : 1;

                Response.AppendInteger(i);
                var list = Yupi.GetGame().GetCatalog().DeliverItems(Session, item2, 1, (string)row["extradata"], 0, 0, string.Empty);

                Response.AppendInteger(list.Count);

                foreach (var current in list)
                    Response.AppendInteger(current.Id);

                SendResponse();
                Session.GetHabbo().GetInventoryComponent().UpdateItems(true);
            }

            Response.Init(LibraryParser.OutgoingRequest("UpdateInventoryMessageComposer"));
            SendResponse();
        }

        internal void GetMoodlight()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            if (room.MoodlightData == null)
                foreach (
                    var current in
                        room.GetRoomItemHandler()
                            .WallItems.Values.Where(
                                current => current.GetBaseItem().InteractionType == Interaction.Dimmer))
                    room.MoodlightData = new MoodlightData(current.Id);

            if (room.MoodlightData == null)
                return;

            Response.Init(LibraryParser.OutgoingRequest("DimmerDataMessageComposer"));
            Response.AppendInteger(room.MoodlightData.Presets.Count);
            Response.AppendInteger(room.MoodlightData.CurrentPreset);

            var num = 0;

            {
                foreach (var current2 in room.MoodlightData.Presets)
                {
                    num++;
                    Response.AppendInteger(num);
                    Response.AppendInteger(
                        int.Parse(Yupi.BoolToEnum(current2.BackgroundOnly)) + 1);
                    Response.AppendString(current2.ColorCode);
                    Response.AppendInteger(current2.ColorIntensity);
                }
                SendResponse();
            }
        }

        internal void UpdateMoodlight()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true) || room.MoodlightData == null)
                return;
            var item = room.GetRoomItemHandler().GetItem(room.MoodlightData.ItemId);
            if (item == null || item.GetBaseItem().InteractionType != Interaction.Dimmer)
                return;
            var num = Request.GetInteger();
            var num2 = Request.GetInteger();
            var color = Request.GetString();
            var intensity = Request.GetInteger();
            var bgOnly = num2 >= 2;

            room.MoodlightData.Enabled = true;
            room.MoodlightData.CurrentPreset = num;
            room.MoodlightData.UpdatePreset(num, color, intensity, bgOnly);
            item.ExtraData = room.MoodlightData.GenerateExtraData();
            item.UpdateState();
        }

        internal void SwitchMoodlightStatus()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true) || room.MoodlightData == null)
                return;
            var item = room.GetRoomItemHandler().GetItem(room.MoodlightData.ItemId);
            if (item == null || item.GetBaseItem().InteractionType != Interaction.Dimmer)
                return;
            if (room.MoodlightData.Enabled)
                room.MoodlightData.Disable();
            else
                room.MoodlightData.Enable();
            item.ExtraData = room.MoodlightData.GenerateExtraData();
            item.UpdateState();
        }

        internal void SaveRoomBg()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            var item = room.GetRoomItemHandler().GetItem(room.TonerData.ItemId);
            if (item == null || item.GetBaseItem().InteractionType != Interaction.RoomBg)
                return;
            Request.GetInteger();
            var num = Request.GetInteger();
            var num2 = Request.GetInteger();
            var num3 = Request.GetInteger();
            if (num > 255 || num2 > 255 || num3 > 255)
                return;
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("UPDATE items_toners SET enabled = '1', data1=", num, " ,data2=", num2, ",data3=", num3, " WHERE id=", item.Id, " LIMIT 1"));
            room.TonerData.Data1 = num;
            room.TonerData.Data2 = num2;
            room.TonerData.Data3 = num3;
            room.TonerData.Enabled = 1;

            var message = new ServerMessage(LibraryParser.OutgoingRequest("UpdateRoomItemMessageComposer"));
            item.Serialize(message);
            room.SendMessage(message);

            item.UpdateState();
        }

        internal void InitTrade()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            if (room.RoomData.TradeState == 0)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled"));
                return;
            }
            if (room.RoomData.TradeState == 1 && !room.CheckRights(Session))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled_no_rights"));
                return;
            }
            if (Yupi.GetDbConfig().DbData["trading_enabled"] != "1")
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled_hotel"));
                return;
            }
            if (!Session.GetHabbo().CheckTrading())
                Session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled_mod"));
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            var roomUserByVirtualId = room.GetRoomUserManager().GetRoomUserByVirtualId(Request.GetInteger());
            if (roomUserByVirtualId == null || roomUserByVirtualId.GetClient() == null ||
                roomUserByVirtualId.GetClient().GetHabbo() == null)
                return;
            room.TryStartTrade(roomUserByHabbo, roomUserByVirtualId);
        }

        internal void TileStackMagicSetHeight()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null) return;
            var itemId = Request.GetUInteger();
            var item = room.GetRoomItemHandler().GetItem(itemId);
            if (item == null || item.GetBaseItem().InteractionType != Interaction.TileStackMagic) return;
            var heightToSet = Request.GetInteger();
            double totalZ;
            if (heightToSet < 0)
            {
                totalZ = room.GetGameMap().SqAbsoluteHeight(item.X, item.Y);

                var message = new ServerMessage(LibraryParser.OutgoingRequest("UpdateTileStackMagicHeight"));
                message.AppendInteger(item.Id);
                message.AppendInteger(Convert.ToUInt32(totalZ * 100));
                Session.SendMessage(message);
            }
            else
            {
                if (heightToSet > 10000) heightToSet = 10000;
                totalZ = (heightToSet / 100.0);

                if (totalZ < room.RoomData.Model.SqFloorHeight[item.X][item.Y])
                {
                    totalZ = room.RoomData.Model.SqFloorHeight[item.X][item.Y];

                    var message = new ServerMessage(LibraryParser.OutgoingRequest("UpdateTileStackMagicHeight"));
                    message.AppendInteger(item.Id);
                    message.AppendInteger(Convert.ToUInt32(totalZ * 100));
                    Session.SendMessage(message);
                }
            }
            room.GetRoomItemHandler().SetFloorItem(item, item.X, item.Y, totalZ, item.Rot, true);
        }

        internal void OfferTradeItem()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CanTradeInRoom)
                return;
            var userTrade = room.GetUserTrade(Session.GetHabbo().Id);
            var item = Session.GetHabbo().GetInventoryComponent().GetItem(Request.GetUInteger());
            if (userTrade == null || item == null)
                return;
            userTrade.OfferItem(Session.GetHabbo().Id, item);
        }

        internal void TakeBackTradeItem()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CanTradeInRoom)
                return;
            var userTrade = room.GetUserTrade(Session.GetHabbo().Id);
            var item = Session.GetHabbo().GetInventoryComponent().GetItem(Request.GetUInteger());
            if (userTrade == null || item == null)
                return;
            userTrade.TakeBackItem(Session.GetHabbo().Id, item);
        }

        internal void StopTrade()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CanTradeInRoom)
                return;
            room.TryStopTrade(Session.GetHabbo().Id);
        }

        internal void AcceptTrade()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CanTradeInRoom)
                return;
            var userTrade = room.GetUserTrade(Session.GetHabbo().Id);
            if (userTrade == null)
                return;
            userTrade.Accept(Session.GetHabbo().Id);
        }

        internal void UnacceptTrade()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CanTradeInRoom)
                return;
            var userTrade = room.GetUserTrade(Session.GetHabbo().Id);
            if (userTrade == null)
                return;
            userTrade.Unaccept(Session.GetHabbo().Id);
        }

        internal void CompleteTrade()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CanTradeInRoom)
                return;
            var userTrade = room.GetUserTrade(Session.GetHabbo().Id);
            if (userTrade == null)
                return;
            userTrade.CompleteTrade(Session.GetHabbo().Id);
        }

        internal void RecycleItems()
        {
            if (!Session.GetHabbo().InRoom)
                return;
            var num = Request.GetInteger();
            if (num != Convert.ToUInt32(Yupi.GetDbConfig().DbData["recycler.number_of_slots"]))
                return;
            var i = 0;

            {
                while (i < num)
                {
                    var item = Session.GetHabbo().GetInventoryComponent().GetItem(Request.GetUInteger());
                    if (item == null || !item.BaseItem.AllowRecycle)
                        return;
                    Session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);
                    using (
                        var queryReactor =
                            Yupi.GetDatabaseManager().GetQueryReactor())
                        queryReactor.RunFastQuery(string.Format("DELETE FROM items_rooms WHERE id={0} LIMIT 1",
                            item.Id));
                    i++;
                }
                var randomEcotronReward =
                    Yupi.GetGame().GetCatalog().GetRandomEcotronReward();
                uint insertId;
                using (var queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryreactor2.SetQuery("INSERT INTO items_rooms (user_id,item_name,extra_data) VALUES ( @userid , @baseName, @timestamp)");

                    queryreactor2.AddParameter("userid", (int)Session.GetHabbo().Id);
                    queryreactor2.AddParameter("timestamp", DateTime.Now.ToLongDateString());
                    queryreactor2.AddParameter("baseName", Yupi.GetDbConfig().DbData["recycler.box_name"]);

                    insertId = (uint)queryreactor2.InsertQuery();

                    queryreactor2.RunFastQuery("INSERT INTO users_gifts (gift_id,item_id,gift_sprite,extradata) VALUES (" + insertId + "," + randomEcotronReward.BaseId + ", " + randomEcotronReward.DisplayId + ",'')");
                }
                Session.GetHabbo().GetInventoryComponent().UpdateItems(true);
                Response.Init(LibraryParser.OutgoingRequest("RecyclingStateMessageComposer"));
                Response.AppendInteger(1);
                Response.AppendInteger(insertId);
                SendResponse();
            }
        }

        internal void RedeemExchangeFurni()
        {
            if (Session == null || Session.GetHabbo() == null) return;

            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true)) return;
            if (Yupi.GetDbConfig().DbData["exchange_enabled"] != "1")
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("bliep_wisselkoers_uitgeschakeld"));
                return;
            }
            var item = room.GetRoomItemHandler().GetItem(Request.GetUInteger());
            if (item == null) return;
            if (!item.GetBaseItem().Name.StartsWith("CF_") && !item.GetBaseItem().Name.StartsWith("CFC_")) return;
            var array = item.GetBaseItem().Name.Split('_');

            int amount;
            if (array[1] == "diamond")
            {
                int.TryParse(array[2], out amount);

                Session.GetHabbo().Diamonds += amount;
                Session.GetHabbo().UpdateSeasonalCurrencyBalance();
            }
            else
            {
                int.TryParse(array[1], out amount);

                Session.GetHabbo().Credits += amount;
                Session.GetHabbo().UpdateCreditsBalance();
            }

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) queryReactor.RunFastQuery(string.Format("DELETE FROM items_rooms WHERE id={0} LIMIT 1;", item.Id));
            room.GetRoomItemHandler().RemoveFurniture(null, item.Id, false);
            Session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);
            Response.Init(LibraryParser.OutgoingRequest("UpdateInventoryMessageComposer"));
            SendResponse();
        }

        internal void TriggerLoveLock(RoomItem loveLock)
        {
            var loveLockParams = loveLock.ExtraData.Split(Convert.ToChar(5));

            try
            {
                if (loveLockParams[0] == "1")
                    return;

                Point pointOne;
                Point pointTwo;

                switch (loveLock.Rot)
                {
                    case 2:
                        pointOne = new Point(loveLock.X, loveLock.Y + 1);
                        pointTwo = new Point(loveLock.X, loveLock.Y - 1);
                        break;

                    case 4:
                        pointOne = new Point(loveLock.X - 1, loveLock.Y);
                        pointTwo = new Point(loveLock.X + 1, loveLock.Y);
                        break;

                    default:
                        return;
                }

                var roomUserOne = loveLock.GetRoom().GetRoomUserManager().GetUserForSquare(pointOne.X, pointOne.Y);
                var roomUserTwo = loveLock.GetRoom().GetRoomUserManager().GetUserForSquare(pointTwo.X, pointTwo.Y);

                RoomUser user = loveLock.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

                if (roomUserOne == null || roomUserTwo == null)
                {
                    user.MoveTo(loveLock.X, loveLock.Y + 1);
                    return;
                }

                if (roomUserOne.GetClient() == null || roomUserTwo.GetClient() == null)
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("lovelock_error_2"));
                    return;
                }

                roomUserOne.CanWalk = false;
                roomUserTwo.CanWalk = false;

                var lockDialogue = new ServerMessage(LibraryParser.OutgoingRequest("LoveLockDialogueMessageComposer"));
                lockDialogue.AppendInteger(loveLock.Id);
                lockDialogue.AppendBool(true);

                loveLock.InteractingUser = roomUserOne.GetClient().GetHabbo().Id;
                loveLock.InteractingUser2 = roomUserTwo.GetClient().GetHabbo().Id;

                roomUserOne.GetClient().SendMessage(lockDialogue);
                roomUserTwo.GetClient().SendMessage(lockDialogue);
            }
            catch
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("lovelock_error_3"));
            }
        }

        internal void GetPetInfo()
        {
            if (Session.GetHabbo() == null || Session.GetHabbo().CurrentRoom == null)
                return;
            var petId = Request.GetUInteger();
            var pet = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetPet(petId);
            if (pet == null || pet.PetData == null)
                return;
            Session.SendMessage(pet.PetData.SerializeInfo());
        }

        internal void CompostMonsterplant()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_1"));
                return;
            }
            var moplaId = Request.GetUInteger();
            var pet = room.GetRoomUserManager().GetPet(moplaId);
            if (pet == null || !pet.IsPet || pet.PetData.Type != 16 || pet.PetData.MoplaBreed == null)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_2"));
                return;
            }
            if (pet.PetData.MoplaBreed.LiveState != MoplaState.Dead)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_3"));
                return;
            }
            var compostItem = Yupi.GetGame().GetItemManager().GetItemByName("mnstr_compost");
            if (compostItem == null)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_4"));
                return;
            }
            var x = pet.X;
            var y = pet.Y;
            var z = pet.Z;
            room.GetRoomUserManager().RemoveBot(pet.VirtualId, false);
            using (var dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("INSERT INTO items_rooms (user_id, room_id, item_name, extra_data, x, y, z) VALUES (@uid, @rid, @bit, '0', @ex, @wai, @zed)");
                dbClient.AddParameter("uid", Session.GetHabbo().Id);
                dbClient.AddParameter("rid", room.RoomId);
                dbClient.AddParameter("bit", compostItem.Name);
                dbClient.AddParameter("ex", x);
                dbClient.AddParameter("wai", y);
                dbClient.AddParameter("zed", z);

                var itemId = (uint)dbClient.InsertQuery();

                var roomItem = new RoomItem(itemId, room.RoomId, compostItem.Name, "0", x, y, z, 0, room,
                    Session.GetHabbo().Id, 0, -1, "", false);

                if (!room.GetRoomItemHandler().SetFloorItem(Session, roomItem, x, y, 0, true, false, true))
                {
                    Session.GetHabbo().GetInventoryComponent().AddItem(roomItem);
                    Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_5"));
                }

                dbClient.RunFastQuery($"DELETE FROM bots_data WHERE id = {moplaId};");
                dbClient.RunFastQuery($"DELETE FROM pets_plants WHERE pet_id = {moplaId};");
                dbClient.RunFastQuery($"DELETE FROM pets_data WHERE id = {moplaId};");
            }
        }

        internal void MovePet()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if ((room == null) || (!room.CheckRights(Session)))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_6"));
                return;
            }

            var petId = Request.GetUInteger();
            var pet = room.GetRoomUserManager().GetPet(petId);

            if (pet == null || !pet.IsPet || pet.PetData.Type != 16)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_7"));
                return;
            }

            var x = Request.GetInteger();
            var y = Request.GetInteger();
            var rot = Request.GetInteger();
            var oldX = pet.X;
            var oldY = pet.Y;

            if ((x != oldX) && (y != oldY))
            {
                if (!room.GetGameMap().CanWalk(x, y, false))
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_8"));
                    return;
                }
            }

            if ((rot < 0) || (rot > 6) || (rot % 2 != 0))
            {
                rot = pet.RotBody;
            }

            pet.PetData.X = x;
            pet.PetData.Y = y;
            pet.X = x;
            pet.Y = y;
            pet.RotBody = rot;
            pet.RotHead = rot;

            if (pet.PetData.DbState != DatabaseUpdateState.NeedsInsert)
            {
                pet.PetData.DbState = DatabaseUpdateState.NeedsUpdate;
            }

            pet.UpdateNeeded = true;
            room.GetGameMap().UpdateUserMovement(new Point(oldX, oldY), new Point(x, y), pet);
        }

        internal void PickUpPet()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (Session == null || Session.GetHabbo() == null ||
                Session.GetHabbo().GetInventoryComponent() == null)
                return;
            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(Session, true)))
                return;
            var petId = Request.GetUInteger();
            var pet = room.GetRoomUserManager().GetPet(petId);
            if (pet == null)
                return;
            if (pet.RidingHorse)
            {
                var roomUserByVirtualId =
                    room.GetRoomUserManager().GetRoomUserByVirtualId(Convert.ToInt32(pet.HorseId));
                if (roomUserByVirtualId != null)
                {
                    roomUserByVirtualId.RidingHorse = false;
                    roomUserByVirtualId.ApplyEffect(-1);
                    roomUserByVirtualId.MoveTo((new Point(roomUserByVirtualId.X + 1, roomUserByVirtualId.Y + 1)));
                }
            }
            if (pet.PetData.DbState != DatabaseUpdateState.NeedsInsert)
                pet.PetData.DbState = DatabaseUpdateState.NeedsUpdate;
            pet.PetData.RoomId = 0u;
            Session.GetHabbo().GetInventoryComponent().AddPet(pet.PetData);
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                room.GetRoomUserManager().SavePets(queryReactor);
            room.GetRoomUserManager().RemoveBot(pet.VirtualId, false);
            Session.SendMessage(Session.GetHabbo().GetInventoryComponent().SerializePetInventory());
        }

        internal void RespectPet()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var petId = Request.GetUInteger();
            var pet = room.GetRoomUserManager().GetPet(petId);
            if (pet == null || pet.PetData == null)
                return;
            pet.PetData.OnRespect();

            {
                if (pet.PetData.Type == 16)
                    Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_MonsterPlantTreater", 1);
                else
                {
                    Session.GetHabbo().DailyPetRespectPoints--;
                    Yupi.GetGame()
                        .GetAchievementManager()
                        .ProgressUserAchievement(Session, "ACH_PetRespectGiver", 1);
                    var value = PetLocale.GetValue("pet.respected");
                    var message = value[new Random().Next(0, (value.Length - 1))];

                    pet.Chat(null, message, false, 0);
                    using (
                        var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        queryReactor.RunFastQuery(
                            string.Format(
                                "UPDATE users_stats SET daily_pet_respect_points = daily_pet_respect_points - 1 WHERE id = {0} LIMIT 1",
                                Session.GetHabbo().Id));
                }
            }
        }

        internal void AllowAllRide()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            var num = Request.GetUInteger();
            var pet = room.GetRoomUserManager().GetPet(num);
            if (pet.PetData.AnyoneCanRide == 1)
            {
                using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery(string.Format("UPDATE pets_data SET anyone_ride=0 WHERE id={0} LIMIT 1",
                        num));
                pet.PetData.AnyoneCanRide = 0;
            }
            else
            {
                using (var queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryreactor2.RunFastQuery(string.Format("UPDATE pets_data SET anyone_ride=1 WHERE id={0} LIMIT 1",
                        num));
                pet.PetData.AnyoneCanRide = 1;
            }
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("PetInfoMessageComposer"));
            serverMessage.AppendInteger(pet.PetData.PetId);
            serverMessage.AppendString(pet.PetData.Name);
            serverMessage.AppendInteger(pet.PetData.Level);
            serverMessage.AppendInteger(20);
            serverMessage.AppendInteger(pet.PetData.Experience);
            serverMessage.AppendInteger(pet.PetData.ExperienceGoal);
            serverMessage.AppendInteger(pet.PetData.Energy);
            serverMessage.AppendInteger(100);
            serverMessage.AppendInteger(pet.PetData.Nutrition);
            serverMessage.AppendInteger(150);
            serverMessage.AppendInteger(pet.PetData.Respect);
            serverMessage.AppendInteger(pet.PetData.OwnerId);
            serverMessage.AppendInteger(pet.PetData.Age);
            serverMessage.AppendString(pet.PetData.OwnerName);
            serverMessage.AppendInteger(1);
            serverMessage.AppendBool(pet.PetData.HaveSaddle);
            serverMessage.AppendBool(
                Yupi.GetGame()
                    .GetRoomManager()
                    .GetRoom(pet.PetData.RoomId)
                    .GetRoomUserManager()
                    .GetRoomUserByVirtualId(pet.PetData.VirtualId)
                    .RidingHorse);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(pet.PetData.AnyoneCanRide);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(0);
            serverMessage.AppendString("");
            serverMessage.AppendBool(false);
            serverMessage.AppendInteger(-1);
            serverMessage.AppendInteger(-1);
            serverMessage.AppendInteger(-1);
            serverMessage.AppendBool(false);
            room.SendMessage(serverMessage);
        }

        internal void AddSaddle()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(Session, true)))
                return;
            var pId = Request.GetUInteger();
            var item = room.GetRoomItemHandler().GetItem(pId);
            if (item == null)
                return;
            var petId = Request.GetUInteger();
            var pet = room.GetRoomUserManager().GetPet(petId);
            if (pet == null || pet.PetData == null || pet.PetData.OwnerId != Session.GetHabbo().Id)
                return;
            bool isForHorse = true;
            {
                if (item.GetBaseItem().Name.Contains("horse_hairdye"))
                {
                    var s = item.GetBaseItem().Name.Split('_')[2];
                    var num = 48;
                    num += int.Parse(s);
                    pet.PetData.HairDye = num;
                    using (
                        var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.RunFastQuery(string.Concat("UPDATE pets_data SET hairdye = '", pet.PetData.HairDye, "' WHERE id = ", pet.PetData.PetId));
                        goto IL_40C;
                    }
                }
                if (item.GetBaseItem().Name.Contains("horse_dye"))
                {
                    var s2 = item.GetBaseItem().Name.Split('_')[2];
                    var num2 = int.Parse(s2);
                    var num3 = 2 + num2 * 4 - 4;
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
                    pet.PetData.Race = num3.ToString();
                    using (
                        var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.RunFastQuery("UPDATE pets_data SET race = '" + pet.PetData.Race + "' WHERE id = " + pet.PetData.PetId);
                        queryReactor.RunFastQuery(string.Format("DELETE FROM items_rooms WHERE id={0} LIMIT 1", item.Id));
                        goto IL_40C;
                    }
                }
                if (item.GetBaseItem().Name.Contains("horse_hairstyle"))
                {
                    var s3 = item.GetBaseItem().Name.Split('_')[2];
                    var num4 = 100;
                    num4 += int.Parse(s3);
                    pet.PetData.PetHair = num4;
                    using (
                        var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.RunFastQuery("UPDATE pets_data SET pethair = '" + pet.PetData.PetHair + "' WHERE id = " + pet.PetData.PetId);
                        queryReactor.RunFastQuery(string.Format("DELETE FROM items_rooms WHERE id={0} LIMIT 1", item.Id));
                        goto IL_40C;
                    }
                }
                if (item.GetBaseItem().Name.Contains("saddle"))
                {
                    pet.PetData.HaveSaddle = true;
                    using (
                        var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.RunFastQuery(string.Format("UPDATE pets_data SET have_saddle = 1 WHERE id = {0}",
                            pet.PetData.PetId));
                        queryReactor.RunFastQuery(string.Format("DELETE FROM items_rooms WHERE id={0} LIMIT 1", item.Id));
                    }
                    goto IL_40C;
                }
                if (item.GetBaseItem().Name == "mnstr_fert")
                {
                    if (pet.PetData.MoplaBreed.LiveState == MoplaState.Grown) return;
                    isForHorse = false;
                    pet.PetData.MoplaBreed.GrowingStatus = 7;
                    pet.PetData.MoplaBreed.LiveState = MoplaState.Grown;
                    pet.PetData.MoplaBreed.UpdateInDb();
                    using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.RunFastQuery(string.Format("DELETE FROM items_rooms WHERE id={0} LIMIT 1", item.Id));
                    }
                }
                IL_40C:
                room.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);
                var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("SetRoomUserMessageComposer"));
                serverMessage.AppendInteger(1);
                pet.Serialize(serverMessage, false);
                room.SendMessage(serverMessage);
                if (isForHorse)
                {
                    var serverMessage2 = new ServerMessage(LibraryParser.OutgoingRequest("SerializePetMessageComposer"));
                    serverMessage2.AppendInteger(pet.PetData.VirtualId);
                    serverMessage2.AppendInteger(pet.PetData.PetId);
                    serverMessage2.AppendInteger(pet.PetData.Type);
                    serverMessage2.AppendInteger(int.Parse(pet.PetData.Race));
                    serverMessage2.AppendString(pet.PetData.Color.ToLower());
                    if (pet.PetData.HaveSaddle)
                    {
                        serverMessage2.AppendInteger(2);
                        serverMessage2.AppendInteger(3);
                        serverMessage2.AppendInteger(4);
                        serverMessage2.AppendInteger(9);
                        serverMessage2.AppendInteger(0);
                        serverMessage2.AppendInteger(3);
                        serverMessage2.AppendInteger(pet.PetData.PetHair);
                        serverMessage2.AppendInteger(pet.PetData.HairDye);
                        serverMessage2.AppendInteger(3);
                        serverMessage2.AppendInteger(pet.PetData.PetHair);
                        serverMessage2.AppendInteger(pet.PetData.HairDye);
                    }
                    else
                    {
                        serverMessage2.AppendInteger(1);
                        serverMessage2.AppendInteger(2);
                        serverMessage2.AppendInteger(2);
                        serverMessage2.AppendInteger(pet.PetData.PetHair);
                        serverMessage2.AppendInteger(pet.PetData.HairDye);
                        serverMessage2.AppendInteger(3);
                        serverMessage2.AppendInteger(pet.PetData.PetHair);
                        serverMessage2.AppendInteger(pet.PetData.HairDye);
                    }
                    serverMessage2.AppendBool(pet.PetData.HaveSaddle);
                    serverMessage2.AppendBool(pet.RidingHorse);
                    room.SendMessage(serverMessage2);
                }
            }
        }

        internal void RemoveSaddle()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(Session, true)))
                return;

            var petId = Request.GetUInteger();

            var pet = room.GetRoomUserManager().GetPet(petId);

            if (pet?.PetData == null || pet.PetData.OwnerId != Session.GetHabbo().Id)
                return;

            pet.PetData.HaveSaddle = false;

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.RunFastQuery($"UPDATE pets_data SET have_saddle = 0 WHERE id = {pet.PetData.PetId}");

                queryReactor.RunFastQuery($"INSERT INTO items_rooms (user_id, item_name) VALUES ({Session.GetHabbo().Id}, 'horse_saddle1');");
            }

            Session.GetHabbo().GetInventoryComponent().UpdateItems(true);

            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("SetRoomUserMessageComposer"));
            serverMessage.AppendInteger(1);
            pet.Serialize(serverMessage, false);
            room.SendMessage(serverMessage);

            var serverMessage2 = new ServerMessage(LibraryParser.OutgoingRequest("SerializePetMessageComposer"));
            serverMessage2.AppendInteger(pet.PetData.VirtualId);
            serverMessage2.AppendInteger(pet.PetData.PetId);
            serverMessage2.AppendInteger(pet.PetData.Type);
            serverMessage2.AppendInteger(int.Parse(pet.PetData.Race));
            serverMessage2.AppendString(pet.PetData.Color.ToLower());
            serverMessage2.AppendInteger(1);
            serverMessage2.AppendInteger(2);
            serverMessage2.AppendInteger(2);
            serverMessage2.AppendInteger(pet.PetData.PetHair);
            serverMessage2.AppendInteger(pet.PetData.HairDye);
            serverMessage2.AppendInteger(3);
            serverMessage2.AppendInteger(pet.PetData.PetHair);
            serverMessage2.AppendInteger(pet.PetData.HairDye);
            serverMessage2.AppendBool(pet.PetData.HaveSaddle);
            serverMessage2.AppendBool(pet.RidingHorse);
            room.SendMessage(serverMessage2);
        }

        internal void CancelMountOnPet()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            var petId = Request.GetUInteger();
            var pet = room.GetRoomUserManager().GetPet(petId);
            if (pet == null || pet.PetData == null)
                return;
            roomUserByHabbo.RidingHorse = false;
            roomUserByHabbo.HorseId = 0u;
            pet.RidingHorse = false;
            pet.HorseId = 0u;

            {
                roomUserByHabbo.MoveTo(roomUserByHabbo.X + 1, roomUserByHabbo.Y + 1);
                roomUserByHabbo.ApplyEffect(-1);
            }
        }

        internal void GiveHanditem()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            var roomUserByHabbo2 = room.GetRoomUserManager().GetRoomUserByHabbo(Request.GetUInteger());
            if (roomUserByHabbo2 == null)
                return;
            if ((!(

                Math.Abs(roomUserByHabbo.X - roomUserByHabbo2.X) < 3 &&
                Math.Abs(roomUserByHabbo.Y - roomUserByHabbo2.Y) < 3) &&
                 roomUserByHabbo.GetClient().GetHabbo().Rank <= 4u) || roomUserByHabbo.CarryItemId <= 0 ||
                roomUserByHabbo.CarryTimer <= 0)
                return;
            if (roomUserByHabbo.CarryItemId == 8)
                Yupi.GetGame()
                    .GetQuestManager()
                    .ProgressUserQuest(Session, QuestType.GiveCoffee);
            roomUserByHabbo2.CarryItem(roomUserByHabbo.CarryItemId);
            roomUserByHabbo.CarryItem(0);
            roomUserByHabbo2.DanceId = 0;
        }

        internal void RedeemVoucher()
        {
            var query = Request.GetString();
            var productName = string.Empty;
            var productDescription = string.Empty;
            var isValid = false;
            DataRow row;
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM items_vouchers WHERE voucher = @vo LIMIT 1");
                queryReactor.AddParameter("vo", query);
                row = queryReactor.GetRow();
            }

            {
                if (row != null)
                {
                    isValid = true;
                    using (
                        var queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryreactor2.SetQuery("DELETE FROM items_vouchers WHERE voucher = @vou LIMIT 1");
                        queryreactor2.AddParameter("vou", query);
                        queryreactor2.RunQuery();
                    }
                    Session.GetHabbo().Credits += (int)row["value"];
                    Session.GetHabbo().UpdateCreditsBalance();
                    Session.GetHabbo().NotifyNewPixels((int)row["extra_duckets"]);
                }
                Session.GetHabbo().NotifyVoucher(isValid, productName, productDescription);
            }
        }

        internal void RemoveHanditem()
        {
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            if (roomUserByHabbo.CarryItemId > 0 && roomUserByHabbo.CarryTimer > 0)
                roomUserByHabbo.CarryItem(0);
        }

        internal void MountOnPet()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;
            var roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            var petId = Request.GetUInteger();
            var flag = Request.GetBool();
            var pet = room.GetRoomUserManager().GetPet(petId);

            if (pet == null || pet.PetData == null)
                return;

            if (pet.PetData.AnyoneCanRide == 0 && pet.PetData.OwnerId != roomUserByHabbo.UserId)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("horse_error_1"));
                return;
            }

            if (flag)
            {
                if (pet.RidingHorse)
                {
                    var value = PetLocale.GetValue("pet.alreadymounted");
                    var random = new Random();
                    pet.Chat(null, value[random.Next(0, (value.Length - 1))], false, 0);
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

                    room.SendMessage(room.GetRoomItemHandler().UpdateUserOnRoller(pet, new Point(x, y), 0u, room.GetGameMap().SqAbsoluteHeight(x, y)));
                    room.GetRoomUserManager().UpdateUserStatus(pet, false);
                    room.SendMessage(room.GetRoomItemHandler().UpdateUserOnRoller(roomUserByHabbo, new Point(x, y), 0u, room.GetGameMap().SqAbsoluteHeight(x, y) + 1.0));
                    room.GetRoomUserManager().UpdateUserStatus(roomUserByHabbo, false);
                    pet.ClearMovement();
                    roomUserByHabbo.RidingHorse = true;
                    pet.RidingHorse = true;
                    pet.HorseId = ((uint)roomUserByHabbo.VirtualId);
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
                roomUserByHabbo.MoveTo((new Point(roomUserByHabbo.X + 2, roomUserByHabbo.Y + 2)));

                roomUserByHabbo.ApplyEffect(-1);
                roomUserByHabbo.UpdateNeeded = true;
                pet.UpdateNeeded = true;
            }
            else
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("horse_error_2"));
                return;
            }

            var clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(Session.GetHabbo().Id);
            if (Session.GetHabbo().Id != pet.PetData.OwnerId)
            {
                if (clientByUserId != null)
                {
                    Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(clientByUserId, "ACH_HorseRent", 1);
                }
            }

            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("SerializePetMessageComposer"));
            serverMessage.AppendInteger(pet.PetData.VirtualId);
            serverMessage.AppendInteger(pet.PetData.PetId);
            serverMessage.AppendInteger(pet.PetData.Type);
            serverMessage.AppendInteger(int.Parse(pet.PetData.Race));
            serverMessage.AppendString(pet.PetData.Color.ToLower());
            serverMessage.AppendInteger(2);
            serverMessage.AppendInteger(3);
            serverMessage.AppendInteger(4);
            serverMessage.AppendInteger(9);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(3);
            serverMessage.AppendInteger(pet.PetData.PetHair);
            serverMessage.AppendInteger(pet.PetData.HairDye);
            serverMessage.AppendInteger(3);
            serverMessage.AppendInteger(pet.PetData.PetHair);
            serverMessage.AppendInteger(pet.PetData.HairDye);
            serverMessage.AppendBool(pet.PetData.HaveSaddle);
            serverMessage.AppendBool(pet.RidingHorse);
            room.SendMessage(serverMessage);
        }

        internal void SaveWired()
        {
            var pId = Request.GetUInteger();
            var item =
                Yupi.GetGame()
                    .GetRoomManager()
                    .GetRoom(Session.GetHabbo().CurrentRoomId)
                    .GetRoomItemHandler()
                    .GetItem(pId);
            WiredSaver.SaveWired(Session, item, Request);
        }

        internal void SaveWiredConditions()
        {
            var pId = Request.GetUInteger();
            var item =
                Yupi.GetGame()
                    .GetRoomManager()
                    .GetRoom(Session.GetHabbo().CurrentRoomId)
                    .GetRoomItemHandler()
                    .GetItem(pId);
            WiredSaver.SaveWired(Session, item, Request);
        }

        internal void ChooseTvPlaylist()
        {
            var num = Request.GetUInteger();
            var video = Request.GetString();

            var item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(num);

            if (item.GetBaseItem().InteractionType != Interaction.YoutubeTv)
                return;
            if (!Session.GetHabbo().GetYoutubeManager().Videos.ContainsKey(video))
                return;
            item.ExtraData = video;
            item.UpdateState();
            var serverMessage = new ServerMessage();
            serverMessage.Init(LibraryParser.OutgoingRequest("YouTubeLoadVideoMessageComposer"));
            serverMessage.AppendInteger(num);
            serverMessage.AppendString(video);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(0);
            Response = serverMessage;
            SendResponse();
        }

        internal void ChooseTvPlayerVideo()
        {
        }

        internal void GetTvPlayer()
        {
            var itemId = Request.GetUInteger();
            var item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(itemId);
            if (item == null) return;
            var videos = Session.GetHabbo().GetYoutubeManager().Videos;
            if (videos == null) return;
            var serverMessage = new ServerMessage();
            serverMessage.Init(LibraryParser.OutgoingRequest("YouTubeLoadVideoMessageComposer"));
            serverMessage.AppendInteger(itemId);
            serverMessage.AppendString(item.ExtraData);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(0);// duration
            serverMessage.AppendInteger(0);
            Response = serverMessage;
            SendResponse();
            var serverMessage2 = new ServerMessage();
            serverMessage2.Init(LibraryParser.OutgoingRequest("YouTubeLoadPlaylistsMessageComposer"));
            serverMessage2.AppendInteger(itemId);
            serverMessage2.AppendInteger(videos.Count);
            foreach (var video in videos.Values)
                video.Serialize(serverMessage2);
            serverMessage2.AppendString(item.ExtraData);
            Response = serverMessage2;
            SendResponse();
        }

        internal void PlaceBot()
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || !room.CheckRights(Session, true))
                return;
            var num = Request.GetUInteger();
            var bot = Session.GetHabbo().GetInventoryComponent().GetBot(num);
            if (bot == null)
                return;

            var x = Request.GetInteger(); // coords
            var y = Request.GetInteger();

            if (!room.GetGameMap().CanWalk(x, y, false) || !room.GetGameMap().ValidTile(x, y))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("bot_error_1"));
                return;
            }
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("UPDATE bots_data SET room_id = '", room.RoomId, "', x = '", x, "', y = '", y, "' WHERE id = '", num, "'"));
            bot.RoomId = room.RoomId;

            bot.X = x;
            bot.Y = y;

            room.GetRoomUserManager().DeployBot(bot, null);
            bot.WasPicked = false;
            Session.GetHabbo().GetInventoryComponent().MoveBotToRoom(num);
            Session.SendMessage(Session.GetHabbo().GetInventoryComponent().SerializeBotInventory());
        }

        internal void PickUpBot()
        {
            var id = Request.GetUInteger();
            var room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            var bot = room.GetRoomUserManager().GetBot(id);

            if (Session == null || Session.GetHabbo() == null ||
                Session.GetHabbo().GetInventoryComponent() == null || bot == null ||
                !room.CheckRights(Session, true))
                return;
            Session.GetHabbo().GetInventoryComponent().AddBot(bot.BotData);
            using (var queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                queryreactor2.RunFastQuery("UPDATE bots_data SET room_id = '0' WHERE id = " + id);
            room.GetRoomUserManager().RemoveBot(bot.VirtualId, false);
            bot.BotData.WasPicked = true;
            Session.SendMessage(Session.GetHabbo().GetInventoryComponent().SerializeBotInventory());
        }

        internal void CancelMysteryBox()
        {
            Request.GetUInteger();
            var roomUserByHabbo =
                Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            var item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(roomUserByHabbo.GateId);
            if (item == null)
                return;
            if (item.InteractingUser == Session.GetHabbo().Id)
                item.InteractingUser = 0u;
            else if (item.InteractingUser2 == Session.GetHabbo().Id)
                item.InteractingUser2 = 0u;
            roomUserByHabbo.GateId = 0u;
            var text = item.ExtraData.Split(Convert.ToChar(5))[0];
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = " + item.Id);
                queryReactor.AddParameter("extraData", text + Convert.ToChar(5) + "2");
                queryReactor.RunQuery();
            }
            item.ExtraData = text + Convert.ToChar(5) + "2";
            item.UpdateNeeded = true;
            item.UpdateState(true, true);
        }

        internal void PlaceBuildersFurniture()
        {
            Request.GetInteger();
            var itemId = Convert.ToUInt32(Request.GetInteger());
            var extradata = Request.GetString();
            var x = Request.GetInteger();
            var y = Request.GetInteger();
            var dir = Request.GetInteger();
            var actualRoom = Session.GetHabbo().CurrentRoom;
            var item = Yupi.GetGame().GetCatalog().GetItem(itemId);

            if (actualRoom == null || item == null)
                return;

            Session.GetHabbo().BuildersItemsUsed++;
            BuildersClubUpdateFurniCount();

            var z = actualRoom.GetGameMap().SqAbsoluteHeight(x, y);
            using (var adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery("INSERT INTO items_rooms (user_id,room_id,item_name,x,y,z,rot,builders) VALUES (@userId,@roomId,@itemName,@x,@y,@z,@rot,'1')");
                adapter.AddParameter("userId", Session.GetHabbo().Id);
                adapter.AddParameter("roomId", actualRoom.RoomId);
                adapter.AddParameter("itemName", item.BaseName);
                adapter.AddParameter("x", x);
                adapter.AddParameter("y", y);
                adapter.AddParameter("z", z);
                adapter.AddParameter("rot", dir);

                var insertId = (uint)adapter.InsertQuery();

                var newItem = new RoomItem(insertId, actualRoom.RoomId, item.BaseName, extradata, x, y, z, dir, actualRoom, Session.GetHabbo().Id, 0, item.GetFirstBaseItem().FlatId, "", true);

                Session.GetHabbo().BuildersItemsUsed++;

                actualRoom.GetRoomItemHandler().FloorItems.TryAdd(newItem.Id, newItem);

                var message = new ServerMessage(LibraryParser.OutgoingRequest("AddFloorItemMessageComposer"));
                newItem.Serialize(message);
                message.AppendString(Session.GetHabbo().UserName);
                actualRoom.SendMessage(message);
                actualRoom.GetGameMap().AddItemToMap(newItem);
            }
        }

        internal void PlaceBuildersWallItem()
        {
            /*var pageId = */
            Request.GetInteger();
            var itemId = Request.GetUInteger();
            var extradata = Request.GetString();
            var wallcoords = Request.GetString();
            var actualRoom = Session.GetHabbo().CurrentRoom;
            var item = Yupi.GetGame().GetCatalog().GetItem(itemId);
            if (actualRoom == null || item == null) return;

            Session.GetHabbo().BuildersItemsUsed++;
            BuildersClubUpdateFurniCount();

            using (var adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery("INSERT INTO items_rooms (user_id,room_id,item_name,wall_pos,builders) VALUES (@userId,@roomId,@baseName,@wallpos,'1')");
                adapter.AddParameter("userId", Session.GetHabbo().Id);
                adapter.AddParameter("roomId", actualRoom.RoomId);
                adapter.AddParameter("baseName", item.BaseName);
                adapter.AddParameter("wallpos", wallcoords);

                var insertId = (uint)adapter.InsertQuery();

                var newItem = new RoomItem(insertId, actualRoom.RoomId, item.BaseName, extradata, new WallCoordinate(wallcoords), actualRoom, Session.GetHabbo().Id, 0, item.GetFirstBaseItem().FlatId, true);

                actualRoom.GetRoomItemHandler().WallItems.TryAdd(newItem.Id, newItem);

                var message = new ServerMessage(LibraryParser.OutgoingRequest("AddWallItemMessageComposer"));
                newItem.Serialize(message);
                message.AppendString(Session.GetHabbo().UserName);
                Session.SendMessage(message);
                actualRoom.GetGameMap().AddItemToMap(newItem);
            }
        }

        internal void BuildersClubUpdateFurniCount()
        {
            if (Session.GetHabbo().BuildersItemsUsed < 0)
                Session.GetHabbo().BuildersItemsUsed = 0;

            var message = new ServerMessage(LibraryParser.OutgoingRequest("BuildersClubUpdateFurniCountMessageComposer"));

            message.AppendInteger(Session.GetHabbo().BuildersItemsUsed);
            Session.SendMessage(message);
        }

        internal void ConfirmLoveLock()
        {
            var pId = Request.GetUInteger();
            var confirmLoveLock = Request.GetBool();

            var room = Session.GetHabbo().CurrentRoom;

            var item = room?.GetRoomItemHandler().GetItem(pId);
            if (item == null || item.GetBaseItem().InteractionType != Interaction.LoveShuffler)
                return;

            var userIdOne = item.InteractingUser;
            var userIdTwo = item.InteractingUser2;
            var userOne = room.GetRoomUserManager().GetRoomUserByHabbo(userIdOne);
            var userTwo = room.GetRoomUserManager().GetRoomUserByHabbo(userIdTwo);

            if (userOne == null && userTwo == null)
            {
                item.InteractingUser = 0;
                item.InteractingUser2 = 0;
                return;
            }

            if (userOne == null)
            {
                userTwo.CanWalk = true;
                userTwo.GetClient().SendNotif("Your partner has left the room or has cancelled the love lock.");
                userTwo.LoveLockPartner = 0;
                item.InteractingUser = 0;
                item.InteractingUser2 = 0;
                return;
            }

            if (userTwo == null)
            {
                userOne.CanWalk = true;
                userOne.GetClient().SendNotif("Your partner has left the room or has cancelled the love lock.");
                userOne.LoveLockPartner = 0;
                item.InteractingUser = 0;
                item.InteractingUser2 = 0;
                return;
            }

            if (!confirmLoveLock)
            {
                item.InteractingUser = 0;
                item.InteractingUser2 = 0;

                userOne.LoveLockPartner = 0;
                userOne.CanWalk = true;
                userTwo.LoveLockPartner = 0;
                userTwo.CanWalk = true;
                return;
            }

            var loock = new ServerMessage(LibraryParser.OutgoingRequest("LoveLockDialogueSetLockedMessageComposer"));
            loock.AppendInteger(item.Id);

            if (userIdOne == Session.GetHabbo().Id)
            {
                userOne.GetClient().SendMessage(loock);
                userOne.LoveLockPartner = userIdTwo;
            }
            else if (userIdTwo == Session.GetHabbo().Id)
            {
                userTwo.GetClient().SendMessage(loock);
                userTwo.LoveLockPartner = userIdOne;
            }

            // Now check if both of the users have confirmed.
            if (userOne.LoveLockPartner == 0 || userTwo.LoveLockPartner == 0)
                return;

            item.ExtraData = $"1{(char) 5}{userOne.GetUserName()}{(char) 5}{userTwo.GetUserName()}{(char) 5}{userOne.GetClient().GetHabbo().Look}{(char) 5}{userTwo.GetClient().GetHabbo().Look}{(char) 5}{DateTime.Now.ToString("dd/MM/yyyy")}";

            userOne.LoveLockPartner = 0;
            userTwo.LoveLockPartner = 0;
            item.InteractingUser = 0;
            item.InteractingUser2 = 0;

            item.UpdateState(true, false);

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = " + item.Id);
                queryReactor.AddParameter("extraData", item.ExtraData);
                queryReactor.RunQuery();
            }

            var message = new ServerMessage(LibraryParser.OutgoingRequest("UpdateRoomItemMessageComposer"));
            item.Serialize(message);
            room.SendMessage(message);

            loock = new ServerMessage(LibraryParser.OutgoingRequest("LoveLockDialogueCloseMessageComposer"));
            loock.AppendInteger(item.Id);
            userOne.GetClient().SendMessage(loock);
            userTwo.GetClient().SendMessage(loock);
            userOne.CanWalk = true;
            userTwo.CanWalk = true;
        }

        internal void SaveFootballOutfit()
        {
            var pId = Request.GetUInteger();
            var gender = Request.GetString();
            var look = Request.GetString();

            var room = Session.GetHabbo().CurrentRoom;

            var item = room?.GetRoomItemHandler().GetItem(pId);

            if (item == null || item.GetBaseItem().InteractionType != Interaction.FootballGate)
                return;

            var figures = item.ExtraData.Split(',');
            var newFigures = new string[2];

            switch (gender.ToUpper())
            {
                case "M":
                    {
                        newFigures[0] = look;

                        newFigures[1] = (figures.Length > 1) ? figures[1] : "hd-99999-99999.ch-630-62.lg-695-62";

                        item.ExtraData = string.Join(",", newFigures);
                    }
                    break;

                case "F":
                    {
                        newFigures[0] = !string.IsNullOrWhiteSpace(figures[0]) ? figures[0] : "hd-99999-99999.lg-270-62";

                        newFigures[1] = look;

                        item.ExtraData = string.Join(",", newFigures);
                    }
                    break;
            }

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = " + item.Id);
                queryReactor.AddParameter("extraData", item.ExtraData);
                queryReactor.RunQuery();
            }

            var message = new ServerMessage(LibraryParser.OutgoingRequest("UpdateRoomItemMessageComposer"));
            item.Serialize(message);
            Session.SendMessage(message);
        }

        internal void SaveMannequin()
        {
            var pId = Request.GetUInteger();
            var text = Request.GetString();
            var item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(pId);

            if (item == null)
                return;

            if (!item.ExtraData.Contains(Convert.ToChar(5)))
                return;

            if (!Session.GetHabbo().CurrentRoom.CheckRights(Session, true))
                return;

            var array = item.ExtraData.Split(Convert.ToChar(5));

            array[2] = text;

            item.ExtraData = string.Concat(array[0], Convert.ToChar(5), array[1], Convert.ToChar(5), array[2]);
            item.Serialize(Response);
            item.UpdateState(true, true);

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = " + item.Id);
                queryReactor.AddParameter("extraData", item.ExtraData);
                queryReactor.RunQuery();
            }
        }

        internal void SaveMannequin2()
        {
            var pId = Request.GetUInteger();
            var item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(pId);
            if (item == null)
                return;
            if (!item.ExtraData.Contains(Convert.ToChar(5)))
                return;
            if (!Session.GetHabbo().CurrentRoom.CheckRights(Session, true))
                return;
            var array = item.ExtraData.Split(Convert.ToChar(5));
            array[0] = Session.GetHabbo().Gender.ToLower();
            array[1] = string.Empty;
            var array2 = Session.GetHabbo().Look.Split('.');

            foreach (
                string text in
                    array2.Where(
                        text =>
                            !text.Contains("hr") && !text.Contains("hd") && !text.Contains("he") && !text.Contains("ea") &&
                            !text.Contains("ha")))
            {
                string[] array3;
                (array3 = array)[1] = $"{array3[1]}{text}.";
            }

            array[1] = array[1].TrimEnd('.');
            item.ExtraData = string.Concat(array[0], Convert.ToChar(5), array[1], Convert.ToChar(5), array[2]);
            item.UpdateNeeded = true;
            item.UpdateState(true, true);

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = " + item.Id);
                queryReactor.AddParameter("extraData", item.ExtraData);
                queryReactor.RunQuery();
            }
        }

        internal void EjectFurni()
        {
            Request.GetInteger();

            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session))
                return;

            var pId = Request.GetUInteger();
            var item = room.GetRoomItemHandler().GetItem(pId);

            if (item == null)
                return;

            var clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(item.UserId);

            if (item.GetBaseItem().InteractionType == Interaction.PostIt)
                return;

            if (clientByUserId != null)
            {
                room.GetRoomItemHandler().RemoveFurniture(Session, item.Id);
                clientByUserId.GetHabbo()
                    .GetInventoryComponent()
                    .AddNewItem(item.Id, item.BaseName, item.ExtraData, item.GroupId, true, true, 0, 0);
                clientByUserId.GetHabbo().GetInventoryComponent().UpdateItems(true);
                return;
            }

            room.GetRoomItemHandler().RemoveFurniture(Session, item.Id);

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"UPDATE items_rooms SET room_id='0' WHERE id='{item.Id}' LIMIT 1");
        }

        internal void UsePurchasableClothing()
        {
            uint furniId = Request.GetUInteger();

            var room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            var item = room?.GetRoomItemHandler().GetItem(furniId);

            if (item?.GetBaseItem().InteractionType != Interaction.Clothing)
                return;
            var clothes = Yupi.GetGame().GetClothingManager().GetClothesInFurni(item.GetBaseItem().Name);

            if (clothes == null)
                return;

            if (Session.GetHabbo().ClothesManagerManager.Clothing.Contains(clothes.ItemName))
                return;

            Session.GetHabbo().ClothesManagerManager.Add(clothes.ItemName);

            GetResponse().Init(LibraryParser.OutgoingRequest("FigureSetIdsMessageComposer"));
            Session.GetHabbo().ClothesManagerManager.Serialize(GetResponse());

            SendResponse();

            room.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);
            Session.SendMessage(StaticMessage.FiguresetRedeemed);

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery("DELETE FROM items_rooms WHERE id = " + item.Id);
        }

        internal void GetUserLook()
        {
            string oldLook = Request.GetString();
        }
    }
}