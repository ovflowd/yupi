using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class UserUpdateLookMessageEvent : AbstractHandler
    {
        private readonly AchievementManager AchievementManager;
        private readonly MessengerController MessengerController;
        private readonly IRepository<UserInfo> UserRepository;

        public UserUpdateLookMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
            MessengerController = DependencyFactory.Resolve<MessengerController>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var gender = message.GetString();
            var look = message.GetString();

            // TODO Validate gender & look
            session.Info.Look = look;
            session.Info.Gender = gender;
            UserRepository.Save(session.Info);

            AchievementManager.ProgressUserAchievement(session, "ACH_AvatarLooks", 1);

            if (session.Room == null)
                return;

            router.GetComposer<UpdateAvatarAspectMessageComposer>().Compose(session, session.Info);

            session.Room.EachUser(
                roomSession =>
                {
                    roomSession.Router.GetComposer<UpdateUserDataMessageComposer>()
                        .Compose(roomSession, session.Info, session.RoomEntity.Id);
                }
            );

            MessengerController.UpdateUser(session.Info);
        }
    }
}