namespace Yupi.Messages.User
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class GiveRespectMessageEvent : AbstractHandler
    {
        #region Fields

        private AchievementManager AchievementManager;

        #endregion Fields

        #region Constructors

        public GiveRespectMessageEvent()
        {
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            Room room = session.Room;

            // TODO Should lock respect points
            if (room == null || session.Info.Respect.DailyRespectPoints <= 0)
                return;

            int userId = message.GetInteger();

            if (userId == session.Info.Id)
            {
                return;
            }

            UserEntity roomUserByHabbo = room.GetEntity(userId) as UserEntity;

            if (roomUserByHabbo == null)
                return;

            AchievementManager.ProgressUserAchievement(session, "ACH_RespectGiven", 1);
            AchievementManager.ProgressUserAchievement(roomUserByHabbo.User, "ACH_RespectEarned", 1);

            session.Info.Respect.DailyRespectPoints--;
            roomUserByHabbo.User.Info.Respect.Respect++;

            room.EachUser(
                (roomSession) =>
                {
                    roomSession.Router.GetComposer<GiveRespectsMessageComposer>()
                        .Compose(roomSession, roomUserByHabbo.Id, roomUserByHabbo.UserInfo.Respect.Respect);
                }
            );
        }

        #endregion Methods
    }
}