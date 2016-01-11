using System.Collections.Generic;
using System.Linq;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorWired : FurniInteractorModel
    {
        public override void OnRemove(GameClient session, RoomItem item)
        {
            Room room = item.GetRoom();
            room.GetWiredHandler().RemoveWired(item);
        }

        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (session == null || item?.GetRoom() == null || !hasRights)
                return;

            IWiredItem wired = item.GetRoom().GetWiredHandler().GetWired(item);

            if (wired == null)
                return;

            string extraInfo = wired.OtherString;
            bool flag = wired.OtherBool;
            int delay = wired.Delay/500;
            List<RoomItem> list = wired.Items.Where(roomItem => roomItem != null).ToList();
            string extraString = wired.OtherExtraString;
            string extraString2 = wired.OtherExtraString2;

            switch (item.GetBaseItem().InteractionType)
            {
                case Interaction.TriggerTimer:
                {
                    ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage.AppendBool(false);
                    serverMessage.AppendInteger(5);
                    serverMessage.AppendInteger(list.Count);
                    foreach (RoomItem current in list) serverMessage.AppendInteger(current.Id);
                    serverMessage.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage.AppendInteger(item.Id);
                    serverMessage.AppendString(extraInfo);
                    serverMessage.AppendInteger(1);
                    serverMessage.AppendInteger(delay);
                    serverMessage.AppendInteger(1);
                    serverMessage.AppendInteger(3);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(0);
                    session.SendMessage(serverMessage);
                    return;
                }
                case Interaction.TriggerRoomEnter:
                {
                    ServerMessage serverMessage2 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage2.AppendBool(false);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(list.Count);
                    foreach (RoomItem current2 in list) serverMessage2.AppendInteger(current2.Id);
                    serverMessage2.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage2.AppendInteger(item.Id);
                    serverMessage2.AppendString(extraInfo);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(7);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(0);
                    session.SendMessage(serverMessage2);
                    return;
                }
                case Interaction.TriggerGameEnd:
                {
                    ServerMessage serverMessage3 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage3.AppendBool(false);
                    serverMessage3.AppendInteger(0);
                    serverMessage3.AppendInteger(list.Count);
                    foreach (RoomItem current3 in list) serverMessage3.AppendInteger(current3.Id);
                    serverMessage3.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage3.AppendInteger(item.Id);
                    serverMessage3.AppendString(extraInfo);
                    serverMessage3.AppendInteger(0);
                    serverMessage3.AppendInteger(0);
                    serverMessage3.AppendInteger(8);
                    serverMessage3.AppendInteger(0);
                    serverMessage3.AppendInteger(0);
                    serverMessage3.AppendInteger(0);
                    session.SendMessage(serverMessage3);
                    return;
                }
                case Interaction.TriggerGameStart:
                {
                    ServerMessage serverMessage4 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage4.AppendBool(false);
                    serverMessage4.AppendInteger(0);
                    serverMessage4.AppendInteger(list.Count);
                    foreach (RoomItem current4 in list) serverMessage4.AppendInteger(current4.Id);
                    serverMessage4.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage4.AppendInteger(item.Id);
                    serverMessage4.AppendString(extraInfo);
                    serverMessage4.AppendInteger(0);
                    serverMessage4.AppendInteger(0);
                    serverMessage4.AppendInteger(8);
                    serverMessage4.AppendInteger(0);
                    serverMessage4.AppendInteger(0);
                    serverMessage4.AppendInteger(0);
                    session.SendMessage(serverMessage4);
                    return;
                }
                case Interaction.TriggerLongRepeater:
                {
                    ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage.AppendBool(false);
                    serverMessage.AppendInteger(5);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage.AppendInteger(item.Id);
                    serverMessage.AppendString("");
                    serverMessage.AppendInteger(1);
                    serverMessage.AppendInteger(delay/10); //fix
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(12);
                    serverMessage.AppendInteger(0);
                    session.SendMessage(serverMessage);
                    return;
                }

                case Interaction.TriggerRepeater:
                {
                    ServerMessage serverMessage5 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage5.AppendBool(false);
                    serverMessage5.AppendInteger(5);
                    serverMessage5.AppendInteger(list.Count);
                    foreach (RoomItem current5 in list) serverMessage5.AppendInteger(current5.Id);
                    serverMessage5.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage5.AppendInteger(item.Id);
                    serverMessage5.AppendString(extraInfo);
                    serverMessage5.AppendInteger(1);
                    serverMessage5.AppendInteger(delay);
                    serverMessage5.AppendInteger(0);
                    serverMessage5.AppendInteger(6);
                    serverMessage5.AppendInteger(0);
                    serverMessage5.AppendInteger(0);
                    session.SendMessage(serverMessage5);
                    return;
                }
                case Interaction.TriggerOnUserSay:
                {
                    ServerMessage serverMessage6 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage6.AppendBool(false);
                    serverMessage6.AppendInteger(0);
                    serverMessage6.AppendInteger(list.Count);
                    foreach (RoomItem current6 in list) serverMessage6.AppendInteger(current6.Id);
                    serverMessage6.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage6.AppendInteger(item.Id);
                    serverMessage6.AppendString(extraInfo);
                    serverMessage6.AppendInteger(0);
                    serverMessage6.AppendInteger(0);
                    serverMessage6.AppendInteger(0);
                    serverMessage6.AppendInteger(0);
                    serverMessage6.AppendInteger(0);
                    serverMessage6.AppendInteger(0);
                    session.SendMessage(serverMessage6);
                    return;
                }
                case Interaction.TriggerScoreAchieved:
                {
                    ServerMessage serverMessage7 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage7.AppendBool(false);
                    serverMessage7.AppendInteger(5);
                    serverMessage7.AppendInteger(0);
                    serverMessage7.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage7.AppendInteger(item.Id);
                    serverMessage7.AppendString("");
                    serverMessage7.AppendInteger(1);
                    serverMessage7.AppendInteger(string.IsNullOrWhiteSpace(extraInfo) ? 100 : int.Parse(extraInfo));
                    serverMessage7.AppendInteger(0);
                    serverMessage7.AppendInteger(10);
                    serverMessage7.AppendInteger(0);
                    serverMessage7.AppendInteger(0);
                    session.SendMessage(serverMessage7);
                    return;
                }
                case Interaction.TriggerStateChanged:
                {
                    ServerMessage serverMessage8 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage8.AppendBool(false);
                    serverMessage8.AppendInteger(5);
                    serverMessage8.AppendInteger(list.Count);
                    foreach (RoomItem current8 in list) serverMessage8.AppendInteger(current8.Id);
                    serverMessage8.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage8.AppendInteger(item.Id);
                    serverMessage8.AppendString(extraInfo);
                    serverMessage8.AppendInteger(0);
                    serverMessage8.AppendInteger(0);
                    serverMessage8.AppendInteger(1);
                    serverMessage8.AppendInteger(delay);
                    serverMessage8.AppendInteger(0);
                    serverMessage8.AppendInteger(0);
                    session.SendMessage(serverMessage8);
                    return;
                }
                case Interaction.TriggerWalkOnFurni:
                {
                    ServerMessage serverMessage9 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage9.AppendBool(false);
                    serverMessage9.AppendInteger(5);
                    serverMessage9.AppendInteger(list.Count);
                    foreach (RoomItem current9 in list) serverMessage9.AppendInteger(current9.Id);
                    serverMessage9.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage9.AppendInteger(item.Id);
                    serverMessage9.AppendString(extraInfo);
                    serverMessage9.AppendInteger(0);
                    serverMessage9.AppendInteger(0);
                    serverMessage9.AppendInteger(1);
                    serverMessage9.AppendInteger(0);
                    serverMessage9.AppendInteger(0);
                    serverMessage9.AppendInteger(0);
                    session.SendMessage(serverMessage9);
                    return;
                }
                case Interaction.ActionMuteUser:
                {
                    ServerMessage serverMessage18 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage18.AppendBool(false);
                    serverMessage18.AppendInteger(5);
                    serverMessage18.AppendInteger(0);
                    serverMessage18.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage18.AppendInteger(item.Id);
                    serverMessage18.AppendString(extraInfo);
                    serverMessage18.AppendInteger(1);
                    serverMessage18.AppendInteger(delay);
                    serverMessage18.AppendInteger(0);
                    serverMessage18.AppendInteger(20);
                    serverMessage18.AppendInteger(0);
                    serverMessage18.AppendInteger(0);
                    session.SendMessage(serverMessage18);
                    return;
                }
                case Interaction.TriggerWalkOffFurni:
                {
                    ServerMessage serverMessage10 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage10.AppendBool(false);
                    serverMessage10.AppendInteger(5);
                    serverMessage10.AppendInteger(list.Count);
                    foreach (RoomItem current10 in list) serverMessage10.AppendInteger(current10.Id);
                    serverMessage10.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage10.AppendInteger(item.Id);
                    serverMessage10.AppendString(extraInfo);
                    serverMessage10.AppendInteger(0);
                    serverMessage10.AppendInteger(0);
                    serverMessage10.AppendInteger(1);
                    serverMessage10.AppendInteger(0);
                    serverMessage10.AppendInteger(0);
                    serverMessage10.AppendInteger(0);
                    serverMessage10.AppendInteger(0);
                    session.SendMessage(serverMessage10);
                    return;
                }

                case Interaction.TriggerCollision:
                {
                    ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage.AppendBool(false);
                    serverMessage.AppendInteger(5);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage.AppendInteger(item.Id);
                    serverMessage.AppendString(string.Empty);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(11);
                    serverMessage.AppendInteger(0);
                    session.SendMessage(serverMessage);
                    return;
                }

                case Interaction.ActionGiveScore:
                {
                    // Por hacer.
                    ServerMessage serverMessage11 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage11.AppendBool(false);
                    serverMessage11.AppendInteger(5);
                    serverMessage11.AppendInteger(0);
                    serverMessage11.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage11.AppendInteger(item.Id);
                    serverMessage11.AppendString("");
                    serverMessage11.AppendInteger(2);
                    if (string.IsNullOrWhiteSpace(extraInfo))
                    {
                        serverMessage11.AppendInteger(10); // Puntos a dar
                        serverMessage11.AppendInteger(1); // Numero de veces por equipo
                    }
                    else
                    {
                        string[] integers = extraInfo.Split(',');
                        serverMessage11.AppendInteger(int.Parse(integers[0])); // Puntos a dar
                        serverMessage11.AppendInteger(int.Parse(integers[1])); // Numero de veces por equipo
                    }
                    serverMessage11.AppendInteger(0);
                    serverMessage11.AppendInteger(6);
                    serverMessage11.AppendInteger(0);
                    serverMessage11.AppendInteger(0);
                    serverMessage11.AppendInteger(0);
                    session.SendMessage(serverMessage11);
                    return;
                }

                case Interaction.ConditionGroupMember:
                case Interaction.ConditionNotGroupMember:
                {
                    ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("WiredConditionMessageComposer"));
                    message.AppendBool(false);
                    message.AppendInteger(5);
                    message.AppendInteger(0);
                    message.AppendInteger(item.GetBaseItem().SpriteId);
                    message.AppendInteger(item.Id);
                    message.AppendString("");
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendInteger(10);
                    session.SendMessage(message);
                    return;
                }

                case Interaction.ConditionItemsMatches:
                case Interaction.ConditionItemsDontMatch:
                {
                    ServerMessage serverMessage21 =
                        new ServerMessage(LibraryParser.OutgoingRequest("WiredConditionMessageComposer"));
                    serverMessage21.AppendBool(false);
                    serverMessage21.AppendInteger(5);
                    serverMessage21.AppendInteger(list.Count);
                    foreach (RoomItem current20 in list) serverMessage21.AppendInteger(current20.Id);
                    serverMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage21.AppendInteger(item.Id);
                    serverMessage21.AppendString(extraString2);
                    serverMessage21.AppendInteger(3);

                    if (string.IsNullOrWhiteSpace(extraInfo) || !extraInfo.Contains(","))
                    {
                        serverMessage21.AppendInteger(0);
                        serverMessage21.AppendInteger(0);
                        serverMessage21.AppendInteger(0);
                    }
                    else
                    {
                        string[] boolz = extraInfo.Split(',');

                        foreach (string stringy in boolz)
                            serverMessage21.AppendInteger(stringy.ToLower() == "true" ? 1 : 0);
                    }
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(0);
                    session.SendMessage(serverMessage21);
                    return;
                }

                case Interaction.ActionPosReset:
                {
                    ServerMessage serverMessage12 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage12.AppendBool(false);
                    serverMessage12.AppendInteger(5);
                    serverMessage12.AppendInteger(list.Count);
                    foreach (RoomItem current12 in list) serverMessage12.AppendInteger(current12.Id);
                    serverMessage12.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage12.AppendInteger(item.Id);
                    serverMessage12.AppendString(extraString2);
                    serverMessage12.AppendInteger(3);

                    if (string.IsNullOrWhiteSpace(extraInfo))
                    {
                        serverMessage12.AppendInteger(0);
                        serverMessage12.AppendInteger(0);
                        serverMessage12.AppendInteger(0);
                    }
                    else
                    {
                        string[] boolz = extraInfo.Split(',');

                        foreach (string stringy in boolz)
                            serverMessage12.AppendInteger(stringy.ToLower() == "true" ? 1 : 0);
                    }
                    serverMessage12.AppendInteger(0);
                    serverMessage12.AppendInteger(3);
                    serverMessage12.AppendInteger(delay); // Delay
                    serverMessage12.AppendInteger(0);
                    session.SendMessage(serverMessage12);
                    return;
                }
                case Interaction.ActionMoveRotate:
                {
                    ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage.AppendBool(false);
                    serverMessage.AppendInteger(5);

                    serverMessage.AppendInteger(list.Count(roomItem => roomItem != null));
                    foreach (RoomItem roomItem in list.Where(roomItem => roomItem != null))
                        serverMessage.AppendInteger(roomItem.Id);

                    serverMessage.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage.AppendInteger(item.Id);
                    serverMessage.AppendString(string.Empty);
                    serverMessage.AppendInteger(2);
                    serverMessage.AppendIntegersArray(extraInfo, ';', 2);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(4);
                    serverMessage.AppendInteger(delay);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(0);
                    session.SendMessage(serverMessage);
                }
                    break;

                case Interaction.ActionMoveToDir:
                {
                    ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage.AppendBool(false);
                    serverMessage.AppendInteger(5);

                    serverMessage.AppendInteger(list.Count(roomItem => roomItem != null));
                    foreach (RoomItem roomItem in list.Where(roomItem => roomItem != null))
                        serverMessage.AppendInteger(roomItem.Id);

                    serverMessage.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage.AppendInteger(item.Id);
                    serverMessage.AppendString(string.Empty);
                    serverMessage.AppendInteger(2);
                    serverMessage.AppendIntegersArray(extraInfo, ';', 2);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(13);
                    serverMessage.AppendInteger(delay);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(0);
                    session.SendMessage(serverMessage);
                }
                    break;

                case Interaction.ActionResetTimer:
                {
                    ServerMessage serverMessage14 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage14.AppendBool(false);
                    serverMessage14.AppendInteger(0);
                    serverMessage14.AppendInteger(0);
                    serverMessage14.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage14.AppendInteger(item.Id);
                    serverMessage14.AppendString(extraInfo);
                    serverMessage14.AppendInteger(0);
                    serverMessage14.AppendInteger(0);
                    serverMessage14.AppendInteger(1);
                    serverMessage14.AppendInteger(delay);
                    serverMessage14.AppendInteger(0);
                    serverMessage14.AppendInteger(0);
                    session.SendMessage(serverMessage14);
                    return;
                }
                case Interaction.ActionShowMessage:
                case Interaction.ActionKickUser:
                case Interaction.ActionEffectUser:
                {
                    ServerMessage serverMessage15 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage15.AppendBool(false);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(list.Count);
                    foreach (RoomItem current15 in list) serverMessage15.AppendInteger(current15.Id);
                    serverMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage15.AppendInteger(item.Id);
                    serverMessage15.AppendString(extraInfo);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(7);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    session.SendMessage(serverMessage15);
                    return;
                }
                case Interaction.ActionTeleportTo:
                {
                    ServerMessage serverMessage16 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage16.AppendBool(false);
                    serverMessage16.AppendInteger(5);

                    serverMessage16.AppendInteger(list.Count);
                    foreach (RoomItem roomItem in list) serverMessage16.AppendInteger(roomItem.Id);

                    serverMessage16.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage16.AppendInteger(item.Id);
                    serverMessage16.AppendString(extraInfo);
                    serverMessage16.AppendInteger(0);
                    serverMessage16.AppendInteger(8);
                    serverMessage16.AppendInteger(0);
                    serverMessage16.AppendInteger(delay);
                    serverMessage16.AppendInteger(0);
                    serverMessage16.AppendByte(2);
                    session.SendMessage(serverMessage16);
                    return;
                }
                case Interaction.ActionToggleState:
                {
                    ServerMessage serverMessage17 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage17.AppendBool(false);
                    serverMessage17.AppendInteger(5);
                    serverMessage17.AppendInteger(list.Count);
                    foreach (RoomItem current17 in list) serverMessage17.AppendInteger(current17.Id);
                    serverMessage17.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage17.AppendInteger(item.Id);
                    serverMessage17.AppendString(extraInfo);
                    serverMessage17.AppendInteger(0);
                    serverMessage17.AppendInteger(8);
                    serverMessage17.AppendInteger(0);
                    serverMessage17.AppendInteger(delay);
                    serverMessage17.AppendInteger(0);
                    serverMessage17.AppendInteger(0);
                    session.SendMessage(serverMessage17);
                    return;
                }
                case Interaction.ActionGiveReward:
                {
                    if (!session.GetHabbo().HasFuse("fuse_use_superwired")) return;
                    ServerMessage serverMessage18 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage18.AppendBool(false);
                    serverMessage18.AppendInteger(5);
                    serverMessage18.AppendInteger(0);
                    serverMessage18.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage18.AppendInteger(item.Id);
                    serverMessage18.AppendString(extraInfo);
                    serverMessage18.AppendInteger(3);
                    serverMessage18.AppendInteger(extraString == "" ? 0 : int.Parse(extraString));
                    serverMessage18.AppendInteger(flag ? 1 : 0);
                    serverMessage18.AppendInteger(extraString2 == "" ? 0 : int.Parse(extraString2));
                    serverMessage18.AppendInteger(0);
                    serverMessage18.AppendInteger(17);
                    serverMessage18.AppendInteger(0);
                    serverMessage18.AppendInteger(0);
                    session.SendMessage(serverMessage18);
                    return;
                }

                case Interaction.ConditionHowManyUsersInRoom:
                case Interaction.ConditionNegativeHowManyUsers:
                {
                    ServerMessage serverMessage19 =
                        new ServerMessage(LibraryParser.OutgoingRequest("WiredConditionMessageComposer"));
                    serverMessage19.AppendBool(false);
                    serverMessage19.AppendInteger(5);
                    serverMessage19.AppendInteger(0);
                    serverMessage19.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage19.AppendInteger(item.Id);
                    serverMessage19.AppendString("");
                    serverMessage19.AppendInteger(2);
                    if (string.IsNullOrWhiteSpace(extraInfo))
                    {
                        serverMessage19.AppendInteger(1);
                        serverMessage19.AppendInteger(50);
                    }
                    else
                        foreach (string integers in extraInfo.Split(','))
                            serverMessage19.AppendInteger(int.Parse(integers));
                    serverMessage19.AppendBool(false);
                    serverMessage19.AppendInteger(0);
                    serverMessage19.AppendInteger(1290);
                    session.SendMessage(serverMessage19);
                    return;
                }

                case Interaction.ConditionFurnisHaveUsers:
                case Interaction.ConditionStatePos:
                case Interaction.ConditionTriggerOnFurni:
                case Interaction.ConditionFurniTypeMatches:
                case Interaction.ConditionFurnisHaveNotUsers:
                case Interaction.ConditionFurniTypeDontMatch:
                case Interaction.ConditionTriggererNotOnFurni:
                {
                    ServerMessage serverMessage19 =
                        new ServerMessage(LibraryParser.OutgoingRequest("WiredConditionMessageComposer"));
                    serverMessage19.AppendBool(false);
                    serverMessage19.AppendInteger(5);
                    serverMessage19.AppendInteger(list.Count);
                    foreach (RoomItem current18 in list) serverMessage19.AppendInteger(current18.Id);
                    serverMessage19.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage19.AppendInteger(item.Id);
                    serverMessage19.AppendInteger(0);
                    serverMessage19.AppendInteger(0);
                    serverMessage19.AppendInteger(0);
                    serverMessage19.AppendBool(false);
                    serverMessage19.AppendBool(true);
                    session.SendMessage(serverMessage19);
                    return;
                }
                case Interaction.ConditionFurniHasNotFurni:
                case Interaction.ConditionFurniHasFurni:
                {
                    ServerMessage serverMessage =
                        new ServerMessage(LibraryParser.OutgoingRequest("WiredConditionMessageComposer"));
                    serverMessage.AppendBool(false);
                    serverMessage.AppendInteger(5);
                    serverMessage.AppendInteger(list.Count);
                    foreach (RoomItem current18 in list) serverMessage.AppendInteger(current18.Id);
                    serverMessage.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage.AppendInteger(item.Id);
                    serverMessage.AppendString(string.Empty);
                    serverMessage.AppendInteger(1);
                    serverMessage.AppendInteger(flag); //bool
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(item.GetBaseItem().InteractionType == Interaction.ConditionFurniHasFurni
                        ? 7
                        : 18);
                    session.SendMessage(serverMessage);
                    return;
                }
                case Interaction.ConditionTimeLessThan:
                case Interaction.ConditionTimeMoreThan:
                {
                    ServerMessage serverMessage21 =
                        new ServerMessage(LibraryParser.OutgoingRequest("WiredConditionMessageComposer"));
                    serverMessage21.AppendBool(false);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage21.AppendInteger(item.Id);
                    serverMessage21.AppendString("");
                    serverMessage21.AppendInteger(1);
                    serverMessage21.AppendInteger(delay); //delay
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(item.GetBaseItem().InteractionType ==
                                                  Interaction.ConditionTimeMoreThan
                        ? 3
                        : 4);
                    session.SendMessage(serverMessage21);
                    return;
                }

                case Interaction.ConditionUserWearingEffect:
                case Interaction.ConditionUserNotWearingEffect:
                {
                    int effect;
                    int.TryParse(extraInfo, out effect);
                    ServerMessage serverMessage21 =
                        new ServerMessage(LibraryParser.OutgoingRequest("WiredConditionMessageComposer"));
                    serverMessage21.AppendBool(false);
                    serverMessage21.AppendInteger(5);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage21.AppendInteger(item.Id);
                    serverMessage21.AppendString("");
                    serverMessage21.AppendInteger(1);
                    serverMessage21.AppendInteger(effect);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(12);
                    session.SendMessage(serverMessage21);
                    return;
                }

                case Interaction.ConditionUserWearingBadge:
                case Interaction.ConditionUserNotWearingBadge:
                case Interaction.ConditionUserHasFurni:
                {
                    ServerMessage serverMessage21 =
                        new ServerMessage(LibraryParser.OutgoingRequest("WiredConditionMessageComposer"));
                    serverMessage21.AppendBool(false);
                    serverMessage21.AppendInteger(5);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage21.AppendInteger(item.Id);
                    serverMessage21.AppendString(extraInfo);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(11);
                    session.SendMessage(serverMessage21);
                    return;
                }

                case Interaction.ConditionDateRangeActive:
                {
                    int date1 = 0;
                    int date2 = 0;

                    try
                    {
                        string[] strArray = extraInfo.Split(',');
                        date1 = int.Parse(strArray[0]);
                        date2 = int.Parse(strArray[1]);
                    }
                    catch
                    {
                    }

                    ServerMessage serverMessage21 =
                        new ServerMessage(LibraryParser.OutgoingRequest("WiredConditionMessageComposer"));
                    serverMessage21.AppendBool(false);
                    serverMessage21.AppendInteger(5);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage21.AppendInteger(item.Id);
                    serverMessage21.AppendString(extraInfo);
                    serverMessage21.AppendInteger(2);
                    serverMessage21.AppendInteger(date1);
                    serverMessage21.AppendInteger(date2);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(24);
                    session.SendMessage(serverMessage21);
                    return;
                }
                case Interaction.ActionJoinTeam:
                {
                    ServerMessage serverMessage15 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage15.AppendBool(false);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage15.AppendInteger(item.Id);
                    serverMessage15.AppendString(extraInfo);
                    serverMessage15.AppendInteger(1);
                    serverMessage15.AppendInteger(delay); // Team (1-4)
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(9);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    session.SendMessage(serverMessage15);
                    return;
                }
                case Interaction.ActionLeaveTeam:
                {
                    ServerMessage serverMessage15 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage15.AppendBool(false);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage15.AppendInteger(item.Id);
                    serverMessage15.AppendString(extraInfo);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(10);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    session.SendMessage(serverMessage15);
                    return;
                }
                case Interaction.TriggerBotReachedAvatar:
                {
                    ServerMessage serverMessage2 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage2.AppendBool(false);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(list.Count);
                    foreach (RoomItem current2 in list) serverMessage2.AppendInteger(current2.Id);
                    serverMessage2.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage2.AppendInteger(item.Id);
                    serverMessage2.AppendString(extraInfo);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(14);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(0);
                    session.SendMessage(serverMessage2);
                    return;
                }
                case Interaction.TriggerBotReachedStuff:
                {
                    ServerMessage serverMessage2 = new ServerMessage(LibraryParser.OutgoingRequest("WiredTriggerMessageComposer"));
                    serverMessage2.AppendBool(false);
                    serverMessage2.AppendInteger(5);
                    serverMessage2.AppendInteger(list.Count);
                    foreach (RoomItem current2 in list) serverMessage2.AppendInteger(current2.Id);
                    serverMessage2.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage2.AppendInteger(item.Id);
                    serverMessage2.AppendString(extraInfo);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(13);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(0);
                    serverMessage2.AppendInteger(0);
                    session.SendMessage(serverMessage2);
                    return;
                }
                case Interaction.ActionBotClothes:
                {
                    ServerMessage serverMessage15 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage15.AppendBool(false);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage15.AppendInteger(item.Id);
                    serverMessage15.AppendString(extraInfo + (char) 9 + extraString);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(26);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    session.SendMessage(serverMessage15);
                    return;
                }
                case Interaction.ActionBotFollowAvatar:
                {
                    ServerMessage serverMessage15 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage15.AppendBool(false);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage15.AppendInteger(item.Id);
                    serverMessage15.AppendString(extraInfo);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(25);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    session.SendMessage(serverMessage15);
                    return;
                }
                case Interaction.ActionBotGiveHanditem:
                {
                    ServerMessage serverMessage15 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage15.AppendBool(false);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage15.AppendInteger(item.Id);
                    serverMessage15.AppendString(extraInfo);
                    serverMessage15.AppendInteger(1);
                    serverMessage15.AppendInteger(delay);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(24);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    session.SendMessage(serverMessage15);
                    return;
                }
                case Interaction.ActionBotMove:
                {
                    ServerMessage serverMessage15 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage15.AppendBool(false);
                    serverMessage15.AppendInteger(5);
                    serverMessage15.AppendInteger(list.Count);
                    foreach (RoomItem current2 in list) serverMessage15.AppendInteger(current2.Id);
                    serverMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage15.AppendInteger(item.Id);
                    serverMessage15.AppendString(extraInfo);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(22);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    session.SendMessage(serverMessage15);
                    return;
                }
                case Interaction.ActionBotTalk:
                {
                    ServerMessage serverMessage15 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage15.AppendBool(false);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage15.AppendInteger(item.Id);
                    serverMessage15.AppendString(extraInfo + (char) 9 + extraString);
                    serverMessage15.AppendInteger(1);
                    serverMessage15.AppendInteger(Yupi.BoolToInteger(flag));
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(23);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    session.SendMessage(serverMessage15);
                    return;
                }
                case Interaction.ActionBotTalkToAvatar:
                {
                    ServerMessage serverMessage15 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage15.AppendBool(false);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage15.AppendInteger(item.Id);
                    serverMessage15.AppendString(extraInfo + (char) 9 + extraString);
                    serverMessage15.AppendInteger(1);
                    serverMessage15.AppendInteger(Yupi.BoolToInteger(flag));
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(27);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    session.SendMessage(serverMessage15);
                    return;
                }
                case Interaction.ActionBotTeleport:
                {
                    ServerMessage serverMessage15 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage15.AppendBool(false);
                    serverMessage15.AppendInteger(5);
                    serverMessage15.AppendInteger(list.Count);
                    foreach (RoomItem current2 in list) serverMessage15.AppendInteger(current2.Id);
                    serverMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage15.AppendInteger(item.Id);
                    serverMessage15.AppendString(extraInfo);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(21);
                    serverMessage15.AppendInteger(0);
                    serverMessage15.AppendInteger(0);
                    session.SendMessage(serverMessage15);
                    return;
                }
                case Interaction.ActionChase:
                {
                    ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage.AppendBool(false);
                    serverMessage.AppendInteger(5);

                    serverMessage.AppendInteger(list.Count);
                    foreach (RoomItem roomItem in list) serverMessage.AppendInteger(roomItem.Id);

                    serverMessage.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage.AppendInteger(item.Id);
                    serverMessage.AppendString(string.Empty);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(11);
                    serverMessage.AppendInteger(0);

                    serverMessage.AppendInteger(0);

                    session.SendMessage(serverMessage);
                    return;
                }
                case Interaction.ConditionUserHasHanditem:
                {
                    ServerMessage serverMessage21 =
                        new ServerMessage(LibraryParser.OutgoingRequest("WiredConditionMessageComposer"));
                    serverMessage21.AppendBool(false);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage21.AppendInteger(item.Id);
                    serverMessage21.AppendString(extraInfo);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(0);
                    serverMessage21.AppendInteger(25);
                    session.SendMessage(serverMessage21);
                    return;
                }
                case Interaction.ActionCallStacks:
                {
                    ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage.AppendBool(false);
                    serverMessage.AppendInteger(5);
                    serverMessage.AppendInteger(list.Count);
                    foreach (RoomItem current15 in list) serverMessage.AppendInteger(current15.Id);
                    serverMessage.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage.AppendInteger(item.Id);
                    serverMessage.AppendString(extraInfo);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(18);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(0);
                    serverMessage.AppendInteger(0);
                    session.SendMessage(serverMessage);
                    return;
                }

                case Interaction.ArrowPlate:
                case Interaction.PressurePad:
                case Interaction.PressurePadBed:
                case Interaction.RingPlate:
                case Interaction.ColorTile:
                case Interaction.ColorWheel:
                case Interaction.FloorSwitch1:
                case Interaction.FloorSwitch2:
                    break;

                case Interaction.SpecialRandom:
                {
                    ServerMessage serverMessage24 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage24.AppendBool(false);
                    serverMessage24.AppendInteger(5);
                    serverMessage24.AppendInteger(list.Count);
                    foreach (RoomItem current23 in list) serverMessage24.AppendInteger(current23.Id);
                    serverMessage24.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage24.AppendInteger(item.Id);
                    serverMessage24.AppendString(extraInfo);
                    serverMessage24.AppendInteger(0);
                    serverMessage24.AppendInteger(8);
                    serverMessage24.AppendInteger(0);
                    serverMessage24.AppendInteger(0);
                    serverMessage24.AppendInteger(0);
                    serverMessage24.AppendInteger(0);
                    session.SendMessage(serverMessage24);
                    return;
                }
                case Interaction.SpecialUnseen:
                {
                    ServerMessage serverMessage25 = new ServerMessage(LibraryParser.OutgoingRequest("WiredEffectMessageComposer"));
                    serverMessage25.AppendBool(false);
                    serverMessage25.AppendInteger(5);
                    serverMessage25.AppendInteger(list.Count);
                    foreach (RoomItem current24 in list) serverMessage25.AppendInteger(current24.Id);
                    serverMessage25.AppendInteger(item.GetBaseItem().SpriteId);
                    serverMessage25.AppendInteger(item.Id);
                    serverMessage25.AppendString(extraInfo);
                    serverMessage25.AppendInteger(0);
                    serverMessage25.AppendInteger(8);
                    serverMessage25.AppendInteger(0);
                    serverMessage25.AppendInteger(0);
                    serverMessage25.AppendInteger(0);
                    serverMessage25.AppendInteger(0);
                    session.SendMessage(serverMessage25);
                    return;
                }
                default:
                    return;
            }
        }
    }
}