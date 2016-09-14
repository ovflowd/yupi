using Yupi.Protocol;

namespace Yupi.Messages.GameCenter
{
    public class GameCenterEnterInGameMessageComposer : Contracts.GameCenterEnterInGameMessageComposer
    {
        public override void Compose(ISender session)
        {
            // TODO  hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(18);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}