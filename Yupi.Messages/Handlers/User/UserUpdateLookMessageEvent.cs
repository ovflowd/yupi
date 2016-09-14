namespace Yupi.Messages.User
{
    using System;

    using Yupi.Controller;
    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class UserUpdateLookMessageEvent : AbstractHandler
    {
        #region Fields

        private AchievementManager AchievementManager;
        private MessengerController MessengerController;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public UserUpdateLookMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
            MessengerController = DependencyFactory.Resolve<MessengerController>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string gender = message.GetString();
            string look = message.GetString();

            // TODO Validate gender & look
            session.Info.Look = look;
            session.Info.Gender = gender;
            UserRepository.Save(session.Info);

            AchievementManager.ProgressUserAchievement(session, "ACH_AvatarLooks", 1);

            if (session.Room == null)
                return;

            router.GetComposer<UpdateAvatarAspectMessageComposer>().Compose(session, session.Info);

            session.Room.EachUser(
                (roomSession) =>
                {
                    roomSession.Router.GetComposer<UpdateUserDataMessageComposer>()
                        .Compose(roomSession, session.Info, session.RoomEntity.Id);
                }
            );

            MessengerController.UpdateUser(session.Info);
        }

        #endregion Methods
    }
}