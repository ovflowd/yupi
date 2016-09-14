using System;

namespace Yupi.Messages.Navigator
{
    public class LandingCommunityGoalMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            throw new NotImplementedException();
            //int onlineFriends = session.GetHabbo().GetMessenger().Friends.Count(x => x.Value.IsOnline);
            //router.GetComposer<LandingCommunityChallengeMessageComposer> ().Compose (session, onlineFriends);
        }
    }
}