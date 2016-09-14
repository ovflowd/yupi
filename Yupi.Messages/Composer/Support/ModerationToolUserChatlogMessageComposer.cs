using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Support
{
    public class ModerationToolUserChatlogMessageComposer : Contracts.ModerationToolUserChatlogMessageComposer
    {
        // TODO Refactor
        public override void Compose(ISender session, UserInfo user)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                message.AppendInteger(user.RecentlyVisitedRooms.Count);

                foreach (var room in user.RecentlyVisitedRooms)
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

                    foreach (var chat in room.Chatlog)
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
    }
}