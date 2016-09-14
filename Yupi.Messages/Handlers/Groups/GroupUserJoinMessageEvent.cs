// ---------------------------------------------------------------------------------
// <copyright file="GroupUserJoinMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class GroupUserJoinMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint groupId = request.GetUInt32();
            throw new NotImplementedException();
            /*
            Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);
            Habbo user = session.GetHabbo();

            if (!group.Members.ContainsKey(user.Id))
            {   // TODO Magic number !!!
                if (group.State == 0)
                {
                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery ("INSERT INTO group_members (group_id, user_id, rank, date_join) VALUES (@group_id, @user_id, @rank, @timestamp)");
                        queryReactor.AddParameter("group_id", groupId);
                        queryReactor.AddParameter("user_id", user.Id);
                        queryReactor.AddParameter("rank", 0);
                        queryReactor.AddParameter("timestamp", Yupi.GetUnixTimeStamp());
                        queryReactor.RunQuery ();

                        queryReactor.SetQuery ("UPDATE user_stats SET favourite_group = @group_id WHERE id = @user_id");
                        queryReactor.AddParameter("group_id", groupId);
                        queryReactor.AddParameter("user_id", user.Id);
                        queryReactor.RunQuery ();
                    }

                    group.Members.Add(user.Id,
                        new GroupMember(user.Id, user.Name, user.Look, group.Id, 0, Yupi.GetUnixTimeStamp()));

                    session.GetHabbo().UserGroups.Add(group.Members[user.Id]);
                }
                else
                {
                    if (!group.Requests.ContainsKey(user.Id))
                    {
                        using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                            queryReactor.SetQuery ("INSERT INTO groups_requests (user_id, group_id) VALUES (@user_id, @group_id)");
                            queryReactor.AddParameter("group_id", groupId);
                            queryReactor.AddParameter("user_id", user.Id);
                            queryReactor.RunQuery ();
                        }

                        GroupMember groupRequest = new GroupMember(user.Id, user.Name, user.Look, group.Id, 0,
                            Yupi.GetUnixTimeStamp());

                        group.Requests.Add(user.Id, groupRequest);
                    }
                }

                router.GetComposer<GroupDataMessageComposer> ().Compose (session, group, session.GetHabbo());
            }*/
        }

        #endregion Methods
    }
}