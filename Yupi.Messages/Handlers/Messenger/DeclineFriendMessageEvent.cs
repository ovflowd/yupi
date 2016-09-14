using System.Collections.Generic;
using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Util;

namespace Yupi.Messages.Messenger
{
    public class DeclineFriendMessageEvent : AbstractHandler
    {
        private readonly IRepository<FriendRequest> RequestRepository;
        private readonly IRepository<UserInfo> UserRepository;

        public DeclineFriendMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            RequestRepository = DependencyFactory.Resolve<IRepository<FriendRequest>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var deleteAll = request.GetBool();

            request.GetInteger(); // TODO Unused

            var toDelete = new List<UserInfo>();

            if (deleteAll)
            {
                var requests = RequestRepository.
                    FilterBy(x => x.To == session.Info)
                    .Select(x => x.To);
                toDelete.AddRange(requests.AsEnumerable());
            }
            else
            {
                var sender = request.GetInteger();
                var user = UserRepository.FindBy(sender);
                if (user != null) toDelete.Add(user);
            }

            foreach (var info in toDelete)
            {
                info.Relationships.SentRequests.RemoveAll(x => x.To == session.Info);
                UserRepository.Save(info);
            }
        }
    }
}