using System;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
    public class GetUserTagsMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            Room room = session.Room;

            int userId = message.GetInteger();

            UserEntity roomUserByHabbo = room?.GetEntity(userId) as UserEntity;

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