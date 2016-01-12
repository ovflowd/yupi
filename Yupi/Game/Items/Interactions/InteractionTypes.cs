using Yupi.Game.Items.Interactions.Enums;

namespace Yupi.Game.Items.Interactions
{
    /// <summary>
    ///     Class InterractionTypes.
    /// </summary>
    internal static class InteractionTypes
    {
        /// <summary>
        ///     Ares the familiar.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="subType">Type of the sub.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal static bool AreFamiliar(GlobalInteractions type, Interaction subType)
        {
            switch (type)
            {
                case GlobalInteractions.Wired:
                    return AreFamiliar(GlobalInteractions.WiredCondition, subType) ||
                           AreFamiliar(GlobalInteractions.WiredEffect, subType) ||
                           AreFamiliar(GlobalInteractions.WiredTrigger, subType);

                case GlobalInteractions.Gate:
                    {
                        switch (subType)
                        {
                            case Interaction.HcGate:
                            case Interaction.Gate:
                            case Interaction.GuildGate:
                            case Interaction.OneWayGate:
                            case Interaction.VipGate:
                            case Interaction.FreezeBlueGate:
                            case Interaction.FreezeRedGate:
                            case Interaction.FreezeGreenGate:
                            case Interaction.FreezeYellowGate:
                            case Interaction.BanzaiGateBlue:
                            case Interaction.BanzaiGateRed:
                            case Interaction.BanzaiGateGreen:
                            case Interaction.BanzaiGateYellow:
                            case Interaction.FootballGate:
                                return true;
                        }
                        break;
                    }
                case GlobalInteractions.GameGate:
                    return AreFamiliar(GlobalInteractions.FreezeGate, subType) ||
                           AreFamiliar(GlobalInteractions.BanzaiGate, subType);

                case GlobalInteractions.BanzaiGate:
                    {
                        switch (subType)
                        {
                            case Interaction.BanzaiGateBlue:
                            case Interaction.BanzaiGateRed:
                            case Interaction.BanzaiGateGreen:
                            case Interaction.BanzaiGateYellow:
                                return true;
                        }
                        break;
                    }
                case GlobalInteractions.FreezeGate:
                    {
                        switch (subType)
                        {
                            case Interaction.FreezeBlueGate:
                            case Interaction.FreezeRedGate:
                            case Interaction.FreezeGreenGate:
                            case Interaction.FreezeYellowGate:
                                return true;
                        }
                        break;
                    }
                case GlobalInteractions.PetBreeding:
                {
                    switch(subType)
                    {
                            case Interaction.BreedingBear:
                            case Interaction.BreedingTerrier:
                            case Interaction.PetBreeding:
                                return true;
                    }
                    break;
                }
                case GlobalInteractions.Pet:
                    {
                        switch (subType)
                        {
                            case Interaction.PetDog:
                            case Interaction.PetCat:
                            case Interaction.PetCrocodile:
                            case Interaction.PetTerrier:
                            case Interaction.PetBear:
                            case Interaction.PetPig:
                            case Interaction.PetLion:
                            case Interaction.PetRhino:
                            case Interaction.PetSpider:
                            case Interaction.PetTurtle:
                            case Interaction.PetChick:
                            case Interaction.PetFrog:
                            case Interaction.PetDragon:
                            case Interaction.PetHorse:
                            case Interaction.PetMonkey:
                            case Interaction.PetGnomo:
                            case Interaction.PetMonsterPlant:
                            case Interaction.PetWhiteRabbit:
                            case Interaction.PetEvilRabbit:
                            case Interaction.PetLoveRabbit:
                            case Interaction.PetCafeRabbit:
                            case Interaction.PetPigeon:
                            case Interaction.PetEvilPigeon:
                            case Interaction.PetDemonMonkey:
                                return true;
                        }
                        break;
                    }
                case GlobalInteractions.WiredCondition:
                    {
                        switch (subType)
                        {
                            case Interaction.ConditionFurnisHaveUsers:
                            case Interaction.ConditionStatePos:
                            case Interaction.ConditionTimeLessThan:
                            case Interaction.ConditionTimeMoreThan:
                            case Interaction.ConditionTriggerOnFurni:
                            case Interaction.ConditionFurniHasFurni:
                            case Interaction.ConditionItemsMatches:
                            case Interaction.ConditionGroupMember:
                            case Interaction.ConditionFurniTypeMatches:
                            case Interaction.ConditionHowManyUsersInRoom:
                            case Interaction.ConditionNegativeHowManyUsers:
                            case Interaction.ConditionTriggererNotOnFurni:
                            case Interaction.ConditionFurniHasNotFurni:
                            case Interaction.ConditionFurnisHaveNotUsers:
                            case Interaction.ConditionItemsDontMatch:
                            case Interaction.ConditionFurniTypeDontMatch:
                            case Interaction.ConditionNotGroupMember:
                            case Interaction.ConditionUserWearingEffect:
                            case Interaction.ConditionUserWearingBadge:
                            case Interaction.ConditionUserHasFurni:
                            case Interaction.ConditionUserNotWearingEffect:
                            case Interaction.ConditionUserNotWearingBadge:
                            case Interaction.ConditionDateRangeActive:
                            case Interaction.ConditionUserHasHanditem:
                                return true;
                        }
                    }
                    break;

                case GlobalInteractions.WiredTrigger:
                    {
                        switch (subType)
                        {
                            case Interaction.TriggerTimer:
                            case Interaction.TriggerRoomEnter:
                            case Interaction.TriggerGameEnd:
                            case Interaction.TriggerGameStart:
                            case Interaction.TriggerRepeater:
                            case Interaction.TriggerLongRepeater:
                            case Interaction.TriggerOnUserSay:
                            case Interaction.TriggerScoreAchieved:
                            case Interaction.TriggerStateChanged:
                            case Interaction.TriggerWalkOnFurni:
                            case Interaction.TriggerWalkOffFurni:
                            case Interaction.TriggerBotReachedAvatar:
                            case Interaction.TriggerBotReachedStuff:
                            case Interaction.TriggerCollision:
                                return true;
                        }
                    }
                    break;

                case GlobalInteractions.WiredEffect:
                    {
                        switch (subType)
                        {
                            case Interaction.ActionGiveScore:
                            case Interaction.ActionPosReset:
                            case Interaction.ActionMoveRotate:
                            case Interaction.ActionMoveToDir:
                            case Interaction.ActionResetTimer:
                            case Interaction.ActionShowMessage:
                            case Interaction.ActionEffectUser:
                            case Interaction.ActionTeleportTo:
                            case Interaction.ActionToggleState:
                            case Interaction.ActionJoinTeam:
                            case Interaction.ActionLeaveTeam:
                            case Interaction.ActionBotClothes:
                            case Interaction.ActionBotFollowAvatar:
                            case Interaction.ActionBotGiveHanditem:
                            case Interaction.ActionBotMove:
                            case Interaction.ActionBotTalk:
                            case Interaction.ActionBotTalkToAvatar:
                            case Interaction.ActionBotTeleport:
                            case Interaction.ActionChase:
                            case Interaction.ActionMuteUser:
                            case Interaction.ActionKickUser:
                            case Interaction.ActionGiveReward:
                            case Interaction.ActionCallStacks:
                                return true;
                        }
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        ///     Gets the type from string.
        /// </summary>
        /// <param name="pType">Type of the p.</param>
        /// <returns>InteractionType.</returns>
        internal static Interaction GetTypeFromString(string pType)
        {
            switch (pType)
            {
                case "":
                case "default":
                    return Interaction.None;

                case "gate":
                    return Interaction.Gate;

                case "postit":
                    return Interaction.PostIt;

                case "roomeffect":
                    return Interaction.RoomEffect;

                case "dimmer":
                    return Interaction.Dimmer;

                case "trophy":
                    return Interaction.Trophy;

                case "bed":
                    return Interaction.Bed;

                case "guillotine":
                case "hween12_guillotine":
                    return Interaction.Guillotine;

                case "scoreboard":
                    return Interaction.ScoreBoard;

                case "vendingmachine":
                    return Interaction.VendingMachine;

                case "alert":
                    return Interaction.Alert;

                case "onewaygate":
                    return Interaction.OneWayGate;

                case "loveshuffler":
                    return Interaction.LoveShuffler;

                case "habbowheel":
                    return Interaction.HabboWheel;

                case "dice":
                    return Interaction.Dice;

                case "hopper":
                    return Interaction.Hopper;

                case "bottle":
                    return Interaction.Bottle;

                case "teleport":
                    return Interaction.Teleport;

                case "rentals":
                    return Interaction.Rentals;

                case "pet":
                    return Interaction.Pet;

                case "pool":
                    return Interaction.Pool;

                case "roller":
                    return Interaction.Roller;

                case "fbgate":
                    return Interaction.FootballGate;

                case "pet_dog":
                    return Interaction.PetDog;

                case "pet_cat":
                    return Interaction.PetCat;

                case "pet_croco":
                    return Interaction.PetCrocodile;

                case "pet_terrier":
                    return Interaction.PetTerrier;

                case "pet_bear":
                    return Interaction.PetBear;

                case "pet_pig":
                    return Interaction.PetPig;

                case "pet_lion":
                    return Interaction.PetLion;

                case "pet_rhino":
                    return Interaction.PetRhino;

                case "pet_spider":
                    return Interaction.PetSpider;

                case "pet_turtle":
                    return Interaction.PetTurtle;

                case "pet_chicken":
                    return Interaction.PetChick;

                case "pet_frog":
                    return Interaction.PetFrog;

                case "pet_dragon":
                    return Interaction.PetDragon;

                case "pet_horse":
                    return Interaction.PetHorse;

                case "pet_monkey":
                    return Interaction.PetMonkey;

                case "pet_gnomo":
                    return Interaction.PetGnomo;

                case "pet_monster":
                    return Interaction.PetMonsterPlant;

                case "pet_bunnyeaster":
                    return Interaction.PetWhiteRabbit;

                case "pet_bunnyevil":
                    return Interaction.PetEvilRabbit;

                case "pet_bunnylove":
                    return Interaction.PetLoveRabbit;

                case "pet_bunnydepressed":
                    return Interaction.PetCafeRabbit;

                case "pet_pigeongood":
                    return Interaction.PetPigeon;

                case "pet_pigeonevil":
                    return Interaction.PetEvilPigeon;

                case "pet_demonmonkey":
                    return Interaction.PetDemonMonkey;

                case "iceskates":
                    return Interaction.IceSkates;

                case "rollerskate":
                    return Interaction.Normslaskates;

                case "lowpool":
                    return Interaction.LowPool;

                case "haloweenpool":
                case "halloweenpool":
                    return Interaction.HaloweenPool;

                case "snowboardslope":
                    return Interaction.SnowBoardSlope;

                case "ball":
                case "football":
                    return Interaction.Football;

                case "jump":
                    return Interaction.Jump;

                case "green_goal":
                    return Interaction.FootballGoalGreen;

                case "yellow_goal":
                    return Interaction.FootballGoalYellow;

                case "red_goal":
                    return Interaction.FootballGoalRed;

                case "blue_goal":
                    return Interaction.FootballGoalBlue;

                case "green_score":
                    return Interaction.FootballCounterGreen;

                case "yellow_score":
                    return Interaction.FootballCounterYellow;

                case "blue_score":
                    return Interaction.FootballCounterBlue;

                case "red_score":
                    return Interaction.FootballCounterRed;

                case "bb_blue_gate":
                case "banzaigateblue":
                    return Interaction.BanzaiGateBlue;

                case "bb_red_gate":
                case "banzaigatered":
                    return Interaction.BanzaiGateRed;

                case "bb_yellow_gate":
                case "banzaigateyellow":
                    return Interaction.BanzaiGateYellow;

                case "bb_green_gate":
                case "banzaigategreen":
                    return Interaction.BanzaiGateGreen;

                case "bb_patch":
                case "banzaifloor":
                    return Interaction.BanzaiFloor;

                case "bb_blue_score":
                case "banzaiscoreblue":
                    return Interaction.BanzaiScoreBlue;

                case "bb_red_score":
                case "banzaiscorered":
                    return Interaction.BanzaiScoreRed;

                case "bb_yellow_score":
                case "banzaiscoreyellow":
                    return Interaction.BanzaiScoreYellow;

                case "bb_green_score":
                case "banzaiscoregreen":
                    return Interaction.BanzaiScoreGreen;

                case "banzaicounter":
                    return Interaction.BanzaiCounter;

                case "bb_teleport":
                case "banzaitele":
                    return Interaction.BanzaiTele;

                case "banzaipuck":
                    return Interaction.BanzaiPuck;

                case "bb_pyramid":
                case "banzaipyramid":
                    return Interaction.BanzaiPyramid;

                case "freezetimer":
                    return Interaction.FreezeTimer;

                case "freezeexit":
                    return Interaction.FreezeExit;

                case "freezeredcounter":
                    return Interaction.FreezeRedCounter;

                case "freezebluecounter":
                    return Interaction.FreezeBlueCounter;

                case "freezeyellowcounter":
                    return Interaction.FreezeYellowCounter;

                case "freezegreencounter":
                    return Interaction.FreezeGreenCounter;

                case "freezeyellowgate":
                    return Interaction.FreezeYellowGate;

                case "freezeredgate":
                    return Interaction.FreezeRedGate;

                case "freezegreengate":
                    return Interaction.FreezeGreenGate;

                case "freezebluegate":
                    return Interaction.FreezeBlueGate;

                case "freezetileblock":
                    return Interaction.FreezeTileBlock;

                case "freezetile":
                    return Interaction.FreezeTile;

                case "jukebox":
                    return Interaction.JukeBox;

                case "musicdisc":
                    return Interaction.MusicDisc;

                case "triggertimer":
                case "wf_trg_at_given_time":
                    return Interaction.TriggerTimer;

                case "triggerroomenter":
                    return Interaction.TriggerRoomEnter;

                case "triggergameend":
                    return Interaction.TriggerGameEnd;

                case "triggergamestart":
                    return Interaction.TriggerGameStart;

                case "wf_trg_periodically":
                case "triggerrepeater":
                    return Interaction.TriggerRepeater;

                case "triggerlongrepeater":
                case "triggerlongperiodic":
                case "wf_trg_period_long":
                    return Interaction.TriggerLongRepeater;

                case "triggeronusersay":
                    return Interaction.TriggerOnUserSay;

                case "triggerscoreachieved":
                case "wf_trg_score_achieved":
                    return Interaction.TriggerScoreAchieved;

                case "wf_trg_state_changed":
                case "triggerstatechanged":
                    return Interaction.TriggerStateChanged;

                case "wf_trg_walks_on_furni":
                case "triggerwalkonfurni":
                    return Interaction.TriggerWalkOnFurni;

                case "wf_trg_walks_off_furni":
                case "triggerwalkofffurni":
                    return Interaction.TriggerWalkOffFurni;

                case "wf_trg_collision":
                case "triggercollision":
                    return Interaction.TriggerCollision;

                case "actiongivescore":
                case "actiongiveteamscore":
                case "wf_act_give_score":
                    return Interaction.ActionGiveScore;

                case "actionposreset":
                case "wf_act_match_to_sshot":
                    return Interaction.ActionPosReset;

                case "actionmoverotate":
                case "wf_act_move_rotate":
                    return Interaction.ActionMoveRotate;

                case "actionmovetodir":
                case "wf_act_move_to_dir":
                    return Interaction.ActionMoveToDir;

                case "actionresettimer":
                case "wf_act_reset_timers":
                    return Interaction.ActionResetTimer;

                case "actionshowmessage":
                case "wf_act_show_message":
                    return Interaction.ActionShowMessage;

                case "actioneffectuser":
                case "action_effect_user":
                    return Interaction.ActionEffectUser;

                case "actionteleportto":
                case "wf_act_moveuser":
                    return Interaction.ActionTeleportTo;

                case "wf_act_toggle_state":
                case "actiontogglestate":
                    return Interaction.ActionToggleState;

                case "actionkickuser":
                case "wf_act_kick_user":
                    return Interaction.ActionKickUser;

                case "actiongivereward":
                case "wf_act_give_reward":
                    return Interaction.ActionGiveReward;

                case "conditionfurnishaveusers":
                case "wf_cnd_furnis_hv_avtrs":
                    return Interaction.ConditionFurnisHaveUsers;

                case "conditionstatepos":
                    return Interaction.ConditionStatePos;

                case "conditiontimelessthan":
                    return Interaction.ConditionTimeLessThan;

                case "conditiontimemorethan":
                    return Interaction.ConditionTimeMoreThan;

                case "conditiontriggeronfurni":
                case "wf_cnd_trggrer_on_frn":
                    return Interaction.ConditionTriggerOnFurni;

                case "conditionfurnihasfurni":
                case "wf_cnd_has_furni_on":
                case "conditionhasfurnion":
                    return Interaction.ConditionFurniHasFurni;

                case "pressure_pad":
                    return Interaction.PressurePad;

                case "pressure_pad_bed":
                    return Interaction.PressurePadBed;

                case "colorwheel":
                    return Interaction.ColorWheel;

                case "switch":
                    return Interaction.FloorSwitch1;

                case "floorswitch2":
                    return Interaction.FloorSwitch2;

                case "specialrandom":
                    return Interaction.SpecialRandom;

                case "specialunseen":
                    return Interaction.SpecialUnseen;

                case "puzzlebox":
                    return Interaction.PuzzleBox;

                case "water":
                    return Interaction.Pool;

                case "gift":
                    return Interaction.Gift;

                case "background":
                    return Interaction.Background;

                case "mannequin":
                    return Interaction.Mannequin;

                case "vip_gate":
                    return Interaction.VipGate;

                case "bgupdater":
                case "roombg":
                    return Interaction.RoomBg;

                case "ads_mpu":
                case "adsmpu":
                    return Interaction.AdsMpu;

                case "mystery_box":
                    return Interaction.MysteryBox;

                case "wear_item":
                    return Interaction.WearItem;

                case "gld_item":
                case "glditem":
                    return Interaction.GuildItem;

                case "gld_forum":
                case "guild_forum":
                case "guildforum":
                    return Interaction.GuildForum;

                case "guildgate":
                case "gld_gate":
                    return Interaction.GuildGate;

                case "badge_display":
                    return Interaction.BadgeDisplay;

                case "pinata":
                    return Interaction.Pinata;

                case "tilestackmagic":
                    return Interaction.TileStackMagic;

                case "tent":
                    return Interaction.Tent;

                case "bedtent":
                    return Interaction.BedTent;

                case "poster":
                    return Interaction.Poster;

                case "runwaysage":
                    return Interaction.RunWaySage;

                case "shower":
                    return Interaction.Shower;

                case "youtubetv":
                case "yttv":
                    return Interaction.YoutubeTv;

                case "moplaseed":
                    return Interaction.Moplaseed;

                case "raremoplaseed":
                    return Interaction.RareMoplaSeed;

                case "fireworks":
                    return Interaction.Fireworks;

                case "groupforumterminal":
                    return Interaction.GroupForumTerminal;

                case "vikingcotie":
                    return Interaction.VikingCotie;

                case "cannon":
                    return Interaction.Cannon;

                case "petball":
                    return Interaction.PetBall;

                case "petnest":
                    return Interaction.PetNest;

                case "petfood":
                    return Interaction.PetFood;

                case "petdrink":
                    return Interaction.PetDrink;

                case "petbreeding":
                    return Interaction.PetBreeding;

                case "breedingterrier":
                    return Interaction.BreedingTerrier;

                case "breedingbear":
                    return Interaction.BreedingBear;

                case "conditionitemsmatches":
                case "wf_cnd_match_snapshot":
                    return Interaction.ConditionItemsMatches;

                case "conditiongroupmember":
                case "wf_cnd_actor_in_group":
                case "conditionactoringroup":
                    return Interaction.ConditionGroupMember;

                case "conditionfurnitypematches":
                case "wf_cnd_stuff_is":
                case "conditionstuffis":
                    return Interaction.ConditionFurniTypeMatches;

                case "conditionhowmanyusersinroom":
                case "wf_cnd_user_count_in":
                case "conditionusercountin":
                    return Interaction.ConditionHowManyUsersInRoom;

                case "conditionitemsdontmatch":
                case "wf_cnd_not_match_snap":
                case "conditionnotstatepos":
                    return Interaction.ConditionItemsDontMatch;

                case "conditionfurnitypedontmatch":
                case "wf_cnd_not_stuff_is":
                case "conditionnotstuffis":
                    return Interaction.ConditionFurniTypeDontMatch;

                case "conditionnotgroupmember":
                case "wf_cnd_not_in_group":
                case "conditionnotingroup":
                    return Interaction.ConditionNotGroupMember;

                case "conditionnotusercount":
                case "wf_cnd_not_user_count":
                case "conditionnegativehowmanyusers":
                    return Interaction.ConditionNegativeHowManyUsers;

                case "conditionnotfurnishaveusers":
                case "wf_cnd_not_hv_avtrs":
                case "conditionfurnishavenotusers":
                    return Interaction.ConditionFurnisHaveNotUsers;

                case "conditionfurnihasnotfurni":
                case "wf_cnd_not_furni_on":
                case "conditionnotfurnion":
                    return Interaction.ConditionFurniHasNotFurni;

                case "conditiontriggerernotonfurni":
                case "wf_cnd_not_trggrer_on":
                case "conditionnottriggeronfurni":
                    return Interaction.ConditionTriggererNotOnFurni;

                case "actionmuteuser":
                case "wf_act_mute_triggerer":
                    return Interaction.ActionMuteUser;

                case "conditionuserwearingeffect":
                case "conditionwearingeffect":
                case "wf_cnd_wearing_effect":
                    return Interaction.ConditionUserWearingEffect;

                case "conditionnotwearingeffect":
                case "wf_cnd_not_wearing_fx":
                case "conditionusernotwearingeffect":
                    return Interaction.ConditionUserNotWearingEffect;

                case "conditionuserwearingbadge":
                case "conditionwearingbadge":
                case "wf_cnd_wearing_badge":
                    return Interaction.ConditionUserWearingBadge;

                case "conditionuserhasfurni":
                    return Interaction.ConditionUserHasFurni;

                case "conditionusernotwearingbadge":
                case "conditionnotwearingbadge":
                case "wf_cnd_not_wearing_b":
                    return Interaction.ConditionUserNotWearingBadge;

                case "conditiondaterangeactive":
                case "conditiondaterange":
                case "wf_cnd_date_rng_active":
                    return Interaction.ConditionDateRangeActive;

                case "lovelock":
                    return Interaction.LoveLock;

                case "wiredhighscore":
                    return Interaction.WiredHighscore;

                case "actionjointeam":
                case "wf_act_join_team":
                    return Interaction.ActionJoinTeam;

                case "actionleaveteam":
                case "wf_act_leave_team":
                    return Interaction.ActionLeaveTeam;

                case "wf_trg_bot_reached_stf":
                    return Interaction.TriggerBotReachedStuff;

                case "wf_trg_bot_reached_avtr":
                    return Interaction.TriggerBotReachedAvatar;

                case "wf_act_bot_follow_avatar":
                    return Interaction.ActionBotFollowAvatar;

                case "wf_act_bot_clothes":
                    return Interaction.ActionBotClothes;

                case "wf_act_bot_talk_to_avatar":
                    return Interaction.ActionBotTalkToAvatar;

                case "wf_act_bot_move":
                    return Interaction.ActionBotMove;

                case "wf_act_bot_teleport":
                    return Interaction.ActionBotTeleport;

                case "wf_act_bot_give_handitem":
                    return Interaction.ActionBotGiveHanditem;

                case "wf_act_bot_talk":
                    return Interaction.ActionBotTalk;

                case "actionchase":
                case "wf_act_chase":
                    return Interaction.ActionChase;

                case "wf_cnd_has_handitem":
                    return Interaction.ConditionUserHasHanditem;

                case "clothing":
                    return Interaction.Clothing;

                case "fxbox":
                    return Interaction.FxBox;

                case "hc_gate":
                    return Interaction.HcGate;

                case "quick_teleport":
                    return Interaction.QuickTeleport;

                case "chair_state":
                    return Interaction.ChairState;

                case "crackable_egg":
                    return Interaction.CrackableEgg;

                case "actioncallstacks":
                case "wf_act_call_stacks":
                    return Interaction.ActionCallStacks;

                case "walk_internal_link":
                    return Interaction.WalkInternalLink;
            }
            return Interaction.None;
        }
    }
}