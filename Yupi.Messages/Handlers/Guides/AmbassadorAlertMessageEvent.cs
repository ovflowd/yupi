namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Controller;
    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol;

    public class AmbassadorAlertMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;

        #endregion Fields

        #region Constructors

        public AmbassadorAlertMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("send_ambassador_alert"))
                return;

            int userId = message.GetInteger();

            Habbo user = ClientManager.GetByUserId(userId);

            user.Router.GetComposer<SuperNotificationMessageComposer>()
                .Compose(user, "${notification.ambassador.alert.warning.title}",
                    "${notification.ambassador.alert.warning.message}");
        }

        #endregion Methods
    }
}