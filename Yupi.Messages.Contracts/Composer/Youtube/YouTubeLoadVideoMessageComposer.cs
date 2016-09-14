namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class YouTubeLoadVideoMessageComposer : AbstractComposer<YoutubeTVItem>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, YoutubeTVItem tv)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}