using System;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
    public class GetUserBadgesMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            // TODO Refactor
            Room room = session.Room;

            int userId = message.GetInteger();

            UserEntity roomUser = room?.GetEntity(userId) as UserEntity;

            if (roomUser != null)
            {
                router.GetComposer<UserBadgesMessageComposer>().Compose(session, roomUser.UserInfo);
            }
        }
    }
}