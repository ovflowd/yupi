using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Trade
{
    public class TradeUpdateMessageComposer : Contracts.TradeUpdateMessageComposer
    {
        public override void Compose(ISender session, TradeUser first, TradeUser second)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                Serialize(first, message);
                Serialize(second, message);
                session.Send(message);
            }
        }

        private void Serialize(TradeUser user, ServerMessage message)
        {
            message.AppendInteger(user.User.Id);
            message.AppendInteger(user.OfferedItems.Count);

            foreach (var current in user.OfferedItems)
            {
                message.AppendInteger(current.Id);
                /*
                message.AppendString(current.BaseItem.Type.ToString().ToLower());
                message.AppendInteger(current.Id);
                message.AppendInteger(current.BaseItem.SpriteId);
                message.AppendInteger(0);
                message.AppendBool(true);
                message.AppendInteger(0);
                message.AppendString(string.Empty);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(0);

                if (current.BaseItem.Type == 's')
                    message.AppendInteger(0);
                    */
                throw new NotImplementedException();
            }
        }
    }
}