// ---------------------------------------------------------------------------------
// <copyright file="AcceptFriendMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;

    public class AcceptFriendMessageEvent : AbstractHandler
    {
        #region Fields

        private AchievementManager AchievementManager;
        private ClientManager ClientManager;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public AcceptFriendMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int count = request.GetInteger();
            for (int i = 0; i < count; i++)
            {
                int userId = request.GetInteger();

                UserInfo friend = UserRepository.FindBy(userId);

                if (friend != null && friend.Relationships.HasSentRequestTo(session.Info))
                {
                    Relationship friendRelation = friend.Relationships.Add(session.Info);
                    Relationship userRelation = session.Info.Relationships.Add(friend);

                    friend.Relationships.SentRequests.RemoveAll(x => x.To == session.Info);

                    AchievementManager.ProgressUserAchievement(session, "ACH_FriendListSize", 1);

                    session.Router.GetComposer<FriendUpdateMessageComposer>().Compose(session, userRelation);

                    var friendSession = ClientManager.GetByInfo(friend);

                    if (friendSession != null)
                    {
                        friendSession.Router.GetComposer<FriendUpdateMessageComposer>()
                            .Compose(friendSession, friendRelation);
                    }

                    UserRepository.Save(friend);
                    UserRepository.Save(session.Info);
                }
            }
        }

        #endregion Methods
    }
}