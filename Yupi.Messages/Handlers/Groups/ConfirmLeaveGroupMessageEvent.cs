// ---------------------------------------------------------------------------------
// <copyright file="ConfirmLeaveGroupMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ConfirmLeaveGroupMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public ConfirmLeaveGroupMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int userId = request.GetInteger();

            Group group = GroupRepository.FindBy(groupId);

            if (group == null)
                return;

            if (group.Creator.Id == userId)
            {
                return;
            } /*
            // TODO Refactor
            if (userId == session.Info.Id || group.Admins.Contains(session.Info))
            {
                GroupMember memberShip;

                int type = 3;

                if (!group.Members.ContainsKey(userId) && !group.Admins.ContainsKey(userId))
                    return;

                if (group.Members.ContainsKey(userId))
                {
                    memberShip = group.Members[userId];

                    type = 3;

                    session.GetHabbo().UserGroups.Remove(memberShip);
                    group.Members.Remove(userId);
                }

                if (group.Admins.ContainsKey(userId))
                {
                    memberShip = group.Admins[userId];

                    type = 1;

                    session.GetHabbo().UserGroups.Remove(memberShip);
                    group.Admins.Remove(userId);
                }

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                    queryReactor.SetQuery ("DELETE FROM groups_members WHERE user_id = @user_id AND group_id = @group_id");
                    queryReactor.AddParameter ("user_id", userId);
                    queryReactor.AddParameter ("group_id", groupId);
                    queryReactor.RunQuery ();
                }

                Habbo byeUser = Yupi.GetHabboById(userId);

                if (byeUser != null)
                {
                    router.GetComposer<GroupConfirmLeaveMessageComposer> ().Compose (session, byeUser, group, type);

                    if (byeUser.FavouriteGroup == groupId) {
                        byeUser.FavouriteGroup = 0;

                        using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                            queryreactor2.RunFastQuery("UPDATE users_stats SET favourite_group = 0 WHERE id = " + userId + " LIMIT 1");

                        Yupi.Messages.Rooms room = session.GetHabbo().CurrentRoom;

                        if (room != null) {
                            router.GetComposer<FavouriteGroupMessageComposer> ().Compose (room, byeUser.Id);
                            router.GetComposer<ChangeFavouriteGroupMessageComposer> ().Compose (room, null, 0);
                        } else {
                            router.GetComposer<FavouriteGroupMessageComposer> ().Compose (session, byeUser.Id);
                        }
                    }
                }

                if (group.AdminOnlyDeco == 0u)
                {
                    if (Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId).GetRoomUserManager().GetRoomUserByHabbo(byeUser?.Name) != null)
                    {
                        router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (session, 0);
                    }
                }

                router.GetComposer<GroupDataMessageComposer> ().Compose (session, group, session.GetHabbo());
                router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group);
                router.GetComposer<GrouprequestReloadMessageComposer> ().Compose (session, groupId);
            }*/
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}