namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;

    // TODO Rename
    public class CancellInviteGuide : AbstractHandler
    {
        #region Fields

        private AchievementManager AchievementManager;

        #endregion Fields

        #region Constructors

        public CancellInviteGuide()
        {
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<OnGuideSessionDetachedMessageComposer>().Compose(session, 2);

            AchievementManager.ProgressUserAchievement(session, "ACH_GuideFeedbackGiver", 1);
        }

        #endregion Methods
    }
}