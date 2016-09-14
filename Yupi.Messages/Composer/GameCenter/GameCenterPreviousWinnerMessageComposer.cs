using Yupi.Protocol;

namespace Yupi.Messages.GameCenter
{
    public class GameCenterPreviousWinnerMessageComposer : Contracts.GameCenterPreviousWinnerMessageComposer
    {
        public override void Compose(ISender session)
        {
            // TODO  hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
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