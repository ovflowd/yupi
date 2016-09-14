using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Wired
{
    public class WiredEffectMessageComposer : Contracts.WiredEffectMessageComposer
    {
        public override void Compose(ISender session, FloorItem item, string extraInfo, int delay,
            List<FloorItem> list = null)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(false);
                message.AppendInteger(5);
                // TODO Probably won't work correctly. Must rewrite the entire composer...
                if (list == null)
                {
                    message.AppendInteger(0);
                }
                else
                {
                    message.AppendInteger(list.Count);
                    foreach (var current in list)
                        message.AppendInteger(current.Id);
                }
                throw new NotImplementedException();
                //message.AppendInteger(item.BaseItem.SpriteId);
                message.AppendInteger(item.Id);
                message.AppendString(extraInfo);
                message.AppendInteger(1);
                message.AppendInteger(delay);
                message.AppendInteger(0);
                message.AppendInteger(20);
                message.AppendInteger(0);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}