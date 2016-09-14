namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class SendAchievementsRequirementsMessageComposer : Yupi.Messages.Contracts.SendAchievementsRequirementsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IDictionary<string, Achievement> achievements)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(achievements.Count);

                foreach (Achievement ach in achievements.Values)
                {
                    message.AppendString(ach.GroupName.Replace("ACH_", string.Empty));
                    message.AppendInteger(ach.Levels.Count);

                    for (int i = 1; i < ach.Levels.Count + 1; i++)
                    {
                        message.AppendInteger(i);
                        message.AppendInteger(ach.Levels[i].Requirement);
                    }
                }

                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}