namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class SendMonsterplantIdMessageComposer : AbstractComposer<uint>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint entityId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}