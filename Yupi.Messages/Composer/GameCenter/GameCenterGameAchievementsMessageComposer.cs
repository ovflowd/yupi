using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.GameCenter
{
    public class GameCenterGameAchievementsMessageComposer :
        Yupi.Messages.Contracts.GameCenterGameAchievementsMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            // TODO Must be implemented
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
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