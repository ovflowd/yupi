namespace Yupi.Messages.Messenger
{
    using System;
    using System.Collections.Generic;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class ConsoleInviteFriendsMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;

        #endregion Fields

        #region Constructors

        public ConsoleInviteFriendsMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int count = request.GetInteger();

            int[] users = new int[count];

            for (int i = 0; i < count; i++)
            {
                users[i] = request.GetInteger();
            }

            string content = request.GetString();

            foreach (int userId in users)
            {
                Relationship relationship = session.Info.Relationships.FindByUser(userId);
                if (relationship == null)
                {
                    continue;
                }

                var friendSession = ClientManager.GetByInfo(relationship.Friend);

                friendSession?.Router.GetComposer<ConsoleInvitationMessageComposer>()
                    .Compose(friendSession, session.Info.Id, content);
            }
        }

        #endregion Methods
    }
}