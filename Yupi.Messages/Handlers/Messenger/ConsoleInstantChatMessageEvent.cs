// ---------------------------------------------------------------------------------
// <copyright file="ConsoleInstantChatMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    // TODO Rename?
    public class ConsoleInstantChatMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<UserInfo> UserRepository;
        private WordfilterManager Wordfilter;

        #endregion Fields

        #region Constructors

        public ConsoleInstantChatMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            Wordfilter = DependencyFactory.Resolve<WordfilterManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int toId = request.GetInteger();
            string text = request.GetString();

            if (string.IsNullOrWhiteSpace(text))
                return;

            Relationship friend = session.Info.Relationships.FindByUser(toId);

            if (friend != null)
            {
                MessengerMessage message = new MessengerMessage()
                {
                    From = session.Info,
                    UnfilteredText = text,
                    Text = Wordfilter.Filter(text),
                    Timestamp = DateTime.Now,
                };

                var friendSession = ClientManager.GetByInfo(friend.Friend);
                friendSession?.Router.GetComposer<ConsoleChatMessageComposer>().Compose(session, message);
                message.Read = friendSession != null;

                // TODO Store for offline
                // TODO Store for chatlog
            }
        }

        #endregion Methods
    }
}