using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
    public class ConsoleSearchFriendsMessageEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public ConsoleSearchFriendsMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var query = request.GetString();

            var friends = session.Info.Relationships
                .Relationships
                .Where(x => x.Friend.Name.StartsWith(query))
                .Select(x => x.Friend)
                .ToList();

            var users = UserRepository
                .FilterBy(x => x.Name.StartsWith(query))
                .Where(x => !friends.Contains(x))
                .Take(50) // TODO Proper limit
                .ToList();

            router.GetComposer<ConsoleSearchFriendMessageComposer>().Compose(session, friends, users);
        }
    }
}