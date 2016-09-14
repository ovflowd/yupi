using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class UserUpdateMottoMessageEvent : AbstractHandler
    {
        private readonly AchievementManager AchievementManager;
        private readonly IRepository<UserInfo> UserRepository;

        public UserUpdateMottoMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var motto = message.GetString();

            // TODO Filter!
            session.Info.Motto = motto;

            UserRepository.Save(session.Info);

            if (session.Room == null) return;

            session.Room.EachUser(
                roomSession =>
                {
                    router.GetComposer<UpdateUserDataMessageComposer>()
                        .Compose(roomSession, session.Info, session.RoomEntity.Id);
                }
            );

            AchievementManager.ProgressUserAchievement(session, "ACH_Motto", 1);
        }
    }
}