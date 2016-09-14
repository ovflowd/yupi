namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;

    public class RequestLatencyTestMessageEvent : AbstractHandler
    {
        #region Fields

        private AchievementManager AchievementManager;

        #endregion Fields

        #region Constructors

        public RequestLatencyTestMessageEvent()
        {
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            // TODO Doesn't seem right here! Could easily be faked by wrong packets!
            AchievementManager.ProgressUserAchievement(session, "ACH_AllTimeHotelPresence", 1);

            session.TimePingReceived = DateTime.Now;
        }

        #endregion Methods
    }
}