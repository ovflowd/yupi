namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class CheckPetNameMessageComposer : AbstractComposer<int, string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int status, string name)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}