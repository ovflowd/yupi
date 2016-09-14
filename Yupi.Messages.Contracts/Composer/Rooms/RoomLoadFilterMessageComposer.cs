using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomLoadFilterMessageComposer : AbstractComposer<List<string>>
	{
		public override void Compose(Yupi.Protocol.ISender session, List<string> wordlist)
		{
		 // Do nothing by default.
		}
	}
}
