namespace Yupi.Messages.User
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class UserUpdateMottoMessageEvent : AbstractHandler
    {
        #region Fields

        private AchievementManager AchievementManager;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public UserUpdateMottoMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string motto = message.GetString();

            // TODO Filter!
            session.Info.Motto = motto;

            UserRepository.Save(session.Info);

            if (session.Room == null)
            {
                return;
            }

            session.Room.EachUser(
                (roomSession) =>
                {
                    router.GetComposer<UpdateUserDataMessageComposer>()
                        .Compose(roomSession, session.Info, session.RoomEntity.Id);
                }
            );

            AchievementManager.ProgressUserAchievement(session, "ACH_Motto", 1);
        }

        #endregion Methods
    }
}