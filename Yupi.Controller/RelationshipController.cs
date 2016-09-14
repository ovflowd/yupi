// ---------------------------------------------------------------------------------
// <copyright file="RelationshipController.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Controller
{
    using System;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;

    public class RelationshipController
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<Relationship> RelationshipRepository;

        #endregion Fields

        #region Constructors

        public RelationshipController()
        {
            RelationshipRepository = DependencyFactory.Resolve<IRepository<Relationship>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public void Remove(Habbo user, int friendId)
        {
            Relationship relationship = user.Info.Relationships.FindByUser(friendId);
            Remove(user.Info, relationship);
            user.Router.GetComposer<FriendUpdateMessageComposer>().Compose(user, null);
        }

        private void Remove(UserInfo user, Relationship relationship)
        {
            if (relationship != null)
            {
                var friendRelation = relationship.Friend.Relationships.FindByUser(user);

                user.Relationships.Relationships.Remove(relationship);
                relationship.Friend.Relationships.Relationships.Remove(friendRelation);

                relationship.Deleted = true;
                var friend = ClientManager.GetByUserId(relationship.Friend.Id);

                if (friend != null && friendRelation != null)
                {
                    friendRelation.Deleted = true;
                    friend.Router.GetComposer<FriendUpdateMessageComposer>().Compose(friend, friendRelation);
                }

                RelationshipRepository.Delete(relationship);
                RelationshipRepository.Delete(friendRelation);
            }
        }

        #endregion Methods
    }
}