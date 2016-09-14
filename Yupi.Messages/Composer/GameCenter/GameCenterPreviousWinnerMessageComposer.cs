using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.GameCenter
{
    public class GameCenterPreviousWinnerMessageComposer :
        Yupi.Messages.Contracts.GameCenterPreviousWinnerMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            // TODO  hardcoded message
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(18);
                message.AppendInteger(0);

                message.AppendString("name");
                message.AppendString("figure");
                message.AppendString("gender");
                message.AppendInteger(0);
                message.AppendInteger(0);

                session.Send(message);
            }
        }
    }
}