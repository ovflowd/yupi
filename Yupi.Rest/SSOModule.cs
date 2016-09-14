namespace Yupi.Rest
{
    using System;

    using Nancy;

    using Yupi.Controller;
    using Yupi.Model;

    public class SSOModule : NancyModule
    {
        #region Fields

        private SSOManager SSOManager;

        #endregion Fields

        #region Constructors

        public SSOModule()
        {
            SSOManager = DependencyFactory.Resolve<SSOManager>();
            Post["/sso/{id:int}"] = parameters =>
            {
                string ticket = SSOManager.GenerateTicket(parameters.id);
                return Response.AsJson(new SSOTicket(ticket));
            };
        }

        #endregion Constructors

        #region Nested Types

        private class SSOTicket
        {
            #region Constructors

            public SSOTicket(string ticket)
            {
                this.Ticket = ticket;
            }

            #endregion Constructors

            #region Properties

            public string Ticket
            {
                get; set;
            }

            #endregion Properties
        }

        #endregion Nested Types
    }
}