using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CompetitionVotingInfoMessageComposer : AbstractComposer<RoomCompetition, int, int>
    {
        public override void Compose(ISender session, RoomCompetition competition, int userVotes, int status = 0)
        {
            // Do nothing by default.
        }
    }
}