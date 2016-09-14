using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class GroupConfirmLeaveMessageComposer : Contracts.GroupConfirmLeaveMessageComposer
    {
        public override void Compose(ISender session, UserInfo user, Group group, int type)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(group.Id);
                message.AppendInteger(type);
                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                message.AppendString(user.Look);
                message.AppendString(string.Empty);
                session.Send(message);
            }
        }
    }
}