using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     public class InteractorWired : FurniInteractorModel
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
                    SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessageBuffer.AppendBool(false);
                    simpleServerMessageBuffer.AppendInteger(5);
                    simpleServerMessageBuffer.AppendInteger(list.Count);
                    foreach (RoomItem current in list) simpleServerMessageBuffer.AppendInteger(current.Id);
                    simpleServerMessageBuffer.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessageBuffer.AppendInteger(item.Id);
                    simpleServerMessageBuffer.AppendString(extraInfo);
                    simpleServerMessageBuffer.AppendInteger(1);
                    simpleServerMessageBuffer.AppendInteger(delay);
                    simpleServerMessageBuffer.AppendInteger(1);
                    simpleServerMessageBuffer.AppendInteger(3);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(0);
                    session.SendMessage(simpleServerMessageBuffer);
                    return;
                }
                case Interaction.TriggerRoomEnter:
                {
                    SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage2.AppendBool(false);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(list.Count);
                    foreach (RoomItem current2 in list) simpleServerMessage2.AppendInteger(current2.Id);
                    simpleServerMessage2.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage2.AppendInteger(item.Id);
                    simpleServerMessage2.AppendString(extraInfo);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(7);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(0);
                    session.SendMessage(simpleServerMessage2);
                    return;
                }
                case Interaction.TriggerGameEnd:
                {
                    SimpleServerMessageBuffer simpleServerMessage3 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage3.AppendBool(false);
                    simpleServerMessage3.AppendInteger(0);
                    simpleServerMessage3.AppendInteger(list.Count);
                    foreach (RoomItem current3 in list) simpleServerMessage3.AppendInteger(current3.Id);
                    simpleServerMessage3.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage3.AppendInteger(item.Id);
                    simpleServerMessage3.AppendString(extraInfo);
                    simpleServerMessage3.AppendInteger(0);
                    simpleServerMessage3.AppendInteger(0);
                    simpleServerMessage3.AppendInteger(8);
                    simpleServerMessage3.AppendInteger(0);
                    simpleServerMessage3.AppendInteger(0);
                    simpleServerMessage3.AppendInteger(0);
                    session.SendMessage(simpleServerMessage3);
                    return;
                }
                case Interaction.TriggerGameStart:
                {
                    SimpleServerMessageBuffer simpleServerMessage4 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage4.AppendBool(false);
                    simpleServerMessage4.AppendInteger(0);
                    simpleServerMessage4.AppendInteger(list.Count);
                    foreach (RoomItem current4 in list) simpleServerMessage4.AppendInteger(current4.Id);
                    simpleServerMessage4.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage4.AppendInteger(item.Id);
                    simpleServerMessage4.AppendString(extraInfo);
                    simpleServerMessage4.AppendInteger(0);
                    simpleServerMessage4.AppendInteger(0);
                    simpleServerMessage4.AppendInteger(8);
                    simpleServerMessage4.AppendInteger(0);
                    simpleServerMessage4.AppendInteger(0);
                    simpleServerMessage4.AppendInteger(0);
                    session.SendMessage(simpleServerMessage4);
                    return;
                }
                case Interaction.TriggerLongRepeater:
                {
                    SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessageBuffer.AppendBool(false);
                    simpleServerMessageBuffer.AppendInteger(5);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessageBuffer.AppendInteger(item.Id);
                    simpleServerMessageBuffer.AppendString("");
                    simpleServerMessageBuffer.AppendInteger(1);
                    simpleServerMessageBuffer.AppendInteger(delay/10); //fix
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(12);
                    simpleServerMessageBuffer.AppendInteger(0);
                    session.SendMessage(simpleServerMessageBuffer);
                    return;
                }

                case Interaction.TriggerRepeater:
                {
                    SimpleServerMessageBuffer simpleServerMessage5 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage5.AppendBool(false);
                    simpleServerMessage5.AppendInteger(5);
                    simpleServerMessage5.AppendInteger(list.Count);
                    foreach (RoomItem current5 in list) simpleServerMessage5.AppendInteger(current5.Id);
                    simpleServerMessage5.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage5.AppendInteger(item.Id);
                    simpleServerMessage5.AppendString(extraInfo);
                    simpleServerMessage5.AppendInteger(1);
                    simpleServerMessage5.AppendInteger(delay);
                    simpleServerMessage5.AppendInteger(0);
                    simpleServerMessage5.AppendInteger(6);
                    simpleServerMessage5.AppendInteger(0);
                    simpleServerMessage5.AppendInteger(0);
                    session.SendMessage(simpleServerMessage5);
                    return;
                }
                case Interaction.TriggerOnUserSay:
                {
                    SimpleServerMessageBuffer simpleServerMessage6 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage6.AppendBool(false);
                    simpleServerMessage6.AppendInteger(0);
                    simpleServerMessage6.AppendInteger(list.Count);
                    foreach (RoomItem current6 in list) simpleServerMessage6.AppendInteger(current6.Id);
                    simpleServerMessage6.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage6.AppendInteger(item.Id);
                    simpleServerMessage6.AppendString(extraInfo);
                    simpleServerMessage6.AppendInteger(0);
                    simpleServerMessage6.AppendInteger(0);
                    simpleServerMessage6.AppendInteger(0);
                    simpleServerMessage6.AppendInteger(0);
                    simpleServerMessage6.AppendInteger(0);
                    simpleServerMessage6.AppendInteger(0);
                    session.SendMessage(simpleServerMessage6);
                    return;
                }
                case Interaction.TriggerScoreAchieved:
                {
                    SimpleServerMessageBuffer simpleServerMessage7 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage7.AppendBool(false);
                    simpleServerMessage7.AppendInteger(5);
                    simpleServerMessage7.AppendInteger(0);
                    simpleServerMessage7.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage7.AppendInteger(item.Id);
                    simpleServerMessage7.AppendString("");
                    simpleServerMessage7.AppendInteger(1);
                    simpleServerMessage7.AppendInteger(string.IsNullOrWhiteSpace(extraInfo) ? 100 : int.Parse(extraInfo));
                    simpleServerMessage7.AppendInteger(0);
                    simpleServerMessage7.AppendInteger(10);
                    simpleServerMessage7.AppendInteger(0);
                    simpleServerMessage7.AppendInteger(0);
                    session.SendMessage(simpleServerMessage7);
                    return;
                }
                case Interaction.TriggerStateChanged:
                {
                    SimpleServerMessageBuffer simpleServerMessage8 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage8.AppendBool(false);
                    simpleServerMessage8.AppendInteger(5);
                    simpleServerMessage8.AppendInteger(list.Count);
                    foreach (RoomItem current8 in list) simpleServerMessage8.AppendInteger(current8.Id);
                    simpleServerMessage8.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage8.AppendInteger(item.Id);
                    simpleServerMessage8.AppendString(extraInfo);
                    simpleServerMessage8.AppendInteger(0);
                    simpleServerMessage8.AppendInteger(0);
                    simpleServerMessage8.AppendInteger(1);
                    simpleServerMessage8.AppendInteger(delay);
                    simpleServerMessage8.AppendInteger(0);
                    simpleServerMessage8.AppendInteger(0);
                    session.SendMessage(simpleServerMessage8);
                    return;
                }
                case Interaction.TriggerWalkOnFurni:
                {
                    SimpleServerMessageBuffer simpleServerMessage9 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage9.AppendBool(false);
                    simpleServerMessage9.AppendInteger(5);
                    simpleServerMessage9.AppendInteger(list.Count);
                    foreach (RoomItem current9 in list) simpleServerMessage9.AppendInteger(current9.Id);
                    simpleServerMessage9.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage9.AppendInteger(item.Id);
                    simpleServerMessage9.AppendString(extraInfo);
                    simpleServerMessage9.AppendInteger(0);
                    simpleServerMessage9.AppendInteger(0);
                    simpleServerMessage9.AppendInteger(1);
                    simpleServerMessage9.AppendInteger(0);
                    simpleServerMessage9.AppendInteger(0);
                    simpleServerMessage9.AppendInteger(0);
                    session.SendMessage(simpleServerMessage9);
                    return;
                }
                case Interaction.ActionMuteUser:
                {
                    SimpleServerMessageBuffer simpleServerMessage18 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage18.AppendBool(false);
                    simpleServerMessage18.AppendInteger(5);
                    simpleServerMessage18.AppendInteger(0);
                    simpleServerMessage18.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage18.AppendInteger(item.Id);
                    simpleServerMessage18.AppendString(extraInfo);
                    simpleServerMessage18.AppendInteger(1);
                    simpleServerMessage18.AppendInteger(delay);
                    simpleServerMessage18.AppendInteger(0);
                    simpleServerMessage18.AppendInteger(20);
                    simpleServerMessage18.AppendInteger(0);
                    simpleServerMessage18.AppendInteger(0);
                    session.SendMessage(simpleServerMessage18);
                    return;
                }
                case Interaction.TriggerWalkOffFurni:
                {
                    SimpleServerMessageBuffer simpleServerMessage10 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage10.AppendBool(false);
                    simpleServerMessage10.AppendInteger(5);
                    simpleServerMessage10.AppendInteger(list.Count);
                    foreach (RoomItem current10 in list) simpleServerMessage10.AppendInteger(current10.Id);
                    simpleServerMessage10.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage10.AppendInteger(item.Id);
                    simpleServerMessage10.AppendString(extraInfo);
                    simpleServerMessage10.AppendInteger(0);
                    simpleServerMessage10.AppendInteger(0);
                    simpleServerMessage10.AppendInteger(1);
                    simpleServerMessage10.AppendInteger(0);
                    simpleServerMessage10.AppendInteger(0);
                    simpleServerMessage10.AppendInteger(0);
                    simpleServerMessage10.AppendInteger(0);
                    session.SendMessage(simpleServerMessage10);
                    return;
                }

                case Interaction.TriggerCollision:
                {
                    SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessageBuffer.AppendBool(false);
                    simpleServerMessageBuffer.AppendInteger(5);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessageBuffer.AppendInteger(item.Id);
                    simpleServerMessageBuffer.AppendString(string.Empty);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(11);
                    simpleServerMessageBuffer.AppendInteger(0);
                    session.SendMessage(simpleServerMessageBuffer);
                    return;
                }

                case Interaction.ActionGiveScore:
                {
                    // Por hacer.
                    SimpleServerMessageBuffer simpleServerMessage11 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage11.AppendBool(false);
                    simpleServerMessage11.AppendInteger(5);
                    simpleServerMessage11.AppendInteger(0);
                    simpleServerMessage11.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage11.AppendInteger(item.Id);
                    simpleServerMessage11.AppendString("");
                    simpleServerMessage11.AppendInteger(2);
                    if (string.IsNullOrWhiteSpace(extraInfo))
                    {
                        simpleServerMessage11.AppendInteger(10); // Puntos a dar
                        simpleServerMessage11.AppendInteger(1); // Numero de veces por equipo
                    }
                    else
                    {
                        string[] integers = extraInfo.Split(',');
                        simpleServerMessage11.AppendInteger(int.Parse(integers[0])); // Puntos a dar
                        simpleServerMessage11.AppendInteger(int.Parse(integers[1])); // Numero de veces por equipo
                    }
                    simpleServerMessage11.AppendInteger(0);
                    simpleServerMessage11.AppendInteger(6);
                    simpleServerMessage11.AppendInteger(0);
                    simpleServerMessage11.AppendInteger(0);
                    simpleServerMessage11.AppendInteger(0);
                    session.SendMessage(simpleServerMessage11);
                    return;
                }

                case Interaction.ConditionGroupMember:
                case Interaction.ConditionNotGroupMember:
                {
                    SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredConditionMessageComposer"));
                    messageBuffer.AppendBool(false);
                    messageBuffer.AppendInteger(5);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(item.GetBaseItem().SpriteId);
                    messageBuffer.AppendInteger(item.Id);
                    messageBuffer.AppendString("");
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(10);
                    session.SendMessage(messageBuffer);
                    return;
                }

                case Interaction.ConditionItemsMatches:
                case Interaction.ConditionItemsDontMatch:
                {
                    SimpleServerMessageBuffer simpleServerMessage21 =
                        new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredConditionMessageComposer"));
                    simpleServerMessage21.AppendBool(false);
                    simpleServerMessage21.AppendInteger(5);
                    simpleServerMessage21.AppendInteger(list.Count);
                    foreach (RoomItem current20 in list) simpleServerMessage21.AppendInteger(current20.Id);
                    simpleServerMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage21.AppendInteger(item.Id);
                    simpleServerMessage21.AppendString(extraString2);
                    simpleServerMessage21.AppendInteger(3);

                    if (string.IsNullOrWhiteSpace(extraInfo) || !extraInfo.Contains(","))
                    {
                        simpleServerMessage21.AppendInteger(0);
                        simpleServerMessage21.AppendInteger(0);
                        simpleServerMessage21.AppendInteger(0);
                    }
                    else
                    {
                        string[] boolz = extraInfo.Split(',');

                        foreach (string stringy in boolz)
                            simpleServerMessage21.AppendInteger(stringy.ToLower() == "true" ? 1 : 0);
                    }
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(0);
                    session.SendMessage(simpleServerMessage21);
                    return;
                }

                case Interaction.ActionPosReset:
                {
                    SimpleServerMessageBuffer simpleServerMessage12 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage12.AppendBool(false);
                    simpleServerMessage12.AppendInteger(5);
                    simpleServerMessage12.AppendInteger(list.Count);
                    foreach (RoomItem current12 in list) simpleServerMessage12.AppendInteger(current12.Id);
                    simpleServerMessage12.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage12.AppendInteger(item.Id);
                    simpleServerMessage12.AppendString(extraString2);
                    simpleServerMessage12.AppendInteger(3);

                    if (string.IsNullOrWhiteSpace(extraInfo))
                    {
                        simpleServerMessage12.AppendInteger(0);
                        simpleServerMessage12.AppendInteger(0);
                        simpleServerMessage12.AppendInteger(0);
                    }
                    else
                    {
                        string[] boolz = extraInfo.Split(',');

                        foreach (string stringy in boolz)
                            simpleServerMessage12.AppendInteger(stringy.ToLower() == "true" ? 1 : 0);
                    }
                    simpleServerMessage12.AppendInteger(0);
                    simpleServerMessage12.AppendInteger(3);
                    simpleServerMessage12.AppendInteger(delay); // Delay
                    simpleServerMessage12.AppendInteger(0);
                    session.SendMessage(simpleServerMessage12);
                    return;
                }
                case Interaction.ActionMoveRotate:
                {
                    SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessageBuffer.AppendBool(false);
                    simpleServerMessageBuffer.AppendInteger(5);

                    simpleServerMessageBuffer.AppendInteger(list.Count(roomItem => roomItem != null));
                    foreach (RoomItem roomItem in list.Where(roomItem => roomItem != null))
                        simpleServerMessageBuffer.AppendInteger(roomItem.Id);

                    simpleServerMessageBuffer.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessageBuffer.AppendInteger(item.Id);
                    simpleServerMessageBuffer.AppendString(string.Empty);
                    simpleServerMessageBuffer.AppendInteger(2);
                    simpleServerMessageBuffer.AppendIntegersArray(extraInfo, ';', 2);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(4);
                    simpleServerMessageBuffer.AppendInteger(delay);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(0);
                    session.SendMessage(simpleServerMessageBuffer);
                }
                    break;

                case Interaction.ActionMoveToDir:
                {
                    SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessageBuffer.AppendBool(false);
                    simpleServerMessageBuffer.AppendInteger(5);

                    simpleServerMessageBuffer.AppendInteger(list.Count(roomItem => roomItem != null));
                    foreach (RoomItem roomItem in list.Where(roomItem => roomItem != null))
                        simpleServerMessageBuffer.AppendInteger(roomItem.Id);

                    simpleServerMessageBuffer.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessageBuffer.AppendInteger(item.Id);
                    simpleServerMessageBuffer.AppendString(string.Empty);
                    simpleServerMessageBuffer.AppendInteger(2);
                    simpleServerMessageBuffer.AppendIntegersArray(extraInfo, ';', 2);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(13);
                    simpleServerMessageBuffer.AppendInteger(delay);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(0);
                    session.SendMessage(simpleServerMessageBuffer);
                }
                    break;

                case Interaction.ActionResetTimer:
                {
                    SimpleServerMessageBuffer simpleServerMessage14 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage14.AppendBool(false);
                    simpleServerMessage14.AppendInteger(0);
                    simpleServerMessage14.AppendInteger(0);
                    simpleServerMessage14.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage14.AppendInteger(item.Id);
                    simpleServerMessage14.AppendString(extraInfo);
                    simpleServerMessage14.AppendInteger(0);
                    simpleServerMessage14.AppendInteger(0);
                    simpleServerMessage14.AppendInteger(1);
                    simpleServerMessage14.AppendInteger(delay);
                    simpleServerMessage14.AppendInteger(0);
                    simpleServerMessage14.AppendInteger(0);
                    session.SendMessage(simpleServerMessage14);
                    return;
                }
                case Interaction.ActionShowMessage:
                case Interaction.ActionKickUser:
                case Interaction.ActionEffectUser:
                {
                    SimpleServerMessageBuffer simpleServerMessage15 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage15.AppendBool(false);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(list.Count);
                    foreach (RoomItem current15 in list) simpleServerMessage15.AppendInteger(current15.Id);
                    simpleServerMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage15.AppendInteger(item.Id);
                    simpleServerMessage15.AppendString(extraInfo);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(7);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    session.SendMessage(simpleServerMessage15);
                    return;
                }
                case Interaction.ActionTeleportTo:
                {
                    SimpleServerMessageBuffer simpleServerMessage16 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage16.AppendBool(false);
                    simpleServerMessage16.AppendInteger(5);

                    simpleServerMessage16.AppendInteger(list.Count);
                    foreach (RoomItem roomItem in list) simpleServerMessage16.AppendInteger(roomItem.Id);

                    simpleServerMessage16.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage16.AppendInteger(item.Id);
                    simpleServerMessage16.AppendString(extraInfo);
                    simpleServerMessage16.AppendInteger(0);
                    simpleServerMessage16.AppendInteger(8);
                    simpleServerMessage16.AppendInteger(0);
                    simpleServerMessage16.AppendInteger(delay);
                    simpleServerMessage16.AppendInteger(0);
                    simpleServerMessage16.AppendByte(2);
                    session.SendMessage(simpleServerMessage16);
                    return;
                }
                case Interaction.ActionToggleState:
                {
                    SimpleServerMessageBuffer simpleServerMessage17 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage17.AppendBool(false);
                    simpleServerMessage17.AppendInteger(5);
                    simpleServerMessage17.AppendInteger(list.Count);
                    foreach (RoomItem current17 in list) simpleServerMessage17.AppendInteger(current17.Id);
                    simpleServerMessage17.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage17.AppendInteger(item.Id);
                    simpleServerMessage17.AppendString(extraInfo);
                    simpleServerMessage17.AppendInteger(0);
                    simpleServerMessage17.AppendInteger(8);
                    simpleServerMessage17.AppendInteger(0);
                    simpleServerMessage17.AppendInteger(delay);
                    simpleServerMessage17.AppendInteger(0);
                    simpleServerMessage17.AppendInteger(0);
                    session.SendMessage(simpleServerMessage17);
                    return;
                }
                case Interaction.ActionGiveReward:
                {
                    if (!session.GetHabbo().HasFuse("fuse_use_superwired")) return;
                    SimpleServerMessageBuffer simpleServerMessage18 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage18.AppendBool(false);
                    simpleServerMessage18.AppendInteger(5);
                    simpleServerMessage18.AppendInteger(0);
                    simpleServerMessage18.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage18.AppendInteger(item.Id);
                    simpleServerMessage18.AppendString(extraInfo);
                    simpleServerMessage18.AppendInteger(3);
                    simpleServerMessage18.AppendInteger(extraString == "" ? 0 : int.Parse(extraString));
                    simpleServerMessage18.AppendInteger(flag ? 1 : 0);
                    simpleServerMessage18.AppendInteger(extraString2 == "" ? 0 : int.Parse(extraString2));
                    simpleServerMessage18.AppendInteger(0);
                    simpleServerMessage18.AppendInteger(17);
                    simpleServerMessage18.AppendInteger(0);
                    simpleServerMessage18.AppendInteger(0);
                    session.SendMessage(simpleServerMessage18);
                    return;
                }

                case Interaction.ConditionHowManyUsersInRoom:
                case Interaction.ConditionNegativeHowManyUsers:
                {
                    SimpleServerMessageBuffer simpleServerMessage19 =
                        new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredConditionMessageComposer"));
                    simpleServerMessage19.AppendBool(false);
                    simpleServerMessage19.AppendInteger(5);
                    simpleServerMessage19.AppendInteger(0);
                    simpleServerMessage19.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage19.AppendInteger(item.Id);
                    simpleServerMessage19.AppendString("");
                    simpleServerMessage19.AppendInteger(2);
                    if (string.IsNullOrWhiteSpace(extraInfo))
                    {
                        simpleServerMessage19.AppendInteger(1);
                        simpleServerMessage19.AppendInteger(50);
                    }
                    else
                        foreach (string integers in extraInfo.Split(','))
                            simpleServerMessage19.AppendInteger(int.Parse(integers));
                    simpleServerMessage19.AppendBool(false);
                    simpleServerMessage19.AppendInteger(0);
                    simpleServerMessage19.AppendInteger(1290);
                    session.SendMessage(simpleServerMessage19);
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
                    SimpleServerMessageBuffer simpleServerMessage19 =
                        new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredConditionMessageComposer"));
                    simpleServerMessage19.AppendBool(false);
                    simpleServerMessage19.AppendInteger(5);
                    simpleServerMessage19.AppendInteger(list.Count);
                    foreach (RoomItem current18 in list) simpleServerMessage19.AppendInteger(current18.Id);
                    simpleServerMessage19.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage19.AppendInteger(item.Id);
                    simpleServerMessage19.AppendInteger(0);
                    simpleServerMessage19.AppendInteger(0);
                    simpleServerMessage19.AppendInteger(0);
                    simpleServerMessage19.AppendBool(false);
                    simpleServerMessage19.AppendBool(true);
                    session.SendMessage(simpleServerMessage19);
                    return;
                }
                case Interaction.ConditionFurniHasNotFurni:
                case Interaction.ConditionFurniHasFurni:
                {
                    SimpleServerMessageBuffer simpleServerMessageBuffer =
                        new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredConditionMessageComposer"));
                    simpleServerMessageBuffer.AppendBool(false);
                    simpleServerMessageBuffer.AppendInteger(5);
                    simpleServerMessageBuffer.AppendInteger(list.Count);
                    foreach (RoomItem current18 in list) simpleServerMessageBuffer.AppendInteger(current18.Id);
                    simpleServerMessageBuffer.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessageBuffer.AppendInteger(item.Id);
                    simpleServerMessageBuffer.AppendString(string.Empty);
                    simpleServerMessageBuffer.AppendInteger(1);
                    simpleServerMessageBuffer.AppendInteger(flag); //bool
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(item.GetBaseItem().InteractionType == Interaction.ConditionFurniHasFurni
                        ? 7
                        : 18);
                    session.SendMessage(simpleServerMessageBuffer);
                    return;
                }
                case Interaction.ConditionTimeLessThan:
                case Interaction.ConditionTimeMoreThan:
                {
                    SimpleServerMessageBuffer simpleServerMessage21 =
                        new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredConditionMessageComposer"));
                    simpleServerMessage21.AppendBool(false);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage21.AppendInteger(item.Id);
                    simpleServerMessage21.AppendString("");
                    simpleServerMessage21.AppendInteger(1);
                    simpleServerMessage21.AppendInteger(delay); //delay
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(item.GetBaseItem().InteractionType ==
                                                  Interaction.ConditionTimeMoreThan
                        ? 3
                        : 4);
                    session.SendMessage(simpleServerMessage21);
                    return;
                }

                case Interaction.ConditionUserWearingEffect:
                case Interaction.ConditionUserNotWearingEffect:
                {
                    int effect;
                    int.TryParse(extraInfo, out effect);
                    SimpleServerMessageBuffer simpleServerMessage21 =
                        new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredConditionMessageComposer"));
                    simpleServerMessage21.AppendBool(false);
                    simpleServerMessage21.AppendInteger(5);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage21.AppendInteger(item.Id);
                    simpleServerMessage21.AppendString("");
                    simpleServerMessage21.AppendInteger(1);
                    simpleServerMessage21.AppendInteger(effect);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(12);
                    session.SendMessage(simpleServerMessage21);
                    return;
                }

                case Interaction.ConditionUserWearingBadge:
                case Interaction.ConditionUserNotWearingBadge:
                case Interaction.ConditionUserHasFurni:
                {
                    SimpleServerMessageBuffer simpleServerMessage21 =
                        new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredConditionMessageComposer"));
                    simpleServerMessage21.AppendBool(false);
                    simpleServerMessage21.AppendInteger(5);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage21.AppendInteger(item.Id);
                    simpleServerMessage21.AppendString(extraInfo);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(11);
                    session.SendMessage(simpleServerMessage21);
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

                    SimpleServerMessageBuffer simpleServerMessage21 =
                        new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredConditionMessageComposer"));
                    simpleServerMessage21.AppendBool(false);
                    simpleServerMessage21.AppendInteger(5);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage21.AppendInteger(item.Id);
                    simpleServerMessage21.AppendString(extraInfo);
                    simpleServerMessage21.AppendInteger(2);
                    simpleServerMessage21.AppendInteger(date1);
                    simpleServerMessage21.AppendInteger(date2);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(24);
                    session.SendMessage(simpleServerMessage21);
                    return;
                }
                case Interaction.ActionJoinTeam:
                {
                    SimpleServerMessageBuffer simpleServerMessage15 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage15.AppendBool(false);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage15.AppendInteger(item.Id);
                    simpleServerMessage15.AppendString(extraInfo);
                    simpleServerMessage15.AppendInteger(1);
                    simpleServerMessage15.AppendInteger(delay); // Team (1-4)
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(9);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    session.SendMessage(simpleServerMessage15);
                    return;
                }
                case Interaction.ActionLeaveTeam:
                {
                    SimpleServerMessageBuffer simpleServerMessage15 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage15.AppendBool(false);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage15.AppendInteger(item.Id);
                    simpleServerMessage15.AppendString(extraInfo);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(10);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    session.SendMessage(simpleServerMessage15);
                    return;
                }
                case Interaction.TriggerBotReachedAvatar:
                {
                    SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage2.AppendBool(false);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(list.Count);
                    foreach (RoomItem current2 in list) simpleServerMessage2.AppendInteger(current2.Id);
                    simpleServerMessage2.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage2.AppendInteger(item.Id);
                    simpleServerMessage2.AppendString(extraInfo);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(14);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(0);
                    session.SendMessage(simpleServerMessage2);
                    return;
                }
                case Interaction.TriggerBotReachedStuff:
                {
                    SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredTriggerMessageComposer"));
                    simpleServerMessage2.AppendBool(false);
                    simpleServerMessage2.AppendInteger(5);
                    simpleServerMessage2.AppendInteger(list.Count);
                    foreach (RoomItem current2 in list) simpleServerMessage2.AppendInteger(current2.Id);
                    simpleServerMessage2.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage2.AppendInteger(item.Id);
                    simpleServerMessage2.AppendString(extraInfo);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(13);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(0);
                    simpleServerMessage2.AppendInteger(0);
                    session.SendMessage(simpleServerMessage2);
                    return;
                }
                case Interaction.ActionBotClothes:
                {
                    SimpleServerMessageBuffer simpleServerMessage15 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage15.AppendBool(false);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage15.AppendInteger(item.Id);
                    simpleServerMessage15.AppendString(extraInfo + (char) 9 + extraString);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(26);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    session.SendMessage(simpleServerMessage15);
                    return;
                }
                case Interaction.ActionBotFollowAvatar:
                {
                    SimpleServerMessageBuffer simpleServerMessage15 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage15.AppendBool(false);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage15.AppendInteger(item.Id);
                    simpleServerMessage15.AppendString(extraInfo);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(25);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    session.SendMessage(simpleServerMessage15);
                    return;
                }
                case Interaction.ActionBotGiveHanditem:
                {
                    SimpleServerMessageBuffer simpleServerMessage15 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage15.AppendBool(false);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage15.AppendInteger(item.Id);
                    simpleServerMessage15.AppendString(extraInfo);
                    simpleServerMessage15.AppendInteger(1);
                    simpleServerMessage15.AppendInteger(delay);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(24);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    session.SendMessage(simpleServerMessage15);
                    return;
                }
                case Interaction.ActionBotMove:
                {
                    SimpleServerMessageBuffer simpleServerMessage15 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage15.AppendBool(false);
                    simpleServerMessage15.AppendInteger(5);
                    simpleServerMessage15.AppendInteger(list.Count);
                    foreach (RoomItem current2 in list) simpleServerMessage15.AppendInteger(current2.Id);
                    simpleServerMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage15.AppendInteger(item.Id);
                    simpleServerMessage15.AppendString(extraInfo);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(22);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    session.SendMessage(simpleServerMessage15);
                    return;
                }
                case Interaction.ActionBotTalk:
                {
                    SimpleServerMessageBuffer simpleServerMessage15 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage15.AppendBool(false);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage15.AppendInteger(item.Id);
                    simpleServerMessage15.AppendString(extraInfo + (char) 9 + extraString);
                    simpleServerMessage15.AppendInteger(1);
                    simpleServerMessage15.AppendInteger(Yupi.BoolToInteger(flag));
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(23);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    session.SendMessage(simpleServerMessage15);
                    return;
                }
                case Interaction.ActionBotTalkToAvatar:
                {
                    SimpleServerMessageBuffer simpleServerMessage15 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage15.AppendBool(false);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage15.AppendInteger(item.Id);
                    simpleServerMessage15.AppendString(extraInfo + (char) 9 + extraString);
                    simpleServerMessage15.AppendInteger(1);
                    simpleServerMessage15.AppendInteger(Yupi.BoolToInteger(flag));
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(27);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    session.SendMessage(simpleServerMessage15);
                    return;
                }
                case Interaction.ActionBotTeleport:
                {
                    SimpleServerMessageBuffer simpleServerMessage15 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage15.AppendBool(false);
                    simpleServerMessage15.AppendInteger(5);
                    simpleServerMessage15.AppendInteger(list.Count);
                    foreach (RoomItem current2 in list) simpleServerMessage15.AppendInteger(current2.Id);
                    simpleServerMessage15.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage15.AppendInteger(item.Id);
                    simpleServerMessage15.AppendString(extraInfo);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(21);
                    simpleServerMessage15.AppendInteger(0);
                    simpleServerMessage15.AppendInteger(0);
                    session.SendMessage(simpleServerMessage15);
                    return;
                }
                case Interaction.ActionChase:
                {
                    SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessageBuffer.AppendBool(false);
                    simpleServerMessageBuffer.AppendInteger(5);

                    simpleServerMessageBuffer.AppendInteger(list.Count);
                    foreach (RoomItem roomItem in list) simpleServerMessageBuffer.AppendInteger(roomItem.Id);

                    simpleServerMessageBuffer.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessageBuffer.AppendInteger(item.Id);
                    simpleServerMessageBuffer.AppendString(string.Empty);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(11);
                    simpleServerMessageBuffer.AppendInteger(0);

                    simpleServerMessageBuffer.AppendInteger(0);

                    session.SendMessage(simpleServerMessageBuffer);
                    return;
                }
                case Interaction.ConditionUserHasHanditem:
                {
                    SimpleServerMessageBuffer simpleServerMessage21 =
                        new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredConditionMessageComposer"));
                    simpleServerMessage21.AppendBool(false);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage21.AppendInteger(item.Id);
                    simpleServerMessage21.AppendString(extraInfo);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(0);
                    simpleServerMessage21.AppendInteger(25);
                    session.SendMessage(simpleServerMessage21);
                    return;
                }
                case Interaction.ActionCallStacks:
                {
                    SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessageBuffer.AppendBool(false);
                    simpleServerMessageBuffer.AppendInteger(5);
                    simpleServerMessageBuffer.AppendInteger(list.Count);
                    foreach (RoomItem current15 in list) simpleServerMessageBuffer.AppendInteger(current15.Id);
                    simpleServerMessageBuffer.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessageBuffer.AppendInteger(item.Id);
                    simpleServerMessageBuffer.AppendString(extraInfo);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(18);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(0);
                    session.SendMessage(simpleServerMessageBuffer);
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
                    SimpleServerMessageBuffer simpleServerMessage24 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage24.AppendBool(false);
                    simpleServerMessage24.AppendInteger(5);
                    simpleServerMessage24.AppendInteger(list.Count);
                    foreach (RoomItem current23 in list) simpleServerMessage24.AppendInteger(current23.Id);
                    simpleServerMessage24.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage24.AppendInteger(item.Id);
                    simpleServerMessage24.AppendString(extraInfo);
                    simpleServerMessage24.AppendInteger(0);
                    simpleServerMessage24.AppendInteger(8);
                    simpleServerMessage24.AppendInteger(0);
                    simpleServerMessage24.AppendInteger(0);
                    simpleServerMessage24.AppendInteger(0);
                    simpleServerMessage24.AppendInteger(0);
                    session.SendMessage(simpleServerMessage24);
                    return;
                }
                case Interaction.SpecialUnseen:
                {
                    SimpleServerMessageBuffer simpleServerMessage25 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("WiredEffectMessageComposer"));
                    simpleServerMessage25.AppendBool(false);
                    simpleServerMessage25.AppendInteger(5);
                    simpleServerMessage25.AppendInteger(list.Count);
                    foreach (RoomItem current24 in list) simpleServerMessage25.AppendInteger(current24.Id);
                    simpleServerMessage25.AppendInteger(item.GetBaseItem().SpriteId);
                    simpleServerMessage25.AppendInteger(item.Id);
                    simpleServerMessage25.AppendString(extraInfo);
                    simpleServerMessage25.AppendInteger(0);
                    simpleServerMessage25.AppendInteger(8);
                    simpleServerMessage25.AppendInteger(0);
                    simpleServerMessage25.AppendInteger(0);
                    simpleServerMessage25.AppendInteger(0);
                    simpleServerMessage25.AppendInteger(0);
                    session.SendMessage(simpleServerMessage25);
                    return;
                }
                default:
                    return;
            }
        }
    }
}