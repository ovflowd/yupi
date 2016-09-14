using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class LandingCommunityChallengeMessageComposer :
        Yupi.Messages.Contracts.LandingCommunityChallengeMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, int onlineFriends)
        {
            // TODO Magic numbers!
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true); //
                message.AppendInteger(0); //points
                message.AppendInteger(0); //my rank
                message.AppendInteger(onlineFriends); //totalAmount
                message.AppendInteger(onlineFriends >= 20 ? 1 : onlineFriends >= 50 ? 2 : onlineFriends >= 80 ? 3 : 0);
                message.AppendInteger(0); //scoreRemainingUntilNextLevel
                message.AppendInteger(0); //percentCompletionTowardsNextLevel
                message.AppendString("friendshipChallenge"); //Type
                message.AppendInteger(0); //unknown
                message.AppendInteger(0); //ranks and loop
                session.Send(message);
            }
        }
    }
}