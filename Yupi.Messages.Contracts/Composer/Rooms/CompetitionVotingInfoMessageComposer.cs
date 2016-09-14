namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class CompetitionVotingInfoMessageComposer : AbstractComposer<RoomCompetition, int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomCompetition competition, int userVotes,
            int status = 0)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}