namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Controller;
    using Yupi.Messages.Notification;
    using Yupi.Model;

    public class ModerationMuteUserMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;

        #endregion Fields

        #region Constructors

        public ModerationMuteUserMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mute"))
                return;

            int userId = request.GetInteger();
            string message = request.GetString();

            session.Info.Muted = true;

            var target = ClientManager.GetByUserId(userId);

            // TODO Log mute

            if (target != null)
            {
                target.Router.GetComposer<AlertNotificationMessageComposer>().Compose(target, message);
            }
        }

        #endregion Methods
    }
}