using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class SendAchievementsRequirementsMessageComposer : AbstractComposer<Dictionary<string, Achievement>>
	{
		public override void Compose(Yupi.Protocol.ISender session, Dictionary<string, Achievement> achievements)
		{
		 // Do nothing by default.
		}
	}
}
