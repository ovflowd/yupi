using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class HeightMapMessageComposer : AbstractComposer<HeightMap>
	{
		public override void Compose(Yupi.Protocol.ISender session, HeightMap map)
		{
		 // Do nothing by default.
		}
	}
}
