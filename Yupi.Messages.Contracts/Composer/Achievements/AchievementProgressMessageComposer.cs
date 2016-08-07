using System;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class AchievementProgressMessageComposer : AbstractComposer
	{
		public virtual void Compose( Yupi.Protocol.ISender session, Achievement achievement, uint targetLevel,
			AchievementLevel targetLevelData, uint totalLevels, UserAchievement userData) {

		}
	}
}

