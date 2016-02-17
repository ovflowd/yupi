using System.Collections.Generic;
using Yupi.Game.Achievements.Structs;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pets;
using Yupi.Game.RoomBots;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Users.Badges.Models;
using Yupi.Game.Users.Inventory;
using Yupi.Game.Users.Messenger.Structs;
using Yupi.Game.Users.Relationships;
using Yupi.Game.Users.Subscriptions;

namespace Yupi.Game.Users.Data.Models
{
    /// <summary>
    ///     Class UserData.
    /// </summary>
    internal class UserData
    {
        /// <summary>
        ///     The achievements
        /// </summary>
        internal Dictionary<string, UserAchievement> Achievements;

        /// <summary>
        ///     The badges
        /// </summary>
        internal List<Badge> Badges;

        /// <summary>
        ///     The bots
        /// </summary>
        internal Dictionary<uint, RoomBot> Bots;

        /// <summary>
        ///     The effects
        /// </summary>
        internal List<AvatarEffect> Effects;

        /// <summary>
        ///     The favourited rooms
        /// </summary>
        internal List<uint> FavouritedRooms;

        /// <summary>
        ///     The friends
        /// </summary>
        internal Dictionary<uint, MessengerBuddy> Friends;

        /// <summary>
        ///     The ignores
        /// </summary>
        internal List<uint> Ignores;

        /// <summary>
        ///     The inventory
        /// </summary>
        internal List<UserItem> Inventory;

        /// <summary>
        ///     The mini mail count
        /// </summary>
        internal uint MiniMailCount;

        /// <summary>
        ///     The pets
        /// </summary>
        internal Dictionary<uint, Pet> Pets;

        /// <summary>
        ///     The quests
        /// </summary>
        internal Dictionary<int, int> Quests;

        /// <summary>
        ///     The relations
        /// </summary>
        internal Dictionary<int, Relationship> Relations;

        /// <summary>
        ///     The requests
        /// </summary>
        internal Dictionary<uint, MessengerRequest> Requests;

        /// <summary>
        ///     The rooms
        /// </summary>
        internal HashSet<RoomData> Rooms;

        /// <summary>
        ///     The subscriptions
        /// </summary>
        internal Subscription Subscriptions;

        /// <summary>
        ///     The suggested polls
        /// </summary>
        internal HashSet<uint> SuggestedPolls;

        /// <summary>
        ///     The tags
        /// </summary>
        internal List<string> Tags;

        /// <summary>
        ///     The talents
        /// </summary>
        internal Dictionary<int, UserTalent> Talents;

        /// <summary>
        ///     The user
        /// </summary>
        internal Habbo User;

        /// <summary>
        ///     The user identifier
        /// </summary>
        internal uint UserId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserData" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="achievements">The achievements.</param>
        /// <param name="talents">The talents.</param>
        /// <param name="favouritedRooms">The favourited rooms.</param>
        /// <param name="ignores">The ignores.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="sub">The sub.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="inventory">The inventory.</param>
        /// <param name="effects">The effects.</param>
        /// <param name="friends">The friends.</param>
        /// <param name="requests">The requests.</param>
        /// <param name="rooms">The rooms.</param>
        /// <param name="pets">The pets.</param>
        /// <param name="quests">The quests.</param>
        /// <param name="user">The user.</param>
        /// <param name="bots">The bots.</param>
        /// <param name="relations">The relations.</param>
        /// <param name="suggestedPolls">The suggested polls.</param>
        /// <param name="miniMailCount">The mini mail count.</param>
        public UserData(uint userId, Dictionary<string, UserAchievement> achievements,
            Dictionary<int, UserTalent> talents, List<uint> favouritedRooms, List<uint> ignores, List<string> tags,
            Subscription sub, List<Badge> badges, List<UserItem> inventory, List<AvatarEffect> effects,
            Dictionary<uint, MessengerBuddy> friends, Dictionary<uint, MessengerRequest> requests,
            HashSet<RoomData> rooms, Dictionary<uint, Pet> pets, Dictionary<int, int> quests, Habbo user,
            Dictionary<uint, RoomBot> bots, Dictionary<int, Relationship> relations, HashSet<uint> suggestedPolls,
            uint miniMailCount)
        {
            UserId = userId;
            Achievements = achievements;
            Talents = talents;
            FavouritedRooms = favouritedRooms;
            Ignores = ignores;
            Tags = tags;
            Subscriptions = sub;
            Badges = badges;
            Inventory = inventory;
            Effects = effects;
            Friends = friends;
            Requests = requests;
            Rooms = rooms;
            Pets = pets;
            Quests = quests;
            User = user;
            Bots = bots;
            Relations = relations;
            SuggestedPolls = suggestedPolls;
            MiniMailCount = miniMailCount;
        }
    }
}