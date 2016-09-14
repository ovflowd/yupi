namespace Yupi.Messages.Navigator
{
    using System;

    public class LandingCommunityGoalMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            throw new NotImplementedException();
            //int onlineFriends = session.GetHabbo().GetMessenger().Friends.Count(x => x.Value.IsOnline);
            //router.GetComposer<LandingCommunityChallengeMessageComposer> ().Compose (session, onlineFriends);
        }

        #endregion Methods
    }
}