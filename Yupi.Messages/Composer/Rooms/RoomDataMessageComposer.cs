using Yupi.Messages.Encoders;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomDataMessageComposer : Contracts.RoomDataMessageComposer
    {
        public override void Compose(ISender session, RoomData room, UserInfo user, bool show, bool isNotReload)
        {
            using (var message = Pool.GetMessageBuffer(Id))
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