using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class ChangeFavouriteGroupMessageComposer : Contracts.ChangeFavouriteGroupMessageComposer
    {
        // TODO Refactor
        public override void Compose(ISender session, Group group, int virtualId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(virtualId);

                if (group == null)
                {
                    message.AppendInteger(-1);
                    message.AppendInteger(-1);
                    message.AppendString(string.Empty);
                }
                else
                {
                    message.AppendInteger(group.Id);
                    message.AppendInteger(3); // TODO Hardcoded
                    message.AppendString(group.Name);
                }
                session.Send(message);
            }
        }
    }
}