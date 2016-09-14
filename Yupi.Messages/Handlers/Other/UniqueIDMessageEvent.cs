namespace Yupi.Messages.Other
{
    using System;

    public class UniqueIDMessageEvent : AbstractHandler
    {
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
            string tmp = request.GetString(); // TODO unused

            // TODO Store in DB?
            session.MachineId = request.GetString();
        }

        #endregion Methods
    }
}