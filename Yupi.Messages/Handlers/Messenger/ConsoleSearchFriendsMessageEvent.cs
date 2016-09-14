// ---------------------------------------------------------------------------------
// <copyright file="ConsoleSearchFriendsMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Messenger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ConsoleSearchFriendsMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public ConsoleSearchFriendsMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string query = request.GetString();

            List<UserInfo> friends = session.Info.Relationships
                .Relationships
                .Where(x => x.Friend.Name.StartsWith(query))
                .Select(x => x.Friend)
                .ToList();

            List<UserInfo> users = UserRepository
                .FilterBy(x => x.Name.StartsWith(query))
                .Where(x => !friends.Contains(x))
                .Take(50) // TODO Proper limit
                .ToList();

            router.GetComposer<ConsoleSearchFriendMessageComposer>().Compose(session, friends, users);
        }

        #endregion Methods
    }
}