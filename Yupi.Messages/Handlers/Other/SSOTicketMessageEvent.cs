namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;

    public class SSOTicketMessageEvent : AbstractHandler
    {
        #region Fields

        private SSOManager SSOManager;

        #endregion Fields

        #region Constructors

        public SSOTicketMessageEvent()
        {
            SSOManager = DependencyFactory.Resolve<SSOManager>();
        }

        #endregion Constructors

        #region Properties

        public override bool RequireUser
        {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string ssoTicket = request.GetString();

            SSOManager.TryLogin(session, ssoTicket);
        }

        #endregion Methods
    }
}