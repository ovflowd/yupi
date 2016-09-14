namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public abstract class LoveLockDialogueMessageComposer : AbstractComposer<Yupi.Protocol.ISender, LovelockItem>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender user1, Yupi.Protocol.ISender user2, LovelockItem loveLock)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}