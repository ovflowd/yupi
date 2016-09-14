namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Messages.Encoders;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RoomDataMessageComposer : Yupi.Messages.Contracts.RoomDataMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room, UserInfo user, bool show,
            bool isNotReload)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(show);

                message.Append(room);

                message.AppendBool(isNotReload);
                message.AppendBool(room.IsPublic);
                message.AppendBool(!isNotReload);
                message.AppendBool(room.IsMuted);

                message.Append(room.ModerationSettings);

                message.AppendBool(room.HasOwnerRights(user));

                message.Append(room.Chat);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}