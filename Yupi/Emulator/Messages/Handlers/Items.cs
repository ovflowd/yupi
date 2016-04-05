using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Catalogs;
using Yupi.Emulator.Game.Catalogs.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items;
using Yupi.Emulator.Game.Items.Datas;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.Pets.Enums;
using Yupi.Emulator.Game.Pets.Structs;
using Yupi.Emulator.Game.RoomBots;
using Yupi.Emulator.Game.RoomBots.Enumerators;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Rooms.User.Trade;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Enums;

namespace Yupi.Emulator.Messages.Handlers
{
     partial class MessageHandler
    {
         void PetBreedCancel()
        {
            if (Session?.GetHabbo() == null)
                return;

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            uint itemId = Request.GetUInt32();

            RoomItem item = room.GetRoomItemHandler().GetItem(itemId);

            if (item == null)
                return;

            if (item.GetBaseItem().InteractionType != Interaction.BreedingTerrier &&
                item.GetBaseItem().InteractionType != Interaction.BreedingBear)
                return;

            foreach (Pet pet in item.PetsList)
            {
                pet.WaitingForBreading = 0;
                pet.BreadingTile = new Point();

                RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);
                user.Freezed = false;
                room.GetGameMap().AddUserToMap(user, user.Coordinate);

                Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                user.MoveTo(nextCoord.X, nextCoord.Y);
            }

            item.PetsList.Clear();
        }

