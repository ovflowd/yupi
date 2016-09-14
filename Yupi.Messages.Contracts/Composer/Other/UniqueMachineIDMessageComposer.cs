namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class UniqueMachineIDMessageComposer : AbstractComposer<string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string machineId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}