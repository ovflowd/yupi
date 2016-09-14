using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Messenger
{
    public class ConsoleChatMessageComposer : Contracts.ConsoleChatMessageComposer
    {
        public override void Compose(ISender session, MessengerMessage msg)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(msg.From.Id);
                message.AppendString(msg.Text);
                message.AppendInteger((int) msg.Diff().TotalSeconds);
                session.Send(message);
            }
        }
    }
}