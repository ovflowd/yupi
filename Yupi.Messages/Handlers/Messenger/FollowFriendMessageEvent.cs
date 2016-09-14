namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Controller;
    using Yupi.Messages.User;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class FollowFriendMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;

        #endregion Fields

        #region Constructors

        public FollowFriendMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        // TODO Refactor
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int userId = request.GetInteger();

            Relationship relationship = session.Info.Relationships.FindByUser(userId);

            if (relationship == null)
            {
                router.GetComposer<FollowFriendErrorMessageComposer>().Compose(session, 0);
            }
            else
            {
                var friendSession = ClientManager.GetByUserId(userId);

                if (friendSession == null || friendSession.Room == null)
                {
                    router.GetComposer<FollowFriendErrorMessageComposer>().Compose(session, 2);
                }
                else
                {
                    router.GetComposer<RoomForwardMessageComposer>().Compose(session, friendSession.Room.Data.Id);
                }
            }
        }

        #endregion Methods
    }
}