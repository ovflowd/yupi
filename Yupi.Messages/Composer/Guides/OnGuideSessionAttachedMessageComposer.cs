using Yupi.Protocol;

namespace Yupi.Messages.Guides
{
    public class OnGuideSessionAttachedMessageComposer : Contracts.OnGuideSessionAttachedMessageComposer
    {
        // TODO Find the meaning of val1 & val2
        public override void Compose(ISender session, bool val1, int userId, string message, int val2)
        {
            using (var response = Pool.GetMessageBuffer(Id))
            {
                response.AppendBool(false);
                response.AppendInteger(userId);
                response.AppendString(message);
                response.AppendInteger(30);
                session.Send(response);
            }
        }
    }
}