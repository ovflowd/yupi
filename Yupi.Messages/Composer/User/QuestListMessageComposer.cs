using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class QuestListMessageComposer : Contracts.QuestListMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0); // TODO What do these values mean?
                message.AppendBool(true);
                session.Send(message);
            }
        }
    }
}