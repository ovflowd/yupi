using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
    public class LoadPostItMessageComposer : Yupi.Messages.Contracts.LoadPostItMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, PostItItem item)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(item.Id.ToString());
                message.AppendString(item.Text);
                session.Send(message);
            }
        }
    }
}