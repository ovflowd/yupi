namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class DanceStatusMessageComposer : AbstractComposer<int, Dance>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, int entityId, Dance dance)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}