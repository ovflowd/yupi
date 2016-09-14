using System.Drawing;
using Yupi.Protocol.Buffers;
using System;

namespace Yupi.Messages.Contracts
{
    public abstract class ItemAnimationMessageComposer : AbstractComposer
    {
        public enum Type
        {
            USER = 0,
            ITEM = 1
        }

        public virtual void Compose(Yupi.Protocol.ISender session, Tuple<Point, double> pos,
            Tuple<Point, double> nextPos, uint rollerId, uint affectedId, ItemAnimationMessageComposer.Type type)
        {
            // Do nothing by default.
        }
    }
}