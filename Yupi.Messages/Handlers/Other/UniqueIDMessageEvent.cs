using System;

namespace Yupi.Messages.Other
{
    public class UniqueIDMessageEvent : AbstractHandler
    {
        public override bool RequireUser
        {
            get { return false; }
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string tmp = request.GetString(); // TODO unused

            // TODO Store in DB?
            session.MachineId = request.GetString();
        }
    }
}