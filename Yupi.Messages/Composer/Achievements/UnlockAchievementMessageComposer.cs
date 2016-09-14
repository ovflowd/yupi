namespace Yupi.Messages.Achievements
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public class UnlockAchievementMessageComposer : Yupi.Messages.Contracts.UnlockAchievementMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Achievement achievement, int level, int pointReward,
            int pixelReward)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(achievement.Id);
                message.AppendInteger(level);
                message.AppendInteger(144);
                message.AppendString($"{achievement.GroupName}{level}");
                message.AppendInteger(pointReward);
                message.AppendInteger(pixelReward);
                message.AppendInteger(0);
                message.AppendInteger(10);
                message.AppendInteger(21);
                message.AppendString(level > 1 ? $"{achievement.GroupName}{level - 1}" : string.Empty);
                message.AppendString(achievement.Category);
                message.AppendBool(true);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}