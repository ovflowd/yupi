using Yupi.Protocol;

namespace Yupi.Messages.Music
{
    public class RetrieveSongIDMessageComposer : Contracts.RetrieveSongIDMessageComposer
    {
        public override void Compose(ISender session, string name, int songId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(name);
                message.AppendInteger(songId);
                session.Send(message);
            }
        }
    }
}