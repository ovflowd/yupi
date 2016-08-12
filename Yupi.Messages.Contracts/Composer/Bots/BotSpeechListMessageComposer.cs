using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class BotSpeechListMessageComposer : AbstractComposer<int, BotInfo>
	{
		public override void Compose(Yupi.Protocol.ISender session, int num, BotInfo bot)
		{
		 // Do nothing by default.
		}
	}
}
