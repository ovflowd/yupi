using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class GetUserBadgesMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            // TODO Refactor
            var room = session.Room;

            var userId = message.GetInteger();

            var roomUser = room?.GetEntity(userId) as UserEntity;

            if (roomUser != null) router.GetComposer<UserBadgesMessageComposer>().Compose(session, roomUser.UserInfo);
        }
    }
}