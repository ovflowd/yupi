using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class NameChangedUpdatesMessageComposer : AbstractComposer<NameChangedUpdatesMessageComposer.Status, string, IList<string>>
	{
		public enum Status {
			OK = 0,
			// TODO What is 1 ?
			TOO_SHORT = 2,
			TOO_LONG = 3,
			INVALID_CHARS = 4,
			IS_TAKEN = 5
		}

		public override void Compose(Yupi.Protocol.ISender session, Status status, string newName, IList<string> alternatives = null)
		{
		 // Do nothing by default.
		}
	}
}
