using Yupi.Protocol;

namespace Yupi.Messages.GameCenter
{
    public class GameCenterGameAchievementsMessageComposer : Contracts.GameCenterGameAchievementsMessageComposer
    {
        public override void Compose(ISender session)
        {
            // TODO Must be implemented
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(18);
                message.AppendInteger(1); //count
                message.AppendInteger(295); //id
                message.AppendInteger(1);
                message.AppendString("ACH_StoryChallengeChampion1");
                message.AppendInteger(0);
                message.AppendInteger(1);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendBool(false);
                message.AppendString("games");
                message.AppendString("elisa_habbo_stories");
                message.AppendInteger(1);
                message.AppendInteger(0);
                message.AppendString("");

                session.Send(message);
            }
        }
    }
}