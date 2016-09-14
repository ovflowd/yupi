namespace Yupi.Messages.Items
{
    using System;
    using System.Drawing;
    using System.Globalization;

    using Yupi.Protocol.Buffers;

    public class ItemAnimationMessageComposer : Yupi.Messages.Contracts.ItemAnimationMessageComposer
    {
        #region Methods

        // TODO Refactor
        public override void Compose(Yupi.Protocol.ISender session, Tuple<Point, double> pos,
            Tuple<Point, double> nextPos, uint rollerId, uint affectedId, ItemAnimationMessageComposer.Type type)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
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

        #endregion Methods
    }
}