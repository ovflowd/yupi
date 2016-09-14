using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class OpenBullyReportMessageComposer : Contracts.OpenBullyReportMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0); // TODO What does 0 mean?
                session.Send(message);
            }
        }
    }
}