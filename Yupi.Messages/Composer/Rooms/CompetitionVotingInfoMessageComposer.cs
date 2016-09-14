using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class CompetitionVotingInfoMessageComposer : Contracts.CompetitionVotingInfoMessageComposer
    {
        // TODO Enum
        public override void Compose(ISender session, RoomCompetition competition, int userVotes, int status = 0)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(competition.Id);
                message.AppendString(competition.Name);
                message.AppendInteger(status); // 0 : vote - 1 : can't vote - 2 : you need the vote badge
                message.AppendInteger(userVotes);
                session.Send(message);
            }
        }
    }
}