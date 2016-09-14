using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Wired
{
    public class WiredTriggerMessageComposer : Contracts.WiredTriggerMessageComposer
    {
        public override void Compose(ISender session, FloorItem item, List<FloorItem> items, int delay, string extraInfo,
            int unknown)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(false);
                message.AppendInteger(unknown);
                message.AppendInteger(items.Count);
                // TODO Won't work properly. Must implement composer correctly...
                foreach (var current in items) message.AppendInteger(current.Id);
                throw new NotImplementedException();
                //message.AppendInteger(item.BaseItem.SpriteId);
                message.AppendInteger(item.Id);
                message.AppendString(extraInfo);

                message.AppendInteger(1);
                message.AppendInteger(delay);
                message.AppendInteger(1);
                message.AppendInteger(3);
                message.AppendInteger(0);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}