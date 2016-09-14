using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class GrouprequestReloadMessageComposer : Contracts.GrouprequestReloadMessageComposer
    {
        public override void Compose(ISender session, int groupId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(groupId);
                session.Send(message);
            }
        }
    }
}