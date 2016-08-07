using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class TalentsTrackMessageComposer : AbstractComposer<string, IList<Talent>>
	{
		public override void Compose(Yupi.Protocol.ISender session, string trackType, IList<Talent> talents)
		{
		 // Do nothing by default.
		}
	}
}
