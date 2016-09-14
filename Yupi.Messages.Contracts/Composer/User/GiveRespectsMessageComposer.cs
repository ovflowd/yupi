namespace Yupi.Messages.Contracts
{
    using Yupi.Net;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public abstract class GiveRespectsMessageComposer : AbstractComposer<int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, int user, int respect)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}