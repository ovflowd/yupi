using System.Collections.Generic;
using Yupi.Emulator.Game.Achievements.Structs;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.RoomBots;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Users.Badges.Models;
using Yupi.Emulator.Game.Users.Inventory;
using Yupi.Emulator.Game.Users.Messenger.Structs;
using Yupi.Emulator.Game.Users.Relationships;
using Yupi.Emulator.Game.Users.Subscriptions;

namespace Yupi.Emulator.Game.Users.Data.Models
{
    /// <summary>
    ///     Class UserData.
    /// </summary>
     public class UserData
    {
        /// <summary>
        ///     The achievements
        /// </summary>
     public Dictionary<string, UserAchievement> Achievements;

        /// <summary>
        ///     The badges
        /// </summary>
     public List<Badge> Badges;

        /// <summary>
        ///     The bots
        /// </summary>
     public Dictionary<uint, RoomBot> Bots;

        /// <summary>
        ///     The effects
        /// </summary>
     public List<AvatarEffect> Effects;

        /// <summary>
        ///     The favourited rooms
        /// </summary>
     public List<uint> FavouritedRooms;

        /// <summary>
        ///     The friends
        /// </summary>
     public Dictionary<uint, MessengerBuddy> Friends;

        /// <summary>
        ///     The ignores
        /// </summary>
     public List<uint> Ignores;

        /// <summary>
        ///     The inventory
        /// </summary>
     public List<UserItem> Inventory;

        /// <summary>
        ///     The mini mail count
        /// </summary>
     public uint MiniMailCount;

        /// <summary>
        ///     The pets
        /// </summary>
     public Dictionary<uint, Pet> Pets;

        /// <summary>
        ///     The quests
        /// </summary>
     public Dictionary<int, int> Quests;

        /// <summary>
        ///     The relations
        /// </summary>
     public Dictionary<int, Relationship> Relations;

        /// <summary>
        ///     The requests
        /// </summary>
     public Dictionary<uint, MessengerRequest> Requests;

        /// <summary>
        ///     The rooms
        /// </summary>
     public HashSet<RoomData> Rooms;

        /// <summary>
        ///     The subscriptions
        /// </summary>
     public Subscription Subscriptions;

        /// <summary>
        ///     The suggested polls
        /// </summary>
     public HashSet<uint> SuggestedPolls;

        /// <summary>
        ///     The tags
        /// </summary>
     public List<string> Tags;

        /// <summary>
        ///     The talents
        /// </summary>
     public Dictionary<int, UserTalent> Talents;

        /// <summary>
        ///     The user
        /// </summary>
     public Habbo User;

        /// <summary>
        ///     The user identifier
        /// </summary>
     public uint UserId;

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