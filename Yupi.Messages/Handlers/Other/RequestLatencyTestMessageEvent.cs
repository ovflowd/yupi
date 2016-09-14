using System;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class RequestLatencyTestMessageEvent : AbstractHandler
    {
        private readonly AchievementManager AchievementManager;

        public RequestLatencyTestMessageEvent()
        {
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            // TODO Doesn't seem right here! Could easily be faked by wrong packets!
            AchievementManager.ProgressUserAchievement(session, "ACH_AllTimeHotelPresence", 1);

            session.TimePingReceived = DateTime.Now;
        }
    }
}