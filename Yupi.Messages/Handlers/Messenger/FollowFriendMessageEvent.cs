// ---------------------------------------------------------------------------------
// <copyright file="FollowFriendMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
    using Yupi.Messages.User;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class FollowFriendMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;

        #endregion Fields

        #region Constructors

        public FollowFriendMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        // TODO Refactor
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int userId = request.GetInteger();

            Relationship relationship = session.Info.Relationships.FindByUser(userId);

            if (relationship == null)
            {
                router.GetComposer<FollowFriendErrorMessageComposer>().Compose(session, 0);
            }
            else
            {
                var friendSession = ClientManager.GetByUserId(userId);

                if (friendSession == null || friendSession.Room == null)
                {
                    router.GetComposer<FollowFriendErrorMessageComposer>().Compose(session, 2);
                }
                else
                {
                    router.GetComposer<RoomForwardMessageComposer>().Compose(session, friendSession.Room.Data.Id);
                }
            }
        }

        #endregion Methods
    }
}