namespace Yupi.Messages.Rooms
{
    using System;

    public class AcceptPollMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint pollId = request.GetUInt32();
            // TODO Unchecked array access!
            /*
            Poll poll = Yupi.GetGame().GetPollManager().Polls[pollId];

            router.GetComposer<PollQuestionsMessageComposer> ().Compose (session, poll);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}