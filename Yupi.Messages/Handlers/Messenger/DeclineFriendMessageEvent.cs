using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Util;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Messages.Messenger
{
    public class DeclineFriendMessageEvent : AbstractHandler
    {
        private IRepository<UserInfo> UserRepository;
        private IRepository<FriendRequest> RequestRepository;

        public DeclineFriendMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            RequestRepository = DependencyFactory.Resolve<IRepository<FriendRequest>>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            bool deleteAll = request.GetBool();

            request.GetInteger(); // TODO Unused

            List<UserInfo> toDelete = new List<UserInfo>();

            if (deleteAll)
            {
                var requests = RequestRepository.
                    FilterBy(x => x.To == session.Info)
                    .Select(x => x.To);
                toDelete.AddRange(requests.AsEnumerable());
            }
            else
            {
                int sender = request.GetInteger();
                UserInfo user = UserRepository.FindBy(sender);
                if (user != null)
                {
                    toDelete.Add(user);
                }
            }

            foreach (UserInfo info in toDelete)
            {
                info.Relationships.SentRequests.RemoveAll(x => x.To == session.Info);
                UserRepository.Save(info);
            }
        }
    }
}