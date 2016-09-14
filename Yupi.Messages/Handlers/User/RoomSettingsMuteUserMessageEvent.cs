using System;
using Yupi.Messages.Contracts;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Util;

namespace Yupi.Messages.User
{
    public class RoomSettingsMuteUserMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var targetUserId = message.GetInteger();

            message.GetUInt32(); // TODO Unknown

            var duration = message.GetInteger();

            var room = session.Room;

            if ((room == null)
                || !room.Data.ModerationSettings.CanMute(session.Info)) return;

            var targetUser = room.GetEntity(targetUserId) as UserEntity;

            if ((targetUser == null) || (targetUser.UserInfo.Rank >= session.Info.Rank))
                return;

            room.Data.MutedEntities.Add(new RoomMute
            {
                Entity = targetUser.UserInfo,
                ExpiresAt = DateTime.Now.AddMinutes(duration)
            });

            targetUser.User.Router.GetComposer<SuperNotificationMessageComposer>()
                .Compose(targetUser.User, T._("Notice"),
                    string.Format(T._("The owner of the room has muted you for {0} minutes!"), duration), "", "", "", 4);
        }
    }
}