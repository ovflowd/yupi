using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class HeightMapMessageComposer : AbstractComposer<Gamemap>
	{
		public override void Compose(Yupi.Protocol.ISender session, Gamemap map)
		{
		 // Do nothing by default.
		}
	}
}
