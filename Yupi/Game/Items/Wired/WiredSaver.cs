using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
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

            var room = item.GetRoom();

            var wiredHandler = room?.GetWiredHandler();

            if (wiredHandler == null)
                return;

            switch (item.GetBaseItem().InteractionType)
            {
                case Interaction.TriggerTimer:
                    {
                        request.GetInteger();
                        var wired = wiredHandler.GetWired(item);
                        var delay = request.GetInteger() * 500;
                        wired.Delay = delay;
                        wiredHandler.ReloadWired(wired);
                        break;
                    }
                case Interaction.TriggerRoomEnter:
                    {
                        request.GetInteger();
                        var otherString = request.GetString();
                        var wired = wiredHandler.GetWired(item);
                        wired.OtherString = otherString;
                        wiredHandler.ReloadWired(wired);
                        break;
                    }

                case Interaction.TriggerLongRepeater:
                    {
                        request.GetInteger();
                        var delay = request.GetInteger() * 5000;
                        var wired2 = wiredHandler.GetWired(item);
                        wired2.Delay = delay;
                        wiredHandler.ReloadWired(wired2);
                        break;
                    }

                case Interaction.TriggerRepeater:
                    {
                        request.GetInteger();
                        var delay = request.GetInteger() * 500;
                        var wired2 = wiredHandler.GetWired(item);
                        wired2.Delay = delay;
                        wiredHandler.ReloadWired(wired2);
                        break;
                    }
                case Interaction.TriggerOnUserSay:
                    {
                        request.GetInteger();
                        var num = request.GetInteger();
                        var otherString2 = request.GetString();
                        var wired3 = wiredHandler.GetWired(item);
                        wired3.OtherString = otherString2;
                        wired3.OtherBool = (num == 1);
                        wiredHandler.ReloadWired(wired3);
                        break;
                    }
                case Interaction.TriggerStateChanged:
                    {
                        request.GetInteger();
                        request.GetString();
                        var furniItems = GetFurniItems(request, room);
                        var num2 = request.GetInteger();
                        var wired4 = wiredHandler.GetWired(item);
                        wired4.Delay = num2 * 500;
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
                        var furniItems2 = GetFurniItems(request, room);
                        var num3 = request.GetInteger();
                        var wired5 = wiredHandler.GetWired(item);
                        wired5.Delay = num3 * 500;
                        wired5.Items = furniItems2;
                        wiredHandler.ReloadWired(wired5);
                        break;
                    }

                case Interaction.TriggerWalkOffFurni:
                    {
                        request.GetInteger();
                        request.GetString();
                        var furniItems3 = GetFurniItems(request, room);
                        var num4 = request.GetInteger();
                        var wired6 = wiredHandler.GetWired(item);
                        wired6.Delay = num4 * 500;
                        wired6.Items = furniItems3;
                        wiredHandler.ReloadWired(wired6);
                        break;
                    }
                case Interaction.ActionMoveRotate:
                case Interaction.ActionMoveToDir:
                    {
                        request.GetInteger();
                        var dir = request.GetInteger();
                        var rot = request.GetInteger();
                        request.GetString();
                        var furniItems4 = GetFurniItems(request, room);
                        var delay = request.GetInteger();
                        var wired7 = wiredHandler.GetWired(item);
                        wired7.Items = furniItems4;
                        wired7.Delay = delay * 500;
                        wired7.OtherString = $"{dir};{rot}";
                        wiredHandler.ReloadWired(wired7);
                        break;
                    }
                case Interaction.ActionShowMessage:
                case Interaction.ActionKickUser:
                    {
                        request.GetInteger();
                        var otherString3 = request.GetString();
                        var wired8 = wiredHandler.GetWired(item);
                        wired8.OtherString = otherString3;
                        wiredHandler.ReloadWired(wired8);
                        break;
                    }
                case Interaction.ActionTeleportTo:
                    {
                        request.GetInteger();
                        request.GetString();
                        var furniItems5 = GetFurniItems(request, room);
                        var num8 = request.GetInteger();
                        var wired9 = wiredHandler.GetWired(item);
                        wired9.Items = furniItems5;
                        wired9.Delay = num8 * 500;
                        wiredHandler.ReloadWired(wired9);
                        break;
                    }
                case Interaction.ActionToggleState:
                    {
                        request.GetInteger();
                        request.GetString();
                        var furniItems6 = GetFurniItems(request, room);
                        var num9 = request.GetInteger();
                        var wired10 = wiredHandler.GetWired(item);
                        wired10.Items = furniItems6;
                        wired10.Delay = num9 * 500;
                        wiredHandler.ReloadWired(wired10);
                        break;
                    }
                case Interaction.ActionGiveReward:
                    {
                        if (!session.GetHabbo().HasFuse("fuse_use_superwired"))
                            return;

                        request.GetInteger();
                        var often = request.GetInteger();
                        var unique = request.GetIntegerAsBool();
                        var limit = request.GetInteger();
                        request.GetInteger();
                        var extrainfo = request.GetString();
                        var furniItems7 = GetFurniItems(request, room);
                        var wired11 = wiredHandler.GetWired(item);
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
                        var minutes = request.GetInteger() * 500;
                        var message = request.GetString();
                        var furniItems7 = GetFurniItems(request, room);
                        var wired11 = wiredHandler.GetWired(item);
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
                        var pointsRequired = request.GetInteger();

                        var wired11 = wiredHandler.GetWired(item);
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
                        var actualExtraData = request.GetIntegerAsBool();
                        var actualRot = request.GetIntegerAsBool();
                        var actualPosition = request.GetIntegerAsBool();

                        var booleans = $"{actualExtraData},{actualRot},{actualPosition}".ToLower();

                        request.GetString();
                        var items = GetFurniItems(request, room);

                        var delay = request.GetInteger() * 500;
                        var wiry = wiredHandler.GetWired(item);

                        var dataToSave = string.Empty;
                        var extraStringForWi = string.Empty;

                        foreach (var aItem in items)
                        {
                            if (aItem.GetBaseItem().InteractionType == Interaction.Dice)
                            {
                                // Why have a RETURN here?
                                dataToSave += string.Format("0|0|0|0,0,0", aItem.Id, aItem.ExtraData, aItem.Rot, aItem.X, aItem.Y, aItem.Z);
                                extraStringForWi += $"{aItem.Id},{((actualExtraData) ? aItem.ExtraData : "N")},{((actualRot) ? aItem.Rot.ToString() : "N")},{((actualPosition) ? aItem.X.ToString() : "N")},{((actualPosition) ? aItem.Y.ToString() : "N")}";

                                return;
                            }

                            dataToSave += $"{aItem.Id}|{aItem.ExtraData}|{aItem.Rot}|{aItem.X},{aItem.Y},{aItem.Z}";
                            extraStringForWi += $"{aItem.Id},{((actualExtraData) ? aItem.ExtraData : "N")},{((actualRot) ? aItem.Rot.ToString() : "N")},{((actualPosition) ? aItem.X.ToString() : "N")},{((actualPosition) ? aItem.Y.ToString() : "N")}";

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
                        var minimum = request.GetInteger();
                        var maximum = request.GetInteger();

                        var ei = $"{minimum},{maximum}";
                        var wired12 = wiredHandler.GetWired(item);
                        wired12.Items = new List<RoomItem>();
                        wired12.OtherString = ei;
                        wiredHandler.ReloadWired(wired12);
                        break;
                    }

                case Interaction.ConditionUserNotWearingEffect:
                case Interaction.ConditionUserWearingEffect:
                    {
                        request.GetInteger();
                        var effect = request.GetInteger();
                        var wired12 = wiredHandler.GetWired(item);
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
                        var badge = request.GetString();
                        var wired12 = wiredHandler.GetWired(item);
                        wired12.Items = new List<RoomItem>();
                        wired12.OtherString = badge;
                        wiredHandler.ReloadWired(wired12);
                        break;
                    }

                case Interaction.ConditionDateRangeActive:
                    {
                        request.GetInteger();

                        var startDate = request.GetInteger();
                        var endDate = request.GetInteger();

                        var wired12 = wiredHandler.GetWired(item);
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
                        var furniItems = GetFurniItems(request, room);
                        var wired12 = wiredHandler.GetWired(item);

                        wired12.Items = furniItems;
                        wiredHandler.ReloadWired(wired12);
                        break;
                    }
                case Interaction.ConditionFurniHasFurni:
                case Interaction.ConditionFurniHasNotFurni:
                    {
                        request.GetInteger();
                        var allItems = request.GetIntegerAsBool();
                        request.GetString();

                        var furniItems = GetFurniItems(request, room);
                        var wired = wiredHandler.GetWired(item);

                        wired.OtherBool = allItems;
                        wired.Items = furniItems;
                        wiredHandler.ReloadWired(wired);
                        break;
                    }
                case Interaction.ActionGiveScore:
                    {
                        request.GetInteger();
                        var scoreToGive = request.GetInteger();
                        var maxTimesPerGame = request.GetInteger();

                        var newExtraInfo = $"{scoreToGive},{maxTimesPerGame}";

                        var furniItems8 = GetFurniItems(request, room);
                        var wired12 = wiredHandler.GetWired(item);
                        wired12.Items = furniItems8;
                        wired12.OtherString = newExtraInfo;
                        wiredHandler.ReloadWired(wired12);
                        break;
                    }
                case Interaction.ActionJoinTeam:
                    {
                        request.GetInteger();
                        var team = request.GetInteger();
                        var wired = wiredHandler.GetWired(item);
                        wired.Delay = team * 500;
                        wiredHandler.ReloadWired(wired);
                        break;
                    }
                case Interaction.ActionBotTalk:
                    {
                        request.GetInteger();
                        var type = request.GetIntegerAsBool();
                        var data = request.GetString().Split((char)9);
                        var wired = wiredHandler.GetWired(item);
                        wired.OtherBool = type;
                        wired.OtherString = data[0];
                        wired.OtherExtraString = data[1];
                        wiredHandler.ReloadWired(wired);
                        break;
                    }
                case Interaction.ActionBotClothes:
                    {
                        request.GetInteger();
                        var data = request.GetString().Split((char)9);
                        var wired = wiredHandler.GetWired(item);
                        wired.OtherString = data[0];
                        wired.OtherExtraString = data[1];
                        wiredHandler.ReloadWired(wired);
                        break;
                    }
                case Interaction.ActionBotTeleport:
                    {
                        request.GetInteger();
                        var botName = request.GetString();
                        var furniItems = GetFurniItems(request, room);
                        var wired = wiredHandler.GetWired(item);
                        wired.Items = furniItems;
                        wired.OtherString = botName;
                        wiredHandler.ReloadWired(wired);
                        break;
                    }
                case Interaction.ActionBotGiveHanditem:
                    {
                        request.GetInteger();
                        var handitem = request.GetInteger();
                        var botName = request.GetString();
                        var wired = wiredHandler.GetWired(item);
                        wired.OtherString = botName;
                        wired.Delay = handitem * 500;
                        wiredHandler.ReloadWired(wired);
                        break;
                    }
                case Interaction.ActionBotMove:
                    {
                        request.GetInteger();
                        var botName = request.GetString();
                        var furniItems = GetFurniItems(request, room);
                        var wired = wiredHandler.GetWired(item);
                        wired.Items = furniItems;
                        wired.OtherString = botName;
                        wiredHandler.ReloadWired(wired);
                        break;
                    }
                case Interaction.ActionCallStacks:
                    {
                        request.GetInteger();
                        request.GetString();
                        var furniItems = GetFurniItems(request, room);
                        var wired = wiredHandler.GetWired(item);
                        var num = request.GetInteger();
                        wired.Items = furniItems;
                        wired.Delay = num * 500;
                        wiredHandler.ReloadWired(wired);
                        break;
                    }
                case Interaction.ActionBotTalkToAvatar:
                    {
                        request.GetInteger();
                        var type = request.GetIntegerAsBool();
                        var data = request.GetString().Split((char)9);
                        var wired = wiredHandler.GetWired(item);
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
                        var time = request.GetInteger();
                        var wired12 = wiredHandler.GetWired(item);
                        Console.WriteLine(time);
                        wired12.Delay = time * 500;
                        wiredHandler.ReloadWired(wired12);
                        break;
                    }
                case Interaction.ConditionUserHasHanditem:
                    {
                        request.GetInteger();
                        var handitem = request.GetInteger();
                        var wired = wiredHandler.GetWired(item);
                        wired.Delay = handitem * 500;
                        wiredHandler.ReloadWired(wired);
                        break;
                    }
            }

            session.SendMessage(new ServerMessage(LibraryParser.OutgoingRequest("SaveWiredMessageComposer")));
        }

        private static List<RoomItem> GetFurniItems(ClientMessage request, Room room)
        {
            var list = new List<RoomItem>();
            var itemCount = request.GetInteger();

            for (var i = 0; i < itemCount; i++)
            {
                var item = room.GetRoomItemHandler().GetItem(request.GetUInteger());

                if (item != null)
                    list.Add(item);
            }

            return list;
        }
    }
}