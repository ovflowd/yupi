using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Items.Wired
{
    public static class WiredSaver
    {
        public static void SaveWired(GameClient session, RoomItem item, ClientMessage request)
        {
            if (item == null || !item.IsWired)
                return;

            Room room = item.GetRoom();

            WiredHandler wiredHandler = room?.GetWiredHandler();

            if (wiredHandler == null)
                return;

            switch (item.GetBaseItem().InteractionType)
            {
                case Interaction.TriggerTimer:
                {
                    request.GetInteger();
                    IWiredItem wired = wiredHandler.GetWired(item);
                    int delay = request.GetInteger()*500;
                    wired.Delay = delay;
                    wiredHandler.ReloadWired(wired);
                    break;
                }
                case Interaction.TriggerRoomEnter:
                {
                    request.GetInteger();
                    string otherString = request.GetString();
                    IWiredItem wired = wiredHandler.GetWired(item);
                    wired.OtherString = otherString;
                    wiredHandler.ReloadWired(wired);
                    break;
                }

                case Interaction.TriggerLongRepeater:
                {
                    request.GetInteger();
                    int delay = request.GetInteger()*5000;
                    IWiredItem wired2 = wiredHandler.GetWired(item);
                    wired2.Delay = delay;
                    wiredHandler.ReloadWired(wired2);
                    break;
                }

                case Interaction.TriggerRepeater:
                {
                    request.GetInteger();
                    int delay = request.GetInteger()*500;
                    IWiredItem wired2 = wiredHandler.GetWired(item);
                    wired2.Delay = delay;
                    wiredHandler.ReloadWired(wired2);
                    break;
                }
                case Interaction.TriggerOnUserSay:
                {
                    request.GetInteger();
                    int num = request.GetInteger();
                    string otherString2 = request.GetString();
                    IWiredItem wired3 = wiredHandler.GetWired(item);
                    wired3.OtherString = otherString2;
                    wired3.OtherBool = num == 1;
                    wiredHandler.ReloadWired(wired3);
                    break;
                }
                case Interaction.TriggerStateChanged:
                {
                    request.GetInteger();
                    request.GetString();
                    List<RoomItem> furniItems = GetFurniItems(request, room);
                    int num2 = request.GetInteger();
                    IWiredItem wired4 = wiredHandler.GetWired(item);
                    wired4.Delay = num2*500;
                    wired4.Items = furniItems;
                    wiredHandler.ReloadWired(wired4);
                    break;
                }
                case Interaction.TriggerWalkOnFurni:
                case Interaction.ActionChase:
                case Interaction.ActionResetTimer:
                {
                    request.GetInteger();
                    request.GetString();
                    List<RoomItem> furniItems2 = GetFurniItems(request, room);
                    int num3 = request.GetInteger();
                    IWiredItem wired5 = wiredHandler.GetWired(item);
                    wired5.Delay = num3*500;
                    wired5.Items = furniItems2;
                    wiredHandler.ReloadWired(wired5);
                    break;
                }

                case Interaction.TriggerWalkOffFurni:
                {
                    request.GetInteger();
                    request.GetString();
                    List<RoomItem> furniItems3 = GetFurniItems(request, room);
                    int num4 = request.GetInteger();
                    IWiredItem wired6 = wiredHandler.GetWired(item);
                    wired6.Delay = num4*500;
                    wired6.Items = furniItems3;
                    wiredHandler.ReloadWired(wired6);
                    break;
                }
                case Interaction.ActionMoveRotate:
                case Interaction.ActionMoveToDir:
                {
                    request.GetInteger();
                    int dir = request.GetInteger();
                    int rot = request.GetInteger();
                    request.GetString();
                    List<RoomItem> furniItems4 = GetFurniItems(request, room);
                    int delay = request.GetInteger();
                    IWiredItem wired7 = wiredHandler.GetWired(item);
                    wired7.Items = furniItems4;
                    wired7.Delay = delay*500;
                    wired7.OtherString = $"{dir};{rot}";
                    wiredHandler.ReloadWired(wired7);
                    break;
                }
                case Interaction.ActionShowMessage:
                case Interaction.ActionKickUser:
                {
                    request.GetInteger();
                    string otherString3 = request.GetString();
                    IWiredItem wired8 = wiredHandler.GetWired(item);
                    wired8.OtherString = otherString3;
                    wiredHandler.ReloadWired(wired8);
                    break;
                }
                case Interaction.ActionTeleportTo:
                {
                    request.GetInteger();
                    request.GetString();
                    List<RoomItem> furniItems5 = GetFurniItems(request, room);
                    int num8 = request.GetInteger();
                    IWiredItem wired9 = wiredHandler.GetWired(item);
                    wired9.Items = furniItems5;
                    wired9.Delay = num8*500;
                    wiredHandler.ReloadWired(wired9);
                    break;
                }
                case Interaction.ActionToggleState:
                {
                    request.GetInteger();
                    request.GetString();
                    List<RoomItem> furniItems6 = GetFurniItems(request, room);
                    int num9 = request.GetInteger();
                    IWiredItem wired10 = wiredHandler.GetWired(item);
                    wired10.Items = furniItems6;
                    wired10.Delay = num9*500;
                    wiredHandler.ReloadWired(wired10);
                    break;
                }
                case Interaction.ActionGiveReward:
                {
                    if (!session.GetHabbo().HasFuse("fuse_use_superwired"))
                        return;

                    request.GetInteger();
                    int often = request.GetInteger();
                    bool unique = request.GetIntegerAsBool();
                    int limit = request.GetInteger();
                    request.GetInteger();
                    string extrainfo = request.GetString();
                    List<RoomItem> furniItems7 = GetFurniItems(request, room);
                    IWiredItem wired11 = wiredHandler.GetWired(item);
                    wired11.Items = furniItems7;
                    wired11.Delay = 0;
                    wired11.OtherBool = unique;
                    wired11.OtherString = extrainfo;
                    wired11.OtherExtraString = often.ToString();
                    wired11.OtherExtraString2 = limit.ToString();
                    wiredHandler.ReloadWired(wired11);
                    break;
                }
                case Interaction.ActionMuteUser:
                {
                    request.GetInteger();
                    int minutes = request.GetInteger()*500;
                    string message = request.GetString();
                    List<RoomItem> furniItems7 = GetFurniItems(request, room);
                    IWiredItem wired11 = wiredHandler.GetWired(item);
                    wired11.Items = furniItems7;
                    wired11.Delay = minutes;
                    wired11.OtherBool = false;
                    wired11.OtherString = message;
                    wiredHandler.ReloadWired(wired11);
                    break;
                }
                case Interaction.TriggerScoreAchieved:
                {
                    request.GetInteger();
                    int pointsRequired = request.GetInteger();

                    IWiredItem wired11 = wiredHandler.GetWired(item);
                    wired11.Delay = 0;
                    wired11.OtherString = pointsRequired.ToString();
                    wiredHandler.ReloadWired(wired11);
                    break;
                }

                case Interaction.ConditionItemsMatches:
                case Interaction.ConditionItemsDontMatch:
                case Interaction.ActionPosReset:
                {
                    request.GetInteger();
                    bool actualExtraData = request.GetIntegerAsBool();
                    bool actualRot = request.GetIntegerAsBool();
                    bool actualPosition = request.GetIntegerAsBool();

                    string booleans = $"{actualExtraData},{actualRot},{actualPosition}".ToLower();

                    request.GetString();
                    List<RoomItem> items = GetFurniItems(request, room);

                    int delay = request.GetInteger()*500;
                    IWiredItem wiry = wiredHandler.GetWired(item);

                    string dataToSave = string.Empty;
                    string extraStringForWi = string.Empty;

                    foreach (RoomItem aItem in items)
                    {
                        if (aItem.GetBaseItem().InteractionType == Interaction.Dice)
                        {
                            // Why have a RETURN here?
                            dataToSave += string.Format("0|0|0|0,0,0", aItem.Id, aItem.ExtraData, aItem.Rot, aItem.X,
                                aItem.Y, aItem.Z);
                            extraStringForWi +=
                                $"{aItem.Id},{(actualExtraData ? aItem.ExtraData : "N")},{(actualRot ? aItem.Rot.ToString() : "N")},{(actualPosition ? aItem.X.ToString() : "N")},{(actualPosition ? aItem.Y.ToString() : "N")}";

                            return;
                        }

                        dataToSave += $"{aItem.Id}|{aItem.ExtraData}|{aItem.Rot}|{aItem.X},{aItem.Y},{aItem.Z}";
                        extraStringForWi +=
                            $"{aItem.Id},{(actualExtraData ? aItem.ExtraData : "N")},{(actualRot ? aItem.Rot.ToString() : "N")},{(actualPosition ? aItem.X.ToString() : "N")},{(actualPosition ? aItem.Y.ToString() : "N")}";

                        if (aItem == items.Last())
                            continue;

                        dataToSave += "/";
                        extraStringForWi += ";";
                    }

                    wiry.Items = items;
                    wiry.Delay = delay;
                    wiry.OtherBool = true;
                    wiry.OtherString = booleans;
                    wiry.OtherExtraString = dataToSave;
                    wiry.OtherExtraString2 = extraStringForWi;
                    wiredHandler.ReloadWired(wiry);
                    break;
                }

                case Interaction.ConditionGroupMember:
                case Interaction.ConditionNotGroupMember:
                case Interaction.TriggerCollision:
                {
                    break;
                }

                case Interaction.ConditionHowManyUsersInRoom:
                case Interaction.ConditionNegativeHowManyUsers:
                {
                    request.GetInteger();
                    int minimum = request.GetInteger();
                    int maximum = request.GetInteger();

                    string ei = $"{minimum},{maximum}";
                    IWiredItem wired12 = wiredHandler.GetWired(item);
                    wired12.Items = new List<RoomItem>();
                    wired12.OtherString = ei;
                    wiredHandler.ReloadWired(wired12);
                    break;
                }

                case Interaction.ConditionUserNotWearingEffect:
                case Interaction.ConditionUserWearingEffect:
                {
                    request.GetInteger();
                    int effect = request.GetInteger();
                    IWiredItem wired12 = wiredHandler.GetWired(item);
                    wired12.Items = new List<RoomItem>();
                    wired12.OtherString = effect.ToString();
                    wiredHandler.ReloadWired(wired12);
                    break;
                }

                case Interaction.ConditionUserWearingBadge:
                case Interaction.ConditionUserNotWearingBadge:
                case Interaction.ConditionUserHasFurni:
                {
                    request.GetInteger();
                    string badge = request.GetString();
                    IWiredItem wired12 = wiredHandler.GetWired(item);
                    wired12.Items = new List<RoomItem>();
                    wired12.OtherString = badge;
                    wiredHandler.ReloadWired(wired12);
                    break;
                }

                case Interaction.ConditionDateRangeActive:
                {
                    request.GetInteger();

                    int startDate = request.GetInteger();
                    int endDate = request.GetInteger();

                    IWiredItem wired12 = wiredHandler.GetWired(item);
                    wired12.Items = new List<RoomItem>();
                    wired12.OtherString = $"{startDate},{endDate}";

                    if (startDate == 0)
                    {
                        wired12.OtherString = string.Empty;
                        session.SendNotif(Yupi.GetLanguage().GetVar("user_wired_con_date_range"));
                    }

                    wiredHandler.ReloadWired(wired12);
                    break;
                }

                case Interaction.ConditionFurnisHaveUsers:
                case Interaction.ConditionTriggerOnFurni:
                case Interaction.ConditionFurniTypeMatches:
                case Interaction.ConditionFurnisHaveNotUsers:
                case Interaction.ConditionTriggererNotOnFurni:
                case Interaction.ConditionFurniTypeDontMatch:
                {
                    request.GetInteger();
                    request.GetString();
                    List<RoomItem> furniItems = GetFurniItems(request, room);
                    IWiredItem wired12 = wiredHandler.GetWired(item);

                    wired12.Items = furniItems;
                    wiredHandler.ReloadWired(wired12);
                    break;
                }
                case Interaction.ConditionFurniHasFurni:
                case Interaction.ConditionFurniHasNotFurni:
                {
                    request.GetInteger();
                    bool allItems = request.GetIntegerAsBool();
                    request.GetString();

                    List<RoomItem> furniItems = GetFurniItems(request, room);
                    IWiredItem wired = wiredHandler.GetWired(item);

                    wired.OtherBool = allItems;
                    wired.Items = furniItems;
                    wiredHandler.ReloadWired(wired);
                    break;
                }
                case Interaction.ActionGiveScore:
                {
                    request.GetInteger();
                    int scoreToGive = request.GetInteger();
                    int maxTimesPerGame = request.GetInteger();

                    string newExtraInfo = $"{scoreToGive},{maxTimesPerGame}";

                    List<RoomItem> furniItems8 = GetFurniItems(request, room);
                    IWiredItem wired12 = wiredHandler.GetWired(item);
                    wired12.Items = furniItems8;
                    wired12.OtherString = newExtraInfo;
                    wiredHandler.ReloadWired(wired12);
                    break;
                }
                case Interaction.ActionJoinTeam:
                {
                    request.GetInteger();
                    int team = request.GetInteger();
                    IWiredItem wired = wiredHandler.GetWired(item);
                    wired.Delay = team*500;
                    wiredHandler.ReloadWired(wired);
                    break;
                }
                case Interaction.ActionBotTalk:
                {
                    request.GetInteger();
                    bool type = request.GetIntegerAsBool();
                    string[] data = request.GetString().Split((char) 9);
                    IWiredItem wired = wiredHandler.GetWired(item);
                    wired.OtherBool = type;
                    wired.OtherString = data[0];
                    wired.OtherExtraString = data[1];
                    wiredHandler.ReloadWired(wired);
                    break;
                }
                case Interaction.ActionBotClothes:
                {
                    request.GetInteger();
                    string[] data = request.GetString().Split((char) 9);
                    IWiredItem wired = wiredHandler.GetWired(item);
                    wired.OtherString = data[0];
                    wired.OtherExtraString = data[1];
                    wiredHandler.ReloadWired(wired);
                    break;
                }
                case Interaction.ActionBotTeleport:
                {
                    request.GetInteger();
                    string botName = request.GetString();
                    List<RoomItem> furniItems = GetFurniItems(request, room);
                    IWiredItem wired = wiredHandler.GetWired(item);
                    wired.Items = furniItems;
                    wired.OtherString = botName;
                    wiredHandler.ReloadWired(wired);
                    break;
                }
                case Interaction.ActionBotGiveHanditem:
                {
                    request.GetInteger();
                    int handitem = request.GetInteger();
                    string botName = request.GetString();
                    IWiredItem wired = wiredHandler.GetWired(item);
                    wired.OtherString = botName;
                    wired.Delay = handitem*500;
                    wiredHandler.ReloadWired(wired);
                    break;
                }
                case Interaction.ActionBotMove:
                {
                    request.GetInteger();
                    string botName = request.GetString();
                    List<RoomItem> furniItems = GetFurniItems(request, room);
                    IWiredItem wired = wiredHandler.GetWired(item);
                    wired.Items = furniItems;
                    wired.OtherString = botName;
                    wiredHandler.ReloadWired(wired);
                    break;
                }
                case Interaction.ActionCallStacks:
                {
                    request.GetInteger();
                    request.GetString();
                    List<RoomItem> furniItems = GetFurniItems(request, room);
                    IWiredItem wired = wiredHandler.GetWired(item);
                    int num = request.GetInteger();
                    wired.Items = furniItems;
                    wired.Delay = num*500;
                    wiredHandler.ReloadWired(wired);
                    break;
                }
                case Interaction.ActionBotTalkToAvatar:
                {
                    request.GetInteger();
                    bool type = request.GetIntegerAsBool();
                    string[] data = request.GetString().Split((char) 9);
                    IWiredItem wired = wiredHandler.GetWired(item);
                    wired.OtherBool = type;
                    wired.OtherString = data[0];
                    wired.OtherExtraString = data[1];
                    wiredHandler.ReloadWired(wired);
                    break;
                }
                case Interaction.ConditionTimeMoreThan:
                case Interaction.ConditionTimeLessThan:
                {
                    request.GetInteger();
                    int time = request.GetInteger();
                    IWiredItem wired12 = wiredHandler.GetWired(item);
                    Console.WriteLine(time);
                    wired12.Delay = time*500;
                    wiredHandler.ReloadWired(wired12);
                    break;
                }
                case Interaction.ConditionUserHasHanditem:
                {
                    request.GetInteger();
                    int handitem = request.GetInteger();
                    IWiredItem wired = wiredHandler.GetWired(item);
                    wired.Delay = handitem*500;
                    wiredHandler.ReloadWired(wired);
                    break;
                }
            }

            session.SendMessage(new ServerMessage(LibraryParser.OutgoingRequest("SaveWiredMessageComposer")));
        }

        private static List<RoomItem> GetFurniItems(ClientMessage request, Room room)
        {
            List<RoomItem> list = new List<RoomItem>();
            int itemCount = request.GetInteger();

            for (int i = 0; i < itemCount; i++)
            {
                RoomItem item = room.GetRoomItemHandler().GetItem(request.GetUInteger());

                if (item != null)
                    list.Add(item);
            }

            return list;
        }
    }
}