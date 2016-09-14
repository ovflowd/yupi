using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class AchievementPointsMessageComposer : Contracts.AchievementPointsMessageComposer
    {
        public override void Compose(ISender session, int points)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(points);
                session.Send(message);
            }
        }
    }
}