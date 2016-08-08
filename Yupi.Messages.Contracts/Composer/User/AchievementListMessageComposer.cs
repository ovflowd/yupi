using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class AchievementListMessageComposer : AbstractComposer<IList<UserAchievement>>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<UserAchievement> achievements)
		{
		 // Do nothing by default.
		}
	}
}
