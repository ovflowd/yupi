namespace Yupi.Messages.Support
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ModerationToolRoomChatlogMessageComposer : Yupi.Messages.Contracts.ModerationToolRoomChatlogMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendByte(1); // TODO Hardcoded
                message.AppendShort(2);
                message.AppendString("roomName");
                message.AppendByte(2);
                message.AppendString(room.Name);
                message.AppendString("roomId");
                message.AppendByte(1);
                message.AppendInteger(room.Id);

                int count = Math.Min(room.Chatlog.Count, 60);

                message.AppendShort((short) count);

                for (int i = 1; i <= count; ++i)
                {
                    ChatMessage entry = room.Chatlog[room.Chatlog.Count - i];
                    message.AppendInteger((int) (DateTime.Now - entry.Timestamp).TotalMilliseconds);
                    message.AppendInteger(entry.User.Id);
                    message.AppendString(entry.User.Name);
                    message.AppendString(entry.Message);
                    message.AppendBool(!entry.Whisper);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}