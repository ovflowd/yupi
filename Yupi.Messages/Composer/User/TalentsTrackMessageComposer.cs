using System;

using System.Collections.Generic;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class TalentsTrackMessageComposer : AbstractComposer
	{
		// TODO Add enum for trackType

		public void Compose(Yupi.Protocol.ISender session, string trackType, List<Talent> talents) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (trackType);
				message.AppendInteger(talents.Count);

				int failLevel = -1;

				foreach (Talent current in talents) {
					message.AppendInteger (current.Level);
					int nm = failLevel == -1 ? 1 : 0; // TODO What does this mean?
					message.AppendInteger (nm);

					List<Talent> children = Yupi.GetGame().GetTalentManager().GetTalents(trackType, current.Id);

					message.AppendInteger (children.Count);

					foreach (Talent child in children) {
						UserAchievement achievment = session.GetHabbo ().GetAchievementData (child.AchievementGroup);

						int num;
						// TODO Refactor What does num mean?!
						if (failLevel != -1 && failLevel < child.Level) {
							num = 0;
						} else if (achievment == null) {
							num = 1;
						} else if (achievment.Level >=
							child.AchievementLevel) {
							num = 2;
						} else {
							num = 1;
						}

						message.AppendInteger (child.GetAchievement ().Id);
						message.AppendInteger (0); // TODO Magic constant

						message.AppendString(child.AchievementGroup+child.AchievementLevel);
						message.AppendInteger(num);

						message.AppendInteger(achievment != null ? achievment.Progress : 0);
						message.AppendInteger(child.GetAchievement() == null ? 0
							: child.GetAchievement().Levels[child.AchievementLevel].Requirement);

						if (num != 2 && failLevel == -1)
							failLevel = child.Level;
					}

					message.AppendInteger (0); // TODO Magic constant

					// TODO Type should be enum?
					if (current.Type == "citizenship" && current.Level == 4)
					{
						message.AppendInteger(2);
						message.AppendString("HABBO_CLUB_VIP_7_DAYS");
						message.AppendInteger(7);
						message.AppendString(current.Prize); // TODO Hardcoded stuff
						message.AppendInteger(0);
					}
					else
					{
						message.AppendInteger(1);
						message.AppendString(current.Prize);
						message.AppendInteger(0);
					}
				}

				session.Send (message);
			}
		}
	}
}

