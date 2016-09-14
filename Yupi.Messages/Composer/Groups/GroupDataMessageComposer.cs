// ---------------------------------------------------------------------------------
// <copyright file="GroupDataMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    public class GroupDataMessageComposer : Yupi.Messages.Contracts.GroupDataMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Group group, UserInfo habbo, bool newWindow = false)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                // TODO Hide conversion between Unix <-> DateTime
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime dateTime2 = dateTime.AddSeconds(group.CreateTime);

                message.AppendInteger(group.Id);
                message.AppendBool(true);
                message.AppendInteger(group.State);
                message.AppendString(group.Name);
                message.AppendString(group.Description);
                message.AppendString(group.Badge);
                message.AppendInteger(group.Room.Id);
                message.AppendString(group.Room.Name);

                /*
                message.AppendInteger(@group.CreatorId == session.GetHabbo().Id
                    ? 3
                    : (group.Requests.ContainsKey(session.GetHabbo().Id)
                        ? 2
                        : (group.Members.ContainsKey(session.GetHabbo().Id) ? 1 : 0)));
                message.AppendInteger(group.Members.Count);
                message.AppendBool(session.GetHabbo().FavouriteGroup == group.Id);
                message.AppendString($"{dateTime2.Day.ToString("00")}-{dateTime2.Month.ToString("00")}-{dateTime2.Year}");
                message.AppendBool(group.CreatorId == session.GetHabbo().Id);
                message.AppendBool(group.Admins.ContainsKey(session.GetHabbo().Id));
                message.AppendString(Yupi.GetHabboById(@group.CreatorId) == null
                    ? string.Empty
                    : Yupi.GetHabboById(group.CreatorId).Name);
                message.AppendBool(newWindow);
                message.AppendBool(group.AdminOnlyDeco == 0u);
                message.AppendInteger(group.Requests.Count);
                message.AppendBool(group.Forum.Id != 0);
                session.Send (message);
                */
                throw new NotImplementedException();
            }
        }

        #endregion Methods
    }
}