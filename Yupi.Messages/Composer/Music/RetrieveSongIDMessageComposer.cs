using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
    public class RetrieveSongIDMessageComposer : Yupi.Messages.Contracts.RetrieveSongIDMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, string name, int songId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(name);
                message.AppendInteger(songId);
                session.Send(message);
            }
        }
    }
}