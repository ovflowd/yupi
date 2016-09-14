using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Util;

namespace Yupi.Messages.Messenger
{
    public class AcceptFriendMessageEvent : AbstractHandler
    {
        private readonly AchievementManager AchievementManager;
        private readonly ClientManager ClientManager;
        private readonly IRepository<UserInfo> UserRepository;

        public AcceptFriendMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var count = request.GetInteger();
            for (var i = 0; i < count; i++)
            {
                var userId = request.GetInteger();

                var friend = UserRepository.FindBy(userId);

                if ((friend != null) && friend.Relationships.HasSentRequestTo(session.Info))
                {
                    var friendRelation = friend.Relationships.Add(session.Info);
                    var userRelation = session.Info.Relationships.Add(friend);

                    friend.Relationships.SentRequests.RemoveAll(x => x.To == session.Info);

                    AchievementManager.ProgressUserAchievement(session, "ACH_FriendListSize", 1);

                    session.Router.GetComposer<FriendUpdateMessageComposer>().Compose(session, userRelation);

                    var friendSession = ClientManager.GetByInfo(friend);

                    if (friendSession != null)
                        friendSession.Router.GetComposer<FriendUpdateMessageComposer>()
                            .Compose(friendSession, friendRelation);

                    UserRepository.Save(friend);
                    UserRepository.Save(session.Info);
                }
            }
        }
    }
}