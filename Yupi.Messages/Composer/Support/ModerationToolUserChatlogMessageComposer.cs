namespace Yupi.Messages.Support
{
    using System;
    using System.Data;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;
    using Yupi.Util;

    public class ModerationToolUserChatlogMessageComposer : Yupi.Messages.Contracts.ModerationToolUserChatlogMessageComposer
    {
        #region Methods

        // TODO Refactor
        public override void Compose(Yupi.Protocol.ISender session, UserInfo user)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                message.AppendInteger(user.RecentlyVisitedRooms.Count);

                foreach (RoomData room in user.RecentlyVisitedRooms)
                {
                    // TODO Doesn't look right!
                    message.AppendByte(1);
                    message.AppendShort(2);
                    message.AppendString("roomName");
                    message.AppendByte(2);
                    message.AppendString(room.Name);
                    message.AppendString("roomId");
                    message.AppendByte(1);
                    message.AppendInteger(room.Id);

                    message.AppendShort((short) room.Chatlog.Count);

                    foreach (ChatMessage chat in room.Chatlog)
                    {
                        message.AppendInteger((int) (DateTime.Now - chat.Timestamp).TotalSeconds);

                        message.AppendInteger(chat.User.Id);
                        message.AppendString(chat.User.Name);
                        message.AppendString(chat.Message);
                        message.AppendBool(false);
                    }

                    message.AppendByte(1);
                    message.AppendShort(0);
                    message.AppendShort(0);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}