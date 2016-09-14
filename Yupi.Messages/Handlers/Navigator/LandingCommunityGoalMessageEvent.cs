using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class LandingCommunityGoalMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            throw new NotImplementedException();
            //int onlineFriends = session.GetHabbo().GetMessenger().Friends.Count(x => x.Value.IsOnline);
            //router.GetComposer<LandingCommunityChallengeMessageComposer> ().Compose (session, onlineFriends);
        }
    }
}