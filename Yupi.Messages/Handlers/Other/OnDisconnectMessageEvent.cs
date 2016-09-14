namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;

    public class OnDisconnectMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;

        #endregion Fields

        #region Properties

        public override bool RequireUser
        {
            get { return false; }
        }

        #endregion Properties

        #region Constructors

        public OnDisconnectMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            ClientManager.Disconnect(session, "User disconnected");
        }

        #endregion Methods
    }
}