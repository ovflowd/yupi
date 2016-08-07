using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class BotSpeechListMessageComposer : AbstractComposer<int, BotEntity>
	{
		public override void Compose(Yupi.Protocol.ISender session, int num, BotEntity bot)
		{
		 // Do nothing by default.
		}
	}
}
