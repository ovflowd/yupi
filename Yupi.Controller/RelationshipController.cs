using System;
using Yupi.Model.Domain;
using Yupi.Util;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Messages.Contracts;

namespace Yupi.Controller
{
    public class RelationshipController
    {
        private IRepository<Relationship> RelationshipRepository;
        private ClientManager ClientManager;

        public RelationshipController()
        {
            RelationshipRepository = DependencyFactory.Resolve<IRepository<Relationship>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

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
    }
}