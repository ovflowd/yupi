using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class CompetitionVotingInfoMessageComposer : AbstractComposer<RoomCompetition, int, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomCompetition competition, int userVotes,
            int status = 0)
        {
            // Do nothing by default.
        }
    }
}