using Yupi.Protocol.Buffers;
using System.Globalization;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class OpenHelpToolMessageComposer : AbstractComposer<IList<SupportTicket>>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<SupportTicket> tickets)
		{
		 // Do nothing by default.
		}
	}
}
