using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class SendAchievementsRequirementsMessageComposer : Contracts.SendAchievementsRequirementsMessageComposer
    {
        public override void Compose(ISender session, IDictionary<string, Achievement> achievements)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(achievements.Count);

                foreach (var ach in achievements.Values)
                {
                    message.AppendString(ach.GroupName.Replace("ACH_", string.Empty));
                    message.AppendInteger(ach.Levels.Count);

                    for (var i = 1; i < ach.Levels.Count + 1; i++)
                    {
                        message.AppendInteger(i);
                        message.AppendInteger(ach.Levels[i].Requirement);
                    }
                }

                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}