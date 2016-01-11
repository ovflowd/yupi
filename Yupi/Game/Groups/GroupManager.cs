/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Groups.Structs;
using Yupi.Game.Rooms;
using Yupi.Game.Users;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Groups
{
    /// <summary>
    ///     Class GroupManager.
    /// </summary>
    internal class GroupManager
    {
        /// <summary>
        ///     The back ground colours
        /// </summary>
        internal HybridDictionary BackGroundColours;

        /// <summary>
        ///     The base colours
        /// </summary>
        internal HashSet<GroupBaseColours> BaseColours;

        /// <summary>
        ///     The bases
        /// </summary>
        internal HashSet<GroupBases> Bases;

        /// <summary>
        ///     The groups
        /// </summary>
        internal HybridDictionary Groups;

        /// <summary>
        ///     The symbol colours
        /// </summary>
        internal HybridDictionary SymbolColours;

        /// <summary>
        ///     The symbols
        /// </summary>
        internal HashSet<GroupSymbols> Symbols;

        /// <summary>
        ///     Initializes the groups.
        /// </summary>
        internal void InitGroups()
        {
            Bases = new HashSet<GroupBases>();
            Symbols = new HashSet<GroupSymbols>();
            BaseColours = new HashSet<GroupBaseColours>();
            SymbolColours = new HybridDictionary();
            BackGroundColours = new HybridDictionary();
            Groups = new HybridDictionary();

            ClearInfo();

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery("SELECT * FROM groups_badges_parts ORDER BY id");

                DataTable table = commitableQueryReactor.GetTable();

                if (table == null)
                    return;

                foreach (DataRow row in table.Rows)
                {
                    switch (row["type"].ToString().ToLower())
                    {
                        case "base":
                            Bases.Add(new GroupBases(int.Parse(row["id"].ToString()), row["code"].ToString(),
                                row["second_code"].ToString()));
                            break;
                        case "symbol":
                            Symbols.Add(new GroupSymbols(int.Parse(row["id"].ToString()), row["code"].ToString(),
                                row["second_code"].ToString()));
                            break;
                        case "base_color":
                            BaseColours.Add(new GroupBaseColours(int.Parse(row["id"].ToString()), row["code"].ToString()));
                            break;
                        case "symbol_color":
                            SymbolColours.Add(int.Parse(row["id"].ToString()),
                                new GroupSymbolColours(int.Parse(row["id"].ToString()), row["code"].ToString()));
                            break;
                        case "other_color":
                            BackGroundColours.Add(int.Parse(row["id"].ToString()),
                                new GroupBackGroundColours(int.Parse(row["id"].ToString()), row["code"].ToString()));
                            break;
                    }
                }
            }
        }

        /// <summary>
        ///     Clears the information.
        /// </summary>
        internal void ClearInfo()
        {
            Bases.Clear();
            Symbols.Clear();
            BaseColours.Clear();
            SymbolColours.Clear();
            BackGroundColours.Clear();
        }

        /// <summary>
        ///     Creates the theGroup.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="session">The session.</param>
        /// <param name="colour1">The colour1.</param>
        /// <param name="colour2">The colour2.</param>
        /// <param name="group">The theGroup.</param>
        internal void CreateGroup(string name, string desc, uint roomId, string badge, GameClient session, int colour1,
            int colour2, out Group group)
        {
            Habbo user = session.GetHabbo();
            Dictionary<uint, GroupMember> emptyDictionary = new Dictionary<uint, GroupMember>();

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"INSERT INTO groups_data (group_name, group_description, group_badge, owner_id, created, room_id, colour1, colour2) VALUES(@name,@desc,@badge,'{session.GetHabbo().Id}',UNIX_TIMESTAMP(),'{roomId}','{colour1}','{colour2}')");
                commitableQueryReactor.AddParameter("name", name);
                commitableQueryReactor.AddParameter("desc", desc);
                commitableQueryReactor.AddParameter("badge", badge);

                uint id = (uint) commitableQueryReactor.InsertQuery();

                commitableQueryReactor.RunFastQuery($"UPDATE rooms_data SET group_id='{id}' WHERE id='{roomId}' LIMIT 1");

                GroupMember memberGroup = new GroupMember(user.Id, user.UserName, user.Look, id, 2, Yupi.GetUnixTimeStamp());
                Dictionary<uint, GroupMember> dictionary = new Dictionary<uint, GroupMember> {{session.GetHabbo().Id, memberGroup}};

                group = new Group(id, name, desc, roomId, badge, Yupi.GetUnixTimeStamp(), user.Id, colour1, colour2,
                    dictionary, emptyDictionary, emptyDictionary, 0, 1,
                    new GroupForum(0, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 1, 1, 2));

                Groups.Add(id, group);

                commitableQueryReactor.RunFastQuery(
                    $"INSERT INTO groups_members (group_id, user_id, rank, date_join) VALUES ('{id}','{session.GetHabbo().Id}','2','{Yupi.GetUnixTimeStamp()}')");

                Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

                if (room != null)
                {
                    room.RoomData.Group = group;
                    room.RoomData.GroupId = id;
                }

                user.UserGroups.Add(memberGroup);
                group.Admins.Add(user.Id, memberGroup);

                commitableQueryReactor.RunFastQuery(
                    $"UPDATE users_stats SET favourite_group='{id}' WHERE id='{user.Id}' LIMIT 1");
                commitableQueryReactor.RunFastQuery($"DELETE FROM rooms_rights WHERE room_id='{roomId}'");
            }
        }

        /// <summary>
        ///     Gets the theGroup.
        /// </summary>
        /// <param name="groupId">The theGroup identifier.</param>
        /// <returns>Guild.</returns>
        internal Group GetGroup(uint groupId)
        {
            if (Groups == null)
                return null;

            if (Groups.Contains(groupId))
                return (Group) Groups[groupId];

            Dictionary<uint, GroupMember> members = new Dictionary<uint, GroupMember>();
            Dictionary<uint, GroupMember> admins = new Dictionary<uint, GroupMember>();
            Dictionary<uint, GroupMember> requests = new Dictionary<uint, GroupMember>();

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery($"SELECT * FROM groups_data WHERE id ='{groupId}' LIMIT 1");

                DataRow row = commitableQueryReactor.GetRow();

                if (row == null)
                    return null;

                commitableQueryReactor.SetQuery($"SELECT * FROM groups_forums_data WHERE group_id='{groupId}' LIMIT 1");

                DataRow row2 = commitableQueryReactor.GetRow();

                GroupForum groupForum;

                if (row2 == null)
                    groupForum = new GroupForum(0, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 1, 1, 2);
                else
                    groupForum = new GroupForum((uint) row2["id"], row2["forum_name"].ToString(),
                        row2["forum_description"].ToString(),
                        (uint) row2["forum_messages_count"], double.Parse(row2["forum_score"].ToString()),
                        (uint) row2["forum_lastposter_id"], row2["forum_lastposter_name"].ToString(),
                        (uint) row2["forum_lastposter_timestamp"],
                        (uint) row2["who_can_read"], (uint) row2["who_can_post"], (uint) row2["who_can_thread"],
                        (uint) row2["who_can_mod"]);


                commitableQueryReactor.SetQuery(
                    "SELECT g.user_id, u.username, u.look, g.rank, g.date_join FROM groups_members g " +
                    $"INNER JOIN users u ON (g.user_id = u.id) WHERE g.group_id='{groupId}'");

                DataTable groupMembersTable = commitableQueryReactor.GetTable();

                commitableQueryReactor.SetQuery("SELECT g.user_id, u.username, u.look FROM groups_requests g " +
                                                $"INNER JOIN users u ON (g.user_id = u.id) WHERE group_id='{groupId}'");

                DataTable groupRequestsTable = commitableQueryReactor.GetTable();

                uint userId;

                foreach (DataRow dataRow in groupMembersTable.Rows)
                {
                    userId = (uint) dataRow["user_id"];

                    int rank = int.Parse(dataRow["rank"].ToString());

                    GroupMember membGroup = new GroupMember(userId, dataRow["username"].ToString(), dataRow["look"].ToString(),
                        groupId, rank, (int) dataRow["date_join"]);

                    members.Add(userId, membGroup);

                    if (rank >= 1)
                        admins.Add(userId, membGroup);
                }

                foreach (DataRow dataRow in groupRequestsTable.Rows)
                {
                    userId = (uint) dataRow["user_id"];

                    GroupMember membGroup = new GroupMember(userId, dataRow["username"].ToString(), dataRow["look"].ToString(),
                        groupId, 0, Yupi.GetUnixTimeStamp());

                    if (!requests.ContainsKey(userId))
                        requests.Add(userId, membGroup);
                }

                Group group = new Group((uint) row["id"], row["group_name"].ToString(),
                    row["group_description"].ToString(), (uint) row["room_id"],
                    row["group_badge"].ToString(), (int) row["created"], (uint) row["owner_id"], (int) row["colour1"],
                    (int) row["colour2"], members, requests,
                    admins, Convert.ToUInt16(row["state"]), Convert.ToUInt16(row["admindeco"]), groupForum);

                Groups.Add((uint) row["id"], group);

                return group;
            }
        }

        /// <summary>
        ///     Gets the user groups.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>HashSet&lt;GroupUser&gt;.</returns>
        internal HashSet<GroupMember> GetUserGroups(uint userId)
        {
            HashSet<GroupMember> list = new HashSet<GroupMember>();

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"SELECT u.username, u.look, g.group_id, g.rank, g.date_join FROM groups_members g INNER JOIN users u ON (g.user_id = u.id) WHERE g.user_id={userId}");

                DataTable table = commitableQueryReactor.GetTable();

                foreach (DataRow dataRow in table.Rows)
                    list.Add(new GroupMember(userId, dataRow["username"].ToString(), dataRow["look"].ToString(),
                        (uint) dataRow["group_id"], Convert.ToInt16(dataRow["rank"]), (int) dataRow["date_join"]));
            }

            return list;
        }

        internal void AddGroupMemberIntoResponse(ServerMessage response, GroupMember member)
        {
            response.AppendInteger(member.Rank == 2 ? 0 : member.Rank == 1 ? 1 : 2);
            response.AppendInteger(member.Id);
            response.AppendString(member.Name);
            response.AppendString(member.Look);
            response.AppendString(Yupi.GetGroupDateJoinString(member.DateJoin));
        }

        /// <summary>
        ///     Serializes the theGroup members.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="theGroup">The theGroup.</param>
        /// <param name="reqType">Type of the req.</param>
        /// <param name="session">The session.</param>
        /// <param name="searchVal">The search value.</param>
        /// <param name="page">The page.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeGroupMembers(ServerMessage response, Group theGroup, uint reqType,
            GameClient session, string searchVal = "", int page = 0)
        {
            if (theGroup == null || session == null)
                return null;

            if (page < 1)
                page = 0;

            response.AppendInteger(theGroup.Id);
            response.AppendString(theGroup.Name);
            response.AppendInteger(theGroup.RoomId);
            response.AppendString(theGroup.Badge);

            List<List<GroupMember>> list = Split(GetGroupUsersByString(theGroup, searchVal, reqType));

            if (reqType == 0)
            {
                response.AppendInteger(list.Count);

                if (theGroup.Members.Count > 0 && list.Count > 0 && list[page] != null)
                {
                    response.AppendInteger(list[page].Count);

                    using (List<GroupMember>.Enumerator enumerator = list[page].GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            GroupMember current = enumerator.Current;

                            AddGroupMemberIntoResponse(response, current);
                        }
                    }
                }
                else
                    response.AppendInteger(0);
            }
            else if (reqType == 1)
            {
                response.AppendInteger(theGroup.Admins.Count);

                List<GroupMember> paging = page <= list.Count ? list[page] : null;

                if ((theGroup.Admins.Count > 0) && (list.Count > 0) && paging != null)
                {
                    response.AppendInteger(list[page].Count);

                    using (List<GroupMember>.Enumerator enumerator = list[page].GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            GroupMember current = enumerator.Current;

                            AddGroupMemberIntoResponse(response, current);
                        }
                    }
                }
                else
                    response.AppendInteger(0);
            }
            else if (reqType == 2)
            {
                response.AppendInteger(theGroup.Requests.Count);

                if (theGroup.Requests.Count > 0 && list.Count > 0 && list[page] != null)
                {
                    response.AppendInteger(list[page].Count);

                    using (List<GroupMember>.Enumerator enumerator = list[page].GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            GroupMember current = enumerator.Current;

                            response.AppendInteger(3);

                            if (current != null)
                            {
                                response.AppendInteger(current.Id);
                                response.AppendString(current.Name);
                                response.AppendString(current.Look);
                            }

                            response.AppendString(string.Empty);
                        }
                    }
                }
                else
                    response.AppendInteger(0);
            }

            response.AppendBool(session.GetHabbo().Id == theGroup.CreatorId);
            response.AppendInteger(14);
            response.AppendInteger(page);
            response.AppendInteger(reqType);
            response.AppendString(searchVal);

            return response;
        }

        /// <summary>
        ///     Gets the theGroup users by string.
        /// </summary>
        /// <param name="theGroup">The theGroup.</param>
        /// <param name="searchVal">The search value.</param>
        /// <param name="req">The req.</param>
        /// <returns>List&lt;GroupUser&gt;.</returns>
        internal List<GroupMember> GetGroupUsersByString(Group theGroup, string searchVal, uint req)
        {
            List<GroupMember> list = new List<GroupMember>();

            switch (req)
            {
                case 0:
                    using (Dictionary<uint, GroupMember>.ValueCollection.Enumerator enumerator = theGroup.Members.Values.GetEnumerator())
                        while (enumerator.MoveNext())
                            list.Add(enumerator.Current);
                    break;

                case 1:
                    using (Dictionary<uint, GroupMember>.ValueCollection.Enumerator enumerator2 = theGroup.Admins.Values.GetEnumerator())
                        while (enumerator2.MoveNext())
                            list.Add(enumerator2.Current);
                    break;

                case 2:
                    list = GetGroupRequestsByString(theGroup, searchVal);
                    break;
            }

            if (!string.IsNullOrWhiteSpace(searchVal))
                list = list.Where(member => member.Name.ToLower().Contains(searchVal.ToLower())).ToList();

            return list;
        }

        /// <summary>
        ///     Gets the theGroup requests by string.
        /// </summary>
        /// <param name="theGroup">The theGroup.</param>
        /// <param name="searchVal">The search value.</param>
        /// <returns>List&lt;System.UInt32&gt;.</returns>
        internal List<GroupMember> GetGroupRequestsByString(Group theGroup, string searchVal)
            =>
                string.IsNullOrWhiteSpace(searchVal)
                    ? theGroup.Requests.Values.ToList()
                    : theGroup.Requests.Values.Where(request => request.Name.ToLower().Contains(searchVal.ToLower()))
                        .ToList();

        /// <summary>
        ///     Serializes the theGroup information.
        /// </summary>
        /// <param name="group">The theGroup.</param>
        /// <param name="response">The response.</param>
        /// <param name="session">The session.</param>
        /// <param name="newWindow">if set to <c>true</c> [new window].</param>
        internal void SerializeGroupInfo(Group group, ServerMessage response, GameClient session, bool newWindow = false)
        {
            if (group == null || session == null)
                return;

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime dateTime2 = dateTime.AddSeconds(group.CreateTime);

            response.Init(LibraryParser.OutgoingRequest("GroupDataMessageComposer"));

            response.AppendInteger(group.Id);
            response.AppendBool(true);
            response.AppendInteger(group.State);
            response.AppendString(group.Name);
            response.AppendString(group.Description);
            response.AppendString(group.Badge);
            response.AppendInteger(group.RoomId);
            response.AppendString(Yupi.GetGame().GetRoomManager().GenerateRoomData(@group.RoomId) == null
                ? "No room found.."
                : Yupi.GetGame().GetRoomManager().GenerateRoomData(@group.RoomId).Name);
            response.AppendInteger(@group.CreatorId == session.GetHabbo().Id
                ? 3
                : (group.Requests.ContainsKey(session.GetHabbo().Id)
                    ? 2
                    : (group.Members.ContainsKey(session.GetHabbo().Id) ? 1 : 0)));
            response.AppendInteger(group.Members.Count);
            response.AppendBool(session.GetHabbo().FavouriteGroup == group.Id);
            response.AppendString($"{dateTime2.Day.ToString("00")}-{dateTime2.Month.ToString("00")}-{dateTime2.Year}");
            response.AppendBool(group.CreatorId == session.GetHabbo().Id);
            response.AppendBool(group.Admins.ContainsKey(session.GetHabbo().Id));
            response.AppendString(Yupi.GetHabboById(@group.CreatorId) == null
                ? string.Empty
                : Yupi.GetHabboById(group.CreatorId).UserName);
            response.AppendBool(newWindow);
            response.AppendBool(group.AdminOnlyDeco == 0u);
            response.AppendInteger(group.Requests.Count);
            response.AppendBool(group.Forum.Id != 0);
            session.SendMessage(response);
        }

        /// <summary>
        ///     Serializes the theGroup information.
        /// </summary>
        /// <param name="group">The theGroup.</param>
        /// <param name="response">The response.</param>
        /// <param name="session">The session.</param>
        /// <param name="room">The room.</param>
        /// <param name="newWindow">if set to <c>true</c> [new window].</param>
        internal void SerializeGroupInfo(Group group, ServerMessage response, GameClient session, Room room,
            bool newWindow = false)
        {
            if (room == null || group == null)
                return;

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime dateTime2 = dateTime.AddSeconds(group.CreateTime);

            response.Init(LibraryParser.OutgoingRequest("GroupDataMessageComposer"));

            response.AppendInteger(group.Id);
            response.AppendBool(true);
            response.AppendInteger(group.State);
            response.AppendString(group.Name);
            response.AppendString(group.Description);
            response.AppendString(group.Badge);
            response.AppendInteger(group.RoomId);
            response.AppendString(Yupi.GetGame().GetRoomManager().GenerateRoomData(@group.RoomId) == null
                ? "No room found.."
                : Yupi.GetGame().GetRoomManager().GenerateRoomData(group.RoomId).Name);
            response.AppendInteger(@group.CreatorId == session.GetHabbo().Id
                ? 3
                : (group.Requests.ContainsKey(session.GetHabbo().Id)
                    ? 2
                    : (group.Members.ContainsKey(session.GetHabbo().Id) ? 1 : 0)));
            response.AppendInteger(group.Members.Count);
            response.AppendBool(session.GetHabbo().FavouriteGroup == group.Id);
            response.AppendString($"{dateTime2.Day.ToString("00")}-{dateTime2.Month.ToString("00")}-{dateTime2.Year}");
            response.AppendBool(group.CreatorId == session.GetHabbo().Id);
            response.AppendBool(group.Admins.ContainsKey(session.GetHabbo().Id));
            response.AppendString(Yupi.GetHabboById(@group.CreatorId) == null
                ? string.Empty
                : Yupi.GetHabboById(group.CreatorId).UserName);
            response.AppendBool(newWindow);
            response.AppendBool(group.AdminOnlyDeco == 0u);
            response.AppendInteger(group.Requests.Count);
            response.AppendBool(group.Forum.Id != 0);
            room.SendMessage(response);
        }

        /// <summary>
        ///     Generates the guild image.
        /// </summary>
        /// <param name="guildBase">The guild base.</param>
        /// <param name="guildBaseColor">Color of the guild base.</param>
        /// <param name="states">The states.</param>
        /// <returns>System.String.</returns>
        internal string GenerateGuildImage(int guildBase, int guildBaseColor, List<int> states)
        {
            StringBuilder image = new StringBuilder($"b{guildBase:00}{guildBaseColor:00}");

            for (int i = 0; i < 3*4; i += 3)
                image.Append(i >= states.Count ? "s" : $"s{states[i]:00}{states[i + 1]:00}{states[i + 2]}");

            return image.ToString();
        }

        /// <summary>
        ///     Gets the theGroup colour.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="colour1">if set to <c>true</c> [colour1].</param>
        /// <returns>System.String.</returns>
        internal string GetGroupColour(int index, bool colour1)
        {
            if (colour1)
            {
                if (SymbolColours.Contains(index))
                    return ((GroupSymbolColours) SymbolColours[index]).Colour;

                if (BackGroundColours.Contains(index))
                    return ((GroupBackGroundColours) BackGroundColours[index]).Colour;

                return "4f8a00";
            }

            if (BackGroundColours.Contains(index))
                return ((GroupBackGroundColours) BackGroundColours[index]).Colour;

            return "4f8a00";
        }

        /// <summary>
        ///     Deletes the theGroup.
        /// </summary>
        /// <param name="id">The identifier.</param>
        internal void DeleteGroup(uint id)
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(string.Format("DELETE FROM groups_members WHERE group_id = {0};" +
                                                              "DELETE FROM groups_requests WHERE group_id = {0};" +
                                                              "DELETE FROM groups_forums_data WHERE group_id = {0}; " +
                                                              "DELETE FROM groups_data WHERE id = {0};" +
                                                              "UPDATE rooms_data SET group_id = 0 WHERE group_id = {0};",
                    id)
                    );
                commitableQueryReactor.RunQuery();

                Groups.Remove(id);
            }
        }

        /// <summary>
        ///     Gets the message count for thread.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>System.Int32.</returns>
        internal int GetMessageCountForThread(uint id)
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery($"SELECT COUNT(*) FROM groups_forums_posts WHERE parent_id='{id}'");
                return int.Parse(commitableQueryReactor.GetString());
            }
        }

        /// <summary>
        ///     Splits the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>List&lt;List&lt;GroupUser&gt;&gt;.</returns>
        private static List<List<GroupMember>> Split(IEnumerable<GroupMember> source)
        {
            return (from x in source.Select((x, i) => new {Index = i, Value = x})
                group x by x.Index/14
                into x
                select (from v in x
                    select v.Value).ToList()).ToList();
        }
    }
}