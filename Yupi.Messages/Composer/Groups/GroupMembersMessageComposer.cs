// ---------------------------------------------------------------------------------
// <copyright file="GroupMembersMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class GroupMembersMessageComposer : Yupi.Messages.Contracts.GroupMembersMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user, Group group)
        {
            Compose(session, group, 0u, user);
        }

        // TODO Refactor?
        public override void Compose(Yupi.Protocol.ISender session, Group group, uint reqType, UserInfo user,
            string searchVal = "", int page = 0)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(group.Id);
                message.AppendString(group.Name);
                message.AppendInteger(group.Room.Id);
                message.AppendString(group.Badge);

                List<UserInfo> groupList = GetGroupUsersByString(group, searchVal, reqType);
                /*
                if(groupList != null)
                {
                    List<List<UserInfo>> list = Split(groupList);

                    if(list != null)
                    {
                        if (reqType == 0)
                        {
                            message.AppendInteger(list.Count);

                            if (group.Members.Count > 0 && list.Count > 0 && list[page] != null)
                            {
                                message.AppendInteger(list[page].Count);

                                using (List<UserInfo>.Enumerator enumerator = list[page].GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        UserInfo current = enumerator.Current;

                                        AddGroupMemberIntoResponse(message, current);
                                    }
                                }
                            }
                            else
                                message.AppendInteger(0);
                        }
                        else if (reqType == 1)
                        {
                            message.AppendInteger(group.Admins.Count);

                            List<UserInfo> paging = page <= list.Count ? list[page] : null;

                            if ((group.Admins.Count > 0) && (list.Count > 0) && paging != null)
                            {
                                message.AppendInteger(list[page].Count);

                                using (List<UserInfo>.Enumerator enumerator = list[page].GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        UserInfo current = enumerator.Current;

                                        AddGroupMemberIntoResponse(message, current);
                                    }
                                }
                            }
                            else
                                message.AppendInteger(0);
                        }
                        else if (reqType == 2)
                        {
                            message.AppendInteger(group.Requests.Count);

                            if (group.Requests.Count > 0 && list.Count > 0 && list[page] != null)
                            {
                                message.AppendInteger(list[page].Count);

                                using (List<UserInfo>.Enumerator enumerator = list[page].GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        UserInfo current = enumerator.Current;

                                        message.AppendInteger(3);

                                        if (current != null)
                                        {
                                            message.AppendInteger(current.Id);
                                            message.AppendString(current.Name);
                                            message.AppendString(current.Look);
                                        }

                                        message.AppendString(string.Empty);
                                    }
                                }
                            }
                            else
                                message.AppendInteger(0);
                        }
                    }
                    else
                        message.AppendInteger(0);
                }
                else
                    message.AppendInteger(0);

                message.AppendBool(user == group.Creator);
                message.AppendInteger(14);
                message.AppendInteger(page);
                message.AppendInteger(reqType);
                message.AppendString(searchVal);
                session.Send (message);
                */
                throw new NotImplementedException();
            }
        }

        private void AddGroupMemberIntoResponse(ServerMessage response, UserInfo member)
        {
            response.AppendInteger(member.Rank == 2 ? 0 : member.Rank == 1 ? 1 : 2);
            response.AppendInteger(member.Id);
            response.AppendString(member.Name);
            response.AppendString(member.Look);
            throw new NotImplementedException();
            //response.AppendString(Yupi.GetGroupDateJoinString(member.DateJoin));
        }

        private List<UserInfo> GetGroupRequestsByString(Group theGroup, string searchVal)
        {
            if (string.IsNullOrWhiteSpace(searchVal))
            {
                return theGroup.Requests.ToList();
            }
            else
            {
                return theGroup.Requests.Where(request => request.Name.ToLower().Contains(searchVal.ToLower()))
                    .ToList();
            }
        }

        // TODO Useless copy to list?
        private List<UserInfo> GetGroupUsersByString(Group theGroup, string searchVal, uint req)
        {
            /*
            List<UserInfo> list = new List<UserInfo>();

            switch (req)
            {
            case 0:

                list = theGroup.Members.Values.ToList();

                break;

            case 1:
                list = theGroup.Admins.Values.ToList();
                break;

            case 2:
                list = GetGroupRequestsByString(theGroup, searchVal);
                break;
            }

            if (!string.IsNullOrWhiteSpace(searchVal))
                list = list.Where(member => member.Name.ToLower().Contains(searchVal.ToLower())).ToList();

            return list;*/
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}