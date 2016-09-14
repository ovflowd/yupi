using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class LagWarningLogEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var lagCount = request.GetInteger();

            // TODO Now what?! 
        }
    }
}