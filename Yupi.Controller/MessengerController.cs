using Yupi.Messages.Contracts;
using Yupi.Model;
using Yupi.Model.Domain;

namespace Yupi.Controller
{
    public class MessengerController
    {
        private readonly ClientManager ClientManager;

        public MessengerController()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public void UpdateUser(UserInfo user)
        {
            foreach (var friend in user.Relationships.Relationships)
                if (ClientManager.IsOnline(friend.Friend))
                {
                    var session = ClientManager.GetByInfo(friend.Friend);
                    var relationship = session.Info.Relationships.FindByUser(user);

                    session.Router.GetComposer<FriendUpdateMessageComposer>()
                        .Compose(session, relationship);
                }
        }
    }
}