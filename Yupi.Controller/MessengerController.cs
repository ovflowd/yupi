namespace Yupi.Controller
{
    using System;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Net;
    using Yupi.Protocol;

    public class MessengerController
    {
        #region Fields

        private ClientManager ClientManager;

        #endregion Fields

        #region Constructors

        public MessengerController()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public void UpdateUser(UserInfo user)
        {
            foreach (Relationship friend in user.Relationships.Relationships)
            {
                if (ClientManager.IsOnline(friend.Friend))
                {
                    Habbo session = ClientManager.GetByInfo(friend.Friend);
                    Relationship relationship = session.Info.Relationships.FindByUser(user);

                    session.Router.GetComposer<FriendUpdateMessageComposer>()
                        .Compose(session, relationship);
                }
            }
        }

        #endregion Methods
    }
}