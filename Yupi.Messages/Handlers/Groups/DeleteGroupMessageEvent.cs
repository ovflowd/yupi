// ---------------------------------------------------------------------------------
// <copyright file="DeleteGroupMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Messages.Groups
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class DeleteGroupMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public DeleteGroupMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();

            Group group = GroupRepository.FindBy(groupId);

            if (group?.Creator != session.Info)
                return;

            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);

            if (room?.RoomData?.Group == null)
                session.SendNotif(Yupi.GetLanguage().GetVar("command_group_has_no_room"));
            else
            {
                // TODO Refactor?
                foreach (GroupMember user in group.Members.Values)
                {
                    GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(user.Id);

                    if (clientByUserId == null)
                        continue;

                    clientByUserId.GetHabbo().UserGroups.Remove(user);

                    if (clientByUserId.GetHabbo().FavouriteGroup == group.Id)
                        clientByUserId.GetHabbo().FavouriteGroup = 0;
                }

                room.RoomData.Group = null;
                room.RoomData.GroupId = 0;

                Yupi.GetGame().GetGroupManager().DeleteGroup(@group.Id);

                router.GetComposer<GroupDeletedMessageComposer> ().Compose (room, groupId);

                List<RoomItem> roomItemList = room.GetRoomItemHandler().RemoveAllFurniture(session);
                room.GetRoomItemHandler().RemoveItemsByOwner(ref roomItemList, ref session);
                RoomData roomData = room.RoomData;
                uint roomId = room.RoomData.Id;

                Yupi.GetGame().GetRoomManager().UnloadRoom(room, "Delete room");
                Yupi.GetGame().GetRoomManager().QueueVoteRemove(roomData);

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    // TODO Use parameters (or ORM)
                    queryReactor.RunFastQuery($"DELETE FROM rooms_data WHERE id = {roomId}");
                    queryReactor.RunFastQuery($"DELETE FROM users_favorites WHERE room_id = {roomId}");
                    queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE room_id = {roomId}");
                    queryReactor.RunFastQuery($"DELETE FROM rooms_rights WHERE room_id = {roomId}");
                    queryReactor.RunFastQuery($"UPDATE users SET home_room = '0' WHERE home_room = {roomId}");
                }

                RoomData roomData2 =
                    (from p in session.GetHabbo().UsersRooms where p.Id == roomId select p).SingleOrDefault();

                if (roomData2 != null)
                    session.GetHabbo().UsersRooms.Remove(roomData2);
            }*/
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}