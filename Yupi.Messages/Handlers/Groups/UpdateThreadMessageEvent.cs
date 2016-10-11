// ---------------------------------------------------------------------------------
// <copyright file="UpdateThreadMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class UpdateThreadMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public UpdateThreadMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int threadId = request.GetInteger();
            bool pin = request.GetBool();
            bool Lock = request.GetBool();

            Group theGroup = GroupRepository.Find(groupId);

            if (theGroup == null)
            {
                return;
            }

            GroupForumThread thread = theGroup.Forum.GetThread(threadId);

            if (thread.Creator == session.Info || theGroup.Admins.Contains(session.Info))
            {
                thread.Locked = Lock;
                thread.Pinned = pin;
            }

            //GroupForumPost thread = new GroupForumPost(row);
            throw new NotImplementedException();
            /*
            if (thread.Pinned != pin) {
                router.GetComposer<SuperNotificationMessageComposer> ().Compose (session, string.Empty, string.Empty, string.Empty, string.Empty,
                    pin ? "forums.thread.pinned" : "forums.thread.unpinned", 0);
            }

            if (thread.Locked != Lock) {
                router.GetComposer<SuperNotificationMessageComposer> ().Compose (session, string.Empty, string.Empty, string.Empty, string.Empty,
                    Lock ? "forums.thread.locked" : "forums.thread.unlocked", 0);
            }

            router.GetComposer<GroupForumThreadUpdateMessageComposer> ().Compose (session, groupId, thread, pin, Lock);
            */
        }

        #endregion Methods
    }
}