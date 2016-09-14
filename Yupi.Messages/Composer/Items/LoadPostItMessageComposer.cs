using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class LoadPostItMessageComposer : Contracts.LoadPostItMessageComposer
    {
        public override void Compose(ISender session, PostItItem item)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(item.Id.ToString());
                message.AppendString(item.Text);
                session.Send(message);
            }
        }
    }
}