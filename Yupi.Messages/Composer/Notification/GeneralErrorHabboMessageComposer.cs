using Yupi.Protocol;

namespace Yupi.Messages.Notification
{
    public class GeneralErrorHabboMessageComposer : Contracts.GeneralErrorHabboMessageComposer
    {
        // TODO Replace errorId with enum
        public override void Compose(ISender session, int errorId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(errorId);
                session.Send(message);
            }
        }
    }
}