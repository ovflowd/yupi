using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class GiveRespectMessageEvent : AbstractHandler
    {
        private readonly AchievementManager AchievementManager;

        public GiveRespectMessageEvent()
        {
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var room = session.Room;

            // TODO Should lock respect points
            if ((room == null) || (session.Info.Respect.DailyRespectPoints <= 0))
                return;

            var userId = message.GetInteger();

            if (userId == session.Info.Id) return;

            var roomUserByHabbo = room.GetEntity(userId) as UserEntity;

            if (roomUserByHabbo == null)
                return;

            AchievementManager.ProgressUserAchievement(session, "ACH_RespectGiven", 1);
            AchievementManager.ProgressUserAchievement(roomUserByHabbo.User, "ACH_RespectEarned", 1);

            session.Info.Respect.DailyRespectPoints--;
            roomUserByHabbo.User.Info.Respect.Respect++;

            room.EachUser(
                roomSession =>
                {
                    roomSession.Router.GetComposer<GiveRespectsMessageComposer>()
                        .Compose(roomSession, roomUserByHabbo.Id, roomUserByHabbo.UserInfo.Respect.Respect);
                }
            );
        }
    }
}