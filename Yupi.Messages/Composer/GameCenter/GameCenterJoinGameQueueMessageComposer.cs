using Yupi.Protocol;

namespace Yupi.Messages.GameCenter
{
    public class GameCenterJoinGameQueueMessageComposer : Contracts.GameCenterJoinGameQueueMessageComposer
    {
        public override void Compose(ISender session)
        {
            // TODO  hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(18);
                session.Send(message);
            }
        }
    }
}