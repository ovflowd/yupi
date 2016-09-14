using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Controller;

namespace Yupi.Messages.User
{
    public class UserUpdateMottoMessageEvent : AbstractHandler
    {
        private IRepository<UserInfo> UserRepository;
        private AchievementManager AchievementManager;

        public UserUpdateMottoMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

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
    }
}