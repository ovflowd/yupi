namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Controller;
    using Yupi.Messages.Notification;
    using Yupi.Model;

    public class ModerationKickUserMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;
        private RoomManager RoomManager;

        #endregion Fields

        #region Constructors

        public ModerationKickUserMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_kick"))
                return;

            int userId = request.GetInteger();
            string message = request.GetString();

            var target = ClientManager.GetByUserId(userId);

            // TODO Log

            if (target != null && target.Info.Rank < session.Info.Rank)
            {
                RoomManager.RemoveUser(target);
                target.Router.GetComposer<AlertNotificationMessageComposer>().Compose(target, message);
            }
        }

        #endregion Methods
    }
}