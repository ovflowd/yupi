using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Messages.Encoders;

namespace Yupi.Messages.Rooms
{
    public class RoomDataMessageComposer : Yupi.Messages.Contracts.RoomDataMessageComposer
    {
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
    }
}