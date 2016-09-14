using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class AcceptPollMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var pollId = request.GetUInt32();
            // TODO Unchecked array access!
            /*
            Poll poll = Yupi.GetGame().GetPollManager().Polls[pollId];

            router.GetComposer<PollQuestionsMessageComposer> ().Compose (session, poll);
            */
            throw new NotImplementedException();
        }
    }
}