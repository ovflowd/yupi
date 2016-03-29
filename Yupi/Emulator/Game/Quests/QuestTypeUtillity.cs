namespace Yupi.Game.Quests
{
    /// <summary>
    ///     Class QuestTypeUtillity.
    /// </summary>
    internal class QuestTypeUtillity
    {
        /// <summary>
        ///     Gets the string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.String.</returns>
        internal static string GetString(QuestType type)
        {
            switch (type)
            {
                case QuestType.FurniMove:
                    return "MOVE_ITEM";

                case QuestType.FurniRotate:
                    return "ROTATE_ITEM";

                case QuestType.FurniPlace:
                    return "PLACE_ITEM";

                case QuestType.FurniPick:
                    return "PICKUP_ITEM";

                case QuestType.FurniSwitch:
                    return "SWITCH_ITEM_STATE";

                case QuestType.FurniStack:
                    return "STACK_ITEM";

                case QuestType.FurniDecorationFloor:
                    return "PLACE_FLOOR";

                case QuestType.FurniDecorationWall:
                    return "PLACE_WALLPAPER";

                case QuestType.SocialVisit:
                    return "ENTER_OTHERS_ROOM";

                case QuestType.SocialChat:
                    return "CHAT_WITH_SOMEONE";

                case QuestType.SocialFriend:
                    return "REQUEST_FRIEND";

                case QuestType.SocialRespect:
                    return "GIVE_RESPECT";

                case QuestType.SocialDance:
                    return "DANCE";

                case QuestType.SocialWave:
                    return "WAVE";

                case QuestType.ProfileChangeLook:
                    return "CHANGE_FIGURE";

                case QuestType.ProfileChangeMotto:
                    return "CHANGE_MOTTO";

                case QuestType.ProfileBadge:
                    return "WEAR_BADGE";

                case QuestType.SummerEnterRoom:
                    return "ENTER_ROOM";

                case QuestType.AddFriends:
                    return "add_25_friends";

                case QuestType.WaveUsers:
                    return "wave_10_users";
            }
            return "FIND_STUFF";
        }
    }
}