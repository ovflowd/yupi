using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Yupi.Core.Io;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Alert. This class cannot be inherited.
    /// </summary>
    internal sealed class Developer : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Developer" /> class.
        /// </summary>
        public Developer()
        {
            MinRank = 8;
            Description = "Developer command";
            Usage = ":developer [info/set/copy/paste/delete]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string mode = pms[0];
            pms = pms.Skip(1).ToArray();

            switch (mode.ToLower())
            {
                case "info":
                {
                    if (pms.Length == 0) session.SendWhisper("Usage :developer info [items/user/users/cache]");
                    else return GetInfo(session, pms);

                    break;
                }
                case "set":
                {
                    if (pms.Length < 2) session.SendWhisper("Usage :developer set [item/baseItem] id");
                    else return Set(session, pms);

                    break;
                }
                case "copy":
                {
                    return Copy(session);
                }
                case "paste":
                {
                    return Paste(session);
                }
                case "delete":
                {
                    return Delete(session);
                }
                default:
                {
                    session.SendWhisper("Usage :developer [info/set/copy/paste/delete]");
                    break;
                }
            }

            return true;
        }

        private static bool Delete(GameClient session)
        {
            Room room = session.GetHabbo().CurrentRoom;

            RoomUser user = room.GetRoomUserManager()
                .GetRoomUserByHabbo(session.GetHabbo().UserName);

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                foreach (
                    RoomItem item in
                        room.GetGameMap()
                            .GetAllRoomItemForSquare(user.LastSelectedX, user.LastSelectedY))
                {
                    commitableQueryReactor.RunFastQuery("DELETE FROM items_rooms WHERE id = " + item.Id);

                    room.GetRoomItemHandler().RemoveRoomItem(item, false);
                    item.Destroy();
                }
            }

            return true;
        }

        private static bool Paste(GameClient session)
        {
            Room room = session.GetHabbo().CurrentRoom;

            RoomUser user = room.GetRoomUserManager()
                .GetRoomUserByHabbo(session.GetHabbo().UserName);

            if (user.CopyX == 0 || user.CopyY == 0)
            {
                session.SendWhisper("First usage :developer copy");
                return true;
            }
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                foreach (
                    RoomItem item in
                        room.GetGameMap()
                            .GetAllRoomItemForSquare(user.CopyX, user.CopyY))
                {
                    commitableQueryReactor.SetQuery(
                        "INSERT INTO items_rooms (item_name, user_id, room_id, extra_data, x, y, z, rot, group_id) VALUES (" +
                        item.GetBaseItem().Name + ", " + user.UserId + ", " + user.RoomId + ", @extraData, " +
                        user.LastSelectedX + ", " + user.LastSelectedY + ", @height, " + item.Rot + ", " + item.GroupId +
                        ")");
                    commitableQueryReactor.AddParameter("extraData", item.ExtraData);
                    commitableQueryReactor.AddParameter("height", ServerUserChatTextHandler.GetString(item.Z));

                    uint insertId = (uint) commitableQueryReactor.InsertQuery();

                    RoomItem roomItem = new RoomItem(insertId, user.RoomId, item.GetBaseItem().Name, item.ExtraData,
                        user.LastSelectedX, user.LastSelectedY, item.Z, item.Rot, session.GetHabbo().CurrentRoom,
                        user.UserId, item.GroupId, item.SongCode,
                        item.IsBuilder);
                    room.GetRoomItemHandler().DeveloperSetFloorItem(session, roomItem);
                }
            }

            return true;
        }

        private static bool Copy(GameClient session)
        {
            RoomUser user =
                session.GetHabbo()
                    .CurrentRoom.GetRoomUserManager()
                    .GetRoomUserByHabbo(session.GetHabbo().UserName);

            user.CopyX = user.LastSelectedX;
            user.CopyY = user.LastSelectedY;

            return true;
        }

        private static bool Set(GameClient session, IReadOnlyList<string> pms)
        {
            string type = pms[0];
            uint id = uint.Parse(pms[1]);

            switch (type.ToLower())
            {
                case "item":
                {
                    if (pms.Count == 2)
                    {
                        session.SendWhisper("Usage :developer set item id [x/y/z] value");
                        break;
                    }

                    RoomItem item = session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(id);
                    if (item == null)
                    {
                        session.SendWhisper("Item no encontrado");
                        return false;
                    }

                    int x = item.X, y = item.Y;
                    double z = item.Z;

                    int i = 2;
                    while (pms.Count >= i + 2)
                    {
                        switch (pms[i].ToLower())
                        {
                            case "x":
                            {
                                x = int.Parse(pms[i + 1]);
                                break;
                            }
                            case "y":
                            {
                                y = int.Parse(pms[i + 1]);
                                break;
                            }
                            case "z":
                            {
                                z = double.Parse(pms[i + 1]);
                                break;
                            }
                            case "rot":
                            {
                                item.Rot = int.Parse(pms[i + 1]);
                                break;
                            }
                        }
                        i += 2;
                    }

                    if (item.IsWallItem) session.GetHabbo().CurrentRoom.GetRoomItemHandler().SetWallItem(session, item);
                    else
                        session.GetHabbo().CurrentRoom.GetRoomItemHandler().SetFloorItem(item, x, y, z, item.Rot, true);
                    break;
                }

                case "baseitem":
                {
                    if (pms.Count == 2)
                    {
                        session.SendWhisper("Usage :developer set baseItem baseId [stack,trade,modes,height] value");
                        break;
                    }

                    Item item = Yupi.GetGame().GetItemManager().GetItem(id);
                    if (item == null)
                    {
                        session.SendWhisper("Item no encontrado");
                        return false;
                    }

                    int i = 2;
                    while (pms.Count >= i + 2)
                    {
                        switch (pms[i].ToLower())
                        {
                            case "stack":
                            {
                                item.Stackable = pms[i + 1] == "1" || pms[i + 1] == "true";
                                break;
                            }
                            case "trade":
                            {
                                item.AllowTrade = pms[i + 1] == "1" || pms[i + 1] == "true";
                                break;
                            }
                            case "modes":
                            {
                                item.Modes = uint.Parse(pms[i + 1]);
                                break;
                            }
                            case "height":
                            {
                                string stackHeightStr = pms[i + 1].Replace(',', '.');
                                if (stackHeightStr.Contains(';'))
                                {
                                    string[] heightsStr = stackHeightStr.Split(';');
                                    item.ToggleHeight =
                                        heightsStr.Select(
                                            heightStr => double.Parse(heightStr, CultureInfo.InvariantCulture))
                                            .ToArray();
                                    item.Height = item.ToggleHeight[0];
                                    item.StackMultipler = true;
                                }
                                else
                                {
                                    item.Height = double.Parse(stackHeightStr, CultureInfo.InvariantCulture);
                                    item.StackMultipler = false;
                                }

                                break;
                            }
                        }
                        i += 2;
                    }

                    Item.Save(item.ItemId, item.Stackable, item.AllowTrade,
                        item.StackMultipler ? item.ToggleHeight : new[] {item.Height}, item.Modes);
                    break;
                }
            }
            return true;
        }

        private static bool GetInfo(GameClient session, IReadOnlyList<string> pms)
        {
            string type = pms[0];

            RoomUser user =
                session.GetHabbo()
                    .CurrentRoom.GetRoomUserManager()
                    .GetRoomUserByHabbo(session.GetHabbo().UserName);
            StringBuilder text = new StringBuilder();
            switch (type)
            {
                case "cache":
                {
                    text.AppendLine("Displaying info of all cached data avaible");
                    text.Append("Users: " + Yupi.UsersCached.Count + '\r');
                    text.Append("Rooms: " + Yupi.GetGame().GetRoomManager().LoadedRooms.Count + '\r');
                    text.Append("Rooms Data: " + Yupi.GetGame().GetRoomManager().LoadedRoomData.Count + '\r');
                    text.Append("Groups: " + Yupi.GetGame().GetGroupManager().Groups.Count + '\r');
                    text.Append("Items: " + Yupi.GetGame().GetItemManager().CountItems() + '\r');
                    text.Append("Catalog Items: " + Yupi.GetGame().GetCatalog().Offers.Count + '\r');

                    session.SendNotifWithScroll(text.ToString());
                    break;
                }
                case "users":
                {
                    text.AppendLine("Displaying info of all users of this room");

                    foreach (RoomUser roomUser in session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUsers())
                        AppendUserInfo(roomUser, text);

                    session.SendNotifWithScroll(text.ToString());
                    break;
                }
                case "user":
                {
                    RoomUser roomUser =
                        session.GetHabbo()
                            .CurrentRoom.GetRoomUserManager()
                            .GetRoomUserByHabbo(session.GetHabbo().LastSelectedUser);
                    if (roomUser == null || roomUser.IsBot || roomUser.GetClient() == null)
                        text.Append("User not found");
                    else AppendUserInfo(roomUser, text);

                    session.SendNotifWithScroll(text.ToString());
                    break;
                }
                case "items":
                {
                    text.AppendLine(
                        $"Displaying info of coordinates: (X/Y)  {user.LastSelectedX}/{user.LastSelectedY}");

                    foreach (
                        RoomItem item in
                            session.GetHabbo()
                                .CurrentRoom.GetGameMap()
                                .GetAllRoomItemForSquare(user.LastSelectedX, user.LastSelectedY))
                    {
                        text.Append($"## itemId: {item.Id}  itemBaseId: {item.GetBaseItem().ItemId} \r");
                        text.Append(
                            $"itemName: {item.GetBaseItem().Name}  itemSpriteId: {item.GetBaseItem().SpriteId} \r");
                        text.Append($"itemInteraction: {item.GetBaseItem().InteractionType} \r");
                        text.Append($"itemInteractionCount: {item.GetBaseItem().Modes} \r");
                        text.Append($"itemPublicName: {item.GetBaseItem().PublicName} \r");
                        text.Append($"X/Y/Z/Rot:  {item.X}/{item.Y}/{item.Z}/{item.Rot}  Height: {item.Height} \r");
                        if (item.GetBaseItem().StackMultipler)
                            text.Append("Heights: " + string.Join("  -  ", item.GetBaseItem().ToggleHeight) + '\r');
                        text.AppendLine(
                            $"Can: {(item.GetBaseItem().Walkable ? "walk" : string.Empty)}  {(item.GetBaseItem().IsSeat ? "sit" : string.Empty)}  {(item.GetBaseItem().Stackable ? "stack" : string.Empty)}");
                    }

                    session.SendNotifWithScroll(text.ToString());
                    break;
                }
            }

            return true;
        }

        private static void AppendUserInfo(RoomUser user, StringBuilder text)
        {
            text.Append(
                $"## userId: {user.UserId}  name: {user.GetUserName()} rank: {user.GetClient().GetHabbo().Rank} \r");
            if (user.IsDancing) text.Append("actions: dancing \r");
            if (user.IsLyingDown) text.Append("actions: lying \r");
            if (user.IsSitting) text.Append("actions: sitting \r");
            if (user.CurrentEffect > 0) text.Append("actions: effect." + user.CurrentEffect);
            if (user.IsWalking) text.Append($" walking.To(X/Y  {user.GoalX}/{user.GoalY})");
            text.Append("\r");

            text.Append("room rights: ");
            if (user.GetClient().GetHabbo().HasFuse("fuse_mod")) text.Append(" staff");
            if (user.GetClient().GetHabbo().HasFuse("fuse_any_room_controller")) text.Append(" controlAnyRoom");
            if (user.GetClient().GetHabbo().CurrentRoom.CheckRights(user.GetClient(), true)) text.Append(" owner");
            if (user.GetClient().GetHabbo().CurrentRoom.CheckRights(user.GetClient(), false, true))
                text.Append(" groupAdmin");
            else if (user.GetClient().GetHabbo().CurrentRoom.CheckRights(user.GetClient(), false, false, true))
                text.Append(" groupMember");
            text.Append("\r");

            text.Append("prohibitions: ");
            if (!user.CanWalk) text.Append(" walk");
            if (user.GetClient().GetHabbo().Muted) text.Append(" chat");
            text.Append("\r");

            text.AppendLine($"X/Y/Z/Rot:  {user.X}/{user.Y}/{user.Z}/{user.RotBody}");
        }
    }
}