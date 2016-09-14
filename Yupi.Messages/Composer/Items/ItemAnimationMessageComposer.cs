using System;
using System.Globalization;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class ItemAnimationMessageComposer : Contracts.ItemAnimationMessageComposer
    {
        // TODO Refactor
        public override void Compose(ISender session, Tuple<Point, double> pos, Tuple<Point, double> nextPos,
            uint rollerId, uint affectedId, Type type)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(pos.Item1.X);
                message.AppendInteger(pos.Item1.Y);
                message.AppendInteger(nextPos.Item1.X);
                message.AppendInteger(nextPos.Item1.Y);

                message.AppendInteger((int) type);

                switch (type)
                {
                    case Type.ITEM:
                        message.AppendInteger(affectedId);
                        break;
                    case Type.USER:
                        message.AppendInteger(rollerId);
                        message.AppendInteger(2);
                        message.AppendInteger(affectedId);
                        break;
                }

                message.AppendString(pos.Item2.ToString(CultureInfo.InvariantCulture));
                message.AppendString(nextPos.Item2.ToString(CultureInfo.InvariantCulture));
                message.AppendInteger(rollerId);
                session.Send(message);
            }
        }
    }
}