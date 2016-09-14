using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class GetUserTagsMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var room = session.Room;

            var userId = message.GetInteger();

            var roomUserByHabbo = room?.GetEntity(userId) as UserEntity;

            if (roomUserByHabbo == null)
                return;

            router.GetComposer<UserTagsMessageComposer>()
                .Compose(session, roomUserByHabbo.UserInfo);

            // TODO Move to proper place!
            /*
            if (session.Info.Tags.Count >= 5) {
                Yupi.GetGame ()
                    .GetAchievementManager ()
                    .ProgressUserAchievement (roomUserByHabbo.GetClient (), "ACH_UserTags", 5);

            }*/
        }
    }
}