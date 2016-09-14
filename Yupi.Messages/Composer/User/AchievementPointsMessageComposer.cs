namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class AchievementPointsMessageComposer : Yupi.Messages.Contracts.AchievementPointsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int points)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(points);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}