         void PetBreedResult()
        {
            if (Session?.GetHabbo() == null)
                return;

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            uint itemId = Request.GetUInt32();

            RoomItem item = room.GetRoomItemHandler().GetItem(itemId);

            if (item == null)
                return;

            if (item.GetBaseItem().InteractionType != Interaction.BreedingTerrier &&
                item.GetBaseItem().InteractionType != Interaction.BreedingBear)
                return;

            string petName = Request.GetString();

            item.ExtraData = "1";
            item.UpdateState();

            int randomNmb = new Random().Next(101);
            int petType = 0;
            int randomResult = 3;

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

            Pet pet = CatalogManager.CreatePet(Session.GetHabbo().Id, petName, item.GetBaseItem().InteractionType == Interaction.BreedingTerrier ? "pet_terrierbaby" : "pet_bearbaby",  petType.ToString(), "ffffff");

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

            Session.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.OutgoingHandler("PetBreedResultMessageComposer"));
            Session.GetMessageHandler().GetResponse().AppendInteger(pet.PetId);
            Session.GetMessageHandler().GetResponse().AppendInteger(randomResult);
            Session.GetMessageHandler().SendResponse();

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
        }

         void GetTrainerPanel()
        {
            uint petId = Request.GetUInt32();

            Room currentRoom = Session.GetHabbo().CurrentRoom;

            if (currentRoom == null)
                return;

            Pet petData;

            if ((petData = currentRoom.GetRoomUserManager().GetPet(petId).PetData) == null)
                return;

            Response.Init(PacketLibraryManager.OutgoingHandler("PetTrainerPanelMessageComposer"));
            Response.AppendInteger(petData.PetId);

            Dictionary<uint, PetCommand> totalPetCommands = PetCommandHandler.GetAllPetCommands();

            Dictionary<uint, PetCommand> petCommands = PetCommandHandler.GetPetCommandByPetType(petData.Type);

            Response.AppendInteger(totalPetCommands.Count);

            foreach (uint sh in totalPetCommands.Keys)
                Response.AppendInteger(sh);

            Response.AppendInteger(petCommands.Count);

            foreach (uint sh in petCommands.Keys)
                Response.AppendInteger(sh);

            SendResponse();
        }

         void PlacePostIt()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session))
                return;

            uint id = Request.GetUInt32();
            string locationData = Request.GetString();

            UserItem item = Session.GetHabbo().GetInventoryComponent().GetItem(id);

            if (item == null)
                return;

            try
            {
                WallCoordinate wallCoord = new WallCoordinate(":" + locationData.Split(':')[1]);

                RoomItem item2 = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData, wallCoord, room,
                    Session.GetHabbo().Id, item.GroupId, false);

                if (room.GetRoomItemHandler().SetWallItem(Session, item2))
                    Session.GetHabbo().GetInventoryComponent().RemoveItem(id, true);
            }
            catch
            {
                // ignored
            }
        }

         void PlaceItem()
        {
            if (Session?.GetHabbo() == null)
                return;

            try
            {
                Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

                if (room == null || Yupi.GetDbConfig().DbData["placing_enabled"] != "1")
                    return;

                if (!room.CheckRights(Session, false, true))
                {
                    Session.SendMessage(StaticMessage.ErrorCantSetNotOwner);
                    return;
                }

                string placementData = Request.GetString();
                string[] dataBits = placementData.Split(' ');

                uint itemId = uint.Parse(dataBits[0].Replace("-", string.Empty));

                UserItem item = Session.GetHabbo().GetInventoryComponent().GetItem(itemId);

                if (item == null)
                    return;

                string type = dataBits[1].StartsWith(":") ? "wall" : "floor";
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
                                if (room.MoodlightData != null &&
                                    room.GetRoomItemHandler().GetItem(room.MoodlightData.ItemId) != null)
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
                                RoomItem roomItemBreed = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name,
                                    item.ExtraData,
                                    x, y, z, rot, room, Session.GetHabbo().Id, 0, string.Empty, false);

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
                                TonerData tonerData = room.TonerData;
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

                WallCoordinate coordinate = new WallCoordinate(":" + placementData.Split(':')[1]);

                RoomItem roomItemWall = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData,
                    coordinate, room, Session.GetHabbo().Id, item.GroupId, false);

                if (room.GetRoomItemHandler().SetWallItem(Session, roomItemWall))
                    Session.GetHabbo().GetInventoryComponent().RemoveItem(itemId, true);

                return;

                PlaceFloor:

                RoomItem roomItem = new RoomItem(item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData, x, y, z, rot, room,
                    Session.GetHabbo().Id, item.GroupId, item.SongCode, false);

                if (room.GetRoomItemHandler().SetFloorItem(Session, roomItem, x, y, rot, true, false, true))
                {
                    Session.GetHabbo().GetInventoryComponent().RemoveItem(itemId, true);

                    if (roomItem.IsWired)
                    {
                        IWiredItem item5 = room.GetWiredHandler().GenerateNewItem(roomItem);

                        room.GetWiredHandler().AddWired(item5);

                        WiredHandler.SaveWired(item5);
                    }
                }

                Yupi.GetGame()
                    .GetAchievementManager()
                    .ProgressUserAchievement(Session, "ACH_RoomDecoFurniCount", 1, true);
                return;

                CannotSetItem:
                Session.SendMessage(StaticMessage.ErrorCantSetItem);
            }
            catch (Exception e)
            {
                Session.SendMessage(StaticMessage.ErrorCantSetItem);

                YupiLogManager.LogException(e, "Failed Handling Item.", "Yupi.Mobi");
            }
        }

         void TakeItem()
        {
            Request.GetInteger();

            Room room = Session.GetHabbo().CurrentRoom;

            if (room?.GetRoomItemHandler() == null || Session.GetHabbo() == null)
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(Request.GetUInt32());

            if (item == null || item.GetBaseItem().InteractionType == Interaction.PostIt)
                return;

            if (item.UserId != Session.GetHabbo().Id && !room.CheckRights(Session, true))
                return;

            switch (item.GetBaseItem().InteractionType)
            {
                case Interaction.BreedingTerrier:

                    if (room.GetRoomItemHandler().BreedingTerrier.ContainsKey(item.Id))
                        room.GetRoomItemHandler().BreedingTerrier.Remove(item.Id);

                    foreach (Pet pet in item.PetsList)
                    {
                        pet.WaitingForBreading = 0;
                        pet.BreadingTile = new Point();

                        RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);

                        if (user == null)
                            continue;

                        user.Freezed = false;
                        room.GetGameMap().AddUserToMap(user, user.Coordinate);

                        Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                        user.MoveTo(nextCoord.X, nextCoord.Y);
                    }

                    item.PetsList.Clear();
                    break;

                case Interaction.BreedingBear:

                    if (room.GetRoomItemHandler().BreedingBear.ContainsKey(item.Id))
                        room.GetRoomItemHandler().BreedingBear.Remove(item.Id);

                    foreach (Pet pet in item.PetsList)
                    {
                        pet.WaitingForBreading = 0;
                        pet.BreadingTile = new Point();

                        RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);

                        if (user == null)
                            continue;

                        user.Freezed = false;
                        room.GetGameMap().AddUserToMap(user, user.Coordinate);

                        Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                        user.MoveTo(nextCoord.X, nextCoord.Y);
                    }

                    item.PetsList.Clear();
                    break;
            }
            if (item.IsBuilder)
            {
                using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    room.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);
                    Session.GetHabbo().BuildersItemsUsed--;
                    BuildersClubUpdateFurniCount();

					adapter.SetQuery("DELETE FROM items_rooms WHERE id = @item_id");
					adapter.AddParameter("item_id", item.Id);
					adapter.RunQuery ();
                }
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

         void MoveItem()
        {
            uint id = Convert.ToUInt32(Math.Abs(Request.GetInteger()));

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;

            if (!room.CheckRights(Session, false, true))
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(id);

            if (item == null)
                return;

            int x = Request.GetInteger();
            int y = Request.GetInteger();
            int rot = Request.GetInteger();

            Request.GetInteger();

            bool flag = item.GetBaseItem().InteractionType == Interaction.Teleport ||
                       item.GetBaseItem().InteractionType == Interaction.Hopper ||
                       item.GetBaseItem().InteractionType == Interaction.QuickTeleport;

            List<Point> oldCoords = item.GetCoords;

            if (!room.GetRoomItemHandler().SetFloorItem(Session, item, x, y, rot, false, false, true, true, false))
            {
                SimpleServerMessageBuffer message3 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateRoomItemMessageComposer"));
                item.Serialize(message3);
                room.SendMessage(message3);

                return;
            }

            if (item.GetBaseItem().InteractionType == Interaction.BreedingTerrier ||
                item.GetBaseItem().InteractionType == Interaction.BreedingBear)
            {
                foreach (Pet pet in item.PetsList)
                {
                    pet.WaitingForBreading = 0;
                    pet.BreadingTile = new Point();

                    RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);

                    if (user == null)
                        continue;

                    user.Freezed = false;
                    room.GetGameMap().AddUserToMap(user, user.Coordinate);

                    Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
                    user.MoveTo(nextCoord.X, nextCoord.Y);
                }

                item.PetsList.Clear();
            }

            List<Point> newcoords = item.GetCoords;
            room.GetRoomItemHandler().OnHeightMapUpdate(oldCoords, newcoords);

            if (!flag)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                room.GetRoomItemHandler().SaveFurniture(queryReactor);
        }

         void MoveWallItem()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session))
                return;

            uint id = Request.GetUInt32();
            string locationData = Request.GetString();

            RoomItem item = room.GetRoomItemHandler().GetItem(id);

            if (item == null)
                return;

            try
            {
                WallCoordinate wallCoord = new WallCoordinate(":" + locationData.Split(':')[1]);
                item.WallCoord = wallCoord;
            }
            catch
            {
                return;
            }

            room.GetRoomItemHandler().AddOrUpdateItem(id);

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateRoomWallItemMessageComposer"));
            item.Serialize(messageBuffer);
            room.SendMessage(messageBuffer);
        }

         void TriggerItem()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;

            int num = Request.GetInteger();

            if (num < 0)
                return;

            uint pId = Convert.ToUInt32(num);

            RoomItem item = room.GetRoomItemHandler().GetItem(pId);

            if (item == null)
                return;

            bool hasRightsOne = room.CheckRights(Session, false, true);
            bool hasRightsTwo = room.CheckRights(Session, true);

            switch (item.GetBaseItem().InteractionType)
            {
                case Interaction.RoomBg:
                {
                    if (!hasRightsTwo)
                        return;

                    room.TonerData.Enabled = room.TonerData.Enabled == 0 ? 1 : 0;

                    SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateRoomItemMessageComposer"));
                    item.Serialize(messageBuffer);
                    room.SendMessage(messageBuffer);

                    item.UpdateState();

                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        queryReactor.RunFastQuery(
                            $"UPDATE items_toners SET enabled = '{room.TonerData.Enabled}' LIMIT 1");

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

            foreach (RoomUser current in room.GetRoomUserManager().UserList.Values.Where(current => current != null))
                room.GetRoomUserManager().UpdateUserStatus(current, true);
        }

         void TriggerItemDiceSpecial()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            RoomItem item = room?.GetRoomItemHandler().GetItem(Request.GetUInt32());

            if (item == null)
                return;

            bool hasRights = room.CheckRights(Session);

            item.Interactor.OnTrigger(Session, item, -1, hasRights);
            item.OnTrigger(room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id));
        }

         void OpenPostit()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            RoomItem item = room?.GetRoomItemHandler().GetItem(Request.GetUInt32());

            if (item == null || item.GetBaseItem().InteractionType != Interaction.PostIt)
                return;

            Response.Init(PacketLibraryManager.OutgoingHandler("LoadPostItMessageComposer"));
            Response.AppendString(item.Id.ToString());
            Response.AppendString(item.ExtraData);
            SendResponse();
        }

         void SavePostit()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            RoomItem item = room?.GetRoomItemHandler().GetItem(Request.GetUInt32());

            if (item == null || item.GetBaseItem().InteractionType != Interaction.PostIt)
                return;

            string text = Request.GetString();
            string text2 = Request.GetString();

            if (!room.CheckRights(Session) && !text2.StartsWith(item.ExtraData))
                return;

            string a;

            if ((a = text) == null || (a != "FFFF33" && a != "FF9CFF" && a != "9CCEFF" && a != "9CFF9C"))
                return;

            item.ExtraData = $"{text} {text2}";
            item.UpdateState(true, true);
        }

         void DeletePostit()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(Request.GetUInt32());

            if (item == null || item.GetBaseItem().InteractionType != Interaction.PostIt)
                return;

            room.GetRoomItemHandler().RemoveFurniture(Session, item.Id);
        }

         void OpenGift()
        {
            if ((DateTime.Now - Session.GetHabbo().LastGiftOpenTime).TotalSeconds <= 15.0)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("gift_one"));
                return;
            }

            Room currentRoom = Session.GetHabbo().CurrentRoom;

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

            uint pId = Request.GetUInt32();

            RoomItem item = currentRoom.GetRoomItemHandler().GetItem(pId);

            if (item == null)
            {
                Session.SendWhisper(Yupi.GetLanguage().GetVar("gift_four"));
                return;
            }

            item.MagicRemove = true;

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateRoomItemMessageComposer"));
            item.Serialize(messageBuffer);
            currentRoom.SendMessage(messageBuffer);

            Session.GetHabbo().LastGiftOpenTime = DateTime.Now;
            IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor();

            queryReactor.SetQuery("SELECT * FROM users_gifts WHERE gift_id = @gift_id");
			queryReactor.AddParameter ("gift_id", item.Id);
            DataRow row = queryReactor.GetRow();

            if (row == null)
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);
                return;
            }

            Item item2 = Yupi.GetGame().GetItemManager().GetItem(Convert.ToUInt32(row["item_id"]));

            if (item2 == null)
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);
                return;
            }

            if (item2.Type.Equals('s'))
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);

                string extraData = row["extradata"].ToString();
                string itemName = row["item_name"].ToString();

                queryReactor.RunFastQuery($"UPDATE items_rooms SET item_name='{itemName}' WHERE id='{item.Id}'");

                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
				queryReactor.AddParameter("id", item.Id);
                queryReactor.AddParameter("extraData", extraData);
                queryReactor.RunQuery();

				queryReactor.SetQuery("DELETE FROM users_gifts WHERE gift_id=@id");
				queryReactor.AddParameter("id", item.Id);
				queryReactor.RunQuery();

                item.BaseName = itemName;
                item.RefreshItem();
                item.ExtraData = extraData;

                if (!currentRoom.GetRoomItemHandler().SetFloorItem(item, item.X, item.Y, item.Z, item.Rot, true))
                    Session.SendNotif(Yupi.GetLanguage().GetVar("error_creating_gift"));
                else
                {
                    Response.Init(PacketLibraryManager.OutgoingHandler("OpenGiftMessageComposer"));

                    Response.AppendString(item2.Type.ToString());
                    Response.AppendInteger(item2.SpriteId);
                    Response.AppendString(item2.Name);
                    Response.AppendInteger(item2.ItemId);
                    Response.AppendString(item2.Type.ToString());
                    Response.AppendBool(true);
                    Response.AppendString(extraData);
                    SendResponse();

                    SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("AddFloorItemMessageComposer"));

                    item.Serialize(simpleServerMessageBuffer);
                    simpleServerMessageBuffer.AppendString(currentRoom.RoomData.Owner);
                    currentRoom.SendMessage(simpleServerMessageBuffer);

                    currentRoom.GetRoomItemHandler().SetFloorItem(Session, item, item.X, item.Y, 0, true, false, true);
                }
            }
            else
            {
                currentRoom.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);

				queryReactor.SetQuery("DELETE FROM users_gifts WHERE gift_id = @id");
				queryReactor.AddParameter ("id", item.Id);
				queryReactor.RunQuery ();

                Response.Init(PacketLibraryManager.OutgoingHandler("NewInventoryObjectMessageComposer"));
                Response.AppendInteger(1);

                int i = 2;

                if (item2.Type == 's')
                    i = item2.InteractionType == Interaction.Pet ? 3 : 1;

                Response.AppendInteger(i);
                List<UserItem> list = Yupi.GetGame()
                    .GetCatalogManager()
                    .DeliverItems(Session, item2, 1, (string) row["extradata"], 0, 0, string.Empty);

                Response.AppendInteger(list.Count);

                foreach (UserItem current in list)
                    Response.AppendInteger(current.Id);

                SendResponse();
                Session.GetHabbo().GetInventoryComponent().UpdateItems(true);
            }

            Response.Init(PacketLibraryManager.OutgoingHandler("UpdateInventoryMessageComposer"));
            SendResponse();
        }

         void GetMoodlight()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            if (room.MoodlightData == null)
            {
                foreach (
                    RoomItem current in
                        room.GetRoomItemHandler()
                            .WallItems.Values.Where(
                                current => current.GetBaseItem().InteractionType == Interaction.Dimmer))
                    room.MoodlightData = new MoodlightData(current.Id);
            }

            if (room.MoodlightData == null)
                return;

            Response.Init(PacketLibraryManager.OutgoingHandler("DimmerDataMessageComposer"));
            Response.AppendInteger(room.MoodlightData.Presets.Count);
            Response.AppendInteger(room.MoodlightData.CurrentPreset);

            int num = 0;

            foreach (MoodlightPreset current2 in room.MoodlightData.Presets)
            {
                num++;

                Response.AppendInteger(num);
                Response.AppendInteger(int.Parse(Yupi.BoolToEnum(current2.BackgroundOnly)) + 1);
                Response.AppendString(current2.ColorCode);
                Response.AppendInteger(current2.ColorIntensity);
            }

            SendResponse();
        }

         void UpdateMoodlight()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true) || room.MoodlightData == null)
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(room.MoodlightData.ItemId);

            if (item == null || item.GetBaseItem().InteractionType != Interaction.Dimmer)
                return;

            int num = Request.GetInteger();
            int num2 = Request.GetInteger();
            string color = Request.GetString();
            int intensity = Request.GetInteger();
            bool bgOnly = num2 >= 2;

            room.MoodlightData.Enabled = true;
            room.MoodlightData.CurrentPreset = num;
            room.MoodlightData.UpdatePreset(num, color, intensity, bgOnly);

            item.ExtraData = room.MoodlightData.GenerateExtraData();
            item.UpdateState();
        }

         void SwitchMoodlightStatus()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true) || room.MoodlightData == null)
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(room.MoodlightData.ItemId);

            if (item == null || item.GetBaseItem().InteractionType != Interaction.Dimmer)
                return;

            if (room.MoodlightData.Enabled)
                room.MoodlightData.Disable();
            else
                room.MoodlightData.Enable();

            item.ExtraData = room.MoodlightData.GenerateExtraData();
            item.UpdateState();
        }

         void SaveRoomBg()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(room.TonerData.ItemId);

            if (item == null || item.GetBaseItem().InteractionType != Interaction.RoomBg)
                return;

            Request.GetInteger();

            int num = Request.GetInteger();
            int num2 = Request.GetInteger();
            int num3 = Request.GetInteger();

            if (num > 255 || num2 > 255 || num3 > 255)
                return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
				queryReactor.SetQuery ("UPDATE items_toners SET enabled = @enabled, data1 = @data1, data2 = @data2, data3 = @data3 WHERE id = @id");
				queryReactor.AddParameter ("enabled", 1);
				// TODO Rename num variables !!!
				queryReactor.AddParameter ("data1", num);
				queryReactor.AddParameter ("data2", num2);
				queryReactor.AddParameter ("data3", num3);
				queryReactor.AddParameter ("id", item.Id);
			}

            room.TonerData.Data1 = num;
            room.TonerData.Data2 = num2;
            room.TonerData.Data3 = num3;
            room.TonerData.Enabled = 1;

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateRoomItemMessageComposer"));
            item.Serialize(messageBuffer);
            room.SendMessage(messageBuffer);

            item.UpdateState();
        }

         void InitTrade()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

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

            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            RoomUser roomUserByVirtualId = room.GetRoomUserManager().GetRoomUserByVirtualId(Request.GetInteger());

            if (roomUserByVirtualId?.GetClient() == null || roomUserByVirtualId.GetClient().GetHabbo() == null)
                return;

            room.TryStartTrade(roomUserByHabbo, roomUserByVirtualId);
        }

         void TileStackMagicSetHeight()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;

            uint itemId = Request.GetUInt32();

            RoomItem item = room.GetRoomItemHandler().GetItem(itemId);

            if (item == null || item.GetBaseItem().InteractionType != Interaction.TileStackMagic)
                return;

            int heightToSet = Request.GetInteger();
            double totalZ;

            if (heightToSet < 0)
            {
                totalZ = room.GetGameMap().SqAbsoluteHeight(item.X, item.Y);

                SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateTileStackMagicHeight"));
                messageBuffer.AppendInteger(item.Id);
                messageBuffer.AppendInteger(Convert.ToUInt32(totalZ*100));
                Session.SendMessage(messageBuffer);
            }
            else
            {
                if (heightToSet > 10000)
                    heightToSet = 10000;

                totalZ = heightToSet/100.0;

                if (totalZ < room.RoomData.Model.SqFloorHeight[item.X][item.Y])
                {
                    totalZ = room.RoomData.Model.SqFloorHeight[item.X][item.Y];

                    SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateTileStackMagicHeight"));
                    messageBuffer.AppendInteger(item.Id);
                    messageBuffer.AppendInteger(Convert.ToUInt32(totalZ*100));
                    Session.SendMessage(messageBuffer);
                }
            }

            room.GetRoomItemHandler().SetFloorItem(item, item.X, item.Y, totalZ, item.Rot, true);
        }

         void OfferTradeItem()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CanTradeInRoom)
                return;

            Trade userTrade = room.GetUserTrade(Session.GetHabbo().Id);
            UserItem item = Session.GetHabbo().GetInventoryComponent().GetItem(Request.GetUInt32());

            if (userTrade == null || item == null)
                return;

            userTrade.OfferItem(Session.GetHabbo().Id, item);
        }

         void TakeBackTradeItem()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CanTradeInRoom)
                return;

            Trade userTrade = room.GetUserTrade(Session.GetHabbo().Id);
            UserItem item = Session.GetHabbo().GetInventoryComponent().GetItem(Request.GetUInt32());

            if (userTrade == null || item == null)
                return;

            userTrade.TakeBackItem(Session.GetHabbo().Id, item);
        }

         void StopTrade()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CanTradeInRoom)
                return;

            room.TryStopTrade(Session.GetHabbo().Id);
        }

         void AcceptTrade()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CanTradeInRoom)
                return;

            Trade userTrade = room.GetUserTrade(Session.GetHabbo().Id);

            userTrade?.Accept(Session.GetHabbo().Id);
        }

         void UnacceptTrade()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CanTradeInRoom)
                return;

            Trade userTrade = room.GetUserTrade(Session.GetHabbo().Id);

            userTrade?.Unaccept(Session.GetHabbo().Id);
        }

         void CompleteTrade()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CanTradeInRoom)
                return;

            Trade userTrade = room.GetUserTrade(Session.GetHabbo().Id);

            userTrade?.CompleteTrade(Session.GetHabbo().Id);
        }

         void RecycleItems()
        {
            if (!Session.GetHabbo().InRoom)
                return;

            int num = Request.GetInteger();

            if (num != Convert.ToUInt32(Yupi.GetDbConfig().DbData["recycler.number_of_slots"]))
                return;

            int i = 0;

            while (i < num)
            {
                UserItem item = Session.GetHabbo().GetInventoryComponent().GetItem(Request.GetUInt32());

                if (item == null || !item.BaseItem.AllowRecycle)
                    return;

                Session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE id={item.Id} LIMIT 1");

                i++;
            }

            EcotronReward randomEcotronReward = Yupi.GetGame().GetCatalogManager().GetRandomEcotronReward();

            uint insertId;

            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryreactor2.SetQuery(
                    "INSERT INTO items_rooms (user_id,item_name,extra_data) VALUES (@userid, @baseName, @timestamp)");

                queryreactor2.AddParameter("userid", (int) Session.GetHabbo().Id);
                queryreactor2.AddParameter("timestamp", DateTime.Now.ToLongDateString());
                queryreactor2.AddParameter("baseName", Yupi.GetDbConfig().DbData["recycler.box_name"]);

                insertId = (uint) queryreactor2.InsertQuery();

				queryreactor2.SetQuery ("INSERT INTO users_gifts (gift_id,item_id,gift_sprite,extradata) VALUES (@gift_id, @item_id, @gift_sprite, @extradata)");
				queryreactor2.AddParameter ("gift_id", insertId);
				queryreactor2.AddParameter ("item_id", randomEcotronReward.BaseId);
				queryreactor2.AddParameter ("gift_sprite", randomEcotronReward.DisplayId);
				queryreactor2.AddParameter ("extradata", "");
				queryreactor2.RunQuery ();
            }

            Session.GetHabbo().GetInventoryComponent().UpdateItems(true);

            Response.Init(PacketLibraryManager.OutgoingHandler("RecyclingStateMessageComposer"));
            Response.AppendInteger(1);
            Response.AppendInteger(insertId);
            SendResponse();
        }

         void RedeemExchangeFurni()
        {
            if (Session?.GetHabbo() == null)
                return;

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            if (Yupi.GetDbConfig().DbData["exchange_enabled"] != "1")
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("exchange_is_disabled"));
                return;
            }

            RoomItem item = room.GetRoomItemHandler().GetItem(Request.GetUInt32());

            if (item == null)
                return;

            if (!item.GetBaseItem().Name.StartsWith("CF_") && !item.GetBaseItem().Name.StartsWith("CFC_"))
                return;

            string[] array = item.GetBaseItem().Name.Split('_');

            uint amount;

            if (array[1] == "diamond")
            {
                uint.TryParse(array[2], out amount);

                Session.GetHabbo().Diamonds += amount;
                Session.GetHabbo().UpdateSeasonalCurrencyBalance();
            }
            else
            {
                uint.TryParse(array[1], out amount);

                Session.GetHabbo().Credits += amount;
                Session.GetHabbo().UpdateCreditsBalance();
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE id={item.Id} LIMIT 1;");

            room.GetRoomItemHandler().RemoveFurniture(null, item.Id, false);

            Session.GetHabbo().GetInventoryComponent().RemoveItem(item.Id, false);

            Response.Init(PacketLibraryManager.OutgoingHandler("UpdateInventoryMessageComposer"));
            SendResponse();
        }

        // Without VOID! //@TODO find void
         void TriggerLoveLock(RoomItem loveLock)
        {
            string[] loveLockParams = loveLock.ExtraData.Split(Convert.ToChar(5));

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

                RoomUser roomUserOne = loveLock.GetRoom().GetRoomUserManager().GetUserForSquare(pointOne.X, pointOne.Y);
                RoomUser roomUserTwo = loveLock.GetRoom().GetRoomUserManager().GetUserForSquare(pointTwo.X, pointTwo.Y);

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

                SimpleServerMessageBuffer lockDialogue = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("LoveLockDialogueMessageComposer"));
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

         void GetPetInfo()
        {
            if (Session.GetHabbo() == null || Session.GetHabbo().CurrentRoom == null)
                return;

            uint petId = Request.GetUInt32();

            RoomUser pet = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetPet(petId);

            if (pet?.PetData == null)
                return;

            Session.SendMessage(pet.PetData.SerializeInfo());
        }

         void CompostMonsterplant()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_1"));
                return;
            }

            uint moplaId = Request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(moplaId);

            if (pet == null || !pet.IsPet || pet.PetData.Type != "pet_monster" || pet.PetData.MoplaBreed == null)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_2"));
                return;
            }

            if (pet.PetData.MoplaBreed.LiveState != MoplaState.Dead)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_3"));
                return;
            }

            Item compostItem = Yupi.GetGame().GetItemManager().GetItemByName("mnstr_compost");

            if (compostItem == null)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_4"));
                return;
            }

            int x = pet.X;
            int y = pet.Y;
            double z = pet.Z;

            room.GetRoomUserManager().RemoveBot(pet.VirtualId, false);

            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery(
                    "INSERT INTO items_rooms (user_id, room_id, item_name, extra_data, x, y, z) VALUES (@uid, @rid, @bit, '0', @ex, @wai, @zed)");
                dbClient.AddParameter("uid", Session.GetHabbo().Id);
                dbClient.AddParameter("rid", room.RoomId);
                dbClient.AddParameter("bit", compostItem.Name);
                dbClient.AddParameter("ex", x);
                dbClient.AddParameter("wai", y);
                dbClient.AddParameter("zed", z);

                uint itemId = (uint) dbClient.InsertQuery();

                RoomItem roomItem = new RoomItem(itemId, room.RoomId, compostItem.Name, "0", x, y, z, 0, room,
                    Session.GetHabbo().Id, 0, string.Empty, false);

                if (!room.GetRoomItemHandler().SetFloorItem(Session, roomItem, x, y, 0, true, false, true))
                {
                    Session.GetHabbo().GetInventoryComponent().AddItem(roomItem);
                    Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_5"));
                }

                dbClient.RunFastQuery($"DELETE FROM pets_data WHERE id = {moplaId};");
                dbClient.RunFastQuery($"DELETE FROM pets_plants WHERE pet_id = {moplaId};");
            }
        }

         void MovePet()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if ((room == null) || !room.CheckRights(Session))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_6"));
                return;
            }

            uint petId = Request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet == null || !pet.IsPet || pet.PetData.Type != "pet_monster")
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_7"));
                return;
            }

            int x = Request.GetInteger();
            int y = Request.GetInteger();
            int rot = Request.GetInteger();
            int oldX = pet.X;
            int oldY = pet.Y;

            if ((x != oldX) && (y != oldY))
            {
                if (!room.GetGameMap().CanWalk(x, y, false))
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_8"));
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
        }

         void PickUpPet()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (Session?.GetHabbo() == null || Session.GetHabbo().GetInventoryComponent() == null)
                return;

            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(Session, true)))
                return;

            uint petId = Request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet == null)
                return;

            if (pet.RidingHorse)
            {
                RoomUser roomUserByVirtualId = room.GetRoomUserManager().GetRoomUserByVirtualId(Convert.ToInt32(pet.HorseId));

                if (roomUserByVirtualId != null)
                {
                    roomUserByVirtualId.RidingHorse = false;
                    roomUserByVirtualId.ApplyEffect(-1);
                    roomUserByVirtualId.MoveTo(new Point(roomUserByVirtualId.X + 1, roomUserByVirtualId.Y + 1));
                }
            }

            if (pet.PetData.DbState != DatabaseUpdateState.NeedsInsert)
                pet.PetData.DbState = DatabaseUpdateState.NeedsUpdate;

            pet.PetData.RoomId = 0;

            Session.GetHabbo().GetInventoryComponent().AddPet(pet.PetData);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                room.GetRoomUserManager().SavePets(queryReactor);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"UPDATE pets_data SET room_id = 0, x = 0, y = 0 WHERE id = {petId}");

            room.GetRoomUserManager().RemoveBot(pet.VirtualId, false);

            Session.SendMessage(Session.GetHabbo().GetInventoryComponent().SerializePetInventory());
        }

         void RespectPet()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;

            uint petId = Request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet?.PetData == null)
                return;

            pet.PetData.OnRespect();

            if (pet.PetData.Type == "pet_monster")
                Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_MonsterPlantTreater", 1);
            else
            {
                Session.GetHabbo().DailyPetRespectPoints--;

                Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_PetRespectGiver", 1);

                string[] value = PetLocale.GetValue("pet.respected");
                string message = value[new Random().Next(0, value.Length - 1)];

                pet.Chat(null, message, false, 0);

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery(
                        $"UPDATE users_stats SET daily_pet_respect_points = daily_pet_respect_points - 1 WHERE id = {Session.GetHabbo().Id} LIMIT 1");
            }
        }

         void AllowAllRide()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            uint num = Request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(num);

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

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("PetInfoMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(pet.PetData.PetId);
            simpleServerMessageBuffer.AppendString(pet.PetData.Name);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.Level);
            simpleServerMessageBuffer.AppendInteger(20);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.Experience);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.ExperienceGoal);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.Energy);
            simpleServerMessageBuffer.AppendInteger(100);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.Nutrition);
            simpleServerMessageBuffer.AppendInteger(150);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.Respect);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.OwnerId);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.Age);
            simpleServerMessageBuffer.AppendString(pet.PetData.OwnerName);
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendBool(pet.PetData.HaveSaddle);
            simpleServerMessageBuffer.AppendBool(
                Yupi.GetGame()
                    .GetRoomManager()
                    .GetRoom(pet.PetData.RoomId)
                    .GetRoomUserManager()
                    .GetRoomUserByVirtualId(pet.PetData.VirtualId)
                    .RidingHorse);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.AnyoneCanRide);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendString(string.Empty);
            simpleServerMessageBuffer.AppendBool(false);
            simpleServerMessageBuffer.AppendInteger(-1);
            simpleServerMessageBuffer.AppendInteger(-1);
            simpleServerMessageBuffer.AppendInteger(-1);
            simpleServerMessageBuffer.AppendBool(false);
            room.SendMessage(simpleServerMessageBuffer);
        }

         void AddSaddle()
        {
            Room room =
                Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(Session, true)))
                return;
            uint pId = Request.GetUInt32();
            RoomItem item = room.GetRoomItemHandler().GetItem(pId);
            if (item == null)
                return;
            uint petId = Request.GetUInt32();
            RoomUser pet = room.GetRoomUserManager().GetPet(petId);
            if (pet?.PetData == null || pet.PetData.OwnerId != Session.GetHabbo().Id)
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
					
                room.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);
                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SetRoomUserMessageComposer"));
                simpleServerMessageBuffer.AppendInteger(1);
                pet.Serialize(simpleServerMessageBuffer, false);
                room.SendMessage(simpleServerMessageBuffer);
                if (isForHorse)
                {
                    SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SerializePetMessageComposer"));
                    simpleServerMessage2.AppendInteger(pet.PetData.VirtualId);
                    simpleServerMessage2.AppendInteger(pet.PetData.PetId);
                    simpleServerMessage2.AppendInteger(pet.PetData.RaceId);
                    simpleServerMessage2.AppendInteger(pet.PetData.Race);
                    simpleServerMessage2.AppendString(pet.PetData.Color.ToLower());
                    if (pet.PetData.HaveSaddle)
                    {
                        simpleServerMessage2.AppendInteger(2);
                        simpleServerMessage2.AppendInteger(3);
                        simpleServerMessage2.AppendInteger(4);
                        simpleServerMessage2.AppendInteger(9);
                        simpleServerMessage2.AppendInteger(0);
                        simpleServerMessage2.AppendInteger(3);
                        simpleServerMessage2.AppendInteger(pet.PetData.PetHair);
                        simpleServerMessage2.AppendInteger(pet.PetData.HairDye);
                        simpleServerMessage2.AppendInteger(3);
                        simpleServerMessage2.AppendInteger(pet.PetData.PetHair);
                        simpleServerMessage2.AppendInteger(pet.PetData.HairDye);
                    }
                    else
                    {
                        simpleServerMessage2.AppendInteger(1);
                        simpleServerMessage2.AppendInteger(2);
                        simpleServerMessage2.AppendInteger(2);
                        simpleServerMessage2.AppendInteger(pet.PetData.PetHair);
                        simpleServerMessage2.AppendInteger(pet.PetData.HairDye);
                        simpleServerMessage2.AppendInteger(3);
                        simpleServerMessage2.AppendInteger(pet.PetData.PetHair);
                        simpleServerMessage2.AppendInteger(pet.PetData.HairDye);
                    }
                    simpleServerMessage2.AppendBool(pet.PetData.HaveSaddle);
                    simpleServerMessage2.AppendBool(pet.RidingHorse);
                    room.SendMessage(simpleServerMessage2);
                }
            }
        }

         void RemoveSaddle()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || (!room.RoomData.AllowPets && !room.CheckRights(Session, true)))
                return;

            uint petId = Request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet?.PetData == null || pet.PetData.OwnerId != Session.GetHabbo().Id)
                return;

            pet.PetData.HaveSaddle = false;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.RunFastQuery(
                    $"UPDATE pets_data SET have_saddle = '0' WHERE id = {pet.PetData.PetId}");

                queryReactor.RunFastQuery(
                    $"INSERT INTO items_rooms (user_id, item_name) VALUES ({Session.GetHabbo().Id}, 'horse_saddle1');");
            }

            Session.GetHabbo().GetInventoryComponent().UpdateItems(true);

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SetRoomUserMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(1);
            pet.Serialize(simpleServerMessageBuffer, false);
            room.SendMessage(simpleServerMessageBuffer);

            SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SerializePetMessageComposer"));
            simpleServerMessage2.AppendInteger(pet.PetData.VirtualId);
            simpleServerMessage2.AppendInteger(pet.PetData.PetId);
            simpleServerMessage2.AppendInteger(pet.PetData.RaceId);
            simpleServerMessage2.AppendInteger(pet.PetData.Race);
            simpleServerMessage2.AppendString(pet.PetData.Color.ToLower());
            simpleServerMessage2.AppendInteger(1);
            simpleServerMessage2.AppendInteger(2);
            simpleServerMessage2.AppendInteger(2);
            simpleServerMessage2.AppendInteger(pet.PetData.PetHair);
            simpleServerMessage2.AppendInteger(pet.PetData.HairDye);
            simpleServerMessage2.AppendInteger(3);
            simpleServerMessage2.AppendInteger(pet.PetData.PetHair);
            simpleServerMessage2.AppendInteger(pet.PetData.HairDye);
            simpleServerMessage2.AppendBool(pet.PetData.HaveSaddle);
            simpleServerMessage2.AppendBool(pet.RidingHorse);
            room.SendMessage(simpleServerMessage2);
        }

         void Unride()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            uint petId = Request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet?.PetData == null)
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

         void GiveHanditem()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            RoomUser roomUserByHabbo2 = room.GetRoomUserManager().GetRoomUserByHabbo(Request.GetUInt32());

            if (roomUserByHabbo2 == null)
                return;

            if ((!(
                Math.Abs(roomUserByHabbo.X - roomUserByHabbo2.X) < 3 &&
                Math.Abs(roomUserByHabbo.Y - roomUserByHabbo2.Y) < 3) &&
                 roomUserByHabbo.GetClient().GetHabbo().Rank <= 4u) || roomUserByHabbo.CarryItemId <= 0 ||
                roomUserByHabbo.CarryTimer <= 0)
                return;

            roomUserByHabbo2.CarryItem(roomUserByHabbo.CarryItemId);
            roomUserByHabbo.CarryItem(0);
            roomUserByHabbo2.DanceId = 0;
        }

         void RedeemVoucher()
        {
            string query = Request.GetString();
            string productName = string.Empty;
            string productDescription = string.Empty;
            bool isValid = false;

            DataRow row;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM items_vouchers WHERE voucher = @vo LIMIT 1");
                queryReactor.AddParameter("vo", query);
                row = queryReactor.GetRow();
            }

            if (row != null)
            {
                isValid = true;

                using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryreactor2.SetQuery("DELETE FROM items_vouchers WHERE voucher = @vou LIMIT 1");
                    queryreactor2.AddParameter("vou", query);
                    queryreactor2.RunQuery();
                }

                Session.GetHabbo().Credits += (uint) row["value"];
                Session.GetHabbo().UpdateCreditsBalance();
                Session.GetHabbo().NotifyNewPixels((uint) row["extra_duckets"]);
            }

            Session.GetHabbo().NotifyVoucher(isValid, productName, productDescription);
        }

         void RemoveHanditem()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUserByHabbo?.CarryItemId > 0 && roomUserByHabbo.CarryTimer > 0)
                roomUserByHabbo.CarryItem(0);
        }

         void Ride()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            uint petId = Request.GetUInt32();
            bool flag = Request.GetBool();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet?.PetData == null)
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

                    room.SendMessage(room.GetRoomItemHandler()
                        .UpdateUserOnRoller(pet, new Point(x, y), 0u, room.GetGameMap().SqAbsoluteHeight(x, y)));
                    room.GetRoomUserManager().UpdateUserStatus(pet, false);
                    room.SendMessage(room.GetRoomItemHandler()
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
                Session.SendNotif(Yupi.GetLanguage().GetVar("horse_error_2"));
                return;
            }

            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(Session.GetHabbo().Id);

            if (Session.GetHabbo().Id != pet.PetData.OwnerId)
            {
                if (clientByUserId != null)
                    Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(clientByUserId, "ACH_HorseRent", 1);
            }

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SerializePetMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(pet.PetData.VirtualId);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.PetId);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.RaceId);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.Race);
            simpleServerMessageBuffer.AppendString(pet.PetData.Color.ToLower());
            simpleServerMessageBuffer.AppendInteger(2);
            simpleServerMessageBuffer.AppendInteger(3);
            simpleServerMessageBuffer.AppendInteger(4);
            simpleServerMessageBuffer.AppendInteger(9);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(3);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.PetHair);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.HairDye);
            simpleServerMessageBuffer.AppendInteger(3);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.PetHair);
            simpleServerMessageBuffer.AppendInteger(pet.PetData.HairDye);
            simpleServerMessageBuffer.AppendBool(pet.PetData.HaveSaddle);
            simpleServerMessageBuffer.AppendBool(pet.RidingHorse);
            room.SendMessage(simpleServerMessageBuffer);
        }

         void SaveWired()
        {
            uint pId = Request.GetUInt32();

            RoomItem item =
                Yupi.GetGame()
                    .GetRoomManager()
                    .GetRoom(Session.GetHabbo().CurrentRoomId)
                    .GetRoomItemHandler()
                    .GetItem(pId);

            WiredSaver.SaveWired(Session, item, Request);
        }

         void SaveWiredCondition()
        {
            uint pId = Request.GetUInt32();

            RoomItem item =
                Yupi.GetGame()
                    .GetRoomManager()
                    .GetRoom(Session.GetHabbo().CurrentRoomId)
                    .GetRoomItemHandler()
                    .GetItem(pId);

            WiredSaver.SaveWired(Session, item, Request);
        }

         void GetTvPlaylist()
        {
            uint num = Request.GetUInt32();

            string video = Request.GetString();

            RoomItem item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(num);

            if (item.GetBaseItem().InteractionType != Interaction.YoutubeTv)
                return;

            if (!Session.GetHabbo().GetYoutubeManager().Videos.ContainsKey(video))
                return;

            item.ExtraData = video;
            item.UpdateState();

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer();
            simpleServerMessageBuffer.Init(PacketLibraryManager.OutgoingHandler("YouTubeLoadVideoMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(num);
            simpleServerMessageBuffer.AppendString(video);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(0);
            Response = simpleServerMessageBuffer;

            SendResponse();
        }

         void ChooseTvPlayerVideo()
        {
            // Not Coded? @TODO
        }

         void GetTvPlayer()
        {
            uint itemId = Request.GetUInt32();

            RoomItem item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(itemId);

            if (item == null)
                return;

            Dictionary<string, YoutubeVideo> videos = Session.GetHabbo().GetYoutubeManager().Videos;

            if (videos == null)
                return;

            Response.Init(PacketLibraryManager.OutgoingHandler("YouTubeLoadVideoMessageComposer"));
            Response.AppendInteger(itemId);
            Response.AppendString(item.ExtraData);
            Response.AppendInteger(0);
            Response.AppendInteger(0);
            Response.AppendInteger(0);

            SendResponse();

            Response.Clear();

            Response.Init(PacketLibraryManager.OutgoingHandler("YouTubeLoadPlaylistsMessageComposer"));
            Response.AppendInteger(itemId);
            Response.AppendInteger(videos.Count);

            foreach (YoutubeVideo video in videos.Values)
                video.Serialize(Response);

            Response.AppendString(item.ExtraData);

            SendResponse();
        }

         void PlaceBot()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session, true))
                return;

            uint num = Request.GetUInt32();

            RoomBot bot = Session.GetHabbo().GetInventoryComponent().GetBot(num);

            if (bot == null)
                return;

            int x = Request.GetInteger(); // coords
            int y = Request.GetInteger();

            if (!room.GetGameMap().CanWalk(x, y, false) || !room.GetGameMap().ValidTile(x, y))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("bot_error_1"));
                return;
            }

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("UPDATE bots_data SET room_id = @room, x = @x, y = @y WHERE id = @id");
				queryReactor.AddParameter ("room", room.RoomId);
				queryReactor.AddParameter ("x", x);
				queryReactor.AddParameter ("y", y);
				queryReactor.AddParameter ("id", num);
				queryReactor.RunQuery ();
			}

            bot.RoomId = room.RoomId;

            bot.X = x;
            bot.Y = y;

            room.GetRoomUserManager().DeployBot(bot, null);
            bot.WasPicked = false;

            Session.GetHabbo().GetInventoryComponent().MoveBotToRoom(num);
            Session.SendMessage(Session.GetHabbo().GetInventoryComponent().SerializeBotInventory());
        }

         void PickUpBot()
        {
            uint id = Request.GetUInt32();

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            RoomUser bot = room.GetRoomUserManager().GetBot(id);

            if (Session?.GetHabbo() == null || Session.GetHabbo().GetInventoryComponent() == null || bot == null ||
                !room.CheckRights(Session, true))
                return;

            Session.GetHabbo().GetInventoryComponent().AddBot(bot.BotData);

			using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor()) {
				queryreactor2.SetQuery ("UPDATE bots_data SET room_id = '0' WHERE id = @id");
				queryreactor2.AddParameter ("id", id);
				queryreactor2.RunQuery ();
			}
            room.GetRoomUserManager().RemoveBot(bot.VirtualId, false);
            bot.BotData.WasPicked = true;

            Session.SendMessage(Session.GetHabbo().GetInventoryComponent().SerializeBotInventory());
        }
			
         void CancelMysteryBox()
        {
            Request.GetUInt32();
            RoomUser roomUserByHabbo =
                Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            RoomItem item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(roomUserByHabbo.GateId);
            if (item == null)
                return;
            if (item.InteractingUser == Session.GetHabbo().Id)
                item.InteractingUser = 0u;
            else if (item.InteractingUser2 == Session.GetHabbo().Id)
                item.InteractingUser2 = 0u;
            roomUserByHabbo.GateId = 0u;
            string text = item.ExtraData.Split(Convert.ToChar(5))[0];
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
				queryReactor.AddParameter("extraData", text + Convert.ToChar(5).ToString() + "2");
				queryReactor.AddParameter("id", item.Id);
				queryReactor.RunQuery();
            }
			item.ExtraData = text + Convert.ToChar(5).ToString() + "2";
            item.UpdateNeeded = true;
            item.UpdateState(true, true);
        }

         void PlaceBuildersFurniture()
        {
            Request.GetInteger();
            uint itemId = Convert.ToUInt32(Request.GetInteger());
            string extradata = Request.GetString();
            int x = Request.GetInteger();
            int y = Request.GetInteger();
            int dir = Request.GetInteger();
            Room actualRoom = Session.GetHabbo().CurrentRoom;
            CatalogItem item = Yupi.GetGame().GetCatalogManager().GetItem(itemId);

            if (actualRoom == null || item == null)
                return;

            Session.GetHabbo().BuildersItemsUsed++;
            BuildersClubUpdateFurniCount();

            double z = actualRoom.GetGameMap().SqAbsoluteHeight(x, y);
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery(
                    "INSERT INTO items_rooms (user_id,room_id,item_name,x,y,z,rot,builders) VALUES (@userId,@roomId,@itemName,@x,@y,@z,@rot,'1')");
                adapter.AddParameter("userId", Session.GetHabbo().Id);
                adapter.AddParameter("roomId", actualRoom.RoomId);
                adapter.AddParameter("itemName", item.BaseName);
                adapter.AddParameter("x", x);
                adapter.AddParameter("y", y);
                adapter.AddParameter("z", z);
                adapter.AddParameter("rot", dir);

                uint insertId = (uint) adapter.InsertQuery();

                RoomItem newItem = new RoomItem(insertId, actualRoom.RoomId, item.BaseName, extradata, x, y, z, dir,
                    actualRoom, Session.GetHabbo().Id, 0, string.Empty, true);

                Session.GetHabbo().BuildersItemsUsed++;

                actualRoom.GetRoomItemHandler().FloorItems.TryAdd(newItem.Id, newItem);

                SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("AddFloorItemMessageComposer"));
                newItem.Serialize(messageBuffer);
                messageBuffer.AppendString(Session.GetHabbo().UserName);
                actualRoom.SendMessage(messageBuffer);
                actualRoom.GetGameMap().AddItemToMap(newItem);
            }
        }

         void PlaceBuildersWallItem()
        {
            /*var pageId = */
            Request.GetInteger();
            uint itemId = Request.GetUInt32();
            string extradata = Request.GetString();
            string wallcoords = Request.GetString();
            Room actualRoom = Session.GetHabbo().CurrentRoom;
            CatalogItem item = Yupi.GetGame().GetCatalogManager().GetItem(itemId);
            if (actualRoom == null || item == null) return;

            Session.GetHabbo().BuildersItemsUsed++;
            BuildersClubUpdateFurniCount();

            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery(
                    "INSERT INTO items_rooms (user_id,room_id,item_name,wall_pos,builders) VALUES (@userId,@roomId,@baseName,@wallpos,'1')");
                adapter.AddParameter("userId", Session.GetHabbo().Id);
                adapter.AddParameter("roomId", actualRoom.RoomId);
                adapter.AddParameter("baseName", item.BaseName);
                adapter.AddParameter("wallpos", wallcoords);

                uint insertId = (uint) adapter.InsertQuery();

                RoomItem newItem = new RoomItem(insertId, actualRoom.RoomId, item.BaseName, extradata,
                    new WallCoordinate(wallcoords), actualRoom, Session.GetHabbo().Id, 0, true);

                actualRoom.GetRoomItemHandler().WallItems.TryAdd(newItem.Id, newItem);

                SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("AddWallItemMessageComposer"));
                newItem.Serialize(messageBuffer);
                messageBuffer.AppendString(Session.GetHabbo().UserName);
                Session.SendMessage(messageBuffer);
                actualRoom.GetGameMap().AddItemToMap(newItem);
            }
        }

         void BuildersClubUpdateFurniCount()
        {
            if (Session.GetHabbo().BuildersItemsUsed < 0)
                Session.GetHabbo().BuildersItemsUsed = 0;

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("BuildersClubUpdateFurniCountMessageComposer"));

            messageBuffer.AppendInteger(Session.GetHabbo().BuildersItemsUsed);
            Session.SendMessage(messageBuffer);
        }

         void ConfirmLoveLock()
        {
            uint pId = Request.GetUInt32();
            bool confirmLoveLock = Request.GetBool();

            Room room = Session.GetHabbo().CurrentRoom;

            RoomItem item = room?.GetRoomItemHandler().GetItem(pId);
            if (item == null || item.GetBaseItem().InteractionType != Interaction.LoveShuffler)
                return;

            uint userIdOne = item.InteractingUser;
            uint userIdTwo = item.InteractingUser2;
            RoomUser userOne = room.GetRoomUserManager().GetRoomUserByHabbo(userIdOne);
            RoomUser userTwo = room.GetRoomUserManager().GetRoomUserByHabbo(userIdTwo);

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

            SimpleServerMessageBuffer loock = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("LoveLockDialogueSetLockedMessageComposer"));
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

            item.ExtraData = $"1{'\u0005'}{userOne.GetUserName()}{'\u0005'}{userTwo.GetUserName()}{'\u0005'}{userOne.GetClient().GetHabbo().Look}{'\u0005'}{userTwo.GetClient().GetHabbo().Look}{'\u0005'}{DateTime.Now.ToString("dd/MM/yyyy")}";

            userOne.LoveLockPartner = 0;
            userTwo.LoveLockPartner = 0;
            item.InteractingUser = 0;
            item.InteractingUser2 = 0;

            item.UpdateState(true, false);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
                queryReactor.AddParameter("extraData", item.ExtraData);
				queryReactor.AddParameter("id", item.Id);
                queryReactor.RunQuery();
            }

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateRoomItemMessageComposer"));
            item.Serialize(messageBuffer);
            room.SendMessage(messageBuffer);

            loock = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("LoveLockDialogueCloseMessageComposer"));
            loock.AppendInteger(item.Id);
            userOne.GetClient().SendMessage(loock);
            userTwo.GetClient().SendMessage(loock);
            userOne.CanWalk = true;
            userTwo.CanWalk = true;
        }

         void SaveFootballOutfit()
        {
            uint pId = Request.GetUInt32();
            string gender = Request.GetString();
            string look = Request.GetString();

            Room room = Session.GetHabbo().CurrentRoom;

            RoomItem item = room?.GetRoomItemHandler().GetItem(pId);

            if (item == null || item.GetBaseItem().InteractionType != Interaction.FootballGate)
                return;

            string[] figures = item.ExtraData.Split(',');
            string[] newFigures = new string[2];

            switch (gender.ToUpper())
            {
                case "M":
                {
                    newFigures[0] = look;

                    newFigures[1] = figures.Length > 1 ? figures[1] : "hd-99999-99999.ch-630-62.lg-695-62";

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

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
				queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
				queryReactor.AddParameter("extraData", item.ExtraData);
				queryReactor.AddParameter("id", item.Id);
                queryReactor.RunQuery();
            }

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateRoomItemMessageComposer"));
            item.Serialize(messageBuffer);
            Session.SendMessage(messageBuffer);
        }

         void SaveMannequin()
        {
            uint pId = Request.GetUInt32();
            string text = Request.GetString();
            RoomItem item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(pId);

            if (item == null)
                return;

            if (!item.ExtraData.Contains(Convert.ToChar(5)))
                return;

            if (!Session.GetHabbo().CurrentRoom.CheckRights(Session, true))
                return;
			// TODO Rename
            string[] array = item.ExtraData.Split(Convert.ToChar(5));

            array[2] = text;

			item.ExtraData = String.Join(Convert.ToChar(5).ToString(), array);
            item.Serialize(Response);
            item.UpdateState(true, true);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
                queryReactor.AddParameter("extraData", item.ExtraData);
				queryReactor.AddParameter("id", item.Id);
                queryReactor.RunQuery();
            }
        }

         void SaveMannequin2()
        {
            uint pId = Request.GetUInt32();
            RoomItem item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(pId);

            if (item == null)
                return;

            if (!item.ExtraData.Contains(Convert.ToChar(5)))
                return;

            if (!Session.GetHabbo().CurrentRoom.CheckRights(Session, true))
                return;

            string[] array = item.ExtraData.Split(Convert.ToChar(5));

            array[0] = Session.GetHabbo().Gender.ToLower();
            array[1] = string.Empty;

            string[] array2 = Session.GetHabbo().Look.Split('.');
			// TODO Use String.Join??? (need more knowlege about figure strings
            foreach (
                string text in
                    array2.Where(
                        text =>
                            !text.Contains("hr") && !text.Contains("hd") && !text.Contains("he") && !text.Contains("ea") &&
                            !text.Contains("ha")))
            {
                array[1] += text + ".";
            }

            array[1] = array[1].TrimEnd('.');
			item.ExtraData = String.Join(Convert.ToChar(5).ToString(), array);
            item.UpdateNeeded = true;
            item.UpdateState(true, true);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
                queryReactor.AddParameter("extraData", item.ExtraData);
				queryReactor.AddParameter("id", item.Id);
                queryReactor.RunQuery();
            }
        }

         void EjectFurni()
        {
            Request.GetInteger();

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(Session))
                return;

            uint pId = Request.GetUInt32();
            RoomItem item = room.GetRoomItemHandler().GetItem(pId);

            if (item == null)
                return;

            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(item.UserId);

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

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
				queryReactor.SetQuery("UPDATE items_rooms SET room_id='0' WHERE id=@id");
				queryReactor.AddParameter("id", item.Id);
				queryReactor.RunQuery();
			}
        }

         void UsePurchasableClothing()
        {
            uint furniId = Request.GetUInt32();

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            RoomItem item = room?.GetRoomItemHandler().GetItem(furniId);

            if (item?.GetBaseItem().InteractionType != Interaction.Clothing)
                return;
            ClothingItem clothes = Yupi.GetGame().GetClothingManager().GetClothesInFurni(item.GetBaseItem().Name);

            if (clothes == null)
                return;

            if (Session.GetHabbo().ClothesManagerManager.Clothing.Contains(clothes.ItemName))
                return;

            Session.GetHabbo().ClothesManagerManager.Add(clothes.ItemName);

            GetResponse().Init(PacketLibraryManager.OutgoingHandler("FigureSetIdsMessageComposer"));
            Session.GetHabbo().ClothesManagerManager.Serialize(GetResponse());

            SendResponse();

            room.GetRoomItemHandler().RemoveFurniture(Session, item.Id, false);
            Session.SendMessage(StaticMessage.FiguresetRedeemed);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
                queryReactor.SetQuery("DELETE FROM items_rooms WHERE id = @id");
				queryReactor.AddParameter("id", item.Id);
				queryReactor.RunQuery();
			}
		}

		// TODO Which request is being referenced here???
         void GetUserLook() => Request.GetString();
    }
}
