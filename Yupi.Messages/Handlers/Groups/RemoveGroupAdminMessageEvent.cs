// ---------------------------------------------------------------------------------
// <copyright file="RemoveGroupAdminMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Messages.Rooms;

    public class RemoveGroupAdminMessageEvent : AbstractHandler
    {
        #region Methods

        // TODO Refactor
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint num = request.GetUInt32();
            uint num2 = request.GetUInt32();
            throw new NotImplementedException();
            /*
            Group group = Yupi.GetGame().GetGroupManager().GetGroup(num);

            if (session.GetHabbo().Id != group.CreatorId || !group.Members.ContainsKey(num2) ||
                !group.Admins.ContainsKey(num2))
                return;

            group.Members[num2].Rank = 0;
            group.Admins.Remove(num2);

            router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group, 0u, session);

            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);
            RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(num2).Name);

            if (roomUserByHabbo != null)
            {
                if (roomUserByHabbo.Statusses.ContainsKey("flatctrl 1"))
                    roomUserByHabbo.RemoveStatus("flatctrl 1");

                router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (session, 0);
                roomUserByHabbo.UpdateNeeded = true;
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                // TODO Magic number !!! (rank)
                queryReactor.SetQuery ("UPDATE groups_members SET rank = 0 WHERE group_id = @group_id AND user_id = @user_id");
                queryReactor.AddParameter("group_id", num);
                queryReactor.AddParameter("user_id", num2);
                queryReactor.RunQuery ();
            }
            */
        }

        #endregion Methods
    }
}