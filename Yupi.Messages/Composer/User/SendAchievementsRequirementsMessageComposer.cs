using System;

using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.User
{
	public class SendAchievementsRequirementsMessageComposer : AbstractComposer<Dictionary<string, Achievement>>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Dictionary<string, Achievement> achievements)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(achievements.Count);

				foreach (Achievement ach in achievements.Values)
				{
					message.AppendString(ach.GroupName.Replace("ACH_", string.Empty));
					message.AppendInteger(ach.Levels.Count);

					for (uint i = 1; i < ach.Levels.Count + 1; i++)
					{
						message.AppendInteger(i);
						message.AppendInteger(ach.Levels[i].Requirement);
					}
				}

				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

