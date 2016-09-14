// ---------------------------------------------------------------------------------
// <copyright file="GroupForumDataMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;
    using Yupi.Util;

    public class GroupForumDataMessageComposer : Yupi.Messages.Contracts.GroupForumDataMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Group group, UserInfo user)
        {
            // TODO Refactor
            string string1 = string.Empty, string2 = string.Empty, string3 = string.Empty, string4 = string.Empty;

            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(group.Id);
                message.AppendString(group.Name);
                message.AppendString(group.Description);
                message.AppendString(group.Badge);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(group.Forum.GetMessageCount());
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(group.Forum.GetLastPost().Poster.Id);
                message.AppendString(group.Forum.GetLastPost().Poster.Name);
                message.AppendInteger((int) group.Forum.GetLastPost().Timestamp.ToUnix().SecondsSinceEpoch);
                message.AppendInteger(group.Forum.WhoCanRead);
                message.AppendInteger(group.Forum.WhoCanPost);
                message.AppendInteger(group.Forum.WhoCanThread);
                message.AppendInteger(group.Forum.WhoCanMod);

                if (!group.Members.Contains(user))
                {
                    if (group.Forum.WhoCanRead == 1)
                    {
                        string1 = "not_member";
                    }

                    if (group.Forum.WhoCanPost == 1)
                    {
                        string2 = "not_member";
                    }

                    if (group.Forum.WhoCanThread == 1)
                    {
                        string3 = "not_member";
                    }
                }

                if (!group.Admins.Contains(user))
                {
                    if (group.Forum.WhoCanRead == 2)
                    {
                        string1 = "not_admin";
                    }

                    if (group.Forum.WhoCanPost == 2)
                    {
                        string2 = "not_admin";
                    }

                    if (group.Forum.WhoCanThread == 2)
                    {
                        string3 = "not_admin";
                    }

                    if (group.Forum.WhoCanMod == 2)
                    {
                        string4 = "not_admin";
                    }
                }

                if (user != group.Creator)
                {
                    if (group.Forum.WhoCanRead == 3)
                    {
                        string1 = "not_owner";
                    }

                    if (group.Forum.WhoCanPost == 3)
                    {
                        string2 = "not_owner";
                    }

                    if (group.Forum.WhoCanThread == 3)
                    {
                        string3 = "not_owner";
                    }

                    if (group.Forum.WhoCanMod == 3)
                    {
                        string4 = "not_owner";
                    }
                }

                message.AppendString(string1);
                message.AppendString(string2);
                message.AppendString(string3);
                message.AppendString(string4);
                message.AppendString(string.Empty);
                message.AppendBool(user == group.Creator);
                message.AppendBool(true);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}