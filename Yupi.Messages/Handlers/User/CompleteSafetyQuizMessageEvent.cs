namespace Yupi.Messages.User
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;

    public class CompleteSafetyQuizMessageEvent : AbstractHandler
    {
        #region Fields

        private AchievementManager AchievementManager;

        #endregion Fields

        #region Constructors

        public CompleteSafetyQuizMessageEvent()
        {
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            AchievementManager.ProgressUserAchievement(session, "ACH_SafetyQuizGraduate", 1);
        }

        #endregion Methods
    }
}