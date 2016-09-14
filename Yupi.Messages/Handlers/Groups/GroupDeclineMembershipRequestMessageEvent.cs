// ---------------------------------------------------------------------------------
// <copyright file="GroupDeclineMembershipRequestMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class GroupDeclineMembershipRequestMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int userId = request.GetInteger();
            /*
            Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

            if (session.GetHabbo().Id != group.CreatorId && !group.Admins.ContainsKey(session.GetHabbo().Id) &&
                !group.Requests.ContainsKey(userId))
                return;

            group.Requests.Remove(userId);

            router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group, 2u, session);

            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);

            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(userId).Name);

            if (roomUserByHabbo != null)
            {
                if (roomUserByHabbo.Statusses.ContainsKey("flatctrl 1"))
                    roomUserByHabbo.RemoveStatus("flatctrl 1");

                roomUserByHabbo.UpdateNeeded = true;
            }

            router.GetComposer<GroupDataMessageComposer> ().Compose (session, group, session.GetHabbo());

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                queryReactor.SetQuery ("DELETE FROM group_requests WHERE group_id = @group_id AND user_id = @user_id");
                queryReactor.AddParameter("group_id", groupId);
                queryReactor.AddParameter("user_id", userId);
                queryReactor.RunQuery ();
            }
            */
        }

        #endregion Methods
    }
}