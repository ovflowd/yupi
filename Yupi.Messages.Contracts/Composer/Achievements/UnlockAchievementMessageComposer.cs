using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class UnlockAchievementMessageComposer : AbstractComposer<Achievement, int, int, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, Achievement achievement, int level, int pointReward, int pixelReward)
		{
		 // Do nothing by default.
		}
	}
}
