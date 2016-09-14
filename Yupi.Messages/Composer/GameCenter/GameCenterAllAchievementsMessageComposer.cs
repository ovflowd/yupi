using Yupi.Protocol;

namespace Yupi.Messages.GameCenter
{
    public class GameCenterAllAchievementsMessageComposer : Contracts.GameCenterAllAchievementsMessageComposer
    {
        public override void Compose(ISender session)
        {
            // TODO  hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0); //count

                message.AppendInteger(0); //gameId
                message.AppendInteger(0); //count
                message.AppendInteger(0); //achId
                message.AppendString("SnowWarTotalScore"); //achName
                message.AppendInteger(0); //levels

                session.Send(message);
            }
        }
    }
}