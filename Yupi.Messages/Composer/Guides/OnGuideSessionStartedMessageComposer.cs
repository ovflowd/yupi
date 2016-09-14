using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Guides
{
    public class OnGuideSessionStartedMessageComposer : Contracts.OnGuideSessionStartedMessageComposer
    {
        public override void Compose(ISender session, UserInfo habbo)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habbo.Id);
                message.AppendString(habbo.Name);
                message.AppendString(habbo.Look);
                message.AppendInteger(habbo.Id);
                message.AppendString(habbo.Name);
                message.AppendString(habbo.Look);
                session.Send(message);
            }
        }
    }
}