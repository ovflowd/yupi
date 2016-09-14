namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class HeightMapMessageComposer : AbstractComposer<HeightMap>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, HeightMap map)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}