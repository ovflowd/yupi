using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Support
{
    public class ModerationToolRoomChatlogMessageComposer : Contracts.ModerationToolRoomChatlogMessageComposer
    {
        public override void Compose(ISender session, RoomData room)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendByte(1); // TODO Hardcoded
                message.AppendShort(2);
                message.AppendString("roomName");
                message.AppendByte(2);
                message.AppendString(room.Name);
                message.AppendString("roomId");
                message.AppendByte(1);
                message.AppendInteger(room.Id);

                var count = Math.Min(room.Chatlog.Count, 60);

                message.AppendShort((short) count);

                for (var i = 1; i <= count; ++i)
                {
                    var entry = room.Chatlog[room.Chatlog.Count - i];
                    message.AppendInteger((int) (DateTime.Now - entry.Timestamp).TotalMilliseconds);
                    message.AppendInteger(entry.User.Id);
                    message.AppendString(entry.User.Name);
                    message.AppendString(entry.Message);
                    message.AppendBool(!entry.Whisper);
                }
                session.Send(message);
            }
        }
    }
}