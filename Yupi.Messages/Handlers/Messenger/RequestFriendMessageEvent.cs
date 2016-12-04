// ---------------------------------------------------------------------------------
// <copyright file="RequestFriendMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class RequestFriendMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public RequestFriendMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string friendName = request.GetString();

            UserInfo friend = UserRepository.Find(x => x.Name == friendName);

            if (friend != null)
            {
                if (friend.HasFriendRequestsDisabled)
                {
                    router.GetComposer<NotAcceptingRequestsMessageComposer>().Compose(session);
                }
                else
                {
                    IRepository<FriendRequest> requests = DependencyFactory.Resolve<IRepository<FriendRequest>> ();

                    if (!requests.Exists (x => x.To == friend && x.From == session.Info))
                    {
                        var friendRequest = new FriendRequest(session.Info, friend);

                        requests.Save(friendRequest);

                        var friendSession = ClientManager.GetByInfo(friend);

                        if (friendSession != null)
                        {
                            friendSession.Router.GetComposer<ConsoleSendFriendRequestMessageComposer>()
                                .Compose(friendSession, friendRequest);
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}