namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class DimmerDataMessageComposer : AbstractComposer<MoodlightData>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, MoodlightData moodlight)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}