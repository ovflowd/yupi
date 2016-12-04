// ---------------------------------------------------------------------------------
// <copyright file="UserProfileMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.User
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Protocol.Buffers;

    public class UserProfileMessageComposer : Yupi.Messages.Contracts.UserProfileMessageComposer
    {
        #region Fields

        private ClientManager Manager;

        #endregion Fields

        #region Constructors

        public UserProfileMessageComposer()
        {
            Manager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user, UserInfo requester)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                IRepository<FriendRequest> requests = DependencyFactory.Resolve<IRepository<FriendRequest>> ();
                bool hasSentRequest = requests.Exists (x => x.To == user && x.From == requester);

                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                message.AppendString(user.Look);
                message.AppendString(user.Motto);
                message.AppendString(user.CreateDate.ToString("dd/MM/yyyy"));
                message.AppendInteger(user.Wallet.AchievementPoints);
                message.AppendInteger(user.Relationships.Relationships.Count);
                message.AppendBool(user.Relationships.IsFriendsWith(requester));
                message.AppendBool(hasSentRequest);
                message.AppendBool(Manager.IsOnline(user));

                message.AppendInteger(user.UserGroups.Count);

                foreach (Group group in user.UserGroups)
                {
                    message.AppendInteger(group.Id);
                    message.AppendString(group.Name);
                    message.AppendString(group.Badge);
                    message.AppendString(group.Colour1.Colour.ToString());
                    message.AppendString(group.Colour2.Colour.ToString());
                    message.AppendBool(group == user.FavouriteGroup);
                    message.AppendInteger(-1); // TODO Constant
                    message.AppendBool(group.Forum != null);
                }

                message.AppendInteger((int) (DateTime.Now - user.LastOnline).TotalSeconds);
                message.AppendBool(true);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}