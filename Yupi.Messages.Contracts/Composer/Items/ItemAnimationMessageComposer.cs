namespace Yupi.Messages.Contracts
{
    using System;
    using System.Drawing;

    using Yupi.Protocol.Buffers;

    public abstract class ItemAnimationMessageComposer : AbstractComposer
    {
        #region Enumerations

        public enum Type
        {
            USER = 0,
            ITEM = 1
        }

        #endregion Enumerations

        #region Methods

        public virtual void Compose(Yupi.Protocol.ISender session, Tuple<Point, double> pos,
            Tuple<Point, double> nextPos, uint rollerId, uint affectedId, ItemAnimationMessageComposer.Type type)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}