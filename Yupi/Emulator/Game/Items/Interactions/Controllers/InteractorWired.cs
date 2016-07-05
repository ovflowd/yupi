using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;



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
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, delay, extraInfo, 5);
                    return;
                }
                case Interaction.TriggerRoomEnter:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 0); 
                    return;
                }
                case Interaction.TriggerGameEnd:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 0); 
                    return;
                }
                case Interaction.TriggerGameStart:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 0); 
                    return;
                }
                case Interaction.TriggerLongRepeater:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 5); 
                    return;
                }

                case Interaction.TriggerRepeater:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 5); 
                    return;
                }
                case Interaction.TriggerOnUserSay:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 0); 
                    return;
                }
                case Interaction.TriggerScoreAchieved:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 5); 
                    return;
                }
                case Interaction.TriggerStateChanged:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 5); 
                    return;
                }
                case Interaction.TriggerWalkOnFurni:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 5); 
                    return;
                }
                case Interaction.ActionMuteUser:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, delay); 
                    return;
                }
                case Interaction.TriggerWalkOffFurni:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 5); 
                    return;
                }

                case Interaction.TriggerCollision:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 5); 
                    return;
                }

                case Interaction.ActionGiveScore:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, "", 0); 
                    return;
                }

                case Interaction.ConditionGroupMember:
                case Interaction.ConditionNotGroupMember:
                {
					session.Router.GetComposer<WiredConditionMessageComposer> ().Compose (session,
						item, null, ""); 
                    return;
                }

                case Interaction.ConditionItemsMatches:
                case Interaction.ConditionItemsDontMatch:
                {
					session.Router.GetComposer<WiredConditionMessageComposer> ().Compose (session,
						item, list, extraString2); 
                    return;
                }

                case Interaction.ActionPosReset:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraString2, 0, list); 
                    return;
                }
                case Interaction.ActionMoveRotate:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraString2, 0, list); 
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, "", 0, list); 
                }
                    break;

                case Interaction.ActionMoveToDir:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, "", 0, list); 
                }
                    break;

                case Interaction.ActionResetTimer:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, delay, list); 
                    return;
                }
                case Interaction.ActionShowMessage:
                case Interaction.ActionKickUser:
                case Interaction.ActionEffectUser:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0, list);    
                    return;
                }
                case Interaction.ActionTeleportTo:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, delay, list);   
                    return;
                }
                case Interaction.ActionToggleState:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, delay, list);  
                    return;
                }
                case Interaction.ActionGiveReward:
                {
                    if (!session.GetHabbo().HasFuse("fuse_use_superwired")) return;
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, delay, list);  
                    return;
                }

                case Interaction.ConditionHowManyUsersInRoom:
                case Interaction.ConditionNegativeHowManyUsers:
                {
					session.Router.GetComposer<WiredConditionMessageComposer> ().Compose (session,
						item, null, "");            
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
					session.Router.GetComposer<WiredConditionMessageComposer> ().Compose (session,
						item, list, ""); 
                    return;
                }
                case Interaction.ConditionFurniHasNotFurni:
                case Interaction.ConditionFurniHasFurni:
                {
					session.Router.GetComposer<WiredConditionMessageComposer> ().Compose (session,
						item, list, "");  
                    return;
                }
                case Interaction.ConditionTimeLessThan:
                case Interaction.ConditionTimeMoreThan:
                {
					session.Router.GetComposer<WiredConditionMessageComposer> ().Compose (session,
						item, null, ""); 
                    return;
                }

                case Interaction.ConditionUserWearingEffect:
                case Interaction.ConditionUserNotWearingEffect:
                {
                    int effect;
                    int.TryParse(extraInfo, out effect);

					session.Router.GetComposer<WiredConditionMessageComposer> ().Compose (session,
						item, null, ""); 
                    return;
                }

                case Interaction.ConditionUserWearingBadge:
                case Interaction.ConditionUserNotWearingBadge:
                case Interaction.ConditionUserHasFurni:
                {
					session.Router.GetComposer<WiredConditionMessageComposer> ().Compose (session,
						item, null, extraInfo);    
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
					session.Router.GetComposer<WiredConditionMessageComposer> ().Compose (session,
						item, null, extraInfo);     
                    return;
                }
                case Interaction.ActionJoinTeam:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, delay);          
                    return;
                }
                case Interaction.ActionLeaveTeam:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0);  
                    return;
                }
                case Interaction.TriggerBotReachedAvatar:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 0);  
                    return;
                }
                case Interaction.TriggerBotReachedStuff:
                {
					session.Router.GetComposer<WiredTriggerMessageComposer> ().Compose (session,
						item, list, 0, extraInfo, 5); 
                    return;
                }
                case Interaction.ActionBotClothes:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0);  
                    return;
                }
                case Interaction.ActionBotFollowAvatar:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0);  
                    return;
                }
                case Interaction.ActionBotGiveHanditem:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, delay);     
                    return;
                }
                case Interaction.ActionBotMove:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0, list);
                    return;
                }
                case Interaction.ActionBotTalk:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0);  
                    return;
                }
                case Interaction.ActionBotTalkToAvatar:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0);  
                    return;
                }
                case Interaction.ActionBotTeleport:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0, list);  
                    return;
                }
                case Interaction.ActionChase:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0, list);  
                    return;
                }
                case Interaction.ConditionUserHasHanditem:
                {
					session.Router.GetComposer<WiredConditionMessageComposer> ().Compose (session,
						item, null, extraInfo);  
                    return;
                }
                case Interaction.ActionCallStacks:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0, list);  
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
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0, list);   
                    return;
                }
                case Interaction.SpecialUnseen:
                {
					session.Router.GetComposer<WiredEffectMessageComposer> ().Compose (session,
						item, extraInfo, 0, list);  
                    return;
                }
                default:
                    return;
            }
        }
    }
}