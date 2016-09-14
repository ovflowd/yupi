using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Linq;


namespace Yupi.Messages.Messenger
{
    public class ConsoleSearchFriendsMessageEvent : AbstractHandler
    {
        private IRepository<UserInfo> UserRepository;

        public ConsoleSearchFriendsMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

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
    }
}