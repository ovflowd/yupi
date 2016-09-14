using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class PongMessageEvent : AbstractHandler
    {
        public override bool RequireUser
        {
            get { return false; }
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            session.TimePingReceived = DateTime.Now;
        }
    }
}