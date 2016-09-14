using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class ReleaseVersionEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            session.ReleaseName = request.GetString();
            var unknown1 = request.GetString();
            var unknown2 = request.GetInteger();
            var unknown3 = request.GetInteger();
        }
    }
}