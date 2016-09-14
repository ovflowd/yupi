// ---------------------------------------------------------------------------------
// <copyright file="GetGroupForumThreadRootMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Data;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GetGroupForumThreadRootMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public GetGroupForumThreadRootMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();

            int startIndex = request.GetInteger();

            Group theGroup = GroupRepository.FindBy(groupId);

            if (theGroup == null)
            {
                return;
            }

            // TODO Magic constant!
            List<GroupForumThread> threads = theGroup.Forum.Threads.Skip(startIndex).Take(20).ToList();

            router.GetComposer<GroupForumThreadRootMessageComposer>().Compose(session, groupId, startIndex, threads);
        }

        #endregion Methods
    }
}