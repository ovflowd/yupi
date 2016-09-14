using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class UniqueIDMessageEvent : AbstractHandler
    {
        public override bool RequireUser
        {
            get { return false; }
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var tmp = request.GetString(); // TODO unused

            // TODO Store in DB?
            session.MachineId = request.GetString();
        }
    }
}