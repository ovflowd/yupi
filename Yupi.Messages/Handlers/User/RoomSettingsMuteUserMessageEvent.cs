namespace Yupi.Messages.User
{
    using System;

    using Yupi.Messages.Contracts;
    using Yupi.Model.Domain;
    using Yupi.Util;

    public class RoomSettingsMuteUserMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int targetUserId = message.GetInteger();

            message.GetUInt32(); // TODO Unknown

            int duration = message.GetInteger();

            Room room = session.Room;

            if (room == null
                || !room.Data.ModerationSettings.CanMute(session.Info))
            {
                return;
            }

            UserEntity targetUser = room.GetEntity(targetUserId) as UserEntity;

            if (targetUser == null || targetUser.UserInfo.Rank >= session.Info.Rank)
                return;

            room.Data.MutedEntities.Add(new RoomMute()
            {
                Entity = targetUser.UserInfo,
                ExpiresAt = DateTime.Now.AddMinutes(duration)
            });

            targetUser.User.Router.GetComposer<SuperNotificationMessageComposer>()
                .Compose(targetUser.User, T._("Notice"),
                    string.Format(T._("The owner of the room has muted you for {0} minutes!"), duration), "", "", "", 4);
        }

        #endregion Methods
    }
}