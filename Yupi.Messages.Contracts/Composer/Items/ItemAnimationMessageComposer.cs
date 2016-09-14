using System;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ItemAnimationMessageComposer : AbstractComposer
    {
        public enum Type
        {
            USER = 0,
            ITEM = 1
        }

        public virtual void Compose(ISender session, Tuple<Point, double> pos, Tuple<Point, double> nextPos,
            uint rollerId, uint affectedId, Type type)
        {
            // Do nothing by default.
        }
    }
}