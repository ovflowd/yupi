namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class CompetitionEntrySubmitResultMessageComposer : AbstractComposer<RoomCompetition, int, RoomData>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomCompetition competition, int status,
            RoomData room = null)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}