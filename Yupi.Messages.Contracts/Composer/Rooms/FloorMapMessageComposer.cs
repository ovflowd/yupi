namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class FloorMapMessageComposer : AbstractComposer<string, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string heightmap, int wallHeight)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}