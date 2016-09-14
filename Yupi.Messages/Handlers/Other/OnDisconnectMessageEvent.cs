using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class OnDisconnectMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;

        public OnDisconnectMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override bool RequireUser
        {
            get { return false; }
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            ClientManager.Disconnect(session, "User disconnected");
        }
    }
}