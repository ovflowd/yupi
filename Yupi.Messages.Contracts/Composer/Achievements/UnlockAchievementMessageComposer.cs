using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class UnlockAchievementMessageComposer : AbstractComposer<Achievement, uint, uint, uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, Achievement achievement, uint level, uint pointReward, uint pixelReward)
		{
		 // Do nothing by default.
		}
	}
}
